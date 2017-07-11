using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome_HttpWatch
{
    public class CefSharpSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public const string SchemeName = "th";

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            return new CefSharpSchemeHandler();
        }
    }
}
