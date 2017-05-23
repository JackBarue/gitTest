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
    public partial class StudentPaperDetailForm : Form
    {
        public Guid StudentExamPaperId;
        private PaperBLL paperBLL;
        private QuestionBLL questionBLL;
        private StudentRepository studentBLL;
        public int index = 0;
        public int x = 0;
        public int y = 0;
        public Guid paperid;
        private int count;

        public StudentPaperDetailForm()
        {
            InitializeComponent();
        }

        private void StudentPaperDetailForm_Load(object sender, EventArgs e)
        {
            paperBLL = new PaperBLL();
            string teststate = paperBLL.MatchingStuPaper(StudentExamPaperId).TestState.ToString();
            if (teststate == "已批卷")
            {
                btnEvaluating.Enabled = false;
            }
            if (teststate == "未批卷")
            {
                btnEvaluating.Enabled = true;
            }

            //this.Location = new Point(x, y - 200);
            ShowWindows();
            CheckAnswer();
            btnNextClick();

            //this.Height = 393;
        }

        //public void Testload(int a) 
        //{
        //    index = a;
        //    //this.Location = new Point(x, y - 200);
        //    ShowWindows();
        //    CheckAnswer();
        //    btnNextClick();
        //}

        private void btnChenckAnswer_Click(object sender, EventArgs e)
        {
            if (btnChenckAnswer.Enabled == true)
            {
                paperBLL = new PaperBLL();
                studentBLL = new StudentRepository();
                string answer = paperBLL.FindStuAnswer(QuestionHelp.stuid, QuestionHelp.SelectQuestionId[index], paperid).Answer;
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
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (index < QuestionHelp.SelectQuestionId.Length - 1)
            {
                index++;
                ShowWindows();
                btnNextClick();
                CheckAnswer();
            }
        }

        private bool show = true;
        //private CardofScoreForm cosf ;
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
                this.Height = 684;
                flowLayoutPanel1.Height = 301;
                flowLayoutPanel1.Controls.Clear();
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
                this.Height = 393;
                flowLayoutPanel1.Height = 0;
                show = true;
            }
        }

        private void txtScore_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScore.Text))
            {
                QuestionHelp.Score[index] = Convert.ToDouble(txtScore.Text);
                QuestionHelp.ScoreInfo[index] = "已评分";
            }
        }

        private void ShowWindows()
        {
            paperBLL = new PaperBLL();
            questionBLL = new QuestionBLL();
            string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
            lblQuestionType.Text = questiontype;
            lblQuestion.Text = string.Format("{0}、 {1}", index + 1, questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Question);
            if (questiontype == "单选题")
            {
                //this.Size = new Size(623, 391);
                label2.Visible = true;
                label2.Location = new Point(10, 143);
                lblCorrent.Visible = true;
                lblCorrent.Location = new Point(154, 143);
                lblScore.Visible = true;
                lblScore.Location = new Point(254, 138);
                rdoAnswerA.Visible = true;
                rdoAnswerA.Enabled = false;
                rdoAnswerA.Location = new Point(40, 168);
                rdoAnswerB.Visible = true;
                rdoAnswerB.Enabled = false;
                rdoAnswerB.Location = new Point(40, 203);
                rdoAnswerC.Visible = true;
                rdoAnswerC.Enabled = false;
                rdoAnswerC.Location = new Point(40, 238);
                rdoAnswerD.Visible = true;
                rdoAnswerD.Enabled = false;
                rdoAnswerD.Location = new Point(40, 273);
                btnChenckAnswer.Visible = false;
                label1.Visible = false;
                txtScore.Visible = false;
                btnAnswerCard.Location = new Point(346, 316);
                btnNext.Location = new Point(441, 316);
                btnEvaluating.Location = new Point(536, 316);
                rdoAnswerA.Text = "A." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerA;
                rdoAnswerB.Text = "B." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerB;
                rdoAnswerC.Text = "C." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerC;
                rdoAnswerD.Text = "D." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerD;
                lblCorrent.Text = "正确答案："+questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Correct;
                lblScore.Text = "本题得分：" + QuestionHelp.Score[index].ToString();

            }
            else if (questiontype == "判断题")
            {
                //this.Size = new Size(623, 315);
                label2.Visible = true;
                label2.Location = new Point(10, 143);
                lblCorrent.Visible = true;
                lblCorrent.Location = new Point(154, 143);
                lblScore.Visible = true;
                lblScore.Location = new Point(254, 138);
                rdoAnswerA.Visible = true;
                rdoAnswerA.Location = new Point(40, 168);
                rdoAnswerB.Visible = true;
                rdoAnswerB.Location = new Point(40, 203);
                rdoAnswerC.Visible = false;
                rdoAnswerD.Visible = false;
                btnChenckAnswer.Visible = false;
                label1.Visible = false;
                txtScore.Visible = false;
                btnChenckAnswer.Visible = false;
                btnAnswerCard.Location = new Point(346, 237);
                btnNext.Location = new Point(441, 237);
                btnEvaluating.Location = new Point(536, 237);
                rdoAnswerA.Text = "A." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerA;
                rdoAnswerB.Text = "B." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerB;
                lblCorrent.Text = "正确答案：" + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Correct;
                lblScore.Text = "本题得分：" + QuestionHelp.Score[index].ToString();
            }
            if (questiontype == "简答题" || questiontype == "论述题")
            {
                //this.Size = new Size(623, 227);
                btnChenckAnswer.Visible = true;
                btnChenckAnswer.Enabled = true;
                btnChenckAnswer.Location = new Point(12, 150);
                label1.Visible = true;
                label1.Location = new Point(126, 155);
                txtScore.Visible = true;
                txtScore.Enabled = true;
                txtScore.Location = new Point(54, 21);
                btnAnswerCard.Location = new Point(346, 150);
                btnNext.Location = new Point(441, 150);
                btnEvaluating.Location = new Point(536, 150);
                if (QuestionHelp.ScoreInfo[index] == "已评分")
                {
                    txtScore.Text = QuestionHelp.Score[index].ToString();
                }
                else
                {
                    txtScore.Text = "";
                }
            }
            else
            {
                btnChenckAnswer.Enabled = false;
                txtScore.Text = QuestionHelp.Score[index].ToString();
                txtScore.Enabled = false;
            }
        }

        private void btnNextClick()
        {
            if (index >= QuestionHelp.AllQuestionId.Length - 1)
            {
                btnNext.Enabled = false;
                btnNext.Text = "最后一题";
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
            ShowWindows();
            CheckAnswer();
            btnNextClick();

            //spdf.index = index;
            //FreshData ads = new FreshData(spdf.Testload);
            //ads(index);
            //spdf.Show();

        }

        private void btnEvaluating_Click(object sender, EventArgs e)
        {
            List<StudentAnswer> studentanswer = new List<StudentAnswer>();
            for (int i = 0; i < QuestionHelp.QuestionCount; i++)
            {
                questionBLL = new QuestionBLL();
                StudentAnswer newscore = new StudentAnswer();
                newscore.StudentId = QuestionHelp.stuid;
                newscore.TestPaperId = QuestionHelp.paperId;
                newscore.TestQuestionId = QuestionHelp.SelectQuestionId[i];
                if (QuestionHelp.ScoreInfo[i] == "未评分")
                {
                    newscore.AnswerScore = 0;
                }
                else
                {
                    newscore.AnswerScore = QuestionHelp.Score[i];
                }
                studentanswer.Add(newscore);
            }
            paperBLL = new PaperBLL();
            if (paperBLL.UpdateScore(studentanswer, QuestionHelp.paperId, QuestionHelp.stuid))
            {
                MessageBox.Show("阅卷成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("阅卷失败！");
            }
        }
    }
}
