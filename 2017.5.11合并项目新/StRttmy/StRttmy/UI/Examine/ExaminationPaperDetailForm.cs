using StRttmy.BLL;
using StRttmy.Common;
using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI
{
    public partial class ExaminationPaperDetailForm : Form
    {
        public TestPapersListForm tplf = null;
        public Guid testpaperId;
        private delegate void FreshData();
        private QuestionBLL questionBLL;
        private PaperBLL paperBLL;
        private StudentRepository studentBLL;
        public int index = 0;
        private int count;

        public ExaminationPaperDetailForm()
        {
            InitializeComponent();
        }

        private void ExaminationPaperForm_Load(object sender, EventArgs e)
        {
            GetQuestionDetail();
            CheckAnswer();
        }

        private void GetQuestionDetail()
        {
            rdoAnswerA.Enabled = false;
            rdoAnswerB.Enabled = false;
            rdoAnswerC.Enabled = false;
            rdoAnswerD.Enabled = false;
            questionBLL = new QuestionBLL();
            questionBLL.Find(QuestionHelp.SelectQuestionId[index]);
            string No = string.Format("{0}、", index + 1);
            lblQuestionType.Text = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
            string question = string.Format("{0}", questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Question);
            string score = string.Format("{0}", questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Score);
            paperBLL = new PaperBLL();
            TestStateType state = paperBLL.SearchState(QuestionHelp.StudentExamPaperId).TestState;
            //if (state == TestStateType.已批卷)
            //{
                lblPaperScore.Text = string.Format(@"试卷满分：{0}分  总得分：{1}分", QuestionHelp.CountScore, QuestionHelp.StudentScore);
                lblScore.Visible = true;
                lblScore.Text = "本题得分：" + QuestionHelp.Score[index] + "分";
            //}
            //else
            //{
            //    lblPaperScore.Text = string.Format(@"试卷满分：{0}分", QuestionHelp.CountScore);
            //    lblScore.Visible = false;
            //}
            lblQuestion.Text = string.Format("{0} {1}  本小题{2}分", No, question, score);
            
            lblCorrent.Text = "正确答案：" + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Correct;

            if (lblQuestionType.Text == TestQuestionType.判断题.ToString())
            {
                //this.Height = 283;
                rdoAnswerA.Visible = true;
                rdoAnswerB.Visible = true;
                rdoAnswerA.Text = "A." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerA;
                rdoAnswerB.Text = "B." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerB;
                rdoAnswerC.Visible = false;
                rdoAnswerD.Visible = false;
                lblCorrent.Location = new Point(154, 139);
                lblScore.Location = new Point(255, 134);
                btnLast.Location = new Point(319, 212);
                btnNext.Location = new Point(412, 212);
                btnAnswerCard.Location = new Point(505, 212);
                
                btnUploadAnswer.Visible = false;
            }
            else if (lblQuestionType.Text == TestQuestionType.简答题.ToString() || lblQuestionType.Text == TestQuestionType.论述题.ToString())
            {
                //this.Height = 283;
                rdoAnswerA.Visible = false;
                rdoAnswerB.Visible = false;
                rdoAnswerC.Visible = false;
                rdoAnswerD.Visible = false;
                btnLast.Location = new Point(319, 212);
                btnNext.Location = new Point(412, 212);
                btnAnswerCard.Location = new Point(505, 212);
                btnUploadAnswer.Location = new Point(40, 139);
                btnUploadAnswer.Visible = true;
                lblCorrent.Visible = false;                
                label2.Visible = false;
            }
            else
            {
                //this.Height = 358;
                btnUploadAnswer.Visible = false;
                rdoAnswerA.Visible = true;
                rdoAnswerB.Visible = true;
                rdoAnswerC.Visible = true;
                rdoAnswerD.Visible = true;
                lblCorrent.Location = new Point(154, 139);
                lblScore.Location = new Point(255, 134);
                btnLast.Location = new Point(319, 266);
                btnNext.Location = new Point(412, 266);
                btnAnswerCard.Location = new Point(505, 266);
                rdoAnswerA.Text = "A." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerA;
                rdoAnswerB.Text = "B." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerB;
                rdoAnswerC.Text = "C." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerC;
                rdoAnswerD.Text = "D." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerD;
            }

        }

        private void CheckAnswer()
        {
            questionBLL = new QuestionBLL();
            string questionType = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
            if (questionType == TestQuestionType.单选题.ToString() || questionType == TestQuestionType.判断题.ToString())
            {
                switch (QuestionHelp.StudentAnswer[index])
                {
                    case "A":
                        rdoAnswerA.Checked = true;
                        break;
                    case "B":
                        rdoAnswerB.Checked = true;
                        break;
                    case "C":
                        rdoAnswerC.Checked = true;
                        break;
                    case "D":
                        rdoAnswerD.Checked = true;
                        break;
                    default:
                        rdoAnswerA.Checked = false;
                        rdoAnswerB.Checked = false;
                        rdoAnswerC.Checked = false;
                        rdoAnswerD.Checked = false;
                        break;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (index < QuestionHelp.SelectQuestionId.Length - 1)
            {
                btnLast.Enabled = true;
                index++;
                GetQuestionDetail();
                CheckAnswer();
                btnLast.Text = "上一题";
            }
            else
            {
                btnNext.Text = "最后一题";
                btnNext.Enabled = false;
            }
        }

        private void btnUploadAnswer_Click(object sender, EventArgs e)
        {
            paperBLL = new PaperBLL();
            studentBLL = new StudentRepository();
            string answer = paperBLL.FindStuAnswer(QuestionHelp.stuid, QuestionHelp.SelectQuestionId[index], QuestionHelp.paperId).Answer;
            string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
            if ((questiontype == "简答题" || questiontype == "论述题") && answer != "未回答")
            {
                var suffix = answer.Split('.');
                string workno = studentBLL.students(QuestionHelp.stuid).WorkNumber.ToString();
                string filepath = Application.StartupPath + @"\StudentAnswerFile\" + workno + @"\";
                int no = index + 1;
                System.Diagnostics.Process.Start(filepath + "第" + no + "题" + "." + suffix[1]);
            }              
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (index <=0)
            {
                btnLast.Text = "第一题";
                btnLast.Enabled = false;
                
            }
            else
            {
                btnNext.Enabled = true;
                btnNext.Text = "下一题";
                index--;
                GetQuestionDetail();
                CheckAnswer(); 
            }
        }

        private bool show = true;
        private void btnAnswerCard_Click(object sender, EventArgs e)
        {
            if (show)
            {
                //cosf = new CardofScoreForm();
                //cosf.x = this.x;
                //cosf.y = this.y;
                //cosf.spdf = this;
                //cosf.spdfHeight = this.Size.Height;
                //cosf.Show();
                btnAnswerCard.Text = "隐藏答题卡";
                this.Height = 757;
                flowLayoutPanel1.Height = 383;
                flowLayoutPanel1.Controls.Clear();
                //GetQuestionDetail();
                ControlShow();
                foreach (Control item in flowLayoutPanel1.Controls)
                {
                    int index1 = Convert.ToInt32(item.Tag);
                    if (index1 != -1)
                    {
                        questionBLL = new QuestionBLL();
                        string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();

                        if ((questiontype == "简答题" || questiontype == "论述题") && QuestionHelp.ScoreInfo[index] == "未评分")
                        {
                            item.Text = "未评分";
                        }
                        else
                        {
                            item.Text = (index1 + 1).ToString() + "、" + QuestionHelp.Score[index1].ToString() + "分";
                        }
                    }

                }
                show = false;
            }
            else if (!show)
            {
                //cosf.Close();
                btnAnswerCard.Text = "显示答题卡";
                this.Height = 336;
                flowLayoutPanel1.Height = 0;
                show = true;
            }
        }

        private void ControlShow()
        {
            questionBLL = new QuestionBLL();
            int btnIndex = 0;
            int btnNo = 1;
            int btnNumber = 0;
            int btnRow = 0;

            //int lblIndex = 0;
            //int lblNumber = 0;
            //int lblRow = 0;
            count = questionBLL.Question(QuestionHelp.paperId).Count;
            if (count <= 30)
            {
                //this.Size = new Size(561, 390);

                foreach (var item in questionBLL.Question(QuestionHelp.paperId))
                {
                    Button button = new Button();
                    if (btnNumber % 5 == 0 && btnNumber != 0)
                    {
                        btnRow++;
                    }
                    button.Name = item.TestQuestionId.ToString();
                    //button.Size = new Size(35, 23);
                    button.Tag = btnIndex;
                    button.Text = btnNo.ToString();
                    button.Location = new Point(20 + btnNumber % 5 * 106, 53 + btnRow * 50);
                    button.Click += new System.EventHandler(this.btnAnswerIndex_Click);
                    btnIndex++;
                    btnNo++;
                    btnNumber++;

                    //Label label = new Label();
                    //if (lblNumber % 5 == 0 && lblNumber != 0)
                    //{
                    //    lblRow++;
                    //}
                    //label.Size = new Size(41, 12);
                    //label.Tag = lblIndex;
                    //label.Location = new Point(61 + lblNumber % 5 * 106, 58 + lblRow * 50);
                    //lblIndex++;
                    //lblNumber++;
                    flowLayoutPanel1.Controls.Add(button);
                    //flowLayoutPanel1.Controls.Add(label);
                }
            }
            else
            {
                this.Size = new Size(1080, 540);
                foreach (var item in questionBLL.Question(QuestionHelp.paperId))
                {
                    Button button = new Button();
                    if (btnNumber % 10 == 0 && btnNumber != 0)
                    {
                        btnRow++;
                    }
                    button.Name = item.TestQuestionId.ToString();
                    button.Size = new Size(35, 23);
                    button.Tag = btnIndex;
                    button.Text = btnNo.ToString();
                    button.Location = new Point(20 + btnNumber % 10 * 106, 53 + btnRow * 50);
                    button.Click += new System.EventHandler(this.btnAnswerIndex_Click);
                    btnIndex++;
                    btnNo++;
                    btnNumber++;

                    //Label label = new Label();
                    //if (lblNumber % 10 == 0 && lblNumber != 0)
                    //{
                    //    lblRow++;
                    //}
                    //label.Size = new Size(41, 12);
                    //label.Tag = lblIndex;
                    //label.Location = new Point(61 + lblNumber % 10 * 106, 58 + lblRow * 50);
                    //lblIndex++;
                    //lblNumber++;
                    flowLayoutPanel1.Controls.Add(button);
                    //flowLayoutPanel1.Controls.Add(label);
                }
            }
        }

        private void btnAnswerIndex_Click(object sender, EventArgs e)
        {
            index = Convert.ToInt32(((Button)sender).Tag);
            if (index < QuestionHelp.SelectQuestionId.Length - 1)
            {
                btnNext.Enabled = true;
                btnNext.Text = "下一题";
            }
            GetQuestionDetail();
            CheckAnswer();
            //btnNextClick();

            //spdf.index = index;
            //FreshData ads = new FreshData(spdf.Testload);
            //ads(index);
            //spdf.Show();

        }
    }
}
