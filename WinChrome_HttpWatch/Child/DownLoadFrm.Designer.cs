namespace WinChrome_HttpWatch
{
    partial class DownLoadFrm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstView = new WinChrome_HttpWatch.ProgressListview(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstView
            // 
            this.lstView.Location = new System.Drawing.Point(13, 32);
            this.lstView.Name = "lstView";
            this.lstView.ProgressColumnIndex = -1;
            this.lstView.Size = new System.Drawing.Size(619, 275);
            this.lstView.TabIndex = 0;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(13, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(57, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(92, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(57, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // DownLoadFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 320);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lstView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DownLoadFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "下载详情";
            this.Load += new System.EventHandler(this.DownLoadFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressListview lstView;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
    }
}