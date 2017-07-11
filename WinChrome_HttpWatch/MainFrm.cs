using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinChrome_HttpWatch
{
    public partial class MainFrm : Form
    {
        private ChromeHelper chrome = null;
        private CaptureListFrm captureFrm = null;
        private DownLoadFrm downFrm = null;

        public MainFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (captureFrm == null)
            {
                captureFrm = new CaptureListFrm(this.chrome);
                captureFrm.Show();
            }
            else
            {
                captureFrm.Show();
            }
        }

        private void btnNav_Click(object sender, EventArgs e)
        {
            this.chrome.JumpUrl(this.txtUrl.Text);
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Application.ApplicationExit += Application_ApplicationExit;
            Execute();

            // 使用线程的bug，需要在创建线程中调用关闭才能起作用
            //Thread t = new Thread(new ThreadStart(Execute));
            //t.IsBackground = true;
            //t.Start();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }

        private void Execute()
        {
            if (chrome == null)
            {
                chrome = new ChromeHelper("th://empty");
                chrome.Init();
                var browser = chrome.CreateBrowser();
                this.Invoke(new Action<Panel>(p =>
                {
                    p.Controls.Add(browser);
                    p.Update();
                }), this.panel1);
            }
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
            this.chrome.Back();
        }

        private void btnAfter_Click(object sender, EventArgs e)
        {
            this.chrome.Forward();
        }

        private void btnDownLoadInfo_Click(object sender, EventArgs e)
        {
            if (downFrm == null)
            {
                downFrm = new DownLoadFrm();
                downFrm.Show();
            }
            else
            {
                downFrm.Show();
            }
        }
    }
}
