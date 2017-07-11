using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome_HttpWatch
{
    public class LifeSpanHandler : ILifeSpanHandler
    {
        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = browserControl;
            newBrowser.Load(targetUrl);

            return true;
        }

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }
    }
}
