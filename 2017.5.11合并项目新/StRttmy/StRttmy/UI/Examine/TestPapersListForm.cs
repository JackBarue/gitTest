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
using StRttmy.BLL;
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class TestPapersListForm : Form
    {
        private PaperBLL paperBll = null;
        private ComboBoxShow comboxShow = null;
        public List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        public List<StTypeSupply> typesupplyList = null;
        public List<StLevel> leveltypeList = null;
        private Guid testpaperId = Guid.Empty;

        public TestPapersListForm()
        {
            InitializeComponent();
        }

        private void TestPapersListForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7, 72, 110);
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

            ShowPapersList();
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
        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void cmbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void cmbSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void cmbTypeofWork_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void dgvPapersList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvPapersList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvPapersList.RowHeadersDefaultCellStyle.Font, rectangle, dgvPapersList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPaperForm AddPaperWindows = new AddPaperForm();
            AddPaperWindows.tplf = this;
            AddPaperWindows.ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowPapersList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvPapersList.DataSource = null;
                dgvPapersList.Columns.Clear();
                paperBll = new PaperBLL();
                IList<TestPaper> paperFindList = new List<TestPaper>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                paperFindList = paperBll.SearchPaper(keyword, sysid, workid, genreid, levelid, subjectid);
                dgvPapersList.DataSource = paperFindList;
                //隔行变色
                dgvPapersList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvPapersList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
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

        public void SetDgv()
        {
            dgvPapersList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPapersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvPapersList.Columns[0].HeaderText = "试卷Id";
            dgvPapersList.Columns[1].HeaderText = "试卷名称";
            dgvPapersList.Columns[1].Width = 300;
            dgvPapersList.Columns[7].HeaderText = "考试时间(分钟)";
            // dgvPapersList.Columns[7].Width = 400;
            dgvPapersList.Columns[8].HeaderText = "创建时间";
            //dgvPapersList.Columns[8].Width = 400;
            dgvPapersList.Columns["TestPaperId"].Visible = false;
            dgvPapersList.Columns["StTypeId"].Visible = false;
            dgvPapersList.Columns["StTypeSupplyId"].Visible = false;
            dgvPapersList.Columns["StLevelId"].Visible = false;
            dgvPapersList.Columns["User"].Visible = false;
            dgvPapersList.Columns["UserId"].Visible = false;
            //dgvPapersList.Columns["ExamPapers"].Visible = false;
            dgvPapersList.EnableHeadersVisualStyles = false;
            dgvPapersList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvPapersList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void DelPaper(Guid paperid)
        {
            DialogResult remindBox = MessageBox.Show("确实要删除此试卷吗？", "删除试卷", MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if (remindBox == DialogResult.OK)
            {
                paperBll = new PaperBLL();
                if (!paperBll.CheckClassExamPaper(paperid))
                {
                    if (paperBll.DeletePaper(paperid))
                    {
                        MessageBox.Show("试卷删除成功！");
                        ShowPapersList();
                    }
                    else
                    {
                        MessageBox.Show("试卷删除失败！");
                    }
                }
                else
                {
                    MessageBox.Show("操作无法完成，因为试卷已被使用，不允许删除！", "试卷已被使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        #region 下拉列表显示悬浮框
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

        private void dgvPapersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Guid paperId = new Guid(dgvPapersList.Rows[e.RowIndex].Cells["TestPaperId"].Value.ToString());
                Guid UserId = paperBll.MatchingUser(paperId);
                if (Program.mf.LoginUser.UserId == UserId)//创建者双击可编辑试题
                {
                    NewEditQuestionForm ef = new NewEditQuestionForm();
                    ef.tplf = this;
                    ef.judge = 0;
                    ef.testpaperId = new Guid(dgvPapersList.Rows[e.RowIndex].Cells["TestPaperId"].Value.ToString());
                    ef.ShowDialog();
                }
                else//非创建者双击可查看试题
                {
                    NewEditQuestionForm ef = new NewEditQuestionForm();
                    ef.tplf = this;
                    ef.judge = 1;
                    ef.testpaperId = new Guid(dgvPapersList.Rows[e.RowIndex].Cells["TestPaperId"].Value.ToString());
                    ef.ShowDialog();
                }
            }
        }

        private void dgvPapersList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex != -1)
                {
                    dgvPapersList.ClearSelection();
                    dgvPapersList.Rows[e.RowIndex].Selected = true;
                    testpaperId = new Guid(dgvPapersList.Rows[e.RowIndex].Cells["TestPaperId"].Value.ToString());
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            NewEditQuestionForm ef = new NewEditQuestionForm();
            ef.tplf = this;
            //ef.judge = 1;
            ef.testpaperId = testpaperId;
            Guid UserId = paperBll.MatchingUser(testpaperId);
            if (Program.mf.LoginUser.UserId != UserId)
            {
                MessageBox.Show("操作无法完成，非本人创建的试卷不允许修改！", "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ef.ShowDialog();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            Guid UserId = paperBll.MatchingUser(testpaperId);
            if (Program.mf.LoginUser.UserId == UserId)
            {
                DelPaper(testpaperId);
            }
            else
            {
                MessageBox.Show("操作无法完成，非本人创建的试卷不允许删除！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void tsmiDerived_Click(object sender, EventArgs e)
        {
            AddPaperForm apf = new AddPaperForm();
            apf.PARM = 2;
            apf.tplf = this;
            apf.paperid = testpaperId;
            apf.ShowDialog();
        }
    }
}
