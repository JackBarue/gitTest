using StRttmy.BLL;
using StRttmy.Model;
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
    public partial class NewEditQuestionForm : Form
    {
        public TestPapersListForm tplf;
        public Guid testpaperId;
        public Guid testquestionId;
        public int judge;
        private QuestionBLL questionBll = null;
        private PaperBLL paperBll = null;
        private int paperinfo;
        private delegate void FreshData();

        public NewEditQuestionForm()
        {
            InitializeComponent();
        }

        private void NewEditQuestionForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();            
            ShowQIPList();
        }

        public void ShowQIPList()
        {
            if (Program.mf.LoginUser != null)
            {
                panel1.Controls.Clear();
                flowLayoutPanel1.Controls.Clear();
                this.flowLayoutPanel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseWheel);
                panel1.ContextMenuStrip = contextMenuStrip1;

                questionBll = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                questionFindList = questionBll.QuestioninPaperList(testpaperId, 0);
                paperBll = new PaperBLL();
                string papaerName = paperBll.GetPaper(testpaperId).TestName;
                double paperscore = (double)questionFindList.Sum(c => c.Score);
                int testtime = paperBll.GetPaper(testpaperId).TestTime;                

                Label lblpapername = new Label();
                lblpapername.Name = "lblpapername";
                //lblpapername.BorderStyle = BorderStyle.FixedSingle;
                lblpapername.ContextMenuStrip = contextMenuStrip1;
                lblpapername.AutoSize = true;
                lblpapername.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lblpapername.Location = new Point(panel1.Width / 2 - lblpapername.Width / 2, panel1.Height / 2 - lblpapername.Height / 2);
                lblpapername.Text = papaerName;

                Label lblpsntt = new Label();
                lblpsntt.Name = "lblpsntt";
                //lblpsntt.BorderStyle = BorderStyle.FixedSingle;
                lblpsntt.ContextMenuStrip = contextMenuStrip1;
                lblpsntt.AutoSize = true;
                lblpsntt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                int lblpsnttX = lblpapername.Location.X + lblpapername.Width + 45;
                int lblpsnttY = Convert.ToInt32(lblpapername.Height / 1.5);
                lblpsntt.Location = new Point(lblpsnttX, lblpsnttY);
                lblpsntt.Text = string.Format(@"满分：{0}分  考试时间：{1}分钟", paperscore, testtime);
                panel1.Controls.Add(lblpapername);
                panel1.Controls.Add(lblpsntt);

                int questionno = 1;
                foreach (var item in questionFindList)
                {
                    Panel panel = new Panel();
                    panel.Name = "panel";
                    panel.ContextMenuStrip = contextMenuStrip1;
                    panel.Tag = item.TestQuestionId;


                    Label question = new Label();
                    question.ContextMenuStrip = contextMenuStrip1;
                    question.Tag = item.TestQuestionId;
                    //panel.BorderStyle = BorderStyle.FixedSingle;
                    if (item.Question.Length > 84)
                    {
                        panel.Height = 100;
                        question.Height = 50;
                    }
                    else
                    {
                        panel.Height = 75;
                    }
                    panel.Width = 980;
                    Label questiontype = new Label();
                    questiontype.AutoSize = true;
                    questiontype.ContextMenuStrip = contextMenuStrip1;
                    questiontype.Tag = item.TestQuestionId;
                    testquestionId = item.TestQuestionId;
                    //questiontype.BorderStyle = BorderStyle.FixedSingle;
                    questiontype.Text = "【" + item.QuestionType.ToString() + "】";
                    questiontype.Location = new Point(0, 1);
                    panel.Controls.Add(questiontype);

                    if (judge == 0) //【试卷管理】试卷创建者双击可编辑试题
                    {
                        CheckBox checkquestion = new CheckBox();
                        checkquestion.Width = 14;
                        checkquestion.Tag = item.TestQuestionId;
                        checkquestion.Location = new Point(0, questiontype.Height);
                        panel.Controls.Add(checkquestion);

                        question.Width = panel.Width - 20;
                        //question.BorderStyle = BorderStyle.FixedSingle;
                        question.AutoSize = false;
                        if (item.QuestionType.ToString() == "单选题" || item.QuestionType.ToString() == "判断题")
                        {
                            question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score + "  " + "正确答案：" + item.Correct;
                        }
                        else
                        {
                            question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score;
                        }
                        question.Location = new Point(checkquestion.Location.X + checkquestion.Width + 2, questiontype.Height + 5);
                        panel.Controls.Add(question);
                    }
                    else //【试卷管理】非试卷创建者双击可查看试题
                    {
                        question.Width = panel.Width - 20;
                        //question.BorderStyle = BorderStyle.FixedSingle;
                        question.AutoSize = false;
                        if (item.QuestionType.ToString() == "单选题" || item.QuestionType.ToString() == "判断题")
                        {
                            question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score + "  " + "正确答案：" + item.Correct;
                        }
                        else
                        {
                            question.Text = questionno.ToString() + "、" + item.Question + "   " + "本题分数：" + item.Score;
                        }
                        question.Location = new Point(0, questiontype.Height + 5);
                        panel.Controls.Add(question);
                    }

                    if (item.QuestionType.ToString() == "单选题")
                    {
                        Label answerA = new Label();
                        answerA.ContextMenuStrip = contextMenuStrip1;
                        answerA.Tag = item.TestQuestionId;
                        answerA.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerA.Location = new Point(18, question.Height + 24);
                        answerA.Text = "A." + item.AnswerA;
                        //answerA.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerA);

                        Label answerB = new Label();
                        answerB.ContextMenuStrip = contextMenuStrip1;
                        answerB.Tag = item.TestQuestionId;
                        answerB.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerB.Location = new Point(answerA.Location.X + answerA.Width + 16, question.Height + 24);
                        answerB.Text = "B." + item.AnswerB;
                        //answerB.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerB);

                        Label answerC = new Label();
                        answerC.ContextMenuStrip = contextMenuStrip1;
                        answerC.Tag = item.TestQuestionId;
                        answerC.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerC.Location = new Point(answerB.Location.X + answerB.Width + 16, question.Height + 24);
                        answerC.Text = "C." + item.AnswerC;
                        //answerC.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerC);
                        if (!string.IsNullOrEmpty(item.AnswerD))
                        {
                            Label answerD = new Label();
                            answerD.ContextMenuStrip = contextMenuStrip1;
                            answerD.Tag = item.TestQuestionId;
                            answerD.Width = flowLayoutPanel1.Width / 4 - 116;
                            answerD.Location = new Point(answerC.Location.X + answerC.Width + 16, question.Height + 24);
                            answerD.Text = "D." + item.AnswerD;
                            panel.Controls.Add(answerD);
                        }
                    }
                    if (item.QuestionType.ToString() == "判断题")
                    {
                        Label answerA = new Label();
                        answerA.ContextMenuStrip = contextMenuStrip1;
                        answerA.Tag = item.TestQuestionId;
                        answerA.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerA.Location = new Point(18, question.Height + 24);
                        answerA.Text = "A." + item.AnswerA;
                        //answerA.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerA);

                        Label answerB = new Label();
                        answerB.ContextMenuStrip = contextMenuStrip1;
                        answerB.Tag = item.TestQuestionId;
                        answerB.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerB.Location = new Point(answerA.Location.X + answerA.Width + 16, question.Height + 24);
                        answerB.Text = "B." + item.AnswerB;
                        //answerB.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerB);
                    }
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

        private void tsmiCheckAll_Click(object sender, EventArgs e)
        {
            if (tsmiCheckAll.Text == "全选")
            {
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c.GetType() == typeof(Panel))
                    {
                        foreach (Control item in c.Controls)
                        {
                            if (item.GetType() == typeof(CheckBox))
                            {
                                CheckBox cb = item as CheckBox;

                                if (!cb.Checked)
                                {
                                    cb.Checked = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                tsmiCheckAll.Text = "取消全选";
                return;
            }
            if (tsmiCheckAll.Text == "取消全选")
            {
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c.GetType() == typeof(Panel))
                    {
                        foreach (Control item in c.Controls)
                        {
                            if (item.GetType() == typeof(CheckBox))
                            {
                                CheckBox cb = item as CheckBox;

                                if (!cb.Checked)
                                {
                                    continue;
                                }
                                else
                                {
                                    cb.Checked = false;
                                }
                            }
                        }
                    }
                }
                tsmiCheckAll.Text = "全选";
                return;
            }
        }

        private void tsmiAddNewQuestion_Click(object sender, EventArgs e)
        {
            AddTestQuestionForm atf = new AddTestQuestionForm();
            atf.PARM = 3;
            atf.neqf = this;
            atf.paperId = testpaperId;
            paperBll = new PaperBLL();
            if (paperBll.CheckClassExamPaper(testpaperId))
            {
                MessageBox.Show("操作无法完成，因为试卷已被使用，不允许新增试题！", "试卷已被使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            atf.ShowDialog();
        }

        private void tsmiAddQuestion_Click(object sender, EventArgs e)
        {
            NewAddPaperQuestionForm npqf = new NewAddPaperQuestionForm();
            npqf.testpaperId = testpaperId;
            npqf.neqf = this;
            paperBll = new PaperBLL();
            if (paperBll.CheckClassExamPaper(testpaperId))
            {
                MessageBox.Show("操作无法完成，因为试卷已被使用，不允许添加试题！", "试卷已被使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            npqf.ShowDialog();
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            if (paperinfo == 1)
            {
                AddPaperForm apf = new AddPaperForm();
                apf.paperid = testpaperId;
                apf.PARM = 3;
                apf.neqf = this;
                apf.ShowDialog();
            }
            else
            {
                AddTestQuestionForm atf = new AddTestQuestionForm();
                atf.PARM = 4;
                atf.testquestionId = testquestionId;
                atf.neqf = this;
                atf.ShowDialog();
            }            
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string sourceControlname = (sender as ContextMenuStrip).SourceControl.Name;
            if (sourceControlname == "panel1" || sourceControlname == "lblpapername" || sourceControlname == "lblpsntt")
            {
                tsmiCheckAll.Visible = false;
                tsmiAddNewQuestion.Visible = false;
                tsmiAddQuestion.Visible = false;
                tsmiDelete.Visible = false;
                tsmiUpdate.Text = "修改试卷信息";
                paperinfo = 1;
            }
            else
            {
                tsmiCheckAll.Visible = true;
                tsmiAddNewQuestion.Visible = true;
                tsmiAddQuestion.Visible = true;
                tsmiDelete.Visible = true;
                tsmiUpdate.Text = "修改试题";
                paperinfo = 0;
                testquestionId = (Guid)(sender as ContextMenuStrip).SourceControl.Tag;
                if (judge == 1)
                {
                    e.Cancel = true;
                }
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DialogResult reminBox = MessageBox.Show("确实要删除试题吗？", "删除试题", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (reminBox == DialogResult.OK)
            {
                paperBll = new PaperBLL();
                List<ExamPaper> epList = new List<ExamPaper>();
                int count = 0;
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c.GetType() == typeof(Panel))
                    {
                        foreach (Control item in c.Controls)
                        {
                            if (item.GetType() == typeof(CheckBox))
                            {
                                CheckBox cb = item as CheckBox;
                                if (cb.Checked)
                                {
                                    count++;
                                    ExamPaper newep = new ExamPaper();
                                    newep.TestPaperId = testpaperId;
                                    newep.TestQuestionId = new Guid(cb.Tag.ToString());
                                    if (!paperBll.CheckClassExamPaper(newep.TestPaperId))
                                    {
                                        epList.Add(newep);
                                    }
                                }
                            }
                        }
                    }
                }
                if (count == 0)
                {
                    ExamPaper newep = new ExamPaper();
                    newep.TestPaperId = testpaperId;
                    newep.TestQuestionId = testquestionId;
                    if (!paperBll.CheckClassExamPaper(newep.TestPaperId))
                    {
                        epList.Add(newep);
                    }
                }
                if (paperBll.DeleteExamQuestion(epList))
                {
                    MessageBox.Show("试题删除成功！");
                    ShowQIPList();
                }
                else
                {
                    MessageBox.Show("操作无法完成，因为试卷已被使用，不允许删除试题！", "试卷已被使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void flowLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
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

        private void NewEditQuestionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            FreshData freshTPlist = new FreshData(tplf.ShowPapersList);
            freshTPlist();
        }
    }
}
