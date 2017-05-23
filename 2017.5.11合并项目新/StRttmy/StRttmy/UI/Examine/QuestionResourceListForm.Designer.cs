namespace StRttmy.UI
{
    partial class QuestionResourceListForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionResourceListForm));
            this.dgvResourceList = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResourceList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResourceList
            // 
            this.dgvResourceList.AllowUserToAddRows = false;
            this.dgvResourceList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResourceList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResourceList.ColumnHeadersHeight = 30;
            this.dgvResourceList.Location = new System.Drawing.Point(13, 121);
            this.dgvResourceList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvResourceList.Name = "dgvResourceList";
            this.dgvResourceList.ReadOnly = true;
            this.dgvResourceList.RowTemplate.Height = 30;
            this.dgvResourceList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvResourceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResourceList.Size = new System.Drawing.Size(385, 320);
            this.dgvResourceList.TabIndex = 5;
            this.dgvResourceList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResourceList_CellContentClick);
            this.dgvResourceList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResourceList_CellDoubleClick);
            this.dgvResourceList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvResourceList_RowPostPaint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 61;
            this.label6.Text = "关键字：";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(315, 11);
            this.cmbSubject.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(83, 22);
            this.cmbSubject.TabIndex = 59;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.SelectionChangeCommitted += new System.EventHandler(this.cmbSubject_SelectionChangeCommitted);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(279, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 60;
            this.label5.Text = "科目：";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(181, 52);
            this.cmbLevel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(83, 22);
            this.cmbLevel.TabIndex = 57;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.SelectionChangeCommitted += new System.EventHandler(this.cmbLevel_SelectionChangeCommitted);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "等级：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(47, 52);
            this.cmbGenre.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(83, 22);
            this.cmbGenre.TabIndex = 55;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.SelectionChangeCommitted += new System.EventHandler(this.cmbGenre_SelectionChangeCommitted);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "类别：";
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(181, 11);
            this.cmbTypeofWork.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTypeofWork.Name = "cmbTypeofWork";
            this.cmbTypeofWork.Size = new System.Drawing.Size(83, 22);
            this.cmbTypeofWork.TabIndex = 53;
            this.cmbTypeofWork.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTypeofWork_DrawItem);
            this.cmbTypeofWork.SelectedIndexChanged += new System.EventHandler(this.cmbTypeofWork_SelectedIndexChanged);
            this.cmbTypeofWork.SelectionChangeCommitted += new System.EventHandler(this.cmbTypeofWork_SelectionChangeCommitted);
            this.cmbTypeofWork.DropDownClosed += new System.EventHandler(this.cmbTypeofWork_DropDownClosed);
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(47, 10);
            this.cmbSystem.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(83, 22);
            this.cmbSystem.TabIndex = 51;
            this.cmbSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSystem_DrawItem);
            this.cmbSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSystem_SelectedIndexChanged);
            this.cmbSystem.SelectionChangeCommitted += new System.EventHandler(this.cmbSystem_SelectionChangeCommitted);
            this.cmbSystem.DropDownClosed += new System.EventHandler(this.cmbSystem_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 54;
            this.label2.Text = "工种：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "系统：";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(333, 91);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(56, 22);
            this.btnQuery.TabIndex = 50;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(68, 93);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(2);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(261, 21);
            this.txtKeyword.TabIndex = 49;
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(13, 458);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(48, 16);
            this.chkCheckAll.TabIndex = 83;
            this.chkCheckAll.Text = "全选";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            this.chkCheckAll.CheckedChanged += new System.EventHandler(this.chkCheckAll_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(333, 454);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 22);
            this.btnSave.TabIndex = 84;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // QuestionResourceListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(408, 482);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkCheckAll);
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
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.dgvResourceList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuestionResourceListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试题关联素材";
            this.Load += new System.EventHandler(this.QuestionResourceListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResourceList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResourceList;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}