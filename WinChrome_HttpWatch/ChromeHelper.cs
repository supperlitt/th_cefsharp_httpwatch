using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace WinChrome_HttpWatch
{
    public class ChromeHelper
    {
        private string url = string.Empty;
        private Random random = new Random((int)DateTime.Now.ToFileTimeUtc());
        public ChromiumWebBrowser browser = null;
        public Image codeImage = null;
        private bool isCapture = false;

        public void Init()
        {
            var setting = new CefSharp.CefSettings();
            setting.RegisterScheme(new CefCustomScheme
            {
                SchemeName = CefSharpSchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new CefSharpSchemeHandlerFactory()
            });

            // 设置语言
            setting.Locale = "zh-CN"; // en-US
            CefSharp.Cef.Initialize(setting, true, false);
        }

        /// <summary>
        /// 操作构造函数
        /// </summary>
        /// <param name="url"></param>
        public ChromeHelper(string url)
        {
            this.url = url;
        }

        public void ReLoad()
        {
            this.browser.Reload();
        }

        /// <summary>
        /// 初始化浏览器信息
        /// </summary>
        /// <returns></returns>
        public ChromiumWebBrowser CreateBrowser()
        {
            browser = new ChromiumWebBrowser(this.url);
            var requestHandler = new RequestHandler();
            requestHandler.NotifyResult += requestHandler_NotifyResult;
            browser.RequestHandler = requestHandler;
            browser.LifeSpanHandler = new LifeSpanHandler();
            browser.DownloadHandler = new DownloadHandler();
            browser.Dock = System.Windows.Forms.DockStyle.Fill;
            CefSharpSettings.ShutdownOnExit = true;

            return browser;
        }

        public void StartCapture()
        {
            isCapture = true;
        }

        public void StopCapture()
        {
            isCapture = false;
        }

        private void requestHandler_NotifyResult(string guid, string url, NameValueCollection request, NameValueCollection response, byte[] data)
        {
            if (isCapture)
            {
                LoadHelper.AddInfo(new LoadInfo()
                {
                    Key = guid,
                    Url = url,
                    RequestHeader = request,
                    ResponseHeader = response,
                    Data = data
                });
            }
        }

        private void NotifyImage(Image obj)
        {
            this.codeImage = obj;
        }

        public void Win32_Click(Point click_p)
        {
            // 1024*768的分辨率下
            var p = this.browser.PointToScreen(click_p);
            p = new Point() { X = p.X, Y = p.Y };
            Win32Api.MouseClick(p);
        }

        public void Win32_Write(string str)
        {
            Win32Api.KeyDownString(str);
        }

        public void Win32_WriteChs(string str)
        {
            // ^c+^v实现中文的输入         
            Clipboard.SetText(str);    // 将字符串复制到剪贴板，相当于^c

            Thread.Sleep(200);
            SendKeys.SendWait("^v");   // CTRL+V，粘贴
        }

        public void SendMainFrameJs(string js)
        {
            if (browser != null)
            {
                browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
            }
        }

        public void SendOtherFrameJs(string frameName, string js)
        {
            if (browser != null)
            {
                IFrame frame = browser.GetBrowser().GetFrame(frameName);
                if (frame != null)
                {
                    frame.ExecuteJavaScriptAsync(js);
                }
            }
        }

        /// <summary>
        /// 跳转页面
        /// </summary>
        /// <param name="url"></param>
        public void JumpUrl(string url)
        {
            browser.Load(url);
        }

        public void Back()
        {
            browser.Back();
        }

        public void Forward()
        {
            browser.Forward();
        }

        public Point GetBrowserPoint()
        {
            return this.browser.PointToScreen(new Point(0, 0));
        }

        public void ClearCookie()
        {
            if (browser != null)
            {
                var cookieManager = Cef.GetGlobalCookieManager();

                // 都为空，清楚所有的cookie
                cookieManager.DeleteCookiesAsync("", "");
            }
        }

        public void Dispose()
        {
            if (browser != null)
            {
                browser.GetBrowser().CloseBrowser(true);
                browser.Dispose();
            }
        }
    }
}
