using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Starry.Services.Core
{
    public abstract class Engine : IDisposable
    {
        private Guid uniqueID = Guid.NewGuid();
        public Guid UniqueID { get { return this.uniqueID; } }

        private object syncLock = new object();
        private DateTime lastRunning;

        protected Engine()
        {
            this.state = EngineState.Standby;
        }

        public event EventHandler<EventArgs.EngineStateChangedEventArgs> EngineStateChanged;
        public event EventHandler<EventArgs.ExceptionHappendEventArgs> ExceptionHappend;

        protected virtual void OnEngineStateChanged(EventArgs.EngineStateChangedEventArgs e)
        {
            if (this.EngineStateChanged != null)
            {
                this.EngineStateChanged(this, e);
            }
        }

        protected virtual void OnExceptionHappend(EventArgs.ExceptionHappendEventArgs e)
        {
            if (ExceptionHappend != null)
            {
                this.ExceptionHappend(this, e);
            }
        }

        private EngineState state = EngineState.Unknown;
        public EngineState State
        {
            private set
            {
                if (value != this.state)
                {
                    var original = this.state;
                    this.state = value;
                    this.OnEngineStateChanged(new EventArgs.EngineStateChangedEventArgs(original, value));
                }
            }
            get { return this.state; }
        }

        public virtual bool IsAlive
        {
            get
            {
                switch (this.State)
                {
                    case EngineState.Disposed: return false;
                    case EngineState.Standup:
                    case EngineState.Running:
                    case EngineState.Stopping:
                    case EngineState.Disposing: return (DateTime.Now - this.lastRunning).TotalSeconds < this.WorkTimeout;
                    default: return true;
                }
            }
        }

        private int workTimeout = 30;
        public int WorkTimeout
        {
            set { this.workTimeout = value; }
            get { return this.workTimeout; }
        }

        public void Start()
        {
            if ((this.State == EngineState.Standby)
                || (this.State == EngineState.Stopped))
            {
                lock (this.syncLock)
                {
                    if ((this.State == EngineState.Standby)
                        || (this.State == EngineState.Stopped))
                    {
                        this.State = EngineState.Standup;
                        try
                        {
                            this.DisposeWorkThread();
                            this.handleThread = new Thread(new ThreadStart(this.Work));
                            this.StartHandle();
                            this.lastRunning = DateTime.Now;
                            this.handleThread.Start();
                        }
                        catch (Exception ex)
                        {
                            this.ExceptionHandle(ex);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        protected virtual void StartHandle() { }

        private Thread handleThread;

        public void Stop()
        {
            if (this.State == EngineState.Running)
            {
                lock (this.syncLock)
                {
                    if (this.State == EngineState.Running)
                    {
                        this.State = EngineState.Stopping;
                        this.DisposeWorkThread();
                        this.StopHandle();
                        this.State = EngineState.Stopped;
                    }
                }
            }
        }

        private void DisposeWorkThread()
        {
            if (this.handleThread != null)
            {
                if (!this.handleThread.Join(TimeSpan.FromSeconds(this.WorkTimeout)))
                {
                    try
                    {
                        this.handleThread.Abort();
                    }
                    catch (Exception exception)
                    {
                        this.ExceptionHandle(exception);
                    }
                }
            }
            this.handleThread = null;
        }

        protected virtual void StopHandle() { }

        private void Work()
        {
            this.lastRunning = DateTime.Now;
            lock (this.syncLock)
            {
                if (this.State == EngineState.Standup)
                {
                    this.State = EngineState.Running;
                }
            }
            while (this.WorkContinue())
            {
                this.lastRunning = DateTime.Now;
                try
                {
                    this.WorkHandle();
                }
                catch (Exception exception)
                {
                    this.ExceptionHandle(exception);
                }
                this.SleepHandle();
            }
            this.FinishedHandle();
        }

        protected virtual bool WorkContinue()
        {
            return this.State == EngineState.Running;
        }

        protected abstract void WorkHandle();

        protected virtual void SleepHandle()
        {
            System.Threading.Thread.Sleep(10);
        }

        protected virtual void FinishedHandle() { }

        protected virtual void ExceptionHandle(Exception ex)
        {
            this.OnExceptionHappend(new EventArgs.ExceptionHappendEventArgs(ex));
        }

        protected virtual void DisposeHandle() { }

        public void Dispose()
        {
            if ((this.State != EngineState.Disposing)
                && (this.State != EngineState.Disposed))
            {
                lock (this.syncLock)
                {
                    if ((this.State != EngineState.Disposing)
                        && (this.State != EngineState.Disposed))
                    {
                        this.State = EngineState.Disposing;
                        this.DisposeWorkThread();
                        this.DisposeHandle();
                        this.State = EngineState.Disposed;
                    }
                }
            }
        }
    }
}
