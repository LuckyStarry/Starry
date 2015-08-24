using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services.Core
{
    public abstract class Loader : Engine
    {
        private IDictionary<string, IModule> Modules;
        private object syncLock = new object();

        public Loader()
        {
            this.Modules = new Dictionary<string, IModule>();
        }

        public void Append(string moduleName)
        {
            this.Append(this.CreateModule(moduleName));
        }

        public void Append(IModule module)
        {
            if (string.IsNullOrWhiteSpace(module.ModuleName))
            {
                throw new ArgumentException("The module'name cannot be empty or null", "moduleName");
            }
            if (module == null)
            {
                throw new ArgumentNullException("module", "The module cannot be null");
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
                    throw new ArgumentException("模块名不能为空", "moduleName");
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

        public abstract IModule CreateModule(string moduleName);

        protected override void DoHandle(System.Threading.CancellationToken cancellationToken)
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

        protected override void DoFinished()
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
            base.DoFinished();
        }
    }
}
