using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Starry.Services
{
    public class CustomizeHandler : Core.Handler
    {
        public CustomizeHandler(Core.IModule module, Action<CancellationToken> action) : this(module, action, false) { }
        public CustomizeHandler(Core.IModule module, Action<CancellationToken> action, bool asycn) : this(module, action, false, null) { }
        public CustomizeHandler(Core.IModule module, Action<CancellationToken> action, bool asycn, Action callback)
            : base(module)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            this.action = action;
            this.Asycn = asycn;
        }

        public CustomizeHandler(Core.IModule module, Action action) : this(module, action, false) { }
        public CustomizeHandler(Core.IModule module, Action action, bool asycn) : this(module, action, false, null) { }
        public CustomizeHandler(Core.IModule module, Action action, bool asycn, Action callback)
            : this(module, cancellationToken => { action.Invoke(); }, asycn, callback) { }


        private Action<CancellationToken> action;
        public bool Asycn { set; get; }
        public Action Callback { set; get; }

        protected sealed override void OnHandle(CancellationToken cancellationToken)
        {
            if (this.Asycn)
            {
                this.action.BeginInvoke(cancellationToken, (ar) =>
                {
                    var callback = this.Callback;
                    if (callback != null) { callback.Invoke(); }
                }, null);
            }
            else
            {
                this.action.Invoke(cancellationToken);
                var callback = this.Callback;
                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }
    }
}
