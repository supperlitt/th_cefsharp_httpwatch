using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinChrome_HttpWatch
{
    public class DownLoadManager
    {
        private static List<DownLoadInfo> dataList = new List<DownLoadInfo>();

        public static void AddDownLoadInfo(DownLoadInfo info)
        {
            lock (dataList)
            {
                var model = dataList.Find(p => p.Guid == info.Guid);
                if (model == null)
                {
                    dataList.Add(info);
                }
                else
                {
                    model.ReceiveSize = info.ReceiveSize;
                    model.Percent = info.Percent;
                    model.Speed = info.Speed;
                }
            }
        }

        public static List<DownLoadInfo> GetAll()
        {
            lock (dataList)
            {
                return dataList;
            }
        }
    }

    public class DownLoadInfo
    {
        public string Guid { get; set; }

        public string SaveFileName { get; set; }

        public string DownLoadUrl { get; set; }

        public DateTime AddTime { get; set; }

        public string AddTimeStr { get { return AddTime.ToString("yyyy-MM-dd HH:mm"); } }

        public long FileSize { get; set; }

        public string FileSizeStr
        {
            get
            {
                if (FileSize >= 1073741824) // 1024 * 1024 * 1024  G
                {
                    return (FileSize / 1073741824f).ToString("F2") + "G";
                }
                else if (FileSize >= 1048576f) // 1024 * 1024  M
                {
                    return (FileSize / 1073741824f).ToString("F2") + "M";
                }
                else if (FileSize >= 1024f) // 1024 K
                {
                    return (FileSize / 1024f).ToString("F2") + "K";
                }
                else // 字节
                {
                    return FileSize + "字节";
                }
            }
        }

        public long ReceiveSize { get; set; }

        public string ReceiveSizeStr
        {
            get
            {
                if (ReceiveSize >= 1073741824) // 1024 * 1024 * 1024  G
                {
                    return (ReceiveSize / 1073741824f).ToString("F2") + "G";
                }
                else if (ReceiveSize >= 1048576f) // 1024 * 1024  M
                {
                    return (ReceiveSize / 1073741824f).ToString("F2") + "M";
                }
                else if (ReceiveSize >= 1024f) // 1024 K
                {
                    return (ReceiveSize / 1024f).ToString("F2") + "K";
                }
                else // 字节
                {
                    return ReceiveSize + "字节";
                }
            }
        }

        public int Percent { get; set; }

        public long Speed { get; set; }

        public string SpeedStr
        {
            get
            {
                if (Speed >= 1073741824) // 1024 * 1024 * 1024  G
                {
                    return (Speed / 1073741824f).ToString("F2") + "G/s";
                }
                else if (Speed >= 1048576f) // 1024 * 1024  M
                {
                    return (Speed / 1073741824f).ToString("F2") + "M/s";
                }
                else if (Speed >= 1024f) // 1024 K
                {
                    return (Speed / 1024f).ToString("F2") + "K/s";
                }
                else // 字节
                {
                    return Speed + "字节/秒";
                }
            }
        }

        public string NeedTime
        {
            get
            {
                // 剩余时间
                if (ReceiveSize < FileSize)
                {
                    long size = FileSize - ReceiveSize;
                    if (Speed == 0)
                    {
                        return "未知";
                    }
                    else
                    {
                        int time = (int)(size / Speed);
                        if (time > 86400)
                        {
                            double v = time / 86400f;
                            if (v > 99)
                            {
                                return "未知";
                            }
                            else
                            {
                                return time + "天";
                            }
                        }
                        else if (time > 3600)
                        {
                            double v = time / 3600f;
                            return v + "小时";
                        }
                        else if (time > 60)
                        {
                            double v = time / 60f;

                            return v + "分钟";
                        }
                        else
                        {
                            return time + "秒";
                        }
                    }
                }
                else
                {
                    return "0秒";
                }
            }
        }
    }
}
