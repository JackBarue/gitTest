namespace StRttmy.UI
{
    partial class ChosePaperClassForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChosePaperClassForm));
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTypeofWork = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSystem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvPapersList = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPaperQuery = new System.Windows.Forms.Button();
            this.txtPaperKeyword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCheckAll = new System.Windows.Forms.CheckBox();
            this.dgvClassList = new System.Windows.Forms.DataGridView();
            this.txtClassKeyword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClassQuery = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPapersList)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(205, 69);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(108, 22);
            this.cmbLevel.TabIndex = 73;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 72;
            this.label5.Text = "等级：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(55, 69);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(108, 22);
            this.cmbGenre.TabIndex = 71;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 70;
            this.label4.Text = "类别：";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(355, 28);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(108, 22);
            this.cmbSubject.TabIndex = 69;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 68;
            this.label3.Text = "科目：";
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(205, 28);
            this.cmbTypeofWork.Name = "cmbTypeofWork";
            this.cmbTypeofWork.Size = new System.Drawing.Size(108, 22);
            this.cmbTypeofWork.TabIndex = 67;
            this.cmbTypeofWork.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTypeofWork_DrawItem);
            this.cmbTypeofWork.SelectedIndexChanged += new System.EventHandler(this.cmbTypeofWork_SelectedIndexChanged);
            this.cmbTypeofWork.DropDownClosed += new System.EventHandler(this.cmbTypeofWork_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 66;
            this.label2.Text = "工种：";
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(55, 28);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(108, 22);
            this.cmbSystem.TabIndex = 65;
            this.cmbSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSystem_DrawItem);
            this.cmbSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSystem_SelectedIndexChanged);
            this.cmbSystem.DropDownClosed += new System.EventHandler(this.cmbSystem_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 64;
            this.label1.Text = "系统：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvPapersList);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnPaperQuery);
            this.groupBox1.Controls.Add(this.txtPaperKeyword);
            this.groupBox1.Controls.Add(this.cmbSubject);
            this.groupBox1.Controls.Add(this.cmbTypeofWork);
            this.groupBox1.Controls.Add(this.cmbSystem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbLevel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbGenre);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 547);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "试卷选择";
            // 
            // dgvPapersList
            // 
            this.dgvPapersList.BackgroundColor = System.Drawing.Color.White;
            this.dgvPapersList.ColumnHeadersHeight = 30;
            this.dgvPapersList.Location = new System.Drawing.Point(0, 147);
            this.dgvPapersList.Name = "dgvPapersList";
            this.dgvPapersList.ReadOnly = true;
            this.dgvPapersList.RowTemplate.Height = 30;
            this.dgvPapersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPapersList.Size = new System.Drawing.Size(486, 400);
            this.dgvPapersList.TabIndex = 77;
            this.dgvPapersList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPapersList_CellContentClick);
            this.dgvPapersList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPapersList_RowPostPaint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 115);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 76;
            this.label6.Text = "关键字：";
            // 
            // btnPaperQuery
            // 
            this.btnPaperQuery.Location = new System.Drawing.Point(407, 109);
            this.btnPaperQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnPaperQuery.Name = "btnPaperQuery";
            this.btnPaperQuery.Size = new System.Drawing.Size(56, 22);
            this.btnPaperQuery.TabIndex = 75;
            this.btnPaperQuery.Text = "查询";
            this.btnPaperQuery.UseVisualStyleBackColor = true;
            this.btnPaperQuery.Click += new System.EventHandler(this.btnPaperQuery_Click);
            // 
            // txtPaperKeyword
            // 
            this.txtPaperKeyword.Location = new System.Drawing.Point(76, 111);
            this.txtPaperKeyword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPaperKeyword.Name = "txtPaperKeyword";
            this.txtPaperKeyword.Size = new System.Drawing.Size(327, 21);
            this.txtPaperKeyword.TabIndex = 74;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkCheckAll);
            this.groupBox2.Controls.Add(this.dgvClassList);
            this.groupBox2.Controls.Add(this.txtClassKeyword);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnClassQuery);
            this.groupBox2.Location = new System.Drawing.Point(528, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 547);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "班级选择";
            // 
            // chkCheckAll
            // 
            this.chkCheckAll.AutoSize = true;
            this.chkCheckAll.Location = new System.Drawing.Point(3, 527);
            this.chkCheckAll.Name = "chkCheckAll";
            this.chkCheckAll.Size = new System.Drawing.Size(48, 16);
            this.chkCheckAll.TabIndex = 82;
            this.chkCheckAll.Text = "全选";
            this.chkCheckAll.UseVisualStyleBackColor = true;
            this.chkCheckAll.CheckedChanged += new System.EventHandler(this.chkCheckAll_CheckedChanged);
            // 
            // dgvClassList
            // 
            this.dgvClassList.BackgroundColor = System.Drawing.Color.White;
            this.dgvClassList.ColumnHeadersHeight = 30;
            this.dgvClassList.Location = new System.Drawing.Point(0, 64);
            this.dgvClassList.Name = "dgvClassList";
            this.dgvClassList.ReadOnly = true;
            this.dgvClassList.RowTemplate.Height = 30;
            this.dgvClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClassList.Size = new System.Drawing.Size(426, 457);
            this.dgvClassList.TabIndex = 81;
            this.dgvClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClassList_CellContentClick);
            this.dgvClassList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvClassList_RowPostPaint);
            // 
            // txtClassKeyword
            // 
            this.txtClassKeyword.Location = new System.Drawing.Point(64, 31);
            this.txtClassKeyword.Margin = new System.Windows.Forms.Padding(2);
            this.txtClassKeyword.Name = "txtClassKeyword";
            this.txtClassKeyword.Size = new System.Drawing.Size(287, 21);
            this.txtClassKeyword.TabIndex = 78;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 80;
            this.label7.Text = "关键字：";
            // 
            // btnClassQuery
            // 
            this.btnClassQuery.Location = new System.Drawing.Point(355, 29);
            this.btnClassQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnClassQuery.Name = "btnClassQuery";
            this.btnClassQuery.Size = new System.Drawing.Size(56, 22);
            this.btnClassQuery.TabIndex = 79;
            this.btnClassQuery.Text = "查询";
            this.btnClassQuery.UseVisualStyleBackColor = true;
            this.btnClassQuery.Click += new System.EventHandler(this.btnClassQuery_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 572);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 79;
            this.button2.Text = "开始考试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChosePaperClassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(970, 607);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChosePaperClassForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试卷及参考班级选择";
            this.Load += new System.EventHandler(this.ChosePaperClassForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPapersList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGenre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTypeofWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSystem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPaperQuery;
        private System.Windows.Forms.TextBox txtPaperKeyword;
        private System.Windows.Forms.DataGridView dgvPapersList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCheckAll;
        private System.Windows.Forms.DataGridView dgvClassList;
        private System.Windows.Forms.TextBox txtClassKeyword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClassQuery;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}