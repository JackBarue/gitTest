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
    public partial class TestingForm : Form
    {
        public TestPapersListForm tplf = null;
        public Guid testpaperId;
        private delegate void FreshData();
        private QuestionBLL questionBLL =null;
        public int index = 0;
        private string AnswerLocation = "";
        private StudentRepository studentBLL;
        private string pathUrl = Application.StartupPath + @"\StudentAnswerFile\";
        private PaperBLL paperBLL;

        public TestingForm()
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
            }
            //// 否则，就提示交卷
            else
            {
                tmrTestingTime.Stop();
                MessageBox.Show("时间到，请交卷！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AnswerCardForm acf = new AnswerCardForm();
                acf.Finish();
                this.Close();
            }
        }

        private void TestingForm_Load(object sender, EventArgs e)
        {
            tmrTestingTime.Start();
            GetQuestionDetail();
            CheckAnswer();
            btnNextClick();            
        }

        private void rdoAnswer_Click(object sender, EventArgs e)
        {
            questionBLL = new QuestionBLL();
            QuestionHelp.StudentAnswer[index] = ((RadioButton)sender).Tag.ToString();
            if (flowLayoutPanel1.Controls.Count >0)
            {
                flowLayoutPanel1.Controls[index].Text = (index + 1).ToString()+"、" + QuestionHelp.StudentAnswer[index];
           }
   
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (index < QuestionHelp.SelectQuestionId.Length - 1)
            {
                index++;
                GetQuestionDetail();
                CheckAnswer();
                btnNextClick();
            }
            else
            {
                DialogResult remindBox = MessageBox.Show("交卷后您将无法修改答案，确认交卷吗？", "提示", MessageBoxButtons.OKCancel);
                if (remindBox == DialogResult.OK)
                {
                    AnswerCardForm acf = new AnswerCardForm();
                    acf.Finish();
                    this.Close();
                }
                
            }
        }
        private bool onoff = true;
        private void btnAnswerCard_Click(object sender, EventArgs e)
        {
            //AnswerCardForm acf = new AnswerCardForm();
            //acf.testpaperId = QuestionHelp.paperId;
            //acf.MdiParent = this.MdiParent;
            //acf.Show();
            //this.Visible = false;

           // tmrTestingTime.Start();
            if (onoff)
            {
                btnAnswerCard.Text = "隐藏答题卡";
                this.Height = 642;
                flowLayoutPanel1.Height = 225;
                flowLayoutPanel1.Controls.Clear();
                ControlShow();
                foreach (Control item in flowLayoutPanel1.Controls)
                {
                   
                        if (item.Name != "lblTime")
                        {
                          int index1 = Convert.ToInt32(item.Tag);
                            if (index1 != -1)
                            {
                                questionBLL = new QuestionBLL();
                                string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[index1]).QuestionType.ToString();
                                if ((questiontype == "简答题" || questiontype == "论述题") && QuestionHelp.StudentAnswer[index1] != "未回答")
                                {
                                    item.Text = "已上传";
                                }
                                else
                                {
                                    item.Text = (index1 + 1).ToString() +"、"+ QuestionHelp.StudentAnswer[index1];
                                }
                            }
                        }
                    
                }
                onoff = false;
            }
            else
            {
                btnAnswerCard.Text = "显示答题卡";
                this.Height = 356;
                flowLayoutPanel1.Height =0;
                onoff = true;
            }
        }

        private void btnUploadAnswer_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "记事本文件(*.txt)|*.txt| Word文件(*.doc)|*.doc| 图片文件(*.jpg)|*.jpg| 图片文件(*.png)|*.png| 图片文件(*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            questionBLL = new QuestionBLL();
            string questionType = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
            if (questionType == TestQuestionType.简答题.ToString() || questionType == TestQuestionType.论述题.ToString())
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    lblaAnswerLocation.Text = Path.GetFileName(openFileDialog1.FileName);
                    AnswerLocation = openFileDialog1.FileName;
                    QuestionHelp.StudentAnswer[index] = lblaAnswerLocation.Text;
                    QuestionHelp.AnswerFile[index] = AnswerLocation;
                }
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

        private void btnNextClick()
        {
            if (index >= QuestionHelp.AllQuestionId.Length - 1)
            {
                btnNext.Enabled = false;
                btnNext.Text = "最后一题";                
            }
        }

        private void GetQuestionDetail()
        {
            questionBLL = new QuestionBLL();
            questionBLL.Find(QuestionHelp.SelectQuestionId[index]);
            string No = string.Format("{0}、", index + 1);
            lblQuestionType.Text = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
            string question=string.Format("{0}",questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Question);
            string score = string.Format("{0}", questionBLL.Find(QuestionHelp.SelectQuestionId[index]).Score.ToString());
            lblQuestion.Text = string.Format("{0} {1}  本小题{2}分",No,question,score);
            
            if (lblQuestionType.Text == TestQuestionType.判断题.ToString())
            {
                this.Height = 283;
                rdoAnswerA.Text = "A." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerA;
                rdoAnswerB.Text = "B." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerB;
                rdoAnswerC.Visible = false;
                rdoAnswerD.Visible = false;
                btnAnswerCard.Location = new Point(356, 282);
                btnNext.Location = new Point(445, 282);
                btnFinsh.Location = new Point(534, 282);
                btnUploadAnswer.Visible = false;
                lblaAnswerLocation.Visible = false;
            }
            else if (lblQuestionType.Text == TestQuestionType.简答题.ToString() || lblQuestionType.Text == TestQuestionType.论述题.ToString())
            {
                this.Height = 283;
                rdoAnswerA.Visible = false;
                rdoAnswerB.Visible = false;
                rdoAnswerC.Visible = false;
                rdoAnswerD.Visible = false;
                btnAnswerCard.Location = new Point(356, 282);
                btnNext.Location = new Point(445, 282);
                btnFinsh.Location = new Point(534, 282);
                btnUploadAnswer.Location = new Point(40, 177);
                lblaAnswerLocation.Location = new Point(141, 177);
                btnUploadAnswer.Visible = true;
                lblaAnswerLocation.Visible = true;
                lblaAnswerLocation.Text = QuestionHelp.StudentAnswer[index].ToString();
            }
            else
            {
                //this.Height = 358;
                btnUploadAnswer.Visible = false;
                lblaAnswerLocation.Visible = false;
                lblaAnswerLocation.Text = "";
                rdoAnswerA.Visible = true;
                rdoAnswerB.Visible = true;
                rdoAnswerC.Visible = true;
                rdoAnswerD.Visible = true;
                btnAnswerCard.Location = new Point(356, 282);
                btnNext.Location = new Point(445, 282);
                btnFinsh.Location = new Point(534, 282);
                rdoAnswerA.Text = "A." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerA;
                rdoAnswerB.Text = "B." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerB;
                rdoAnswerC.Text = "C." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerC;
                rdoAnswerD.Text = "D." + questionBLL.Find(QuestionHelp.SelectQuestionId[index]).AnswerD;
            }
           
        }

        private int count;
       
        private void ControlShow()
        {
            questionBLL = new QuestionBLL();
            int btnIndex = 0;
            
            int btnNumber = 0;
            int btnRow = 0;
            int btnNo = 1;
            //int lblIndex = 0;
            //int lblNumber = 0;
            //int lblRow = 0;
            count = questionBLL.Question(testpaperId).Count;
            if (count <= 30)
            {
               // this.Size = new Size(561, 390);
                lblTime.Location = new Point(389, 9);
                //btnFinish.Location = new Point(414, 317);
                //btnReturn.Location = new Point(469, 317);
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
                    button.Text = btnNo.ToString() ;
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
               // this.Size = new Size(1080, 540);
                lblTime.Location = new Point(908, 9);
                //btnFinish.Location = new Point(933, 467);
                //btnReturn.Location = new Point(988, 467);
                foreach (var item in questionBLL.Question(testpaperId))
                {
                    Button button = new Button();
                    if (btnNumber % 10 == 0 && btnNumber != 0)
                    {
                        btnRow++;
                    }
                    button.Name = item.TestQuestionId.ToString();
                   // button.Size = new Size(35, 23);
                    button.Tag = btnIndex ;
                    button.Text = btnNo.ToString() + QuestionHelp.StudentAnswer[index];
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
           // button.Text = btnNo.ToString() + QuestionHelp.StudentAnswer[index];
            //tmrTestingTime.Start();
            GetQuestionDetail();
            CheckAnswer();
            btnNextClick();   

            //spdf.index = index;
            //FreshData ads = new FreshData(spdf.Testload);
            //ads(index);
            //spdf.Show();

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
                if ((questiontype == "简答题" || questiontype == "论述题") && StudentAnswer != "未回答")
                {
                    var stuff = AnswerFile.Split('.');
                    File.Copy(AnswerFile, AnswerFilePath + "\\" + "第" + index + "题" + "." + stuff[1], true);
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

        private void btnFinsh_Click(object sender, EventArgs e)
        {
            DialogResult remindBox = MessageBox.Show("交卷后您将无法修改答案，确认交卷吗？", "提示", MessageBoxButtons.OKCancel);
            if (remindBox == DialogResult.OK)
            {
                Finish();
                this.Close();
            }
        }
    }
}
