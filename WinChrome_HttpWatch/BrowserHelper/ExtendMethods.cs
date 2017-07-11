using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome_HttpWatch
{
    public static class ExtendMethods
    {
        public static void ClickId(this IFrame frame, string id)
        {
            frame.ExecuteJavaScriptAsync("if(document.getElementById('" + id + "')){ document.getElementById('" + id + "').click(); }");
        }

        public static void Focus(this IFrame frame, string id)
        {
            frame.ExecuteJavaScriptAsync("if(document.getElementById('" + id + "')){ document.getElementById('" + id + "').focus(); }");
        }

        public static void Blur(this IFrame frame, string id)
        {
            frame.ExecuteJavaScriptAsync("if(document.getElementById('" + id + "')){ document.getElementById('" + id + "').blur(); }");
        }

        public static void SetIdValue(this IFrame frame, string id, string value)
        {
            frame.ExecuteJavaScriptAsync("if(document.getElementById('" + id + "')){ document.getElementById('" + id + "').value='" + value + "'; }");
        }
    }
}
