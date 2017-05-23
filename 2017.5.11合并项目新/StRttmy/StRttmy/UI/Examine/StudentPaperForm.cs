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
    public partial class StudentPaperForm : Form
    {
        public TestPapersListForm tplf = null;
        public TestQuestionsListForm tqlf = null;
        public Guid testpaperId;
        private ComboBoxShow comboxShow = null;
        public List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private delegate void FreshData();
        private PaperBLL paperBLL = null;
        private QuestionBLL questionBLL = null;
        private int count = 0;

        public StudentPaperForm()
        {
            InitializeComponent();
        }

        private void StudentPaperForm_Load(object sender, EventArgs e)
        {
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
            #region 考试状态ComboBox
            cmbTestState.DataSource = TestStateSelectItem.TypeSelectList;
            cmbTestState.DisplayMember = "Text";
            cmbTestState.ValueMember = "Value";
            #endregion
            ShowStudentPapersList();
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

        private void cmbSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowStudentPapersList();
        }

        private void cmbTypeofWork_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowStudentPapersList();
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowStudentPapersList();
        }

        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowStudentPapersList();
        }

        private void cmbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowStudentPapersList();
        }

        private void cmbTestState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowStudentPapersList();
        }

        private void dgvStudentPaperList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvStudentPaperList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvStudentPaperList.RowHeadersDefaultCellStyle.Font, rectangle, dgvStudentPaperList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        public void ShowStudentPapersList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvStudentPaperList.DataSource = null;
                dgvStudentPaperList.Columns.Clear();
                paperBLL = new PaperBLL();
                IList<StudentPaperListShowClass> studentpaperFindList = new List<StudentPaperListShowClass>();
                Guid userid = Program.mf.LoginUser.UserId;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                int teststate = Convert.ToInt32(cmbTestState.SelectedValue);
                studentpaperFindList = paperBLL.SearchStudentPaper(sysid, workid, genreid, levelid, subjectid, userid, teststate);
                if (studentpaperFindList == null)
                {
                    dgvStudentPaperList.DataSource = null;                    
                    Label lblmessage = new Label();
                    lblmessage.Text = "没有试卷信息！";
                    lblmessage.AutoSize = true;
                    lblmessage.Font = new System.Drawing.Font("黑体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    dgvStudentPaperList.Controls.Add(lblmessage);
                    lblmessage.Location = new Point(dgvStudentPaperList.Width / 2 - lblmessage.Width / 2, dgvStudentPaperList.Height / 2 - lblmessage.Height / 2);
                    dgvStudentPaperList.Controls.Add(lblmessage);
                }
                else
                {                      
                    dgvStudentPaperList.DataSource = studentpaperFindList;
                    SetDgv();
                }
                
                //隔行变色
                dgvStudentPaperList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvStudentPaperList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        private void SetDgv()
        {
            dgvStudentPaperList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStudentPaperList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvStudentPaperList.Columns[0].HeaderText = "学生试卷Id";
            dgvStudentPaperList.Columns[1].HeaderText = "试卷名称";
            dgvStudentPaperList.Columns[4].HeaderText = "考试时间(分钟)";
            dgvStudentPaperList.Columns[5].HeaderText = "考试状态";
            dgvStudentPaperList.Columns[6].HeaderText = "试卷总分";
            dgvStudentPaperList.Columns["StudentExamPaperId"].Visible = false;
            dgvStudentPaperList.Columns["ClassName"].Visible = false;
            dgvStudentPaperList.Columns["Name"].Visible = false;
            dgvStudentPaperList.Columns["score"].Visible = false;
            dgvStudentPaperList.EnableHeadersVisualStyles = false;
            dgvStudentPaperList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvStudentPaperList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        public void SetAllQuestionId(Guid paperId)
        {
            int i = 0;
            int index = 0;
            questionBLL = new QuestionBLL();
            foreach (var item in questionBLL.Question(paperId))
            {
                QuestionHelp.AllQuestionId[i] = (Guid)item.TestQuestionId;
                QuestionHelp.SelectQuestionId[index] = QuestionHelp.AllQuestionId[i];
                index++;
                i++;
            }
        }

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

        private void cmbTestState_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(TestStateSelectItem.TypeSelectList[e.Index].Text, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(TestStateSelectItem.TypeSelectList[e.Index].Text, cmbTestState, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbTestState_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbTestState);
        }

        private void dgvStudentPaperList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                questionBLL = new QuestionBLL();
                testpaperId = paperBLL.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestPaperId;
                TestStateType state = paperBLL.SearchState(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestState;
                if (state == TestStateType.未考)
                {
                    NewTestingForm ntf = new NewTestingForm();
                    ntf.testpaperId = paperBLL.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestPaperId;
                    ntf.testtime = questionBLL.TestTime(testpaperId) * 60;
                    ntf.studentid = Program.mf.LoginUser.UserId;
                    ntf.judge = 1;
                    ntf.spf = this;
                    ntf.ShowDialog();
                }
                if (state == TestStateType.未批卷)
                {
                    NewTestingForm ntf = new NewTestingForm();
                    ntf.testpaperId = paperBLL.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestPaperId;
                    ntf.studentid = Program.mf.LoginUser.UserId;
                    ntf.judge = 2;
                    ntf.ShowDialog();
                }
                if (state == TestStateType.已批卷)
                {
                    NewTestingForm ntf = new NewTestingForm();
                    ntf.testpaperId = paperBLL.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestPaperId;
                    ntf.studentid = Program.mf.LoginUser.UserId;
                    ntf.judge = 4;
                    ntf.ShowDialog();
                }
            }
        }
    }
}
