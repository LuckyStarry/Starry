﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starry.Web.Controls;

namespace Starry
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var control = new HtmlContainerControl("div").Css("height", "100px").Css("height", "100%");
                control.Children.Add(new HtmlControl("input").Attr("type", "text").Class("label label-success").Hide().Css("width", "100"));
                control.Children.Add(new HtmlControl("input").Attr("type", "submit").Class("btn").Class("btn-success").RemoveClass("btn-success"));
                control.Children.Add(new HtmlInputButton());
                control.Children.Add(new HtmlAnchor().HRef("http://www.sonhaku.com"));
                Console.WriteLine(control);
                Console.WriteLine("---FINISHED---");
            }
            catch (Exception ex)
            {
                Console.WriteLine("---EXCEPTION---");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }

    class C
    {
        public int V1 { set; get; }
        public int V2 { set; get; }
        public int V3 { set; get; }
        public int V4 { set; get; }
        public int V5 { set; get; }
    }
}
