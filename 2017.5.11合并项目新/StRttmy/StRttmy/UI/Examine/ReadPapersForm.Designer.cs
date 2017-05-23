namespace StRttmy.UI
{
    partial class ReadPapersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReadPapersForm));
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dgvStudentPaperList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentPaperList)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKeyword
            // 
            this.txtKeyword.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txtKeyword.Location = new System.Drawing.Point(59, 19);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(2);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(449, 21);
            this.txtKeyword.TabIndex = 2;
            this.txtKeyword.Text = "试卷\\班级名\\学员姓名";
            this.txtKeyword.Click += new System.EventHandler(this.txtKeyword_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 81;
            this.label7.Text = "关键字：";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(530, 17);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(56, 22);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dgvStudentPaperList
            // 
            this.dgvStudentPaperList.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudentPaperList.ColumnHeadersHeight = 30;
            this.dgvStudentPaperList.Location = new System.Drawing.Point(0, 45);
            this.dgvStudentPaperList.Name = "dgvStudentPaperList";
            this.dgvStudentPaperList.RowTemplate.Height = 30;
            this.dgvStudentPaperList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentPaperList.Size = new System.Drawing.Size(612, 388);
            this.dgvStudentPaperList.TabIndex = 84;
            this.dgvStudentPaperList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentPaperList_CellDoubleClick);
            // 
            // ReadPapersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(612, 433);
            this.Controls.Add(this.dgvStudentPaperList);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadPapersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试卷批阅";
            this.Load += new System.EventHandler(this.ReadPapersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentPaperList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvStudentPaperList;
    }
}