using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Starry.Services
{
    public class CustomizeHandler : Core.Handler
    {
        public CustomizeHandler(Core.IModule module, Action action) : this(module, action, false) { }
        public CustomizeHandler(Core.IModule module, Action action, bool asycn) : this(module, action, false, null) { }
        public CustomizeHandler(Core.IModule module, Action action, bool asycn, Action callback)
            : base(module)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action = action;
            this.Asycn = asycn;
        }


        private Action action;
        public bool Asycn { set; get; }
        public Action Callback { set; get; }

        protected sealed override void WorkHandle()
        {
            if (this.Asycn)
            {
                this.action.BeginInvoke((ar) =>
                {
                    var callback = this.Callback;
                    if (callback != null) { callback.Invoke(); }
                }, null);
            }
            else
            {
                this.action.Invoke();
                var callback = this.Callback;
                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }
    }
}
