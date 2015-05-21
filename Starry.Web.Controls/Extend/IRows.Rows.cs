using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class IRowsExtend
    {
        public static TControl Rows<TControl>(this TControl control, int rows) where TControl : Attributes.IRows
        {
            control.Rows = rows;
            return control;
        }

        public static int Rows<TControl>(this TControl control) where TControl : Attributes.IRows
        {
            return control.Rows;
        }
    }
}
