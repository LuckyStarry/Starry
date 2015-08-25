using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public abstract class Module<TLoader, THandler> : Engine, IModule, IModule<TLoader>
        where TLoader : ILoader
        where THandler : IHandler
    {
        private List<THandler> handlers;

        public Module(TLoader loader)
        {
            this.Loader = loader;
            this.MaxConcurrent = 1;
            this.handlers = new List<THandler>();
        }
        public Module(TLoader loader, string moduleName)
            : this(loader)
        {
            this.ModuleName = moduleName;
        }

        public TLoader Loader { private set; get; }
        ILoader IModule.Loader { get { return this.Loader; } }

        public int MaxConcurrent { set; get; }

        public string ModuleName { set; get; }

        protected sealed override void DoHandle(System.Threading.CancellationToken cancellationToken)
        {
            for (int i = 0; i < this.handlers.Count; i++)
            {
                var handler = this.handlers[i];
                if (handler.State == EngineState.Disposed)
                {
                    this.handlers[i] = default(THandler);
                }
                switch (this.State)
                {
                    case EngineState.Running:
                        switch (handler.State)
                        {
                            case EngineState.Standby:
                            case EngineState.Stopped:
                                handler.Start();
                                break;
                            case EngineState.Running:
                                if (!handler.IsAlive)
                                {
                                    try
                                    {
                                        handler.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.OnException(ex);
                                    }
                                }
                                break;
                        }
                        break;
                    case EngineState.Stopping:
                    case EngineState.Stopped:
                    case EngineState.Disposing:
                    case EngineState.Disposed:
                        switch (handler.State)
                        {
                            case EngineState.Running:
                                try
                                {
                                    handler.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    this.OnException(ex);
                                }
                                break;
                        }
                        break;
                }
            }
            this.handlers.RemoveAll(i => i == null);
            while (this.handlers.Count < this.MaxConcurrent)
            {
                var moduleHandler = this.CreateModuleHandler();
                if (moduleHandler == null)
                {
                    break;
                }
                this.handlers.Add(moduleHandler);
            }
            while (this.handlers.Count > this.MaxConcurrent)
            {
                var lastHandler = this.handlers[this.handlers.Count - 1];
                if (this.handlers.Remove(lastHandler))
                {
                    try
                    {
                        lastHandler.Dispose();
                    }
                    catch (Exception ex)
                    {
                        this.OnException(ex);
                    }
                }
            }
        }

        protected override void DoFinished()
        {
            this.handlers.RemoveAll(i => i == null);
            while (this.handlers.Count > 0)
            {
                var lastHandler = this.handlers[this.handlers.Count - 1];
                if (this.handlers.Remove(lastHandler))
                {
                    try
                    {
                        lastHandler.Dispose();
                    }
                    catch (Exception ex)
                    {
                        this.OnException(ex);
                    }
                }
            }
            base.DoFinished();
        }

        protected abstract THandler CreateModuleHandler();
    }

    public abstract class Module<THandler> : Module<ILoader, THandler>
        where THandler : IHandler
    {
        public Module(ILoader loader)
            : base(loader)
        {

        }
        public Module(ILoader loader, string moduleName)
            : this(loader)
        {
            this.ModuleName = moduleName;
        }
    }

    public abstract class Module : Module<IHandler>
    {
        public Module(ILoader loader)
            : base(loader)
        {

        }
        public Module(ILoader loader, string moduleName)
            : this(loader)
        {
            this.ModuleName = moduleName;
        }
    }
}
