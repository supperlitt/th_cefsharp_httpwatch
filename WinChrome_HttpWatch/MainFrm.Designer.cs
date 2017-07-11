namespace WinChrome_HttpWatch
{
    partial class MainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnNav = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnBefore = new System.Windows.Forms.Button();
            this.btnAfter = new System.Windows.Forms.Button();
            this.btnDownLoadInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 660);
            this.panel1.TabIndex = 0;
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUrl.Location = new System.Drawing.Point(2, 8);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(376, 23);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "th://empty";
            // 
            // btnNav
            // 
            this.btnNav.Location = new System.Drawing.Point(396, 8);
            this.btnNav.Name = "btnNav";
            this.btnNav.Size = new System.Drawing.Size(59, 23);
            this.btnNav.TabIndex = 2;
            this.btnNav.Text = "跳转";
            this.btnNav.UseVisualStyleBackColor = true;
            this.btnNav.Click += new System.EventHandler(this.btnNav_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(478, 8);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 3;
            this.btnCapture.Text = "抓包管理";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnBefore
            // 
            this.btnBefore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnBefore.FlatAppearance.BorderSize = 0;
            this.btnBefore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBefore.Location = new System.Drawing.Point(600, 7);
            this.btnBefore.Name = "btnBefore";
            this.btnBefore.Size = new System.Drawing.Size(35, 23);
            this.btnBefore.TabIndex = 4;
            this.btnBefore.Text = "<-";
            this.btnBefore.UseVisualStyleBackColor = false;
            this.btnBefore.Click += new System.EventHandler(this.btnBefore_Click);
            // 
            // btnAfter
            // 
            this.btnAfter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAfter.FlatAppearance.BorderSize = 0;
            this.btnAfter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAfter.Location = new System.Drawing.Point(641, 8);
            this.btnAfter.Name = "btnAfter";
            this.btnAfter.Size = new System.Drawing.Size(35, 23);
            this.btnAfter.TabIndex = 4;
            this.btnAfter.Text = "->";
            this.btnAfter.UseVisualStyleBackColor = false;
            this.btnAfter.Click += new System.EventHandler(this.btnAfter_Click);
            // 
            // btnDownLoadInfo
            // 
            this.btnDownLoadInfo.Location = new System.Drawing.Point(701, 8);
            this.btnDownLoadInfo.Name = "btnDownLoadInfo";
            this.btnDownLoadInfo.Size = new System.Drawing.Size(71, 23);
            this.btnDownLoadInfo.TabIndex = 5;
            this.btnDownLoadInfo.Text = "下载详情";
            this.btnDownLoadInfo.UseVisualStyleBackColor = true;
            this.btnDownLoadInfo.Click += new System.EventHandler(this.btnDownLoadInfo_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 702);
            this.Controls.Add(this.btnDownLoadInfo);
            this.Controls.Add(this.btnAfter);
            this.Controls.Add(this.btnBefore);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnNav);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.panel1);
            this.Name = "MainFrm";
            this.Text = "th";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnNav;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnBefore;
        private System.Windows.Forms.Button btnAfter;
        private System.Windows.Forms.Button btnDownLoadInfo;
    }
}

