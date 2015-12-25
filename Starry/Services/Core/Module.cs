using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public abstract class Module<TService, THandler> : Engine, IModule, IModule<TService>
        where TService : IService
        where THandler : IHandler
    {

#if DEBUG_CORE_DEBUGGER
        protected internal override string EngineID { get { return "MODULE " + (this as IModule).ModuleName; } }
#endif

        private List<THandler> handlers;

        public Module(TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            this.Service = service;
            this.MaxConcurrent = 1;
            this.handlers = new List<THandler>();
            this.ModuleName = this.GetType().Name;
        }

        public Module(TService service, string moduleName)
            : this(service)
        {
            this.ModuleName = moduleName;
        }

        public TService Service { private set; get; }
        IService IModule.Service { get { return this.Service; } }

        public int MaxConcurrent { set; get; }

        public int Concurrent { get { return this.handlers.Count; } }

        public string ModuleName { set; get; }

        protected override bool HandleContinue()
        {
            return base.HandleContinue()
                && this.Service != null
                && this.Service.State == EngineState.Running
                && this.Service.IsAlive;
        }

        protected sealed override void OnHandle()
        {
#if DEBUG_CORE_DEBUGGER
            //CoreDebugger.WriteLine("Module [{0}] OnHandle Start", this.ModuleName);
#endif
            this.handlers.RemoveAll(handle => handle == null);
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
#if DEBUG_CORE_DEBUGGER
            //CoreDebugger.WriteLine("Module [{0}] OnHandle End", this.ModuleName);
#endif
        }

        protected override void OnFinished()
        {
#if DEBUG_CORE_DEBUGGER
            CoreDebugger.WriteLine("Module [{0}] OnFinished Start", this.ModuleName);
#endif
            this.handlers.RemoveAll(handle => handle == null);
            while (this.handlers.Count > 0)
            {
                var lastHandler = this.handlers[this.handlers.Count - 1];
                if (this.handlers.Remove(lastHandler))
                {
#if DEBUG_CORE_DEBUGGER
                    var engineid = string.Empty;
                    if (lastHandler is Engine)
                    {
                        engineid = (lastHandler as Engine).EngineID;
                    }
#endif
                    try
                    {
#if DEBUG_CORE_DEBUGGER
                        CoreDebugger.WriteLine("[{0}] {1} Dispose Start", this.EngineID, engineid);
#endif
                        lastHandler.Dispose();
#if DEBUG_CORE_DEBUGGER
                        CoreDebugger.WriteLine("[{0}] {1} Dispose End", this.EngineID, engineid);
#endif
                    }
                    catch (Exception ex)
                    {
#if DEBUG_CORE_DEBUGGER
                        CoreDebugger.WriteLine("[{0}] {1} Dispose Exception {2}", this.EngineID, engineid, ex.Message);
#endif
                        this.OnException(ex);
                    }
                }
            }
            base.OnFinished();
#if DEBUG_CORE_DEBUGGER
            CoreDebugger.WriteLine("Module [{0}] OnFinished End", this.ModuleName);
#endif
        }

        protected abstract THandler CreateModuleHandler();
    }

    public abstract class Module<THandler> : Module<IService, THandler>
        where THandler : IHandler
    {
        public Module(IService service)
            : base(service)
        {

        }

        public Module(IService service, string moduleName)
            : this(service)
        {
            this.ModuleName = moduleName;
        }
    }

    public abstract class Module : Module<IHandler>
    {
        public Module(IService service)
            : base(service)
        {

        }

        public Module(IService service, string moduleName)
            : this(service)
        {
            this.ModuleName = moduleName;
        }
    }
}
