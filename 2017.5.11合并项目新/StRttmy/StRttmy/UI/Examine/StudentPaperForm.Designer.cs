namespace StRttmy.UI
{
    partial class StudentPaperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentPaperForm));
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTypeofWork = new System.Windows.Forms.ComboBox();
            this.cmbSystem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTestState = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgvStudentPaperList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentPaperList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(565, 13);
            this.cmbSubject.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(83, 22);
            this.cmbSubject.TabIndex = 99;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.SelectionChangeCommitted += new System.EventHandler(this.cmbSubject_SelectionChangeCommitted);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(527, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 100;
            this.label5.Text = "科目：";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(437, 14);
            this.cmbLevel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(83, 22);
            this.cmbLevel.TabIndex = 97;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.SelectionChangeCommitted += new System.EventHandler(this.cmbLevel_SelectionChangeCommitted);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 98;
            this.label4.Text = "等级：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(302, 14);
            this.cmbGenre.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(83, 22);
            this.cmbGenre.TabIndex = 95;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.SelectionChangeCommitted += new System.EventHandler(this.cmbGenre_SelectionChangeCommitted);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 96;
            this.label3.Text = "类别：";
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(174, 14);
            this.cmbTypeofWork.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTypeofWork.Name = "cmbTypeofWork";
            this.cmbTypeofWork.Size = new System.Drawing.Size(83, 22);
            this.cmbTypeofWork.TabIndex = 93;
            this.cmbTypeofWork.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTypeofWork_DrawItem);
            this.cmbTypeofWork.SelectedIndexChanged += new System.EventHandler(this.cmbTypeofWork_SelectedIndexChanged);
            this.cmbTypeofWork.SelectionChangeCommitted += new System.EventHandler(this.cmbTypeofWork_SelectionChangeCommitted);
            this.cmbTypeofWork.DropDownClosed += new System.EventHandler(this.cmbTypeofWork_DropDownClosed);
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(47, 13);
            this.cmbSystem.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(83, 22);
            this.cmbSystem.TabIndex = 91;
            this.cmbSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSystem_DrawItem);
            this.cmbSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSystem_SelectedIndexChanged);
            this.cmbSystem.SelectionChangeCommitted += new System.EventHandler(this.cmbSystem_SelectionChangeCommitted);
            this.cmbSystem.DropDownClosed += new System.EventHandler(this.cmbSystem_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 94;
            this.label2.Text = "工种：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 92;
            this.label1.Text = "系统：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(656, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "考试状态：";
            // 
            // cmbTestState
            // 
            this.cmbTestState.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTestState.FormattingEnabled = true;
            this.cmbTestState.Location = new System.Drawing.Point(717, 13);
            this.cmbTestState.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTestState.Name = "cmbTestState";
            this.cmbTestState.Size = new System.Drawing.Size(69, 22);
            this.cmbTestState.TabIndex = 102;
            this.cmbTestState.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTestState_DrawItem);
            this.cmbTestState.SelectionChangeCommitted += new System.EventHandler(this.cmbTestState_SelectionChangeCommitted);
            this.cmbTestState.DropDownClosed += new System.EventHandler(this.cmbTestState_DropDownClosed);
            // 
            // dgvStudentPaperList
            // 
            this.dgvStudentPaperList.BackgroundColor = System.Drawing.Color.White;
            this.dgvStudentPaperList.ColumnHeadersHeight = 30;
            this.dgvStudentPaperList.Location = new System.Drawing.Point(0, 39);
            this.dgvStudentPaperList.Name = "dgvStudentPaperList";
            this.dgvStudentPaperList.RowTemplate.Height = 30;
            this.dgvStudentPaperList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentPaperList.Size = new System.Drawing.Size(797, 313);
            this.dgvStudentPaperList.TabIndex = 90;
            this.dgvStudentPaperList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentPaperList_CellDoubleClick);
            this.dgvStudentPaperList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvStudentPaperList_RowPostPaint);
            // 
            // StudentPaperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(797, 352);
            this.Controls.Add(this.cmbTestState);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbGenre);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTypeofWork);
            this.Controls.Add(this.cmbSystem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvStudentPaperList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StudentPaperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试卷信息";
            this.Load += new System.EventHandler(this.StudentPaperForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentPaperList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbGenre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTypeofWork;
        private System.Windows.Forms.ComboBox cmbSystem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTestState;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dgvStudentPaperList;
    }
}