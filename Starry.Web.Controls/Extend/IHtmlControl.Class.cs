using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IHtmlControlExtend
    {
        public static TControl Class<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            if (!control.Classes.Any(@class => @class == name))
            {
                control.Classes.Add(name);
            }
            return control;
        }

        public static TControl RemoveClass<TControl>(this TControl control, string name) where TControl : Interface.IHtmlControl
        {
            control.Classes.Remove(name);
            return control;
        }
    }
}
