using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services
{
    public class Module<TLoader, THandler> : Core.Engine, Core.IModule, Core.IModule<TLoader>
        where TLoader : Core.ILoader
        where THandler : Core.IHandler
    {
        private List<THandler> handlers;

        public Module(TLoader loader)
        {
            this.Loader = loader;
            this.MaxConcurrent = 1;
        }

        public TLoader Loader { private set; get; }
        Core.ILoader Core.IModule.Loader { get { return this.Loader; } }

        public int MaxConcurrent { set; get; }

        public string ModuleName { set; get; }

        protected override void DoHandle(System.Threading.CancellationToken cancellationToken)
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
                moduleHandler.SystemModule = this;
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
    }

    public class Module<THandler> : Module<Core.ILoader, THandler>
        where THandler : Core.IHandler
    {
        public Module(Core.ILoader loader)
            : base(loader)
        {

        }
    }

    public class Module : Module<Core.IHandler>
    {
        public Module(Core.ILoader loader)
            : base(loader)
        {

        }
    }
}
