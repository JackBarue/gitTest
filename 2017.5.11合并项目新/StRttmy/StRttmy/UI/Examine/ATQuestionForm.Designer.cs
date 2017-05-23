namespace StRttmy.UI
{
    partial class ATQuestionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ATQuestionForm));
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
            this.chkChoice = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtChoiceCount = new System.Windows.Forms.TextBox();
            this.txtChoiceScore = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtJudgeScore = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtJudgeCount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkJudge = new System.Windows.Forms.CheckBox();
            this.txtSAQScore = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSAQCount = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkSAQ = new System.Windows.Forms.CheckBox();
            this.txtEssayScore = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEssayCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkEssay = new System.Windows.Forms.CheckBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(232, 62);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(108, 22);
            this.cmbLevel.TabIndex = 73;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 72;
            this.label5.Text = "等级：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(53, 62);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(108, 22);
            this.cmbGenre.TabIndex = 71;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 70;
            this.label4.Text = "类别：";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(417, 21);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(108, 22);
            this.cmbSubject.TabIndex = 69;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(381, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 68;
            this.label3.Text = "科目：";
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(232, 21);
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
            this.label2.Location = new System.Drawing.Point(196, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 66;
            this.label2.Text = "工种：";
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(53, 21);
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
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 64;
            this.label1.Text = "系统：";
            // 
            // chkChoice
            // 
            this.chkChoice.AutoSize = true;
            this.chkChoice.Location = new System.Drawing.Point(19, 108);
            this.chkChoice.Name = "chkChoice";
            this.chkChoice.Size = new System.Drawing.Size(60, 16);
            this.chkChoice.TabIndex = 74;
            this.chkChoice.Tag = "1";
            this.chkChoice.Text = "选择题";
            this.chkChoice.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(118, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 75;
            this.label6.Text = "题数：";
            // 
            // txtChoiceCount
            // 
            this.txtChoiceCount.Location = new System.Drawing.Point(155, 106);
            this.txtChoiceCount.Name = "txtChoiceCount";
            this.txtChoiceCount.Size = new System.Drawing.Size(75, 21);
            this.txtChoiceCount.TabIndex = 76;
            this.txtChoiceCount.Tag = "cn";
            // 
            // txtChoiceScore
            // 
            this.txtChoiceScore.Location = new System.Drawing.Point(327, 106);
            this.txtChoiceScore.Name = "txtChoiceScore";
            this.txtChoiceScore.Size = new System.Drawing.Size(75, 21);
            this.txtChoiceScore.TabIndex = 78;
            this.txtChoiceScore.Tag = "cs";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(266, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 77;
            this.label7.Text = "每题分数：";
            // 
            // txtJudgeScore
            // 
            this.txtJudgeScore.Location = new System.Drawing.Point(327, 154);
            this.txtJudgeScore.Name = "txtJudgeScore";
            this.txtJudgeScore.Size = new System.Drawing.Size(75, 21);
            this.txtJudgeScore.TabIndex = 83;
            this.txtJudgeScore.Tag = "js";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(266, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 82;
            this.label8.Text = "每题分数：";
            // 
            // txtJudgeCount
            // 
            this.txtJudgeCount.Location = new System.Drawing.Point(155, 154);
            this.txtJudgeCount.Name = "txtJudgeCount";
            this.txtJudgeCount.Size = new System.Drawing.Size(75, 21);
            this.txtJudgeCount.TabIndex = 81;
            this.txtJudgeCount.Tag = "jn";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(118, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 80;
            this.label9.Text = "题数：";
            // 
            // chkJudge
            // 
            this.chkJudge.AutoSize = true;
            this.chkJudge.Location = new System.Drawing.Point(19, 156);
            this.chkJudge.Name = "chkJudge";
            this.chkJudge.Size = new System.Drawing.Size(60, 16);
            this.chkJudge.TabIndex = 79;
            this.chkJudge.Tag = "2";
            this.chkJudge.Text = "判断题";
            this.chkJudge.UseVisualStyleBackColor = true;
            // 
            // txtSAQScore
            // 
            this.txtSAQScore.Location = new System.Drawing.Point(327, 202);
            this.txtSAQScore.Name = "txtSAQScore";
            this.txtSAQScore.Size = new System.Drawing.Size(75, 21);
            this.txtSAQScore.TabIndex = 88;
            this.txtSAQScore.Tag = "ss";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(266, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 87;
            this.label10.Text = "每题分数：";
            // 
            // txtSAQCount
            // 
            this.txtSAQCount.Location = new System.Drawing.Point(155, 202);
            this.txtSAQCount.Name = "txtSAQCount";
            this.txtSAQCount.Size = new System.Drawing.Size(75, 21);
            this.txtSAQCount.TabIndex = 86;
            this.txtSAQCount.Tag = "sn";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(118, 205);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 85;
            this.label11.Text = "题数：";
            // 
            // chkSAQ
            // 
            this.chkSAQ.AutoSize = true;
            this.chkSAQ.Location = new System.Drawing.Point(19, 204);
            this.chkSAQ.Name = "chkSAQ";
            this.chkSAQ.Size = new System.Drawing.Size(60, 16);
            this.chkSAQ.TabIndex = 84;
            this.chkSAQ.Tag = "3";
            this.chkSAQ.Text = "简答题";
            this.chkSAQ.UseVisualStyleBackColor = true;
            // 
            // txtEssayScore
            // 
            this.txtEssayScore.Location = new System.Drawing.Point(327, 250);
            this.txtEssayScore.Name = "txtEssayScore";
            this.txtEssayScore.Size = new System.Drawing.Size(75, 21);
            this.txtEssayScore.TabIndex = 93;
            this.txtEssayScore.Tag = "es";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(266, 253);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 92;
            this.label12.Text = "每题分数：";
            // 
            // txtEssayCount
            // 
            this.txtEssayCount.Location = new System.Drawing.Point(155, 250);
            this.txtEssayCount.Name = "txtEssayCount";
            this.txtEssayCount.Size = new System.Drawing.Size(75, 21);
            this.txtEssayCount.TabIndex = 91;
            this.txtEssayCount.Tag = "en";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(118, 253);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 90;
            this.label13.Text = "题数：";
            // 
            // chkEssay
            // 
            this.chkEssay.AutoSize = true;
            this.chkEssay.Location = new System.Drawing.Point(19, 252);
            this.chkEssay.Name = "chkEssay";
            this.chkEssay.Size = new System.Drawing.Size(60, 16);
            this.chkEssay.TabIndex = 89;
            this.chkEssay.Tag = "4";
            this.chkEssay.Text = "论述题";
            this.chkEssay.UseVisualStyleBackColor = true;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(297, 286);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(53, 23);
            this.btnReturn.TabIndex = 95;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(97, 286);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(64, 23);
            this.btnGenerate.TabIndex = 94;
            this.btnGenerate.Text = "生成试卷";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // ATQuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(556, 324);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtEssayScore);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtEssayCount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.chkEssay);
            this.Controls.Add(this.txtSAQScore);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSAQCount);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.chkSAQ);
            this.Controls.Add(this.txtJudgeScore);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtJudgeCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkJudge);
            this.Controls.Add(this.txtChoiceScore);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtChoiceCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkChoice);
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
            this.Name = "ATQuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "试题选择";
            this.Load += new System.EventHandler(this.ATQuestionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.CheckBox chkChoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtChoiceCount;
        private System.Windows.Forms.TextBox txtChoiceScore;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtJudgeScore;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtJudgeCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkJudge;
        private System.Windows.Forms.TextBox txtSAQScore;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSAQCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkSAQ;
        private System.Windows.Forms.TextBox txtEssayScore;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEssayCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkEssay;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}