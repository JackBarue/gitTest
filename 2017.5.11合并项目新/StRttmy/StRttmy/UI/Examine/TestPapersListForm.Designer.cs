namespace StRttmy.UI
{
    partial class TestPapersListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestPapersListForm));
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.dgvPapersList = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDerived = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPapersList)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(698, 13);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 48;
            this.label6.Text = "关键字：";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(559, 9);
            this.cmbSubject.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(83, 22);
            this.cmbSubject.TabIndex = 46;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.SelectionChangeCommitted += new System.EventHandler(this.cmbSubject_SelectionChangeCommitted);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(521, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = "科目：";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(431, 10);
            this.cmbLevel.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(83, 22);
            this.cmbLevel.TabIndex = 44;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.SelectionChangeCommitted += new System.EventHandler(this.cmbLevel_SelectionChangeCommitted);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(393, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 45;
            this.label4.Text = "等级：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(303, 10);
            this.cmbGenre.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(83, 22);
            this.cmbGenre.TabIndex = 42;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.SelectionChangeCommitted += new System.EventHandler(this.cmbGenre_SelectionChangeCommitted);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(265, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "类别：";
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(175, 10);
            this.cmbTypeofWork.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTypeofWork.Name = "cmbTypeofWork";
            this.cmbTypeofWork.Size = new System.Drawing.Size(83, 22);
            this.cmbTypeofWork.TabIndex = 40;
            this.cmbTypeofWork.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTypeofWork_DrawItem);
            this.cmbTypeofWork.SelectedIndexChanged += new System.EventHandler(this.cmbTypeofWork_SelectedIndexChanged);
            this.cmbTypeofWork.SelectionChangeCommitted += new System.EventHandler(this.cmbTypeofWork_SelectionChangeCommitted);
            this.cmbTypeofWork.DropDownClosed += new System.EventHandler(this.cmbTypeofWork_DropDownClosed);
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(48, 9);
            this.cmbSystem.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(83, 22);
            this.cmbSystem.TabIndex = 38;
            this.cmbSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSystem_DrawItem);
            this.cmbSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSystem_SelectedIndexChanged);
            this.cmbSystem.SelectionChangeCommitted += new System.EventHandler(this.cmbSystem_SelectionChangeCommitted);
            this.cmbSystem.DropDownClosed += new System.EventHandler(this.cmbSystem_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(137, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "工种：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "系统：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(1170, 7);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 22);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "新建试卷";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(1087, 7);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(56, 22);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(755, 9);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(2);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(309, 21);
            this.txtKeyword.TabIndex = 0;
            // 
            // dgvPapersList
            // 
            this.dgvPapersList.AllowUserToAddRows = false;
            this.dgvPapersList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPapersList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPapersList.ColumnHeadersHeight = 30;
            this.dgvPapersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPapersList.Location = new System.Drawing.Point(0, 38);
            this.dgvPapersList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPapersList.Name = "dgvPapersList";
            this.dgvPapersList.ReadOnly = true;
            this.dgvPapersList.RowTemplate.Height = 30;
            this.dgvPapersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPapersList.Size = new System.Drawing.Size(1264, 692);
            this.dgvPapersList.TabIndex = 6;
            this.dgvPapersList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPapersList_CellDoubleClick);
            this.dgvPapersList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPapersList_CellMouseDown);
            this.dgvPapersList.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPapersList_RowPostPaint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbSubject);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cmbLevel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbGenre);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbTypeofWork);
            this.panel1.Controls.Add(this.cmbSystem);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.txtKeyword);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 38);
            this.panel1.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDerived,
            this.tsmiUpdate,
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // tsmiUpdate
            // 
            this.tsmiUpdate.Name = "tsmiUpdate";
            this.tsmiUpdate.Size = new System.Drawing.Size(152, 22);
            this.tsmiUpdate.Text = "修改试卷";
            this.tsmiUpdate.Click += new System.EventHandler(this.tsmiUpdate_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
            this.tsmiDelete.Text = "删除试卷";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiDerived
            // 
            this.tsmiDerived.Name = "tsmiDerived";
            this.tsmiDerived.Size = new System.Drawing.Size(152, 22);
            this.tsmiDerived.Text = "派生试卷";
            this.tsmiDerived.Click += new System.EventHandler(this.tsmiDerived_Click);
            // 
            // TestPapersListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.dgvPapersList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestPapersListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试卷管理";
            this.Load += new System.EventHandler(this.TestPapersListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPapersList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.DataGridView dgvPapersList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiDerived;
    }
}