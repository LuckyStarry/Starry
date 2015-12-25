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

        private object syncLock = new object();
        private DateTime lastRunning;

        protected Engine()
        {
            this.state = EngineState.Standby;
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
                if (this.State == EngineState.Disposed)
                {
                    return false;
                }
                if ((this.State == EngineState.Standup)
                    || (this.State == EngineState.Running)
                    || (this.State == EngineState.Stopping)
                    || (this.State == EngineState.Disposing))
                {
                    return (DateTime.Now - this.lastRunning).TotalSeconds < this.AliveTimeout;
                }
                return true;
            }
        }

        protected virtual int AliveTimeout { get { return 300; } }

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
                            this.cancellationTokenSource = new CancellationTokenSource();
                            this.task = new Task(this.Handle, (object)this.cancellationTokenSource.Token);
                            this.OnStart();
                            this.lastRunning = DateTime.Now;
                            this.task.Start();
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

        private Task task;
        private CancellationTokenSource cancellationTokenSource;

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
            if (this.task != null)
            {
                if (!this.task.Wait(this.AliveTimeout))
                {
                    this.cancellationTokenSource.Cancel();
                    try
                    {
                        this.task.Wait(this.AliveTimeout);
                    }
                    catch (Exception ex)
                    {
                        this.OnException(ex);
                    }
                }
                try
                {
                    this.task.Dispose();
                }
                catch (Exception ex)
                {
                    this.OnException(ex);
                }
                if (this.cancellationTokenSource != null)
                {
                    try
                    {
                        this.cancellationTokenSource.Dispose();
                    }
                    catch (Exception ex)
                    {
                        this.OnException(ex);
                    }
                }
            }
            this.task = null;
            this.cancellationTokenSource = null;
        }

        protected virtual void OnStop() { }

        private void Handle(object objCancellationToken)
        {
            this.State = EngineState.Running;
            var cancellationToken = (CancellationToken)objCancellationToken;
            while (this.HandleContinue())
            {
                this.lastRunning = DateTime.Now;
                try
                {
                    this.OnHandle(cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
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

        protected abstract void OnHandle(CancellationToken cancellationToken);

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
        }
    }
}
