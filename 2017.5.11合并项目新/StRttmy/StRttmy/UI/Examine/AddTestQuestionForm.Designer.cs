namespace StRttmy.UI
{
    partial class AddTestQuestionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTestQuestionForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSystem = new System.Windows.Forms.ComboBox();
            this.cmbTypeofWork = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGenre = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbQuestionType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.txtAnswerA = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAnswerB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAnswerC = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAnswerD = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbCorrect = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtScore = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnResource = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统：";
            // 
            // cmbSystem
            // 
            this.cmbSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(61, 21);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(108, 22);
            this.cmbSystem.TabIndex = 1;
            this.cmbSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSystem_DrawItem);
            this.cmbSystem.SelectedIndexChanged += new System.EventHandler(this.cmbSystem_SelectedIndexChanged);
            this.cmbSystem.DropDownClosed += new System.EventHandler(this.cmbSystem_DropDownClosed);
            // 
            // cmbTypeofWork
            // 
            this.cmbTypeofWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTypeofWork.FormattingEnabled = true;
            this.cmbTypeofWork.Location = new System.Drawing.Point(240, 21);
            this.cmbTypeofWork.Name = "cmbTypeofWork";
            this.cmbTypeofWork.Size = new System.Drawing.Size(108, 22);
            this.cmbTypeofWork.TabIndex = 3;
            this.cmbTypeofWork.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbTypeofWork_DrawItem);
            this.cmbTypeofWork.SelectedIndexChanged += new System.EventHandler(this.cmbTypeofWork_SelectedIndexChanged);
            this.cmbTypeofWork.DropDownClosed += new System.EventHandler(this.cmbTypeofWork_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "工种：";
            // 
            // cmbSubject
            // 
            this.cmbSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(425, 21);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(108, 22);
            this.cmbSubject.TabIndex = 5;
            this.cmbSubject.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbSubject_DrawItem);
            this.cmbSubject.DropDownClosed += new System.EventHandler(this.cmbSubject_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "科目：";
            // 
            // cmbGenre
            // 
            this.cmbGenre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGenre.FormattingEnabled = true;
            this.cmbGenre.Location = new System.Drawing.Point(61, 62);
            this.cmbGenre.Name = "cmbGenre";
            this.cmbGenre.Size = new System.Drawing.Size(108, 22);
            this.cmbGenre.TabIndex = 7;
            this.cmbGenre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbGenre_DrawItem);
            this.cmbGenre.DropDownClosed += new System.EventHandler(this.cmbGenre_DropDownClosed);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "类别：";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(240, 62);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(108, 22);
            this.cmbLevel.TabIndex = 9;
            this.cmbLevel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbLevel_DrawItem);
            this.cmbLevel.DropDownClosed += new System.EventHandler(this.cmbLevel_DropDownClosed);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(204, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "等级：";
            // 
            // cmbQuestionType
            // 
            this.cmbQuestionType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuestionType.FormattingEnabled = true;
            this.cmbQuestionType.Location = new System.Drawing.Point(425, 62);
            this.cmbQuestionType.Name = "cmbQuestionType";
            this.cmbQuestionType.Size = new System.Drawing.Size(108, 22);
            this.cmbQuestionType.TabIndex = 11;
            this.cmbQuestionType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbQuestionType_DrawItem);
            this.cmbQuestionType.SelectedIndexChanged += new System.EventHandler(this.cmbQuestionType_SelectedIndexChanged);
            this.cmbQuestionType.DropDownClosed += new System.EventHandler(this.cmbQuestionType_DropDownClosed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(365, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "试题类型：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "试   题 ：";
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(85, 109);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(455, 94);
            this.txtQuestion.TabIndex = 13;
            this.txtQuestion.Tag = "";
            // 
            // txtAnswerA
            // 
            this.txtAnswerA.Location = new System.Drawing.Point(85, 219);
            this.txtAnswerA.Name = "txtAnswerA";
            this.txtAnswerA.Size = new System.Drawing.Size(455, 21);
            this.txtAnswerA.TabIndex = 15;
            this.txtAnswerA.Tag = "A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "答 案 A ：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 352);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "正确答案：";
            // 
            // txtAnswerB
            // 
            this.txtAnswerB.Location = new System.Drawing.Point(85, 251);
            this.txtAnswerB.Name = "txtAnswerB";
            this.txtAnswerB.Size = new System.Drawing.Size(455, 21);
            this.txtAnswerB.TabIndex = 18;
            this.txtAnswerB.Tag = "B";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "答 案 B ：";
            // 
            // txtAnswerC
            // 
            this.txtAnswerC.Location = new System.Drawing.Point(85, 283);
            this.txtAnswerC.Name = "txtAnswerC";
            this.txtAnswerC.Size = new System.Drawing.Size(455, 21);
            this.txtAnswerC.TabIndex = 20;
            this.txtAnswerC.Tag = "C";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 286);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "答 案 C ：";
            // 
            // txtAnswerD
            // 
            this.txtAnswerD.Location = new System.Drawing.Point(85, 315);
            this.txtAnswerD.Name = "txtAnswerD";
            this.txtAnswerD.Size = new System.Drawing.Size(455, 21);
            this.txtAnswerD.TabIndex = 22;
            this.txtAnswerD.Tag = "D";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 318);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "答 案 D ：";
            // 
            // cmbCorrect
            // 
            this.cmbCorrect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCorrect.FormattingEnabled = true;
            this.cmbCorrect.Location = new System.Drawing.Point(85, 349);
            this.cmbCorrect.Name = "cmbCorrect";
            this.cmbCorrect.Size = new System.Drawing.Size(84, 20);
            this.cmbCorrect.TabIndex = 23;
            this.cmbCorrect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbCorrect_MouseClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(170, 391);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(53, 23);
            this.btnAdd.TabIndex = 24;
            this.btnAdd.Text = "新建";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(367, 391);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(53, 23);
            this.btnReturn.TabIndex = 25;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtScore
            // 
            this.txtScore.Location = new System.Drawing.Point(281, 349);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(53, 21);
            this.txtScore.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(221, 352);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "本题分数：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(171, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 62;
            this.label14.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(350, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 63;
            this.label15.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(535, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 64;
            this.label16.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(535, 65);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 65;
            this.label17.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(546, 159);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(11, 12);
            this.label18.TabIndex = 66;
            this.label18.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(171, 352);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 67;
            this.label19.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(337, 352);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 68;
            this.label20.Text = "*";
            // 
            // btnResource
            // 
            this.btnResource.Location = new System.Drawing.Point(367, 347);
            this.btnResource.Name = "btnResource";
            this.btnResource.Size = new System.Drawing.Size(75, 23);
            this.btnResource.TabIndex = 69;
            this.btnResource.Text = "关联素材";
            this.btnResource.UseVisualStyleBackColor = true;
            this.btnResource.Click += new System.EventHandler(this.btnResource_Click);
            // 
            // AddTestQuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(573, 425);
            this.Controls.Add(this.btnResource);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbCorrect);
            this.Controls.Add(this.txtAnswerD);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtAnswerC);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtAnswerB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAnswerA);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbQuestionType);
            this.Controls.Add(this.label6);
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
            this.Name = "AddTestQuestionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddTestQuestionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSystem;
        private System.Windows.Forms.ComboBox cmbTypeofWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGenre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbQuestionType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.TextBox txtAnswerA;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAnswerB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtAnswerC;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAnswerD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbCorrect;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnResource;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}