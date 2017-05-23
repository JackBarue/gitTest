namespace StRttmy.UI
{
    partial class AddPaperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPaperForm));
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbGenerate = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPaperName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.txtTestTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(342, 196);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(53, 23);
            this.btnReturn.TabIndex = 57;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(145, 196);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(53, 23);
            this.btnAdd.TabIndex = 56;
            this.btnAdd.Text = "新建";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbGenerate
            // 
            this.cmbGenerate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenerate.FormattingEnabled = true;
            this.cmbGenerate.Location = new System.Drawing.Point(260, 150);
            this.cmbGenerate.Name = "cmbGenerate";
            this.cmbGenerate.Size = new System.Drawing.Size(84, 22);
            this.cmbGenerate.TabIndex = 55;
            this.cmbGenerate.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenerate_DrawItem);
            this.cmbGenerate.SelectionChangeCommitted += new System.EventHandler(this.cmbGenerate_SelectionChangeCommitted);
            this.cmbGenerate.DropDownClosed += new System.EventHandler(this.cmbGenerate_DropDownClosed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 54;
            this.label9.Text = "试卷生成：";
            // 
            // txtPaperName
            // 
            this.txtPaperName.Location = new System.Drawing.Point(81, 106);
            this.txtPaperName.Name = "txtPaperName";
            this.txtPaperName.Size = new System.Drawing.Size(448, 21);
            this.txtPaperName.TabIndex = 45;
            this.txtPaperName.Tag = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 52;
            this.label7.Text = "试卷名称：";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(236, 60);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(108, 22);
            this.cmbLevel.TabIndex = 51;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 50;
            this.label5.Text = "等级：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(57, 60);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(108, 22);
            this.cmbGenre.TabIndex = 49;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 48;
            this.label4.Text = "类别：";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(421, 19);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(108, 22);
            this.cmbSubject.TabIndex = 47;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 46;
            this.label3.Text = "科目：";
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(236, 19);
            this.cmbTypeofWork.Name = "cmbTypeofWork";
            this.cmbTypeofWork.Size = new System.Drawing.Size(108, 22);
            this.cmbTypeofWork.TabIndex = 45;
            this.cmbTypeofWork.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTypeofWork_DrawItem);
            this.cmbTypeofWork.SelectedIndexChanged += new System.EventHandler(this.cmbTypeofWork_SelectedIndexChanged);
            this.cmbTypeofWork.DropDownClosed += new System.EventHandler(this.cmbTypeofWork_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 44;
            this.label2.Text = "工种：";
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(57, 19);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(108, 22);
            this.cmbSystem.TabIndex = 43;
            this.cmbSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSystem_DrawItem);
            this.cmbSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSystem_SelectedIndexChanged);
            this.cmbSystem.DropDownClosed += new System.EventHandler(this.cmbSystem_DropDownClosed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "系统：";
            // 
            // txtTestTime
            // 
            this.txtTestTime.Location = new System.Drawing.Point(81, 150);
            this.txtTestTime.Name = "txtTestTime";
            this.txtTestTime.Size = new System.Drawing.Size(43, 21);
            this.txtTestTime.TabIndex = 59;
            this.txtTestTime.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 58;
            this.label6.Text = "考试时间：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(127, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 60;
            this.label8.Text = "分钟";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(167, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 61;
            this.label10.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(346, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 62;
            this.label11.Text = "*";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(531, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 63;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(154, 153);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 64;
            this.label13.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(346, 153);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 65;
            this.label14.Text = "*";
            // 
            // AddPaperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(554, 237);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTestTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbGenerate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPaperName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbGenre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTypeofWork);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSystem);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPaperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建试卷";
            this.Load += new System.EventHandler(this.AddPaperForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbGenerate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPaperName;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.TextBox txtTestTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}