namespace StRttmy.UI
{
    partial class StudentPaperDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentPaperDetailForm));
            this.btnChenckAnswer = new System.Windows.Forms.Button();
            this.lblQuestionType = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtScore = new System.Windows.Forms.TextBox();
            this.btnAnswerCard = new System.Windows.Forms.Button();
            this.lblCorrent = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.rdoAnswerB = new System.Windows.Forms.RadioButton();
            this.rdoAnswerA = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rdoAnswerC = new System.Windows.Forms.RadioButton();
            this.rdoAnswerD = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEvaluating = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChenckAnswer
            // 
            this.btnChenckAnswer.Location = new System.Drawing.Point(12, 316);
            this.btnChenckAnswer.Name = "btnChenckAnswer";
            this.btnChenckAnswer.Size = new System.Drawing.Size(75, 23);
            this.btnChenckAnswer.TabIndex = 20;
            this.btnChenckAnswer.Text = "查看答案";
            this.btnChenckAnswer.UseVisualStyleBackColor = true;
            this.btnChenckAnswer.Click += new System.EventHandler(this.btnChenckAnswer_Click);
            // 
            // lblQuestionType
            // 
            this.lblQuestionType.AutoSize = true;
            this.lblQuestionType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQuestionType.Location = new System.Drawing.Point(12, 18);
            this.lblQuestionType.Name = "lblQuestionType";
            this.lblQuestionType.Size = new System.Drawing.Size(0, 14);
            this.lblQuestionType.TabIndex = 19;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(441, 316);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = "下一题";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblQuestion
            // 
            this.lblQuestion.Location = new System.Drawing.Point(12, 42);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(581, 88);
            this.lblQuestion.TabIndex = 16;
            this.lblQuestion.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "评分：";
            // 
            // txtScore
            // 
            this.txtScore.Location = new System.Drawing.Point(163, 318);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(54, 21);
            this.txtScore.TabIndex = 22;
            this.txtScore.TextChanged += new System.EventHandler(this.txtScore_TextChanged);
            // 
            // btnAnswerCard
            // 
            this.btnAnswerCard.Location = new System.Drawing.Point(346, 316);
            this.btnAnswerCard.Name = "btnAnswerCard";
            this.btnAnswerCard.Size = new System.Drawing.Size(75, 23);
            this.btnAnswerCard.TabIndex = 23;
            this.btnAnswerCard.Text = "显示答题卡";
            this.btnAnswerCard.UseVisualStyleBackColor = true;
            this.btnAnswerCard.Click += new System.EventHandler(this.btnAnswerCard_Click);
            // 
            // lblCorrent
            // 
            this.lblCorrent.AutoSize = true;
            this.lblCorrent.Location = new System.Drawing.Point(154, 143);
            this.lblCorrent.Name = "lblCorrent";
            this.lblCorrent.Size = new System.Drawing.Size(71, 12);
            this.lblCorrent.TabIndex = 36;
            this.lblCorrent.Text = "正确答案：A";
            // 
            // lblScore
            // 
            this.lblScore.Location = new System.Drawing.Point(254, 138);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(90, 23);
            this.lblScore.TabIndex = 35;
            this.lblScore.Text = "本题得分：100";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoAnswerB
            // 
            this.rdoAnswerB.AutoSize = true;
            this.rdoAnswerB.Location = new System.Drawing.Point(40, 203);
            this.rdoAnswerB.Name = "rdoAnswerB";
            this.rdoAnswerB.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerB.TabIndex = 32;
            this.rdoAnswerB.TabStop = true;
            this.rdoAnswerB.Tag = "B";
            this.rdoAnswerB.Text = "radioButton1";
            this.rdoAnswerB.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerA
            // 
            this.rdoAnswerA.AutoSize = true;
            this.rdoAnswerA.Location = new System.Drawing.Point(40, 168);
            this.rdoAnswerA.Name = "rdoAnswerA";
            this.rdoAnswerA.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerA.TabIndex = 31;
            this.rdoAnswerA.TabStop = true;
            this.rdoAnswerA.Tag = "A";
            this.rdoAnswerA.Text = "radioButton1";
            this.rdoAnswerA.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "答案：";
            // 
            // rdoAnswerC
            // 
            this.rdoAnswerC.AutoSize = true;
            this.rdoAnswerC.Location = new System.Drawing.Point(40, 238);
            this.rdoAnswerC.Name = "rdoAnswerC";
            this.rdoAnswerC.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerC.TabIndex = 33;
            this.rdoAnswerC.TabStop = true;
            this.rdoAnswerC.Tag = "C";
            this.rdoAnswerC.Text = "radioButton1";
            this.rdoAnswerC.UseVisualStyleBackColor = true;
            // 
            // rdoAnswerD
            // 
            this.rdoAnswerD.AutoSize = true;
            this.rdoAnswerD.Location = new System.Drawing.Point(40, 273);
            this.rdoAnswerD.Name = "rdoAnswerD";
            this.rdoAnswerD.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerD.TabIndex = 34;
            this.rdoAnswerD.TabStop = true;
            this.rdoAnswerD.Tag = "D";
            this.rdoAnswerD.Text = "radioButton1";
            this.rdoAnswerD.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 351);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(607, 0);
            this.flowLayoutPanel1.TabIndex = 37;
            // 
            // btnEvaluating
            // 
            this.btnEvaluating.Location = new System.Drawing.Point(536, 316);
            this.btnEvaluating.Name = "btnEvaluating";
            this.btnEvaluating.Size = new System.Drawing.Size(56, 23);
            this.btnEvaluating.TabIndex = 38;
            this.btnEvaluating.Text = "批卷";
            this.btnEvaluating.UseVisualStyleBackColor = true;
            this.btnEvaluating.Click += new System.EventHandler(this.btnEvaluating_Click);
            // 
            // StudentPaperDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(607, 351);
            this.Controls.Add(this.btnEvaluating);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblCorrent);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.rdoAnswerD);
            this.Controls.Add(this.rdoAnswerC);
            this.Controls.Add(this.rdoAnswerB);
            this.Controls.Add(this.rdoAnswerA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAnswerCard);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChenckAnswer);
            this.Controls.Add(this.lblQuestionType);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StudentPaperDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.StudentPaperDetailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChenckAnswer;
        private System.Windows.Forms.Label lblQuestionType;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.Button btnAnswerCard;
        private System.Windows.Forms.Label lblCorrent;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.RadioButton rdoAnswerB;
        private System.Windows.Forms.RadioButton rdoAnswerA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdoAnswerC;
        private System.Windows.Forms.RadioButton rdoAnswerD;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnEvaluating;
    }
}