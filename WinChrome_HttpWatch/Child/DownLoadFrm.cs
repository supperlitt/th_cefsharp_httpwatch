using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinChrome_HttpWatch
{
    public partial class DownLoadFrm : Form
    {
        public DownLoadFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.lstView.FullRowSelect = true;
            this.lstView.ProgressColumnIndex = 5;
            this.lstView.Columns.Add("文件名", 180, HorizontalAlignment.Left);
            this.lstView.Columns.Add("文件大小", 80, HorizontalAlignment.Left);
            this.lstView.Columns.Add("开始时间", 110, HorizontalAlignment.Left);
            this.lstView.Columns.Add("下载速度", 60, HorizontalAlignment.Left);
            this.lstView.Columns.Add("剩余时间", 100, HorizontalAlignment.Left);
            this.lstView.Columns.Add("状态", 100, HorizontalAlignment.Left);
        }

        private void DownLoadFrm_Load(object sender, EventArgs e)
        {
            var list = DownLoadManager.GetAll();
            if (list.Count > 0)
            {
                this.Invoke(new Action<ProgressListview>(p =>
                {
                    foreach (var model in list)
                    {
                        ListViewItem item = new ListViewItem(model.SaveFileName);
                        item.SubItems.AddRange(new string[] { model.FileSizeStr, model.AddTimeStr, model.SpeedStr,  });
                    }
                }), this.lstView);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
