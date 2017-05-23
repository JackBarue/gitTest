using StRttmy.BLL;
using StRttmy.Common;
using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI
{
    public partial class NewTestingForm : Form
    {
        public User LoginUser = null;
        public StudentPaperForm spf = null;
        public ReadPapersForm rpf = null;
        public Guid testpaperId;
        public Guid studentid;
        public int index = 0;
        public int testtime = 0;
        public int judge;
        private delegate void FreshData();
        private QuestionBLL questionBLL = null;
        private string AnswerLocation = "";
        private StudentRepository studentBLL;
        private string pathUrl = Application.StartupPath + @"\StudentAnswerFile\";
        private PaperBLL paperBLL;
        private int minute;
        private int second;
        private IList<TestQuestion> questionFindList = new List<TestQuestion>();
        private Label lblFileName = new Label();


        public NewTestingForm()
        {
            InitializeComponent();
        }

        private void NewTestingForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();
            if (judge == 1)
            {
                tmrTestingTime.Start();
                
                lblTime.ForeColor = Color.White;
                lblTime.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            ShowPaper();
        }

        private void tmrTestingTime_Tick(object sender, EventArgs e)
        {
            if (judge == 1)
            {
                if (testtime > 0)
                {
                    testtime--;
                    minute = testtime / 60;
                    second = testtime % 60;
                    lblTime.Text = string.Format(@"考试时间剩余：{0:00}分{1:00}秒", minute, second);
                    lblTime.Location = new Point(panel1.Width / 2 - lblTime.Width / 2, 80);
                }
                else
                {
                    tmrTestingTime.Stop();
                    MessageBox.Show("考试时间到，请交卷！", "请交卷", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Finsh();
                    this.Close();
                }
            }
            else
            {
                tmrTestingTime.Stop();
            }
        }

        private void ShowPaper()
        {
            panel1.BackColor = Color.FromArgb(21, 111, 160);
            if (Program.mf.LoginUser != null)
            {
                panel1.Controls.Clear();
                flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseWheel);

                questionBLL = new QuestionBLL();
                Label lblpapername = new Label();
                Label lblpsntt = new Label();
                if (judge != 4)
                {
                    questionFindList = questionBLL.QuestioninPaperList(testpaperId, 0);
                    paperBLL = new PaperBLL();
                    string papaerName = paperBLL.GetPaper(testpaperId).TestName;
                    double paperscore = (double)questionFindList.Sum(c => c.Score);
                    int testtime = paperBLL.GetPaper(testpaperId).TestTime;

                    lblpapername.Name = "lblpapername";
                    lblpapername.Text = papaerName;
                    lblpapername.AutoSize = true;
                    lblpapername.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lblpapername.ForeColor = Color.White;
                    panel1.Controls.Add(lblpapername);
                    lblpapername.Location = new Point(panel1.Width / 2 - lblpapername.Width / 2, panel1.Height / 2 - lblpapername.Height / 2 - 20);
                    panel1.Controls.Add(lblpapername);


                    lblpsntt.Name = "lblpsntt";
                    lblpsntt.Text = string.Format(@"满分：{0}分  考试时间：{1}分钟", paperscore, testtime);
                    lblpsntt.AutoSize = true;
                    lblpsntt.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lblpsntt.ForeColor = Color.White;
                    panel1.Controls.Add(lblpsntt);
                    int lblpsnttX = panel1.Width / 2 - lblpsntt.Width / 2;
                    int lblpsnttY = lblpapername.Location.Y + lblpapername.Height + 5;
                    lblpsntt.Location = new Point(lblpsnttX, lblpsnttY);
                    panel1.Controls.Add(lblpsntt);
                }
                else
                {
                    questionFindList = questionBLL.QuestioninPaperList(testpaperId, 0);
                    paperBLL = new PaperBLL();
                    string papaerName = paperBLL.GetPaper(testpaperId).TestName;
                    double paperscore = (double)questionFindList.Sum(c => c.Score);
                    int testtime = paperBLL.GetPaper(testpaperId).TestTime;

                    lblpapername.Name = "lblpapername";
                    lblpapername.AutoSize = true;
                    int x = lblpapername.Height;
                    lblpapername.Font = new System.Drawing.Font("黑体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lblpapername.ForeColor = Color.White;
                    lblpapername.Text = papaerName;
                    panel1.Controls.Add(lblpapername);
                    lblpapername.Location = new Point(panel1.Width / 2 - lblpapername.Width / 2, panel1.Height / 2 - lblpapername.Height / 2 - 20);
                    panel1.Controls.Add(lblpapername);

                    lblpsntt.Name = "lblpsntt";
                    lblpsntt.Text = string.Format(@"满分：{0}分  考试时间：{1}分钟", paperscore, testtime);
                    lblpsntt.AutoSize = true;
                    lblpsntt.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lblpsntt.ForeColor = Color.White;
                    panel1.Controls.Add(lblpsntt);

                    Label lblpaperscore = new Label();
                    lblpaperscore.Font = new System.Drawing.Font("幼圆", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lblpaperscore.Text = string.Format(@"考试得分：{0}分", paperBLL.FindStuTestScore(studentid, testpaperId).Sum(c => c.AnswerScore).ToString());
                    lblpaperscore.ForeColor = Color.FromArgb(255, 132, 0);
                    lblpaperscore.AutoSize = true;
                    panel1.Controls.Add(lblpaperscore);

                    lblpsntt.Location = new Point(panel1.Width / 2 - (lblpsntt.Width + lblpaperscore.Width) / 2, lblpapername.Location.Y + lblpapername.Height + 5);
                    panel1.Controls.Add(lblpsntt);
                    lblpaperscore.Location = new Point(lblpsntt.Location.X + lblpsntt.Width, lblpsntt.Location.Y);
                    panel1.Controls.Add(lblpaperscore);                    
                }

                

                int questionno = 1;
                foreach (var item in questionFindList)
                {
                    Panel panel = new Panel();
                    panel.Name = "panel";
                    panel.Tag = item.TestQuestionId;

                    LabelTx question = new LabelTx();
                    question.LineDistance = 3;
                    question.Tag = item.TestQuestionId;

                    if (item.Question.Length > 50)
                    {
                        panel.Height = 100;
                    }
                    else
                    {
                        panel.Height = 75;
                    }
                    panel.Width = 980;
                    Label questiontype = new Label();
                    questiontype.AutoSize = true;
                    questiontype.Tag = item.TestQuestionId;
                    
                    questiontype.Text = "【" + item.QuestionType.ToString() + "】";
                    questiontype.Location = new Point(20, 1);
                    panel.Controls.Add(questiontype);
                    question.Width = panel.Width - 28;
                    question.Location = new Point(20, questiontype.Height + 5);
                    question.AutoSize = false;
                    #region 未考
                    if (judge == 1)
                    {
                        this.Text = "考试中";
                        btnFinsh.Enabled = true;
                        btnFinsh.Visible = true;
                        question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score + "分";
                        panel.Controls.Add(question);
                        if (item.QuestionType.ToString() == "单选题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = true;
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = true;
                            panel.Controls.Add(rbAnswerB);

                            RadioButton rbAnswerC = new RadioButton();
                            rbAnswerC.Text = string.Format(@"C.{0}", item.AnswerA);
                            rbAnswerC.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 16, question.Height + 22);
                            rbAnswerC.Tag = item.TestQuestionId + "," + "C";
                            rbAnswerC.Enabled = true;
                            panel.Controls.Add(rbAnswerC);

                            if (!string.IsNullOrEmpty(item.AnswerD))
                            {
                                RadioButton rbAnswerD = new RadioButton();
                                rbAnswerD.Text = string.Format(@"D.{0}", item.AnswerA);
                                rbAnswerD.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 16, question.Height + 22);
                                rbAnswerD.Tag = item.TestQuestionId + "," + "D";
                                rbAnswerD.Enabled = true;
                                panel.Controls.Add(rbAnswerD);
                            }
                        }
                        if (item.QuestionType.ToString() == "判断题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = true;
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = true;
                            panel.Controls.Add(rbAnswerB);
                        }
                        if (item.QuestionType.ToString() == "简答题" || item.QuestionType.ToString() == "论述题")
                        {
                            Button btnFile = new Button();
                            btnFile.Text = "上传答案";
                            btnFile.Location = new Point(18, question.Height + 30);
                            btnFile.Tag = item.TestQuestionId;
                            btnFile.Click += btnFile_Click;
                            panel.Controls.Add(btnFile);

                            lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                            lblFileName.Text = "请上传答案";
                            lblFileName.Tag = item.TestQuestionId;
                            panel.Controls.Add(lblFileName);
                        }
                    }
                    #endregion
                    #region 已考
                    if (judge == 2)
                    {
                        this.Text = "考卷查阅";
                        btnFinsh.Enabled = false;
                        btnFinsh.Visible = false;
                        question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score + "分";
                        panel.Controls.Add(question);
                        if (item.QuestionType.ToString() == "单选题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "A")
                            {
                                rbAnswerA.Checked = true;
                            }
                            else
                            {
                                rbAnswerA.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerA);


                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "B")
                            {
                                rbAnswerB.Checked = true;
                            }
                            else
                            {
                                rbAnswerB.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerB);


                            RadioButton rbAnswerC = new RadioButton();
                            rbAnswerC.Text = string.Format(@"C.{0}", item.AnswerA);
                            rbAnswerC.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 16, question.Height + 22);
                            rbAnswerC.Tag = item.TestQuestionId + "," + "C";
                            rbAnswerC.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "C")
                            {
                                rbAnswerC.Checked = true;
                            }
                            else
                            {
                                rbAnswerC.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerC);


                            if (!string.IsNullOrEmpty(item.AnswerD))
                            {
                                RadioButton rbAnswerD = new RadioButton();
                                rbAnswerD.Text = string.Format(@"D.{0}", item.AnswerA);
                                rbAnswerD.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 16, question.Height + 22);
                                rbAnswerD.Tag = item.TestQuestionId + "," + "D";
                                rbAnswerD.Enabled = false;
                                if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "D")
                                {
                                    rbAnswerD.Checked = true;
                                }
                                else
                                {
                                    rbAnswerD.Checked = false;
                                }
                                panel.Controls.Add(rbAnswerD);
                            }
                        }
                        if (item.QuestionType.ToString() == "判断题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "A")
                            {
                                rbAnswerA.Checked = true;
                            }
                            else
                            {
                                rbAnswerA.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "B")
                            {
                                rbAnswerB.Checked = true;
                            }
                            else
                            {
                                rbAnswerB.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerB);
                        }
                        if (item.QuestionType.ToString() == "简答题" || item.QuestionType.ToString() == "论述题")
                        {
                            Button btnFile = new Button();
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "未回答")
                            {
                                btnFile.Location = new Point(18, question.Height + 30);
                                btnFile.Enabled = false;
                                btnFile.Text = "未回答";
                                lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                                lblFileName.Text = "未回答";
                                panel.Controls.Add(btnFile);
                                panel.Controls.Add(lblFileName);
                            }
                            else
                            {
                                btnFile.Text = "查看答案";
                                btnFile.Location = new Point(18, question.Height + 30);
                                btnFile.Tag = item.TestQuestionId;
                                btnFile.Click += btnFile_Click;
                                panel.Controls.Add(btnFile);

                                lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                                lblFileName.Tag = item.TestQuestionId;
                                paperBLL = new PaperBLL();
                                string fileName = paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer;
                                lblFileName.Text = fileName;
                                panel.Controls.Add(lblFileName);
                            }

                        }
                    }
                    #endregion
                    #region 批卷
                    if (judge == 3)
                    {
                        this.Text = "试卷批阅";
                        btnFinsh.Enabled = true;
                        btnFinsh.Visible = true;
                        btnFinsh.Text = "批卷";
                        if (item.QuestionType.ToString() == "单选题" || item.QuestionType.ToString() == "判断题")
                        {
                            question.Text = string.Format(@"{0}、{1}   本题得分：{2}分   正确答案：{3}", questionno.ToString(),
                            item.Question, paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).AnswerScore, item.Correct);
                        }
                        else
                        {
                            question.Text = string.Format(@"{0}、{1}   本题分数：{2}分", questionno.ToString(), item.Question, item.Score);
                        }
                        panel.Controls.Add(question);

                        Label lblrnw = new Label();
                        lblrnw.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                        if (item.QuestionType.ToString() == "单选题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "A")
                            {
                                rbAnswerA.Checked = true;
                            }
                            else
                            {
                                rbAnswerA.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerA);


                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "B")
                            {
                                rbAnswerB.Checked = true;
                            }
                            else
                            {
                                rbAnswerB.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerB);


                            RadioButton rbAnswerC = new RadioButton();
                            rbAnswerC.Text = string.Format(@"C.{0}", item.AnswerA);
                            rbAnswerC.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 16, question.Height + 22);
                            rbAnswerC.Tag = item.TestQuestionId + "," + "C";
                            rbAnswerC.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "C")
                            {
                                rbAnswerC.Checked = true;
                            }
                            else
                            {
                                rbAnswerC.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerC);

                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == item.Correct)
                            {
                                lblrnw.Text = "√";
                                lblrnw.ForeColor = Color.LimeGreen;
                            }
                            else
                            {
                                lblrnw.Text = "×";
                                lblrnw.ForeColor = Color.Red;
                            }
                            lblrnw.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 2, question.Height + 25);

                            if (!string.IsNullOrEmpty(item.AnswerD))
                            {
                                RadioButton rbAnswerD = new RadioButton();
                                rbAnswerD.Text = string.Format(@"D.{0}", item.AnswerA);
                                rbAnswerD.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 16, question.Height + 22);
                                rbAnswerD.Tag = item.TestQuestionId + "," + "D";
                                rbAnswerD.Enabled = false;
                                if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "D")
                                {
                                    rbAnswerD.Checked = true;
                                }
                                else
                                {
                                    rbAnswerD.Checked = false;
                                }
                                panel.Controls.Add(rbAnswerD);
                                lblrnw.Location = new Point(rbAnswerD.Location.X + rbAnswerD.Width + 2, question.Height + 25);
                            }
                            panel.Controls.Add(lblrnw);

                        }
                        if (item.QuestionType.ToString() == "判断题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "A")
                            {
                                rbAnswerA.Checked = true;
                            }
                            else
                            {
                                rbAnswerA.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "B")
                            {
                                rbAnswerB.Checked = true;
                            }
                            else
                            {
                                rbAnswerB.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerB);

                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == item.Correct)
                            {
                                lblrnw.Text = "√";
                                lblrnw.ForeColor = Color.LimeGreen;
                            }
                            else
                            {
                                lblrnw.Text = "×";
                                lblrnw.ForeColor = Color.Red;
                            }
                            lblrnw.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 2, question.Height + 25);
                            panel.Controls.Add(lblrnw);
                        }
                        if (item.QuestionType.ToString() == "简答题" || item.QuestionType.ToString() == "论述题")
                        {
                            Button btnFile = new Button();
                            Label lblscore = new Label();
                            TextBox txtScore = new TextBox();

                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "未回答")
                            {
                                btnFile.Enabled = false;
                                btnFile.Location = new Point(18, question.Height + 30);
                                btnFile.Text = "未回答";

                                lblFileName.AutoSize = true;
                                lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                                lblFileName.Text = "未回答";
                                lblFileName.Tag = item.TestQuestionId;

                                txtScore.Enabled = false;
                                txtScore.Text = "0";
                                panel.Controls.Add(btnFile);
                                panel.Controls.Add(lblFileName);
                            }
                            else
                            {
                                btnFile.Enabled = true;
                                btnFile.Text = "查看答案";
                                btnFile.Location = new Point(18, question.Height + 30);
                                btnFile.Tag = item.TestQuestionId;
                                btnFile.Click += btnFile_Click;

                                lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                                lblFileName.Tag = item.TestQuestionId;
                                paperBLL = new PaperBLL();
                                string fileName = paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer;
                                lblFileName.Text = fileName;

                                panel.Controls.Add(btnFile);
                                panel.Controls.Add(lblFileName);
                            }
                            lblscore.Text = "评分：";
                            lblscore.AutoSize = true;

                            lblscore.Location = new Point(lblFileName.Location.X + lblFileName.Width + 10, lblFileName.Location.Y);
                            panel.Controls.Add(lblscore);
                            txtScore.Width = 30;
                            txtScore.Tag = item.TestQuestionId;
                            txtScore.Location = new Point(lblscore.Location.X + lblscore.Width, btnFile.Location.Y + 1);
                            panel.Controls.Add(txtScore);
                        }
                    }
                    #endregion
                    #region 已批卷
                    if (judge == 4)
                    {
                        this.Text = "考卷查阅";
                        btnFinsh.Enabled = false;
                        btnFinsh.Visible = false;
                        if (item.QuestionType.ToString() == "单选题" || item.QuestionType.ToString() == "判断题")
                        {
                            question.Text = string.Format(@"{0}、{1}   本题得分：{2}分   正确答案：{3}", questionno.ToString(),
                            item.Question, paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).AnswerScore, item.Correct);
                        }
                        else
                        {
                            question.Text = string.Format(@"{0}、{1}   本题得分：{2}分", questionno.ToString(), item.Question, paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).AnswerScore);
                        }
                        panel.Controls.Add(question);

                        Label lblrnw = new Label();
                        lblrnw.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                        if (item.QuestionType.ToString() == "单选题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.BackColor = Color.White;
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(20, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "A")
                            {
                                rbAnswerA.Checked = true;
                            }
                            else
                            {
                                rbAnswerA.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerA);


                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.BackColor = Color.White;
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "B")
                            {
                                rbAnswerB.Checked = true;
                            }
                            else
                            {
                                rbAnswerB.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerB);


                            RadioButton rbAnswerC = new RadioButton();
                            rbAnswerC.BackColor = Color.White;
                            rbAnswerC.Text = string.Format(@"C.{0}", item.AnswerA);
                            rbAnswerC.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 16, question.Height + 22);
                            rbAnswerC.Tag = item.TestQuestionId + "," + "C";
                            rbAnswerC.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "C")
                            {
                                rbAnswerC.Checked = true;
                            }
                            else
                            {
                                rbAnswerC.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerC);

                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == item.Correct)
                            {
                                lblrnw.Text = "√";
                                lblrnw.ForeColor = Color.LimeGreen;
                            }
                            else
                            {
                                lblrnw.Text = "×";
                                lblrnw.ForeColor = Color.Red;
                            }
                            lblrnw.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 2, question.Height + 25);

                            if (!string.IsNullOrEmpty(item.AnswerD))
                            {
                                RadioButton rbAnswerD = new RadioButton();
                                rbAnswerD.Text = string.Format(@"D.{0}", item.AnswerA);
                                rbAnswerD.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 16, question.Height + 22);
                                rbAnswerD.Tag = item.TestQuestionId + "," + "D";
                                rbAnswerD.Enabled = false;
                                if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "D")
                                {
                                    rbAnswerD.Checked = true;
                                }
                                else
                                {
                                    rbAnswerD.Checked = false;
                                }
                                panel.Controls.Add(rbAnswerD);
                                lblrnw.Location = new Point(rbAnswerD.Location.X + rbAnswerD.Width + 2, question.Height + 25);
                            }
                            panel.Controls.Add(lblrnw);

                        }
                        if (item.QuestionType.ToString() == "判断题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "A")
                            {
                                rbAnswerA.Checked = true;
                            }
                            else
                            {
                                rbAnswerA.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "B")
                            {
                                rbAnswerB.Checked = true;
                            }
                            else
                            {
                                rbAnswerB.Checked = false;
                            }
                            panel.Controls.Add(rbAnswerB);

                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == item.Correct)
                            {
                                lblrnw.Text = "√";
                                lblrnw.ForeColor = Color.LimeGreen;
                            }
                            else
                            {
                                lblrnw.Text = "×";
                                lblrnw.ForeColor = Color.Red;
                            }
                            lblrnw.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 2, question.Height + 25);
                            panel.Controls.Add(lblrnw);
                        }
                        if (item.QuestionType.ToString() == "简答题" || item.QuestionType.ToString() == "论述题")
                        {
                            Button btnFile = new Button();
                            Label lblscore = new Label();
                            TextBox txtScore = new TextBox();

                            if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer == "未回答")
                            {
                                btnFile.Enabled = false;
                                btnFile.Location = new Point(18, question.Height + 30);
                                btnFile.Text = "未回答";
                                panel.Controls.Add(btnFile);
                                lblrnw.Text = "×";
                                lblrnw.ForeColor = Color.Red;
                                lblrnw.Location = new Point(btnFile.Location.X + btnFile.Width + 2, btnFile.Location.Y + 2);
                                panel.Controls.Add(lblrnw);
                            }
                            else
                            {
                                btnFile.Enabled = true;
                                btnFile.Text = "查看答案";
                                btnFile.Location = new Point(18, question.Height + 30);
                                btnFile.Tag = item.TestQuestionId;
                                btnFile.Click += btnFile_Click;

                                lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                                lblFileName.Tag = item.TestQuestionId;
                                paperBLL = new PaperBLL();
                                string fileName = paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).Answer;
                                lblFileName.Text = fileName;
                                if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).AnswerScore == item.Score)
                                {
                                    lblrnw.Text = "√";
                                    lblrnw.ForeColor = Color.LimeGreen;
                                }
                                if (paperBLL.FindStuAnswer(studentid, item.TestQuestionId, testpaperId).AnswerScore < item.Score)
                                {
                                    lblrnw.Text = "√";
                                    lblrnw.ForeColor = Color.Orange;
                                }
                                panel.Controls.Add(btnFile);
                                panel.Controls.Add(lblFileName);

                                lblrnw.Location = new Point(lblFileName.Location.X + lblFileName.Width + 2, lblFileName.Location.Y - 5);
                                panel.Controls.Add(lblrnw);
                            }

                        }
                    }
                    #endregion
                    #region 试卷查阅"未考"状态
                    if (judge == 5)
                    {
                        this.Text = "考卷查阅";
                        btnFinsh.Enabled = false;
                        btnFinsh.Visible = false;
                        question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score + "分";
                        panel.Controls.Add(question);
                        if (item.QuestionType.ToString() == "单选题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            panel.Controls.Add(rbAnswerB);

                            RadioButton rbAnswerC = new RadioButton();
                            rbAnswerC.Text = string.Format(@"C.{0}", item.AnswerA);
                            rbAnswerC.Location = new Point(rbAnswerB.Location.X + rbAnswerB.Width + 16, question.Height + 22);
                            rbAnswerC.Tag = item.TestQuestionId + "," + "C";
                            rbAnswerC.Enabled = false;
                            panel.Controls.Add(rbAnswerC);

                            if (!string.IsNullOrEmpty(item.AnswerD))
                            {
                                RadioButton rbAnswerD = new RadioButton();
                                rbAnswerD.Text = string.Format(@"D.{0}", item.AnswerA);
                                rbAnswerD.Location = new Point(rbAnswerC.Location.X + rbAnswerC.Width + 16, question.Height + 22);
                                rbAnswerD.Tag = item.TestQuestionId + "," + "D";
                                rbAnswerD.Enabled = false;
                                panel.Controls.Add(rbAnswerD);
                            }
                        }
                        if (item.QuestionType.ToString() == "判断题")
                        {
                            RadioButton rbAnswerA = new RadioButton();
                            rbAnswerA.Text = string.Format(@"A.{0}", item.AnswerA);
                            rbAnswerA.Location = new Point(18, question.Height + 22);
                            rbAnswerA.Tag = item.TestQuestionId + "," + "A";
                            rbAnswerA.Enabled = false;
                            panel.Controls.Add(rbAnswerA);

                            RadioButton rbAnswerB = new RadioButton();
                            rbAnswerB.Text = string.Format(@"B.{0}", item.AnswerA);
                            rbAnswerB.Location = new Point(rbAnswerA.Location.X + rbAnswerA.Width + 16, question.Height + 22);
                            rbAnswerB.Tag = item.TestQuestionId + "," + "B";
                            rbAnswerB.Enabled = false;
                            panel.Controls.Add(rbAnswerB);
                        }
                        //if (item.QuestionType.ToString() == "简答题" || item.QuestionType.ToString() == "论述题")
                        //{
                        //    Button btnFile = new Button();
                        //    btnFile.Text = "上传答案";
                        //    btnFile.Location = new Point(18, question.Height + 30);
                        //    btnFile.Tag = item.TestQuestionId;
                        //    btnFile.Click += btnFile_Click;
                        //    btnFile
                        //    panel.Controls.Add(btnFile);

                        //    lblFileName.Location = new Point(btnFile.Location.X + btnFile.Width + 16, btnFile.Location.Y + 5);
                        //    lblFileName.Text = "请上传答案";
                        //    lblFileName.Tag = item.TestQuestionId;
                        //    panel.Controls.Add(lblFileName);
                        //}
                    }
                    #endregion
                    flowLayoutPanel1.Controls.Add(panel);
                    questionno++;
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Application.StartupPath;
            openFile.Filter = "记事本文件(*.txt)|*.txt| Word文件(*.doc)|*.doc| 图片文件(*.jpg)|*.jpg| 图片文件(*.png)|*.png| 图片文件(*.bmp)|*.bmp";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            studentBLL = new StudentRepository();
            string workNumber = studentBLL.students(studentid).WorkNumber.ToString();
            if (judge == 1)
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    lblFileName.Text = Path.GetFileName(openFile.FileName);
                    AnswerLocation = openFile.FileName;
                    string FilePath = pathUrl + workNumber;
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    File.Copy(AnswerLocation, FilePath + "\\" + lblFileName.Tag.ToString() + "." + AnswerLocation.Split('.')[1], true);
                }
            }
            else
            {
                paperBLL = new PaperBLL();
                string fileName = paperBLL.FindStuAnswer(studentid, new Guid(lblFileName.Tag.ToString()), testpaperId).Answer;
                string filePath = string.Format(@"{0}\StudentAnswerFile\{1}\", Application.StartupPath, workNumber);
                lblFileName.Text = fileName;
                string fileAllname = string.Format(@"{0}{1}.{2}", filePath, lblFileName.Tag.ToString(), fileName.Split('.')[1]);
                System.Diagnostics.Process.Start(fileAllname);
            }
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_MouseWheel(object sender, MouseEventArgs e)
        {
            int lastRightPanelVerticalScrollValue = -1;
            if (!(flowLayoutPanel1.VerticalScroll.Visible == false || (flowLayoutPanel1.VerticalScroll.Value == 0 && e.Delta > 0) || (flowLayoutPanel1.VerticalScroll.Value == lastRightPanelVerticalScrollValue && e.Delta < 0)))
            {
                flowLayoutPanel1.VerticalScroll.Value += 10;
                lastRightPanelVerticalScrollValue = flowLayoutPanel1.VerticalScroll.Value;
                flowLayoutPanel1.Refresh();
                flowLayoutPanel1.Invalidate();
                flowLayoutPanel1.Update();
            }
        }

        private void btnFinsh_Click(object sender, EventArgs e)
        {
            if (judge == 1)
            {
                DialogResult remindBox = MessageBox.Show("交卷后您将无法修改答案，确认交卷吗？", "确认交卷", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (remindBox == DialogResult.OK)
                {
                    Finsh();
                    FreshData freshdata = new FreshData(spf.ShowStudentPapersList);
                    freshdata();
                    this.Close();
                }
            }
            if (judge == 3)
            {
                DialogResult remindBox = MessageBox.Show("阅卷后您将无法修改分数，确认阅卷吗？", "确认阅卷", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (remindBox == DialogResult.OK)
                {
                    Finsh();
                    FreshData freshdata = new FreshData(rpf.ShowReadPapersList);
                    freshdata();
                    this.Close();
                }
            }
        }

        private void Finsh()
        {
            questionBLL = new QuestionBLL();
            paperBLL = new PaperBLL();
            #region 交卷
            if (judge == 1)
            {
                List<StudentAnswer> saList = new List<StudentAnswer>();
                List<RadioButton> rbList = new List<RadioButton>();
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c.GetType() == typeof(Panel))
                    {
                        foreach (Control con in c.Controls)
                        {
                            if (con.GetType() == typeof(RadioButton))
                            {
                                RadioButton rb = con as RadioButton;
                                rbList.Add(rb);
                            }
                        }
                    }
                }

                questionFindList = questionBLL.QuestioninPaperList(testpaperId, 0);
                foreach (var item in questionFindList)
                {
                    StudentAnswer newsa = new StudentAnswer();
                    newsa.AnswerId = Guid.NewGuid();
                    newsa.TestPaperId = testpaperId;
                    newsa.TestQuestionId = item.TestQuestionId;
                    newsa.StudentId = Program.mf.LoginUser.UserId;
                    var listrb = rbList.Where(c => c.Tag.ToString().Split(',')[0] == item.TestQuestionId.ToString() && c.Checked == true).SingleOrDefault();
                    if (item.QuestionType.ToString() == "单选题" || item.QuestionType.ToString() == "判断题")
                    {
                        if (listrb != null)
                        {
                            newsa.Answer = listrb.Tag.ToString().Split(',')[1];
                            if (newsa.Answer == item.Correct)
                            {
                                newsa.AnswerScore = (double)item.Score;
                            }
                        }
                        else
                        {
                            newsa.Answer = "未回答";
                            newsa.AnswerScore = 0;
                        }
                    }
                    else
                    {
                        if (lblFileName.Text != "请上传答案")
                        {
                            newsa.Answer = lblFileName.Text;
                        }
                        else
                        {
                            newsa.Answer = "未回答";
                        }
                    }
                    saList.Add(newsa);
                }
                if (paperBLL.AddStudentAnswer(saList, testpaperId, Program.mf.LoginUser.UserId))
                {
                    MessageBox.Show("试卷提交成功！");
                }
                else
                {
                    MessageBox.Show("试卷提交失败！");
                }
            }
            #endregion
            #region 批卷
            if (judge == 3)
            {
                List<StudentAnswer> saList = new List<StudentAnswer>();
                List<TextBox> txtList = new List<TextBox>();
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c.GetType() == typeof(Panel))
                    {
                        foreach (Control con in c.Controls)
                        {
                            if (con.GetType() == typeof(TextBox))
                            {
                                TextBox txt = con as TextBox;
                                txtList.Add(txt);
                            }
                        }
                    }
                }
                questionFindList = questionBLL.QuestioninPaperList(testpaperId, 0);
                foreach (var item in questionFindList)
                {
                    StudentAnswer newsa = new StudentAnswer();
                    newsa.TestPaperId = testpaperId;
                    newsa.TestQuestionId = item.TestQuestionId;
                    newsa.StudentId = studentid;
                    var listtb = txtList.Where(c => c.Tag.ToString() == item.TestQuestionId.ToString()).SingleOrDefault();
                    if (item.QuestionType.ToString() != "单选题" && item.QuestionType.ToString() != "判断题")
                    {
                        if (listtb != null)
                        {
                            newsa.AnswerScore = Convert.ToDouble(listtb.Text);

                        }
                        else
                        {
                            newsa.Answer = "未回答";
                            newsa.AnswerScore = 0;
                        }
                        saList.Add(newsa);
                    }
                }
                if (paperBLL.UpdateScore(saList, testpaperId, studentid))
                {
                    MessageBox.Show("阅卷成功！");
                }
            }
            #endregion
        }
    }
}
