using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Services
{
    public class CustomModule : Core.Module
    {
        public CustomModule(Core.IService service, string moduleName, Action action)
            : base(service, moduleName)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action = action;
        }

        private Action action;

        protected sealed override Core.IHandler CreateModuleHandler()
        {
            return new CustomHandler(this, this.action);
        }
    }
}
