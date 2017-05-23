using StRttmy.BLL;
using StRttmy.Common;
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
    public partial class NewAddPaperQuestionForm : Form
    {
        public NewEditQuestionForm neqf = null;
        public Guid testpaperId;
        private QuestionBLL questionBll = null;
        private PaperBLL paperBll = null;
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private int pageSize = 0;//每页记录数
        private int nMax = 0;//总记录数
        private int pageCount = 0;//总页数
        private int pageCurrent = 0;//当前页数
        private Guid sysid = Guid.Empty;
        private Guid workid = Guid.Empty;
        private Guid genreid = Guid.Empty;
        private Guid levelid = Guid.Empty;
        private Guid subjectid = Guid.Empty;
        private delegate void FreshDatas();

        public NewAddPaperQuestionForm()
        {
            InitializeComponent();
        }

        private void NewAddPaperQuestionForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7, 72, 110);
            chkCheckAll.Location = new Point(10, panel3.Height / 2 - chkCheckAll.Height / 2);
            typeList = new List<StType>();
            typesupplyList = new List<StTypeSupply>();
            leveltypeList = new List<StLevel>();
            #region 系统ComboBox
            Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
            comboxShow = new ComboBoxShow();
            typeList = comboxShow.ThreeTypecmbShow(gid);
            cmbSystem.DataSource = typeList;
            cmbSystem.DisplayMember = "Name";
            cmbSystem.ValueMember = "StTypeId";
            #endregion
            #region 类别ComboBox
            comboxShow = new ComboBoxShow();
            typesupplyList = comboxShow.GenrecmbShow();
            cmbGenre.DataSource = typesupplyList;
            cmbGenre.DisplayMember = "StTypeName";
            cmbGenre.ValueMember = "StTypeSupplyId";
            #endregion
            #region 等级ComboBox
            comboxShow = new ComboBoxShow();
            leveltypeList = comboxShow.LevelcmbShow();
            cmbLevel.DataSource = leveltypeList;
            cmbLevel.DisplayMember = "StLevelName";
            cmbLevel.ValueMember = "StLevelId";
            #endregion
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }

        public void ShowQIPList(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent, Guid testpaperId)
        {
            if (Program.mf.LoginUser != null)
            {
                flowLayoutPanel1.Controls.Clear();

                questionBll = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                questionFindList = questionBll.NewSearchPaperQuestion(keyword, sysid, workid, genreid, levelid, subjectid, pageSize, pageCurrent, testpaperId);
                foreach (var item in questionFindList)
                {
                    Panel panel = new Panel();
                    panel.Name = "panel";
                    panel.Tag = item.TestQuestionId;


                    Label question = new Label();
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
                    questiontype.Tag = item.TestQuestionId;
                    //questiontype.BorderStyle = BorderStyle.FixedSingle;
                    questiontype.Text = "【" + item.QuestionType.ToString() + "】";
                    questiontype.Location = new Point(0, 1);
                    panel.Controls.Add(questiontype);

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
                        question.Text = item.Question + "   " + "本题分数：" + item.Score + "  " + "正确答案：" + item.Correct;
                    }
                    else
                    {
                        question.Text = item.Question + "   " + "本题分数：" + item.Score;
                    }
                    question.Location = new Point(checkquestion.Location.X + checkquestion.Width + 2, questiontype.Height + 6);
                    panel.Controls.Add(question);
                    if (item.QuestionType.ToString() == "单选题")
                    {
                        Label answerA = new Label();
                        answerA.Tag = item.TestQuestionId;
                        answerA.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerA.Location = new Point(18, question.Height + 24);
                        answerA.Text = "A." + item.AnswerA;
                        //answerA.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerA);

                        Label answerB = new Label();
                        answerB.Tag = item.TestQuestionId;
                        answerB.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerB.Location = new Point(answerA.Location.X + answerA.Width + 16, question.Height + 24);
                        answerB.Text = "B." + item.AnswerB;
                        //answerB.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerB);

                        Label answerC = new Label();
                        answerC.Tag = item.TestQuestionId;
                        answerC.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerC.Location = new Point(answerB.Location.X + answerB.Width + 16, question.Height + 24);
                        answerC.Text = "C." + item.AnswerC;
                        //answerC.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerC);
                        if (!string.IsNullOrEmpty(item.AnswerD))
                        {
                            Label answerD = new Label();
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
                        answerA.Tag = item.TestQuestionId;
                        answerA.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerA.Location = new Point(18, question.Height + 24);
                        answerA.Text = "A." + item.AnswerA;
                        //answerA.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerA);

                        Label answerB = new Label();
                        answerB.Tag = item.TestQuestionId;
                        answerB.Width = flowLayoutPanel1.Width / 4 - 116;
                        answerB.Location = new Point(answerA.Location.X + answerA.Width + 16, question.Height + 24);
                        answerB.Text = "B." + item.AnswerB;
                        //answerB.BorderStyle = BorderStyle.FixedSingle;
                        panel.Controls.Add(answerB);
                    }           
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        private void Page()
        {
            questionBll = new QuestionBLL();
            nMax = questionBll.paperquestioncount(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), 
                new Guid(cmbGenre.SelectedValue.ToString()), new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), testpaperId);
            if (nMax == 0)
            {
                tslblFirstPage.Enabled = false;
                tslblPreviousPage.Enabled = false;
                tslblNextPage.Enabled = false;
                tslblLastPage.Enabled = false;
                tstxtCurrentPage.Text = "0";
                tslblCountPage.Text = "0";
                return;
            }
            else if (nMax > 0)
            {
                tslblFirstPage.Enabled = false;
                tslblPreviousPage.Enabled = false;
                pageCurrent = 1;
            }
            pageSize = 8;
            int yushu = nMax % pageSize;
            if (yushu == 0)
            {
                if (pageCount > 0 && pageCount <= pageSize)
                {
                    tslblFirstPage.Enabled = false;
                    tslblPreviousPage.Enabled = false;
                    pageCount = 1;
                }
                else
                {
                    tslblNextPage.Enabled = true;
                    tslblLastPage.Enabled = true;
                    pageCount = nMax / pageSize;
                }
            }
            else
            {
                tslblNextPage.Enabled = true;
                tslblLastPage.Enabled = true;
                pageCount = nMax / pageSize + 1;
            }

            tslblCountPage.Text = string.Format(@"共 {0} 页", pageCount.ToString());
            tstxtCurrentPage.Text = pageCurrent.ToString();
        }

        private void cmbSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            StType sysitem = cmbSystem.SelectedItem as StType;
            comboxShow = new ComboBoxShow();
            typeofworkList = new List<StType>();
            typeofworkList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
            cmbTypeofWork.DataSource = typeofworkList;
            cmbTypeofWork.DisplayMember = "Name";
            cmbTypeofWork.ValueMember = "StTypeId";
        }

        private void cmbTypeofWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            StType subitem = cmbTypeofWork.SelectedItem as StType;
            comboxShow = new ComboBoxShow();
            subjecttypeList = new List<StType>();
            subjecttypeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
            cmbSubject.DataSource = subjecttypeList;
            cmbSubject.DisplayMember = "Name";
            cmbSubject.ValueMember = "StTypeId";
        }       

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }

        #region 下拉列表悬浮框
        private void cmbSystem_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(typeList[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(typeList[e.Index].Name, cmbSystem, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbSystem_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbSystem);
        }

        private void cmbTypeofWork_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(typeofworkList[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(typeofworkList[e.Index].Name, cmbTypeofWork, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbTypeofWork_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbTypeofWork);
        }

        private void cmbGenre_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(typesupplyList[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(typesupplyList[e.Index].StTypeName, cmbGenre, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbGenre_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbGenre);
        }

        private void cmbLevel_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(leveltypeList[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(leveltypeList[e.Index].StLevelName, cmbLevel, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbLevel_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbLevel);
        }

        private void cmbSubject_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(subjecttypeList[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(subjecttypeList[e.Index].Name, cmbSubject, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbSubject_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbSubject);
        }
        #endregion

        #region 下拉列表查询方法
        private void cmbSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }

        private void cmbTypeofWork_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }

        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }

        private void cmbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
        }
        #endregion

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckAll.Checked)
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
            }
            else
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
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int checkcount = 0;
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
                                checkcount++;
                            }                            
                        }
                    }
                }
            }
            if (checkcount == 8)
            {
                MessageBox.Show("请选择要添加的试题！","添加试题",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                List<ExamPaper> epList = new List<ExamPaper>();
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
                                    ExamPaper newep = new ExamPaper();
                                    newep.ExamPaperId = Guid.NewGuid();
                                    newep.TestPaperId = testpaperId;
                                    newep.TestQuestionId =new Guid(cb.Tag.ToString());
                                    epList.Add(newep);
                                }
                            }
                        }
                    }
                }
                paperBll = new PaperBLL();
                if (paperBll.MSAddExamPaper(epList))
                {
                    MessageBox.Show("试题添加成功！");
                    ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                }
            }
        }

        private void tslblFirstPage_Click(object sender, EventArgs e)
        {
            if (pageCount > 1)
            {
                pageCurrent = 1;
                tstxtCurrentPage.Text = pageCurrent.ToString();
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                tslblNextPage.Enabled = true;
                tslblLastPage.Enabled = true;
                tslblFirstPage.Enabled = false;
                tslblPreviousPage.Enabled = false;
            }
        }

        private void tslblPreviousPage_Click(object sender, EventArgs e)
        {
            if (pageCurrent - 1 > 1)
            {
                pageCurrent--;
                tstxtCurrentPage.Text = pageCurrent.ToString();
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                tslblNextPage.Enabled = true;
                tslblLastPage.Enabled = true;
            }
            else
            {
                pageCurrent--;
                tstxtCurrentPage.Text = pageCurrent.ToString();
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                tslblFirstPage.Enabled = false;
                tslblPreviousPage.Enabled = false;
            }
        }

        private void tslblNextPage_Click(object sender, EventArgs e)
        {
            if (pageCurrent < pageCount - 1)
            {
                pageCurrent++;
                tstxtCurrentPage.Text = pageCurrent.ToString();
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                tslblFirstPage.Enabled = true;
                tslblPreviousPage.Enabled = true;
            }
            else
            {
                pageCurrent++;
                tstxtCurrentPage.Text = pageCurrent.ToString();
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                tslblNextPage.Enabled = false;
                tslblLastPage.Enabled = false;
            }
        }

        private void tslblLastPage_Click(object sender, EventArgs e)
        {
            if (pageCount > 0)
            {
                pageCurrent = pageCount;
                tstxtCurrentPage.Text = pageCurrent.ToString();
                //tsslblCurrentPage.Text = pageCurrent.ToString();
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                tslblFirstPage.Enabled = true;
                tslblPreviousPage.Enabled = true;
                tslblNextPage.Enabled = false;
                tslblLastPage.Enabled = false;
            }
        }

        private void tstxtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pageCurrent = Convert.ToInt32(tstxtCurrentPage.Text);
                ShowQIPList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent, testpaperId);
                if (pageCurrent == 1)
                {
                    tslblFirstPage.Enabled = false;
                    tslblPreviousPage.Enabled = false;
                    tslblNextPage.Enabled = true;
                    tslblLastPage.Enabled = true;
                }
                else if (pageCurrent == pageCount)
                {
                    tslblFirstPage.Enabled = true;
                    tslblPreviousPage.Enabled = true;
                    tslblNextPage.Enabled = false;
                    tslblLastPage.Enabled = false;
                }
                else
                {
                    tslblFirstPage.Enabled = true;
                    tslblPreviousPage.Enabled = true;
                    tslblNextPage.Enabled = true;
                    tslblLastPage.Enabled = true;
                }
            }
        }
    }
}
