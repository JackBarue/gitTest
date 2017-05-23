namespace StRttmy.UI.Help
{
    partial class AboutUs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutUs));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tSB3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tSB2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tSB1 = new System.Windows.Forms.ToolStripButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSB3,
            this.toolStripSeparator3,
            this.tSB2,
            this.toolStripSeparator2,
            this.tSB1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(684, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tSB3
            // 
            this.tSB3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tSB3.Image = ((System.Drawing.Image)(resources.GetObject("tSB3.Image")));
            this.tSB3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB3.Name = "tSB3";
            this.tSB3.Size = new System.Drawing.Size(60, 22);
            this.tSB3.Text = "产品应用";
            this.tSB3.Click += new System.EventHandler(this.tSB3_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tSB2
            // 
            this.tSB2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tSB2.Image = ((System.Drawing.Image)(resources.GetObject("tSB2.Image")));
            this.tSB2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB2.Name = "tSB2";
            this.tSB2.Size = new System.Drawing.Size(60, 22);
            this.tSB2.Text = "产品特点";
            this.tSB2.Click += new System.EventHandler(this.tSB2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tSB1
            // 
            this.tSB1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tSB1.Image = ((System.Drawing.Image)(resources.GetObject("tSB1.Image")));
            this.tSB1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB1.Name = "tSB1";
            this.tSB1.Size = new System.Drawing.Size(60, 22);
            this.tSB1.Text = "公司介绍";
            this.tSB1.Click += new System.EventHandler(this.tSB1_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(684, 437);
            this.webBrowser1.TabIndex = 2;
            // 
            // AboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutUs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于我们";
            this.Load += new System.EventHandler(this.AboutUs_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tSB3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tSB2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tSB1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}