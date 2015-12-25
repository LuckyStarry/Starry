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
        public Guid UniqueID { get { return uniqueID; } }

#if DEBUG_CORE_DEBUGGER
        protected internal abstract string EngineID { get; }
#endif

        private object syncLock = new object();
        private DateTime lastRunning;

        protected Engine()
        {
            this.state = EngineState.Standby;

#if DEBUG_CORE_DEBUGGER
            this.EngineStateChangedEventArgs += (sender, e) =>
            {
                CoreDebugger.WriteLine("[{0}] STATE {1} => {2}", this.EngineID, e.Before, e.After);
            };
            this.ExceptionHappend += (sender, e) =>
            {
                CoreDebugger.WriteLine("[{0}] EXCEPTION {1}", this.EngineID, e.Exception.Message);
            };
#endif
        }

        public event EventHandler<EventArgs.EngineStateChangedEventArgs> EngineStateChangedEventArgs;
        public event EventHandler<EventArgs.ExceptionHappendEventArgs> ExceptionHappend;

        protected void DoEngineStateChangedEventArgs(object sender, EventArgs.EngineStateChangedEventArgs e)
        {
            this.EngineStateChangedEventArgs(sender, e);
        }

        private EngineState state;
        public EngineState State
        {
            private set
            {
                if (value != this.state)
                {
                    var ogri = this.state;
                    this.state = value;
                    if (this.EngineStateChangedEventArgs != null)
                    {
                        this.EngineStateChangedEventArgs(this, new EventArgs.EngineStateChangedEventArgs(ogri, value));
                    }
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
                    case EngineState.Disposing: return (DateTime.Now - this.lastRunning).TotalSeconds < this.AliveTimeout;
                    default: return true;
                }
            }
        }

        protected virtual int AliveTimeout { get { return 30; } }

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
                            this.DisposeTaskAndCancelToken();
                            this.handleThread = new Thread(new ThreadStart(this.Handle));
                            this.OnStart();
                            this.lastRunning = DateTime.Now;
#if DEBUG_CORE_DEBUGGER
                            CoreDebugger.WriteLine("[{0}] TaskStart Start", this.EngineID);
#endif
                            this.handleThread.Start();
#if DEBUG_CORE_DEBUGGER
                            CoreDebugger.WriteLine("[{0}] TaskStart End", this.EngineID);
#endif
                        }
                        catch (Exception ex)
                        {
                            this.OnException(ex);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        protected virtual void OnStart() { }

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
                        this.DisposeTaskAndCancelToken();
                        this.OnStop();
                        this.State = EngineState.Stopped;
                    }
                }
            }
        }

        private void DisposeTaskAndCancelToken()
        {
            if (this.handleThread != null)
            {
#if DEBUG_CORE_DEBUGGER
                CoreDebugger.WriteLine("[{0}] TaskWaiting Start", this.EngineID);
#endif
                if (!this.handleThread.Join(TimeSpan.FromSeconds(this.AliveTimeout)))
                {
#if DEBUG_CORE_DEBUGGER
                    CoreDebugger.WriteLine("[{0}] Task cancel Start", this.EngineID);
#endif
                    try
                    {
                        this.handleThread.Abort();
#if DEBUG_CORE_DEBUGGER
                        CoreDebugger.WriteLine("[{0}] Task cancel waiting Start", this.EngineID);
#endif
                    }
                    catch (Exception ex)
                    {
                        this.OnException(ex);
                    }
                }
            }
            this.handleThread = null;
        }

        protected virtual void OnStop() { }

        private void Handle()
        {
#if DEBUG_CORE_DEBUGGER
            CoreDebugger.WriteLine("[{0}] TaskStart Success", this.EngineID);
#endif
            this.lastRunning = DateTime.Now;
            lock (this.syncLock)
            {
                if (this.State == EngineState.Standup)
                {
                    this.State = EngineState.Running;
                }
            }
            while (this.HandleContinue())
            {
                this.lastRunning = DateTime.Now;
                try
                {
                    this.OnHandle();
                }
                catch (Exception ex)
                {
                    this.OnException(ex);
                }
                this.OnSleep();
            }
            this.OnFinished();
        }

        protected virtual bool HandleContinue()
        {
            return this.State == EngineState.Running;
        }

        protected abstract void OnHandle();

        protected virtual void OnSleep()
        {
            System.Threading.Thread.Sleep(10);
        }

        protected virtual void OnFinished() { }

        protected virtual void OnException(Exception ex)
        {
            if (this.ExceptionHappend != null)
            {
                this.ExceptionHappend(this, new EventArgs.ExceptionHappendEventArgs(ex));
            }
        }

        protected virtual void OnDispose() { }

        public void Dispose()
        {
#if DEBUG_CORE_DEBUGGER
            CoreDebugger.WriteLine("[{0}] Dispose Start", this.EngineID);
#endif
            if ((this.State != EngineState.Disposing)
                && (this.State != EngineState.Disposed))
            {
                lock (this.syncLock)
                {
                    if ((this.State != EngineState.Disposing)
                        && (this.State != EngineState.Disposed))
                    {
                        this.State = EngineState.Disposing;
                        this.DisposeTaskAndCancelToken();
                        this.OnDispose();
                        this.State = EngineState.Disposed;
                    }
                }
            }
#if DEBUG_CORE_DEBUGGER
            CoreDebugger.WriteLine("[{0}] Dispose End", this.EngineID);
#endif
        }
    }
}
