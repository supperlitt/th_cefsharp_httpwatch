using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinChrome_HttpWatch
{
    public class Win32Api
    {
        public static Random random = new Random((int)DateTime.Now.ToFileTimeUtc());

        #region Win32 API
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int MK_LBUTTON = 0x1;
        public const int WM_SYSKEYUP = 0X105;
        public const int WM_SYSKEYDOWN = 0X104;
        public const int MOUSEEVENTF_MOVE = 0x0001;//模拟鼠标移动
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标左键按下
        public const int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起

        // 缓冲区变量
        private const uint PURGE_TXABORT = 0x0001; // Kill the pending/current writes to the comm port. 
        private const uint PURGE_RXABORT = 0x0002; // Kill the pending/current reads to the comm port. 
        private const uint PURGE_TXCLEAR = 0x0004; // Kill the transmit queue if there. 
        private const uint PURGE_RXCLEAR = 0x0008; // Kill the typeahead buffer if there. 

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hwnd, uint Msg, long wParam, long lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// 设置鼠标位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern int SetCursorPos(int x, int y);

        /// <summary>
        /// 最大化窗口，最小化窗口，正常大小窗口；
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        /// 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        /// <summary>
        /// 获取窗口大小及位置
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        /// <summary>
        /// 得到当前活动的窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern System.IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public extern static int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
        byte bVk, //虚拟键值
        byte bScan,// 一般为0
        int dwFlags, //这里是整数类型 0 为按下，2为释放
        int dwExtraInfo //这里是整数类型 一般情况下设成为 0
        );
        #endregion

        /// <summary>
        /// 单击左键
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        public static void MouseClick(int rX, int rY)
        {
            SetCursorPos(rX, rY);
            // 使用组合构造一次单击
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, rX, rY, 0, 0);
        }

        /// <summary>
        /// 单击左键
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        public static void MouseClick(Point p)
        {
            SetCursorPos(p.X, p.Y);
            // 使用组合构造一次单击
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, p.X, p.Y, 0, 0);
        }

        public static void MoveAndClick(Point p)
        {
            SetCursorPos(p.X, p.Y);
            Thread.Sleep(500);
            // 使用组合构造一次单击
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, p.X, p.Y, 0, 0);
        }

        public static void KeyDown(Keys key)
        {
            keybd_event((byte)key, 0, 0, 0);
            Thread.Sleep(50);
            keybd_event((byte)key, 0, 2, 0);
        }


        /// <summary>
        /// 输入英文时间
        /// </summary>
        /// <param name="value"></param>
        public static void KeyDownString(string value)
        {
            foreach (var c in value)
            {
                SendKeys.SendWait(c.ToString());
                Thread.Sleep(random.Next(300, 500));
            }
        }

        /// <summary>
        /// 输入中文时间加长
        /// </summary>
        /// <param name="value"></param>
        public static void KeyDownChsString(string value)
        {
            foreach (var c in value)
            {
                SendKeys.SendWait(c.ToString());
                Thread.Sleep(random.Next(200, 300) * 4);
            }
        }

        /// <summary>
        /// 设置窗口在最前面
        /// </summary>
        /// <param name="handle"></param>
        public static void SetWindowFirst(IntPtr handle)
        {
            Win32Api.ShowWindow(handle, 1);
            Thread.Sleep(300);
            Win32Api.SetWindowPos(handle, -1, 0, 0, 0, 0, 1 | 2);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left; //最左坐标
        public int Top; //最上坐标
        public int Right; //最右坐标
        public int Bottom; //最下坐标
    }
}
