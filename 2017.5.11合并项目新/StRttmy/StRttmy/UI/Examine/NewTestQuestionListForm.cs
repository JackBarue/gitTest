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
    public partial class NewTestQuestionListForm : Form
    {
        private QuestionBLL questionBll = null;
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private Guid questionId = Guid.Empty;
        private int pageSize = 0;//每页记录数
        private int nMax = 0;//总记录数
        private int pageCount = 0;//总页数
        private int pageCurrent = 0;//当前页数
        //private int nCurrent = 0;//当前记录数
        private Guid sysid = Guid.Empty;
        private Guid workid = Guid.Empty;
        private Guid genreid = Guid.Empty;
        private Guid levelid = Guid.Empty;
        private Guid subjectid = Guid.Empty;

        public NewTestQuestionListForm()
        {
            InitializeComponent();
        }

        private void NewTestQuestionListForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7, 72, 110);
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
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }

        #region 下拉菜单联动
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
        #endregion

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
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }

        private void cmbTypeofWork_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }

        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }

        private void cmbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Page();
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }
        #endregion

        #region 页面显示方法
        public void ShowQuestionList(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent)
        {
            if (Program.mf.LoginUser != null)
            {
                flowLayoutPanel1.Controls.Clear();
                questionBll = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                questionFindList = questionBll.SearchQuestions(keyword, sysid, workid, genreid, levelid, subjectid, pageSize, pageCurrent);
                foreach (TestQuestion item in questionFindList)
                {
                    Panel panel = new Panel();
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
                    questiontype.ContextMenuStrip = contextMenuStrip1;
                    questiontype.Tag = item.TestQuestionId;
                    //questiontype.BorderStyle = BorderStyle.FixedSingle;
                    questiontype.Text = "【" + item.QuestionType.ToString() + "】";
                    questiontype.Location = new Point(0, 1);
                    panel.Controls.Add(questiontype);

                    question.Width = panel.Width;
                    //question.BorderStyle = BorderStyle.FixedSingle;
                    question.AutoSize = false;
                    //questionId = item.TestQuestionId;
                    if (item.QuestionType.ToString() == "单选题" || item.QuestionType.ToString() == "判断题")
                    {
                        question.Text = item.Question + "   " + "本题分数：" + item.Score + "  " + "正确答案：" + item.Correct;
                    }
                    else
                    {
                        question.Text = item.Question + "   " + "本题分数：" + item.Score;
                    }
                    question.Location = new Point(0, questiontype.Height + 1);
                    panel.Controls.Add(question);
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
                    pageCurrent++;
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }
        #endregion

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Page();
            ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
        }

        private void tsslblFirstPage_Click(object sender, EventArgs e)
        {
            if (pageCount > 1)
            {
                pageCurrent = 1;
                tsslblCurrentPage.Text = pageCurrent.ToString();
                ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
                tsslblNextPage.Enabled = true;
                tsslblLastPage.Enabled = true;
                tsslblFirstPage.Enabled = false;
                tsslblPreviousPage.Enabled = false;
            }
        }

        private void tsslblPreviousPage_Click(object sender, EventArgs e)
        {
            if (pageCurrent > 1)
            {
                pageCurrent--;
                tsslblCurrentPage.Text = pageCurrent.ToString();
                ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
                tsslblNextPage.Enabled = true;
                tsslblLastPage.Enabled = true;
            }
            else
            {
                tsslblCurrentPage.Text = pageCurrent.ToString();
                tsslblFirstPage.Enabled = false;
                tsslblPreviousPage.Enabled = false;
            }
        }

        private void tsslblNextPage_Click(object sender, EventArgs e)
        {
            if (pageCurrent < pageCount)
            {
                pageCurrent++;
                tsslblCurrentPage.Text = pageCurrent.ToString();
                ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
                tsslblFirstPage.Enabled = true;
                tsslblPreviousPage.Enabled = true;
            }
            else
            {
                tsslblNextPage.Enabled = false;
                tsslblLastPage.Enabled = false;
            }
        }

        private void tsslblLastPage_Click(object sender, EventArgs e)
        {
            if (pageCount > 0)
            {
                pageCurrent = pageCount;
                tsslblCurrentPage.Text = pageCurrent.ToString();
                ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()), pageSize, pageCurrent);
                tsslblFirstPage.Enabled = true;
                tsslblPreviousPage.Enabled = true;
                tsslblNextPage.Enabled = false;
                tsslblLastPage.Enabled = false;
            }
        }

        private void Page()
        {
            questionBll = new QuestionBLL();
            nMax = questionBll.testquestioncount(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()));
            if (nMax == 0)
            {
                tsslblFirstPage.Enabled = false;
                tsslblPreviousPage.Enabled = false;
                tsslblNextPage.Enabled = false;
                tsslblLastPage.Enabled = false;
                tsslblCurrentPage.Text = "0";
                tsslblCountPage.Text = "0";
                return;
            }
            else if (nMax > 0)
            {
                tsslblFirstPage.Enabled = false;
                tsslblPreviousPage.Enabled = false;
                pageCurrent = 1;
            }
            pageSize = 8;
            int yushu = nMax % pageSize;
            if (yushu == 0)
            {
                if (pageCount > 0 && pageCount <= pageSize)
                {
                    tsslblFirstPage.Enabled = false;
                    tsslblPreviousPage.Enabled = false;
                    pageCount = 1;
                }
                else
                {
                    tsslblNextPage.Enabled = true;
                    tsslblLastPage.Enabled = true;
                    pageCount = nMax / pageSize;
                }
            }
            else
            {
                tsslblNextPage.Enabled = true;
                tsslblLastPage.Enabled = true;
                pageCount = nMax / pageSize + 1;
            }

            tsslblCountPage.Text = pageCount.ToString();
            tsslblCurrentPage.Text = pageCurrent.ToString();
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {            
            Guid UserId = questionBll.MatchingUser(questionId);
            if (Program.mf.LoginUser.UserId == UserId)
            {
                AddTestQuestionForm AddWindow = new AddTestQuestionForm();
                AddWindow.PARM = 0;
                AddWindow.testquestionId = questionId;
                AddWindow.pageCurrent = pageCurrent;
                AddWindow.oldkeyword = txtKeyword.Text;
                AddWindow.oldsysid = new Guid(cmbSystem.SelectedValue.ToString());
                AddWindow.oldworkid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                AddWindow.oldgenreid = new Guid(cmbGenre.SelectedValue.ToString());
                AddWindow.oldlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                AddWindow.oldsubjectid = new Guid(cmbSubject.SelectedValue.ToString());
                AddWindow.ntqlf = this;
                AddWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("操作无法完成，非本人创建的试题不允许修改！", "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            questionId = (Guid)(sender as ContextMenuStrip).SourceControl.Tag;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTestQuestionForm AddWindow = new AddTestQuestionForm();
            AddWindow.PARM = 1;
            AddWindow.ntqlf = this;
            AddWindow.ShowDialog();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            Guid UserId = questionBll.MatchingUser(questionId);
            if (Program.mf.LoginUser.UserId == UserId)
            {
                Guid questionid = questionId;//删除需要的id
                DelQuestion(questionid);
            }
            else
            {
                MessageBox.Show("操作无法完成，非本人创建的试题不允许删除！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        //删除试题
        private void DelQuestion(Guid questionId)
        {
            DialogResult remindBox = MessageBox.Show("确实要删除此试题吗？", "删除试题", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (remindBox == DialogResult.OK)
            {
                questionBll = new QuestionBLL();
                TestQuestion testquestion = null;

                if (questionBll.DeleteQuestion(questionId, out testquestion))
                {
                    MessageBox.Show("试题删除成功！");
                    ShowQuestionList(txtKeyword.Text, new Guid(cmbSystem.SelectedValue.ToString()), new Guid(cmbTypeofWork.SelectedValue.ToString()), new Guid(cmbGenre.SelectedValue.ToString())
                , new Guid(cmbLevel.SelectedValue.ToString()), new Guid(cmbSubject.SelectedValue.ToString()),8,pageCurrent);//删除后按照之前的查询条件刷新列表
                }
                else
                {
                    MessageBox.Show("操作无法完成，因为试题已被使用，不允许删除！", "试题已被使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }      
    }
}
