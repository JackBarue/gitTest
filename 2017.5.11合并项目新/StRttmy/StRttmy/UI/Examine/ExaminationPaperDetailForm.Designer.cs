namespace StRttmy.UI
{
    partial class ExaminationPaperDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExaminationPaperDetailForm));
            this.lblPaperScore = new System.Windows.Forms.Label();
            this.btnUploadAnswer = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.rdoAnswerD = new System.Windows.Forms.RadioButton();
            this.rdoAnswerC = new System.Windows.Forms.RadioButton();
            this.rdoAnswerB = new System.Windows.Forms.RadioButton();
            this.rdoAnswerA = new System.Windows.Forms.RadioButton();
            this.lblQuestionType = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblCorrent = new System.Windows.Forms.Label();
            this.btnAnswerCard = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // lblPaperScore
            // 
            this.lblPaperScore.Location = new System.Drawing.Point(406, 12);
            this.lblPaperScore.Name = "lblPaperScore";
            this.lblPaperScore.Size = new System.Drawing.Size(187, 23);
            this.lblPaperScore.TabIndex = 27;
            this.lblPaperScore.Text = "试卷满分：100分   总得分：90分";
            this.lblPaperScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnUploadAnswer
            // 
            this.btnUploadAnswer.Location = new System.Drawing.Point(40, 307);
            this.btnUploadAnswer.Name = "btnUploadAnswer";
            this.btnUploadAnswer.Size = new System.Drawing.Size(75, 23);
            this.btnUploadAnswer.TabIndex = 26;
            this.btnUploadAnswer.Text = "查看答案";
            this.btnUploadAnswer.UseVisualStyleBackColor = true;
            this.btnUploadAnswer.Click += new System.EventHandler(this.btnUploadAnswer_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(319, 266);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 25;
            this.btnLast.Text = "上一题";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // rdoAnswerD
            // 
            this.rdoAnswerD.AutoSize = true;
            this.rdoAnswerD.Location = new System.Drawing.Point(40, 269);
            this.rdoAnswerD.Name = "rdoAnswerD";
            this.rdoAnswerD.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerD.TabIndex = 24;
            this.rdoAnswerD.TabStop = true;
            this.rdoAnswerD.Tag = "D";
            this.rdoAnswerD.Text = "radioButton1";
            this.rdoAnswerD.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerC
            // 
            this.rdoAnswerC.AutoSize = true;
            this.rdoAnswerC.Location = new System.Drawing.Point(40, 234);
            this.rdoAnswerC.Name = "rdoAnswerC";
            this.rdoAnswerC.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerC.TabIndex = 23;
            this.rdoAnswerC.TabStop = true;
            this.rdoAnswerC.Tag = "C";
            this.rdoAnswerC.Text = "radioButton1";
            this.rdoAnswerC.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerB
            // 
            this.rdoAnswerB.AutoSize = true;
            this.rdoAnswerB.Location = new System.Drawing.Point(40, 199);
            this.rdoAnswerB.Name = "rdoAnswerB";
            this.rdoAnswerB.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerB.TabIndex = 22;
            this.rdoAnswerB.TabStop = true;
            this.rdoAnswerB.Tag = "B";
            this.rdoAnswerB.Text = "radioButton1";
            this.rdoAnswerB.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerA
            // 
            this.rdoAnswerA.AutoSize = true;
            this.rdoAnswerA.Location = new System.Drawing.Point(40, 164);
            this.rdoAnswerA.Name = "rdoAnswerA";
            this.rdoAnswerA.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerA.TabIndex = 21;
            this.rdoAnswerA.TabStop = true;
            this.rdoAnswerA.Tag = "A";
            this.rdoAnswerA.Text = "radioButton1";
            this.rdoAnswerA.UseVisualStyleBackColor = true;
            // 
            // lblQuestionType
            // 
            this.lblQuestionType.AutoSize = true;
            this.lblQuestionType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQuestionType.Location = new System.Drawing.Point(12, 15);
            this.lblQuestionType.Name = "lblQuestionType";
            this.lblQuestionType.Size = new System.Drawing.Size(49, 14);
            this.lblQuestionType.TabIndex = 20;
            this.lblQuestionType.Text = "单选题";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(412, 266);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 19;
            this.btnNext.Text = "下一题";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "答案：";
            // 
            // lblQuestion
            // 
            this.lblQuestion.Location = new System.Drawing.Point(12, 39);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(581, 88);
            this.lblQuestion.TabIndex = 17;
            this.lblQuestion.Text = "label1";
            // 
            // lblScore
            // 
            this.lblScore.Location = new System.Drawing.Point(254, 134);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(90, 23);
            this.lblScore.TabIndex = 28;
            this.lblScore.Text = "本题得分：100";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCorrent
            // 
            this.lblCorrent.AutoSize = true;
            this.lblCorrent.Location = new System.Drawing.Point(154, 139);
            this.lblCorrent.Name = "lblCorrent";
            this.lblCorrent.Size = new System.Drawing.Size(71, 12);
            this.lblCorrent.TabIndex = 29;
            this.lblCorrent.Text = "正确答案：A";
            // 
            // btnAnswerCard
            // 
            this.btnAnswerCard.Location = new System.Drawing.Point(505, 266);
            this.btnAnswerCard.Name = "btnAnswerCard";
            this.btnAnswerCard.Size = new System.Drawing.Size(75, 23);
            this.btnAnswerCard.TabIndex = 30;
            this.btnAnswerCard.Text = "显示答题卡";
            this.btnAnswerCard.UseVisualStyleBackColor = true;
            this.btnAnswerCard.Click += new System.EventHandler(this.btnAnswerCard_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 298);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 0);
            this.flowLayoutPanel1.TabIndex = 38;
            // 
            // ExaminationPaperDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(605, 298);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnAnswerCard);
            this.Controls.Add(this.lblCorrent);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblPaperScore);
            this.Controls.Add(this.btnUploadAnswer);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.rdoAnswerD);
            this.Controls.Add(this.rdoAnswerC);
            this.Controls.Add(this.rdoAnswerB);
            this.Controls.Add(this.rdoAnswerA);
            this.Controls.Add(this.lblQuestionType);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExaminationPaperDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考卷查阅";
            this.Load += new System.EventHandler(this.ExaminationPaperForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPaperScore;
        private System.Windows.Forms.Button btnUploadAnswer;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.RadioButton rdoAnswerD;
        private System.Windows.Forms.RadioButton rdoAnswerC;
        private System.Windows.Forms.RadioButton rdoAnswerB;
        private System.Windows.Forms.RadioButton rdoAnswerA;
        private System.Windows.Forms.Label lblQuestionType;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblCorrent;
        private System.Windows.Forms.Button btnAnswerCard;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}