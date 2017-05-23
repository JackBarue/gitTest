using StRttmy.BLL;
using StRttmy.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.Model;
using StRttmy.Repository;
using System.IO;

namespace StRttmy.UI
{
    public partial class AnswerCardForm : Form
    {
        private QuestionBLL questionBLL;
        private StudentRepository studentBLL;
        private PaperBLL paperBLL;
        public Guid testpaperId;
        private int count;
        private string pathUrl = Application.StartupPath + @"\StudentAnswerFile\";
        public AnswerCardForm()
        {
            InitializeComponent();
        }

        private void tmrTestingTime_Tick(object sender, EventArgs e)
        {
            int minute;   // 当前的分钟
            int second;   // 秒
            // 如果还有剩余时间，就显示剩余的分钟和秒数
            if (QuestionHelp.RemainSeconds > 0)
            {
                QuestionHelp.RemainSeconds--;
                minute = QuestionHelp.RemainSeconds / 60;
                second = QuestionHelp.RemainSeconds % 60;
                lblTime.Text = string.Format("考试时间剩余：{0:00}:{1:00}", minute, second);
                QuestionHelp.RemainSeconds--;
            }
            //// 否则，就提示交卷
            else
            {
                tmrTestingTime.Stop();
                MessageBox.Show("时间到，将自动交卷！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Finish();
            }
        }

        private void AnswerCardForm_Load(object sender, EventArgs e)
        {
            tmrTestingTime.Start();
            ControlShow();
            int index = 0;
            foreach (Control item in this.Controls)
            {
                if (item is Label)
                {
                    if (item.Name != "lblTime")
                    {
                        index = Convert.ToInt32(item.Tag);
                        if (index != -1)
                        {
                            questionBLL = new QuestionBLL();
                            string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
                            if ((questiontype == "简答题" || questiontype == "论述题") && QuestionHelp.StudentAnswer[index]!="未回答")
                            {
                                item.Text = "已上传";
                            }
                            else
                            {
                                item.Text = QuestionHelp.StudentAnswer[index];
                            }
                        }
                    }
                }
            }
        }

        private void btnAnswerIndex_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(((Button)sender).Tag);
            TestingForm testingform = new TestingForm();
            testingform.index = index;
            testingform.MdiParent = this.MdiParent;
            testingform.Show();
            this.Close();
        }

        private void ControlShow()
        {
            questionBLL = new QuestionBLL();
            int btnIndex = 0;
            int btnNo = 1;
            int btnNumber = 0;
            int btnRow = 0;

            int lblIndex = 0;
            int lblNumber = 0;
            int lblRow = 0;
            count = questionBLL.Question(testpaperId).Count;
            if (count <= 30)
            {
                this.Size = new Size(561, 390);
                lblTime.Location = new Point(389, 9);
                btnFinish.Location = new Point(414, 317);
                btnReturn.Location = new Point(469, 317);
                foreach (var item in questionBLL.Question(QuestionHelp.paperId))
                {
                    Button button = new Button();
                    if (btnNumber % 5 == 0 && btnNumber != 0)
                    {
                        btnRow++;
                    }
                    button.Name = item.TestQuestionId.ToString();
                    button.Size = new Size(35, 23);
                    button.Tag = btnIndex;
                    button.Text = btnNo.ToString();
                    button.Location = new Point(20 + btnNumber % 5 * 106, 53 + btnRow * 50);
                    button.Click += new System.EventHandler(this.btnAnswerIndex_Click);
                    btnIndex++;
                    btnNo++;
                    btnNumber++;

                    Label label = new Label();
                    if (lblNumber % 5 == 0 && lblNumber != 0)
                    {
                        lblRow++;
                    }
                    label.Size = new Size(41, 12);
                    label.Tag = lblIndex;
                    label.Location = new Point(61 + lblNumber % 5 * 106, 58 + lblRow * 50);
                    lblIndex++;
                    lblNumber++;
                    this.Controls.Add(button);
                    this.Controls.Add(label);
                }
            }
            else
            {
                this.Size = new Size(1080, 540);
                lblTime.Location = new Point(908, 9);
                btnFinish.Location = new Point(933, 467);
                btnReturn.Location = new Point(988, 467);
                foreach (var item in questionBLL.Question(testpaperId))
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

                    Label label = new Label();
                    if (lblNumber % 10 == 0 && lblNumber != 0)
                    {
                        lblRow++;
                    }
                    label.Size = new Size(41, 12);
                    label.Tag = lblIndex;
                    label.Location = new Point(61 + lblNumber % 10 * 106, 58 + lblRow * 50);
                    lblIndex++;
                    lblNumber++;
                    this.Controls.Add(button);
                    this.Controls.Add(label);
                }
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            DialogResult remindBox = MessageBox.Show("交卷后您将无法修改答案，确认交卷吗？", "提示", MessageBoxButtons.OKCancel);
            if (remindBox == DialogResult.OK)
            {
                Finish();
            }
            
        }

        public void Finish()
        {
            studentBLL = new StudentRepository();
            string workNumber = studentBLL.students(Program.mf.LoginUser.UserId).WorkNumber.ToString();
            List<StudentAnswer> studentanswer = new List<StudentAnswer>();
            int index = 1;
            for (int i = 0; i < QuestionHelp.QuestionCount; i++)
            {
                questionBLL = new QuestionBLL();
                string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[i]).QuestionType.ToString();
                StudentAnswer newanswer = new StudentAnswer();
                newanswer.AnswerId = Guid.NewGuid();
                newanswer.StudentId = Program.mf.LoginUser.UserId;
                newanswer.TestPaperId = QuestionHelp.paperId;
                newanswer.TestQuestionId = QuestionHelp.SelectQuestionId[i];

                string AnswerFilePath = pathUrl + workNumber;
                if (!Directory.Exists(AnswerFilePath))
                {
                    Directory.CreateDirectory(AnswerFilePath);
                }
                string StudentAnswer = QuestionHelp.StudentAnswer[i];
                string AnswerFile = QuestionHelp.AnswerFile[i];                    
                if ((questiontype == "简答题" || questiontype == "论述题")&&StudentAnswer!="未回答")
                {
                    var stuff = AnswerFile.Split('.');
                    File.Copy(AnswerFile, AnswerFilePath + "\\" + "第" + index + "题"+"."+stuff[1], true);
                    newanswer.Answer = StudentAnswer;
                }
                else
                {
                    newanswer.Answer = QuestionHelp.StudentAnswer[i];
                }
                string Correct = questionBLL.Find(QuestionHelp.SelectQuestionId[i]).Correct;
                if (StudentAnswer == Correct)
                {
                    newanswer.AnswerScore = Convert.ToDouble(questionBLL.Find(QuestionHelp.SelectQuestionId[i]).Score);
                }
                else
                {
                    newanswer.AnswerScore = 0;
                }
                studentanswer.Add(newanswer);
                index++;
            }
            paperBLL = new PaperBLL();
            if (paperBLL.AddStudentAnswer(studentanswer, QuestionHelp.paperId, Program.mf.LoginUser.UserId))
            {
                MessageBox.Show("试卷提交成功！");
            }
            else
            {
                MessageBox.Show("试卷提交失败！");
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            TestingForm testingform = new TestingForm();
            testingform.MdiParent = this.MdiParent;
            testingform.Show();
            this.Close();
        }
    }
}
