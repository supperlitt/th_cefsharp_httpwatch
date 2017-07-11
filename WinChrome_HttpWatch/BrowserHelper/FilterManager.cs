using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome_HttpWatch
{
    public class FilterManager
    {
        private static Dictionary<string, TestPageFilter> dataList = new Dictionary<string, TestPageFilter>();

        public static TestPageFilter CreateFilter(string guid, string url)
        {
            lock (dataList)
            {
                if (dataList.ContainsKey(guid))
                {
                    return dataList[guid];
                }

                var filter = new TestPageFilter(guid, url);
                dataList.Add(guid, filter);

                return filter;
            }
        }

        public static TestPageFilter GetFileter(string guid)
        {
            lock (dataList)
            {
                if (dataList.ContainsKey(guid))
                {
                    var item = dataList[guid];
                    dataList.Remove(guid);

                    return item;
                }

                return null;
            }
        }
    }
}
