using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.Controls
{
    public static partial class HtmlContainerControlExtend
    {
        public static IEnumerable<THtmlElement> Find<TControl, THtmlElement>(this TControl control)
            where TControl : Interface.IHtmlContainerControl
            where THtmlElement : Interface.IHtmlElement
        {
            return control.Children.Where(ele => ele is THtmlElement).Select(ele => (THtmlElement)ele).ToList();
        }

        public static IEnumerable<THtmlElement> Find<TControl, THtmlElement>(this IEnumerable<TControl> controls)
            where TControl : Interface.IHtmlContainerControl
            where THtmlElement : Interface.IHtmlElement
        {
            var results = new List<THtmlElement>();
            if (controls != null && controls.Any())
            {
                foreach (var control in controls)
                {
                    results.AddRange(control.Find<TControl, THtmlElement>());
                }
            }
            return results;
        }

        public static IEnumerable<THtmlElement> Find<TControl, THtmlElement>(this TControl control, Func<THtmlElement, bool> expression)
            where TControl : Interface.IHtmlContainerControl
            where THtmlElement : Interface.IHtmlElement
        {
            return control.Find<TControl, THtmlElement>().Where(expression).ToList();
        }

        public static IEnumerable<THtmlElement> Find<TControl, THtmlElement>(this IEnumerable<TControl> controls, Func<THtmlElement, bool> expression)
            where TControl : Interface.IHtmlContainerControl
            where THtmlElement : Interface.IHtmlElement
        {
            return controls.Find<TControl, THtmlElement>().Where(expression).ToList();
        }
    }
}
