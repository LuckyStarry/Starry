using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public class Service : Engine, IService
    {
        private IDictionary<string, IModule> Modules;
        private object syncLock = new object();

        public Service()
        {
            this.Modules = new Dictionary<string, IModule>();
        }

        public void Append(IModule module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }
            if (string.IsNullOrWhiteSpace(module.ModuleName))
            {
                throw new ArgumentException("The module's name cannot be empty or null", "moduleName");
            }
            var moduleName = module.ModuleName.Trim().ToLower();
            if (!this.Modules.ContainsKey(moduleName))
            {
                lock (this.syncLock)
                {
                    if (!this.Modules.ContainsKey(moduleName))
                    {
                        this.Modules.Add(moduleName, module);
                    }
                }
            }
        }

        public IModule this[string moduleName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(moduleName))
                {
                    throw new ArgumentException("The module's name cannot be empty or null", "moduleName");
                }
                moduleName = moduleName.Trim().ToLower();
                lock (this.syncLock)
                {
                    if (this.Modules.ContainsKey(moduleName))
                    {
                        return this.Modules[moduleName];
                    }
                }
                return null;
            }
        }

        public IModule[] GetModules()
        {
            return this.Modules.Values.ToArray();
        }

        protected override void OnHandle(System.Threading.CancellationToken cancellationToken)
        {
            var removeModules = new List<string>();
            foreach (var module in this.Modules)
            {
                if (module.Value == null)
                {
                    removeModules.Add(module.Key);
                }
                else if (module.Value.State == EngineState.Disposed)
                {
                    removeModules.Add(module.Key);
                    this.Modules[module.Key] = null;
                }
                else
                {
                    switch (this.State)
                    {
                        case EngineState.Running:
                            switch (module.Value.State)
                            {
                                case EngineState.Standby:
                                case EngineState.Stopped:
                                    module.Value.Start();
                                    break;
                                case EngineState.Running:
                                    if (!module.Value.IsAlive)
                                    {
                                        try
                                        {
                                            module.Value.Dispose();
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
                            switch (module.Value.State)
                            {
                                case EngineState.Running:
                                    try
                                    {
                                        module.Value.Dispose();
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
            }
            foreach (var removeModule in removeModules)
            {
                this.Modules.Remove(removeModule);
            }
        }

        protected override void OnFinished()
        {
            foreach (var module in this.Modules)
            {
                if (module.Value != null)
                {
                    if (module.Value.State == EngineState.Running)
                    {
                        try
                        {
                            module.Value.Stop();
                        }
                        catch (Exception ex)
                        {
                            this.OnException(ex);
                            try
                            {
                                module.Value.Dispose();
                            }
                            catch (Exception exDisp)
                            {
                                this.OnException(exDisp);
                            }
                        }
                    }
                }
            }
            base.OnFinished();
        }
    }
}
