using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;

namespace WinChrome_HttpWatch
{
    public class LoadHelper
    {
        private static List<LoadInfo> dataList = new List<LoadInfo>();

        public static void AddInfo(LoadInfo info)
        {
            lock (dataList)
            {
                info.Index = dataList.Count + 1;
                dataList.Add(info);
            }
        }

        public static List<LoadInfo> GetAll()
        {
            lock (dataList)
            {
                return dataList;
            }
        }

        public static List<LoadInfo> GetNew(int index)
        {
            lock (dataList)
            {
                return dataList.FindAll(p => p.Index > index);
            }
        }

        public static LoadInfo GetInfo(string key)
        {
            lock (dataList)
            {
                return dataList.Find(p => p.Key == key);
            }
        }

        public static void ClearAll()
        {
            lock (dataList)
            {
                dataList.Clear();
                Thread.Sleep(200);
            }
        }
    }

    public class LoadInfo
    {
        public int Index { get; set; }

        public string Key { get; set; }

        public string Url { get; set; }

        private NameValueCollection requestHeader = new NameValueCollection();

        public NameValueCollection RequestHeader
        {
            get { return requestHeader; }
            set { requestHeader = value; }
        }

        private NameValueCollection responseHeader = new NameValueCollection();

        public NameValueCollection ResponseHeader
        {
            get { return responseHeader; }
            set { responseHeader = value; }
        }

        public byte[] Data { get; set; }
    }
}
