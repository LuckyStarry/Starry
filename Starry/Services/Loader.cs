using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services
{
    public abstract class Loader : Core.Engine
    {
        private IDictionary<string, Core.IModule> Modules;
        private object syncLock = new object();

        public Loader()
        {
            this.Modules = new Dictionary<string, Core.IModule>();
        }

        public void Append(string moduleName)
        {
            this.Append(moduleName, this.CreateModule(moduleName));
        }

        public void Append(string moduleName, Core.IModule systemModule)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
            {
                throw new ArgumentException("模块名不能为空", "moduleName");
            }
            if (systemModule == null)
            {
                throw new ArgumentNullException("systemModule", "模块不能为Null");
            }
            moduleName = moduleName.Trim().ToLower();
            if (!this.Modules.ContainsKey(moduleName))
            {
                lock (this.syncLock)
                {
                    if (!this.Modules.ContainsKey(moduleName))
                    {
                        this.Modules.Add(moduleName, systemModule);
                    }
                }
            }
        }

        public Core.IModule this[string moduleName]
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

        public Core.IModule[] GetModules()
        {
            return this.Modules.Values.ToArray();
        }

        public virtual Core.IModule CreateModule(string moduleName)
        {
            throw new NotImplementedException("需要指定创建模块实例的方法");
        }

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
