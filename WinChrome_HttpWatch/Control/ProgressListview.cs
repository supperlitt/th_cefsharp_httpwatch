using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinChrome_HttpWatch
{
    public partial class ProgressListview : ListView
    {
        /// <summary>  
        /// 进度条列索引  
        /// </summary>  
        private int _progressColumnIndex = -1;
        public int ProgressColumnIndex
        {
            get
            {
                return _progressColumnIndex;
            }
            set
            {
                _progressColumnIndex = value;
            }
        }

        /// <summary>  
        /// 进度条最大值  
        /// </summary>  
        private int _progressMaximun = 100;
        public int ProgressMaximun
        {
            get
            {
                return _progressMaximun;
            }
        }

        public ProgressListview()
        {
            InitializeComponent();
        }

        public ProgressListview(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == ProgressColumnIndex)
            {
                var item = e.Item.SubItems[4];
                var rect = item.Bounds;

                //绘制进度条  
                var g = e.Graphics;
                var progressRect = new Rectangle(rect.X + 1, rect.Y + 3, rect.Width - 2, rect.Height - 5);
                g.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 1), progressRect);

                //绘制进度  
                var progressMaxWidth = progressRect.Width - 1;
                var unit = (progressMaxWidth * 1.0) / (_progressMaximun * 100);
                var fValue = float.Parse(item.Text);
                var percent = fValue * unit * 100;
                if (percent >= progressMaxWidth) percent = progressMaxWidth;
                g.FillRectangle(new SolidBrush(Color.Red), new RectangleF(progressRect.X + 1, progressRect.Y + 1, float.Parse(percent.ToString()), progressRect.Height - 1));

                //绘制进度百分比  
                percent = fValue;
                var percentText = string.Format("{0}% ...", percent);
                if (fValue >= _progressMaximun) percentText = "已完成";
                var size = TextRenderer.MeasureText(percentText.ToString(), Font);
                var x = rect.X + (progressRect.Width - size.Width) / 2.0;
                var y = rect.Y + (progressRect.Height - size.Height) / 2.0 + 3;
                g.DrawString(percentText, this.Font, new SolidBrush(Color.Black), float.Parse(x.ToString()), float.Parse(y.ToString()));
            }
            else
            {
                e.DrawDefault = true;
            }

            base.OnDrawSubItem(e);
        }

        public void SetProgress(int itemIndex, int value)
        {
            try
            {
                var columnWidth = this.Columns[ProgressColumnIndex].Width;
                var progressSubItem = this.Items[itemIndex].SubItems[ProgressColumnIndex];
                progressSubItem.Text = value.ToString();
            }
            catch (Exception ex)
            {
            }
        }
    }

    partial class ProgressListview
    {
        /// <summary>  
        /// Required designer variable.  
        /// </summary>  
        private System.ComponentModel.IContainer components = null;

        /// <summary>   
        /// Clean up any resources being used.  
        /// </summary>  
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>  
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>  
        /// Required method for Designer support - do not modify  
        /// the contents of this method with the code editor.  
        /// </summary>  
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        #endregion
    }
}
