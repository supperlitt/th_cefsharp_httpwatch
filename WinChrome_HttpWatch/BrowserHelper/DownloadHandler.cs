using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome_HttpWatch
{
    public class DownloadHandler : IDownloadHandler
    {
        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            DownLoadManager.AddDownLoadInfo(new DownLoadInfo()
            {
                Guid = downloadItem.Id.ToString(),
                FileSize = downloadItem.TotalBytes,
                ReceiveSize = downloadItem.ReceivedBytes,
                AddTime = DateTime.Now,
                DownLoadUrl = downloadItem.Url,
                Speed = downloadItem.CurrentSpeed,
                Percent = downloadItem.PercentComplete,
                SaveFileName = downloadItem.SuggestedFileName
            });
        }
    }
}
