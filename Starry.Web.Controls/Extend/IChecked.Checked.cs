using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class ICheckedExtend
    {
        public static TControl Checked<TControl>(this TControl control, bool @checked) where TControl : Attributes.IChecked
        {
            if (@checked)
            {
                return control.Attr("checked", "checked");
            }
            else
            {
                return control.RemoveAttr("checked");
            }
        }

        public static bool Checked<TControl>(this TControl control) where TControl : Attributes.IChecked
        {
            return !string.IsNullOrEmpty(control.Attr("checked").Trim());
        }
    }
}
