using CefSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinChrome_HttpWatch
{
    public class RequestHandler : IRequestHandler
    {
        public event Action<string, string, NameValueCollection, NameValueCollection, byte[]> NotifyResult;

        public static readonly string VersionNumberString = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}",
            Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion);

        bool IRequestHandler.OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        {
            return false;
        }

        bool IRequestHandler.OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return OnOpenUrlFromTab(browserControl, browser, frame, targetUrl, targetDisposition, userGesture);
        }

        protected virtual bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return false;
        }

        bool IRequestHandler.OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
            //callback.Dispose();
            //return false;

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    //To allow certificate
                    //callback.Continue(true);
                    //return true;
                }
            }

            return false;
        }

        void IRequestHandler.OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        {
            // TODO: Add your own code here for handling scenarios where a plugin crashed, for one reason or another.
        }

        CefReturnValue IRequestHandler.OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            //Example of how to set Referer
            // Same should work when setting any header

            // For this example only set Referer when using our custom scheme
            var url = new Uri(request.Url);

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    if (request.Method == "POST")
                    {
                        using (var postData = request.PostData)
                        {
                            if (postData != null)
                            {
                                var elements = postData.Elements;

                                var charSet = request.GetCharSet();

                                foreach (var element in elements)
                                {
                                    if (element.Type == PostDataElementType.Bytes)
                                    {
                                        var body = element.GetBody(charSet);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return CefReturnValue.Continue;
        }

        bool IRequestHandler.GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.

            callback.Dispose();
            return false;
        }

        void IRequestHandler.OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        {
            // TODO: Add your own code here for handling scenarios where the Render Process terminated for one reason or another.
            // browserControl.Load(CefExample.RenderProcessCrashedUrl);
        }

        bool IRequestHandler.OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
            //callback.Dispose();
            //return false;

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    //Accept Request to raise Quota
                    //callback.Continue(true);
                    //return true;
                }
            }

            return false;
        }

        void IRequestHandler.OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
        {
            //Example of how to redirect - need to check `newUrl` in the second pass
            //if (string.Equals(frame.GetUrl(), "https://www.google.com/", StringComparison.OrdinalIgnoreCase) && !newUrl.Contains("github"))
            //{
            //	newUrl = "https://github.com";
            //}
        }

        bool IRequestHandler.OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            return url.StartsWith("mailto");
        }

        void IRequestHandler.OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        {

        }

        bool IRequestHandler.OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //NOTE: You cannot modify the response, only the request
            // You can now access the headers
            //var headers = response.ResponseHeaders;

            //NOTE: You cannot modify the response, only the request
            // You can now access the headers
            //var headers = response.ResponseHeaders;

            return false;
        }

        IResponseFilter IRequestHandler.GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            var url = new Uri(request.Url);
            if (url.AbsoluteUri == "th://empty")
            {
                return new EmptyPageFilter();
            }
            else
            {
                var pageFilter = FilterManager.CreateFilter(request.Identifier.ToString(), url.AbsoluteUri);
                pageFilter.NotifyResult += pageFilter_NotifyResult;

                return pageFilter;
            }
        }

        private void pageFilter_NotifyResult(string guid, string url, NameValueCollection req, NameValueCollection resp, byte[] data)
        {
            if (NotifyResult != null)
            {
                NotifyResult(guid, url, req, resp, data);
            }
        }

        void IRequestHandler.OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            try
            {
                var pageFilter = FilterManager.GetFileter(request.Identifier.ToString());
                if (pageFilter != null)
                {
                    pageFilter.SetHeader(request.Headers, response.ResponseHeaders);
                    pageFilter.SendNotify();
                }
            }
            catch { }
        }
    }

    public class PageFilter : IResponseFilter
    {
        public event Action<string, string, NameValueCollection, NameValueCollection, byte[]> NotifyResult;
        private List<byte> dataAll = new List<byte>();
        private int contentLength = 0;
        private string guid = string.Empty;
        private string url = string.Empty;
        private NameValueCollection requestHeader;
        private NameValueCollection responseHeader;

        public PageFilter(string guid, string url)
        {
            this.guid = guid;
            this.url = url;
        }

        public void SetContentLength(int contentLength)
        {
            this.contentLength = contentLength;
        }

        public void SetHeader(NameValueCollection requestHeader, NameValueCollection responseHeader)
        {
            this.requestHeader = requestHeader;
            this.responseHeader = responseHeader;
        }

        public FilterStatus Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            try
            {
                if (dataIn == null)
                {
                    dataInRead = 0;
                    dataOutWritten = 0;

                    return FilterStatus.Done;
                }

                dataInRead = dataIn.Length;
                dataOutWritten = Math.Min(dataInRead, dataOut.Length);

                dataIn.CopyTo(dataOut);
                dataIn.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[dataIn.Length];
                dataIn.Read(bs, 0, bs.Length);
                dataAll.AddRange(bs);
                if (this.contentLength == 0)
                {
                    if (NotifyResult != null)
                    {
                        NotifyResult(guid, this.url, this.requestHeader, this.responseHeader, dataAll.ToArray());
                    }

                    return FilterStatus.Done;
                }
                else if (dataAll.Count == this.contentLength)
                {
                    if (NotifyResult != null)
                    {
                        NotifyResult(guid, this.url, this.requestHeader, this.responseHeader, dataAll.ToArray());
                    }

                    return FilterStatus.Done;
                }
                else if (dataAll.Count < this.contentLength)
                {
                    dataInRead = dataIn.Length;
                    dataOutWritten = dataIn.Length;

                    return FilterStatus.NeedMoreData;
                }
                else
                {
                    return FilterStatus.Error;
                }
            }
            catch (Exception ex)
            {
                dataInRead = dataIn.Length;
                dataOutWritten = dataIn.Length;
                // Logs.WriteLog(LogType.Error, "dataIn.Length:" + dataIn.Length + ";dataOut.Length:" + dataOut.Length);
                // Logs.WriteLog(LogType.Error, ex.ToString() + "\r\n");

                return FilterStatus.Done;
            }
        }

        public bool InitFilter()
        {
            return true;
        }
    }

    public class TestPageFilter : IResponseFilter
    {
        public event Action<string, string, NameValueCollection, NameValueCollection, byte[]> NotifyResult;
        private string guid = string.Empty;
        private string url = string.Empty;
        private NameValueCollection requestHeader;
        private NameValueCollection responseHeader;
        public List<byte> dataAll = new List<byte>();

        public TestPageFilter(string guid, string url)
        {
            this.guid = guid;
            this.url = url;
        }

        public void SetHeader(NameValueCollection requestHeader, NameValueCollection responseHeader)
        {
            this.requestHeader = requestHeader;
            this.responseHeader = responseHeader;
        }

        public FilterStatus Filter(System.IO.Stream dataIn, out long dataInRead, System.IO.Stream dataOut, out long dataOutWritten)
        {
            try
            {
                if (dataIn == null || dataIn.Length == 0)
                {
                    dataInRead = 0;
                    dataOutWritten = 0;

                    return FilterStatus.NeedMoreData;
                }

                dataInRead = dataIn.Length;
                dataOutWritten = Math.Min(dataInRead, dataOut.Length);

                dataIn.CopyTo(dataOut);
                dataIn.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[dataIn.Length];
                dataIn.Read(bs, 0, bs.Length);
                dataAll.AddRange(bs);

                dataInRead = dataIn.Length;
                dataOutWritten = dataIn.Length;

                return FilterStatus.NeedMoreData;
            }
            catch (Exception ex)
            {
                dataInRead = dataIn.Length;
                dataOutWritten = dataIn.Length;

                return FilterStatus.Done;
            }
        }

        public bool InitFilter()
        {
            return true;
        }

        public void SendNotify()
        {
            if (NotifyResult != null)
            {
                NotifyResult(guid, this.url, this.requestHeader, this.responseHeader, dataAll.ToArray());
            }
        }
    }

    public class EmptyPageFilter : IResponseFilter
    {
        public FilterStatus Filter(Stream dataIn, out long dataInRead, Stream dataOut, out long dataOutWritten)
        {
            dataInRead = 0;
            string emptyString = "<h2>空白页</h2>";
            var bs = Encoding.UTF8.GetBytes(emptyString);
            dataOut.Write(bs, 0, bs.Length);
            dataOutWritten = dataOut.Length;

            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            return true;
        }
    }
}
