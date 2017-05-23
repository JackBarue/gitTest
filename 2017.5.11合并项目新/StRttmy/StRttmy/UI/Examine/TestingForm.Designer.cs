namespace StRttmy.UI
{
    partial class TestingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestingForm));
            this.lblQuestion = new System.Windows.Forms.Label();
            this.tmrTestingTime = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblQuestionType = new System.Windows.Forms.Label();
            this.rdoAnswerA = new System.Windows.Forms.RadioButton();
            this.rdoAnswerB = new System.Windows.Forms.RadioButton();
            this.rdoAnswerC = new System.Windows.Forms.RadioButton();
            this.rdoAnswerD = new System.Windows.Forms.RadioButton();
            this.btnAnswerCard = new System.Windows.Forms.Button();
            this.btnUploadAnswer = new System.Windows.Forms.Button();
            this.lblaAnswerLocation = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFinsh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.Location = new System.Drawing.Point(12, 55);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(581, 88);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "label1";
            // 
            // tmrTestingTime
            // 
            this.tmrTestingTime.Interval = 1000;
            this.tmrTestingTime.Tick += new System.EventHandler(this.tmrTestingTime_Tick);
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.Location = new System.Drawing.Point(449, 7);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(144, 23);
            this.lblTime.TabIndex = 2;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "答案：";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(445, 282);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "下一题";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblQuestionType
            // 
            this.lblQuestionType.AutoSize = true;
            this.lblQuestionType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblQuestionType.Location = new System.Drawing.Point(12, 31);
            this.lblQuestionType.Name = "lblQuestionType";
            this.lblQuestionType.Size = new System.Drawing.Size(49, 14);
            this.lblQuestionType.TabIndex = 9;
            this.lblQuestionType.Text = "单选题";
            // 
            // rdoAnswerA
            // 
            this.rdoAnswerA.AutoSize = true;
            this.rdoAnswerA.Location = new System.Drawing.Point(40, 180);
            this.rdoAnswerA.Name = "rdoAnswerA";
            this.rdoAnswerA.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerA.TabIndex = 10;
            this.rdoAnswerA.TabStop = true;
            this.rdoAnswerA.Tag = "A";
            this.rdoAnswerA.Text = "radioButton1";
            this.rdoAnswerA.UseVisualStyleBackColor = true;
            this.rdoAnswerA.Click += new System.EventHandler(this.rdoAnswer_Click);
            // 
            // rdoAnswerB
            // 
            this.rdoAnswerB.AutoSize = true;
            this.rdoAnswerB.Location = new System.Drawing.Point(40, 215);
            this.rdoAnswerB.Name = "rdoAnswerB";
            this.rdoAnswerB.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerB.TabIndex = 11;
            this.rdoAnswerB.TabStop = true;
            this.rdoAnswerB.Tag = "B";
            this.rdoAnswerB.Text = "radioButton1";
            this.rdoAnswerB.UseVisualStyleBackColor = true;
            this.rdoAnswerB.Click += new System.EventHandler(this.rdoAnswer_Click);
            // 
            // rdoAnswerC
            // 
            this.rdoAnswerC.AutoSize = true;
            this.rdoAnswerC.Location = new System.Drawing.Point(40, 250);
            this.rdoAnswerC.Name = "rdoAnswerC";
            this.rdoAnswerC.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerC.TabIndex = 12;
            this.rdoAnswerC.TabStop = true;
            this.rdoAnswerC.Tag = "C";
            this.rdoAnswerC.Text = "radioButton1";
            this.rdoAnswerC.UseVisualStyleBackColor = true;
            this.rdoAnswerC.Click += new System.EventHandler(this.rdoAnswer_Click);
            // 
            // rdoAnswerD
            // 
            this.rdoAnswerD.AutoSize = true;
            this.rdoAnswerD.Location = new System.Drawing.Point(40, 285);
            this.rdoAnswerD.Name = "rdoAnswerD";
            this.rdoAnswerD.Size = new System.Drawing.Size(95, 16);
            this.rdoAnswerD.TabIndex = 13;
            this.rdoAnswerD.TabStop = true;
            this.rdoAnswerD.Tag = "D";
            this.rdoAnswerD.Text = "radioButton1";
            this.rdoAnswerD.UseVisualStyleBackColor = true;
            this.rdoAnswerD.Click += new System.EventHandler(this.rdoAnswer_Click);
            // 
            // btnAnswerCard
            // 
            this.btnAnswerCard.Location = new System.Drawing.Point(356, 282);
            this.btnAnswerCard.Name = "btnAnswerCard";
            this.btnAnswerCard.Size = new System.Drawing.Size(75, 23);
            this.btnAnswerCard.TabIndex = 14;
            this.btnAnswerCard.Text = "显示答题卡";
            this.btnAnswerCard.UseVisualStyleBackColor = true;
            this.btnAnswerCard.Click += new System.EventHandler(this.btnAnswerCard_Click);
            // 
            // btnUploadAnswer
            // 
            this.btnUploadAnswer.Location = new System.Drawing.Point(157, 282);
            this.btnUploadAnswer.Name = "btnUploadAnswer";
            this.btnUploadAnswer.Size = new System.Drawing.Size(75, 23);
            this.btnUploadAnswer.TabIndex = 15;
            this.btnUploadAnswer.Text = "上传答案";
            this.btnUploadAnswer.UseVisualStyleBackColor = true;
            this.btnUploadAnswer.Click += new System.EventHandler(this.btnUploadAnswer_Click);
            // 
            // lblaAnswerLocation
            // 
            this.lblaAnswerLocation.Location = new System.Drawing.Point(121, 323);
            this.lblaAnswerLocation.Name = "lblaAnswerLocation";
            this.lblaAnswerLocation.Size = new System.Drawing.Size(254, 23);
            this.lblaAnswerLocation.TabIndex = 16;
            this.lblaAnswerLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 319);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 1);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // btnFinsh
            // 
            this.btnFinsh.Location = new System.Drawing.Point(534, 282);
            this.btnFinsh.Name = "btnFinsh";
            this.btnFinsh.Size = new System.Drawing.Size(57, 23);
            this.btnFinsh.TabIndex = 18;
            this.btnFinsh.Text = "交卷";
            this.btnFinsh.UseVisualStyleBackColor = true;
            this.btnFinsh.Click += new System.EventHandler(this.btnFinsh_Click);
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(605, 320);
            this.Controls.Add(this.btnFinsh);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.lblaAnswerLocation);
            this.Controls.Add(this.btnUploadAnswer);
            this.Controls.Add(this.btnAnswerCard);
            this.Controls.Add(this.rdoAnswerD);
            this.Controls.Add(this.rdoAnswerC);
            this.Controls.Add(this.rdoAnswerB);
            this.Controls.Add(this.rdoAnswerA);
            this.Controls.Add(this.lblQuestionType);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblQuestion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考试中";
            this.Load += new System.EventHandler(this.TestingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Timer tmrTestingTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblQuestionType;
        private System.Windows.Forms.RadioButton rdoAnswerA;
        private System.Windows.Forms.RadioButton rdoAnswerB;
        private System.Windows.Forms.RadioButton rdoAnswerC;
        private System.Windows.Forms.RadioButton rdoAnswerD;
        private System.Windows.Forms.Button btnAnswerCard;
        private System.Windows.Forms.Button btnUploadAnswer;
        private System.Windows.Forms.Label lblaAnswerLocation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFinsh;
    }
}