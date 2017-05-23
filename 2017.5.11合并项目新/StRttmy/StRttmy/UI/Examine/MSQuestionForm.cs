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
    public partial class MSQuestionForm : Form
    {
        public TestPapersListForm tplf = null;
        public TestQuestionsListForm tqlf = null;
        public Guid testpaperId;
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private delegate void FreshData();
        private QuestionBLL questionBLL = null;
        private PaperBLL paperBll = null;
        DataGridViewCheckBoxColumn dgvCheckBoxs = new DataGridViewCheckBoxColumn();

        public MSQuestionForm()
        {
            InitializeComponent();
        }

        private void ChoseQuestionForm_Load(object sender, EventArgs e)
        {
            #region 试题类型ComboBox
            cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
            cmbQuestionType.DisplayMember = "Text";
            cmbQuestionType.ValueMember = "Value";
            #endregion

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

            ShowQuestionList();
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

        private void cmbSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void cmbTypeofWork_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void cmbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void cmbQuestionType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void dgvQuestionList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvQuestionList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvQuestionList.RowHeadersDefaultCellStyle.Font, rectangle, dgvQuestionList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void dgvQuestionList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {

                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvQuestionList.Rows[e.RowIndex].Cells[0];
                bool flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    checkCell.Value = false;
                }
                else
                {
                    checkCell.Value = true;
                }

            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            paperBll = new PaperBLL();
            List<ExamPaper> epList = new List<ExamPaper>();
            int selectChecked = 0;
            for (int i = 0; i < dgvQuestionList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvQuestionList.Rows[i].Cells[0];
                //bool flag = Convert.ToBoolean(checkCell.Value);
                if (checkCell.Value == null)
                {
                    selectChecked++;
                }
            }
            if (selectChecked >= dgvQuestionList.Rows.Count)
            {
                MessageBox.Show("请选择试题！");
                return;
            }
            foreach (DataGridViewRow item in dgvQuestionList.Rows)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)item.Cells[0];
                bool flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    ExamPaper newexampaper = new ExamPaper();
                    newexampaper.ExamPaperId = Guid.NewGuid();
                    newexampaper.TestPaperId = testpaperId;
                    newexampaper.TestQuestionId = new Guid(item.Cells[1].Value.ToString());
                    epList.Add(newexampaper);
                }
            }
            if (paperBll.MSAddExamPaper(epList))
            {
                MessageBox.Show("试题添加成功！");
                ShowQuestionList();
            }
            else
            {
                MessageBox.Show("试题添加失败！");
            }
        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            SetAllRowChecked();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowQuestionList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvQuestionList.DataSource = null;
                dgvQuestionList.Columns.Clear();
                questionBLL = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                int questiontype = Convert.ToInt32(cmbQuestionType.SelectedValue.ToString());
                questionFindList = questionBLL.SearchPaperQuestion(keyword, sysid, workid, genreid, levelid, subjectid, questiontype, testpaperId);
                dgvQuestionList.DataSource = questionFindList;
                //隔行变色
                dgvQuestionList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvQuestionList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv();
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
            dgvQuestionList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvQuestionList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvQuestionList.Columns[0].Width = 40;
            dgvQuestionList.Columns[1].HeaderText = "试题";
            dgvQuestionList.Columns[1].Width = 100;
            dgvQuestionList.Columns[2].HeaderText = "试题类型";
            dgvQuestionList.Columns[2].Width = 80;
            dgvQuestionList.Columns["TestQuestionId"].Visible = false;
            dgvQuestionList.Columns["StTypeId"].Visible = false;
            dgvQuestionList.Columns["StType"].Visible = false;
            dgvQuestionList.Columns["StTypeSupplyId"].Visible = false;
            dgvQuestionList.Columns["StLevelId"].Visible = false;
            dgvQuestionList.Columns["ResourceId"].Visible = false;
            dgvQuestionList.Columns["Correct"].Visible = false;
            //dgvQuestionList.Columns["QuestionType"].Visible = false;
            dgvQuestionList.Columns["AnswerA"].Visible = false;
            dgvQuestionList.Columns["AnswerB"].Visible = false;
            dgvQuestionList.Columns["AnswerC"].Visible = false;
            dgvQuestionList.Columns["AnswerD"].Visible = false;
            dgvQuestionList.Columns["Score"].Visible = false;
            dgvQuestionList.Columns["CreateTime"].Visible = false;
            dgvQuestionList.Columns["UserId"].Visible = false;
            dgvQuestionList.Columns.Insert(0, dgvCheckBoxs);
            dgvCheckBoxs.HeaderText = "选择";
            dgvQuestionList.EnableHeadersVisualStyles = false;
            dgvQuestionList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvQuestionList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void SetAllRowChecked()
        {
            int count = 0;
            if (chkCheckAll.Checked)
            {
                count = dgvQuestionList.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvQuestionList.Rows[i].Cells[0];
                    Boolean flag = Convert.ToBoolean(checkCell.Value);
                    if (flag == false) //查找被选择的数据行
                    {
                        checkCell.Value = true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                count = dgvQuestionList.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvQuestionList.Rows[i].Cells[0];
                    Boolean flag = Convert.ToBoolean(checkCell.Value);
                    if (flag == true) //查找被选择的数据行
                    {
                        checkCell.Value = false;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        private void ShowOtherQuestionList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvQuestionList.DataSource = null;
                dgvQuestionList.Columns.Clear();
                questionBLL = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                int questiontype = Convert.ToInt32(cmbQuestionType.SelectedValue.ToString());
                questionFindList = questionBLL.SearchPaperQuestion(keyword, sysid, workid, genreid, levelid, subjectid, questiontype, testpaperId);
                dgvQuestionList.DataSource = questionFindList;
                //隔行变色
                dgvQuestionList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvQuestionList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
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
        private void cmbQuestionType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(QuestionSelectItem.TypeSelectList[e.Index].Text, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(QuestionSelectItem.TypeSelectList[e.Index].Text, cmbQuestionType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbQuestionType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbQuestionType);
        }
    }
}
