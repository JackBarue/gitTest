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
using StRttmy.Model;
using StRttmy.Common;

namespace StRttmy.UI
{    public partial class TestQuestionsListForm : Form
    {
        private QuestionBLL questionBll = null;
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;       


        public TestQuestionsListForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void CusCourseListForm_Load(object sender, EventArgs e)
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

            ShowQuestionList();
        }

        #region 下拉列表悬浮提示框
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

        //新建试题
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTestQuestionForm AddWindow = new AddTestQuestionForm();
            AddWindow.PARM = 1;
            //AddWindow.ntqlf = this;
            AddWindow.ShowDialog();
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        //显示试题列表
        public void ShowQuestionList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvQuestionList.DataSource = null;
                dgvQuestionList.Columns.Clear();
                questionBll = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                questionFindList = questionBll.SearchQuestion(keyword, sysid, workid, genreid, levelid, subjectid);
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

        //列表表头和操作按钮赋值及属性值
        private void SetDgv()
        {
            dgvQuestionList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvQuestionList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvQuestionList.Columns[0].HeaderText = "试题Id";
            dgvQuestionList.Columns[1].HeaderText = "试题";
            dgvQuestionList.Columns[1].Width = 312;
            dgvQuestionList.Columns[2].HeaderText = "试题类型";
            dgvQuestionList.Columns[10].HeaderText = "答案A";
            dgvQuestionList.Columns[11].HeaderText = "答案B";
            dgvQuestionList.Columns[12].HeaderText = "答案C";
            dgvQuestionList.Columns[13].HeaderText = "答案D";
            dgvQuestionList.Columns[9].HeaderText = "本题分数";
            dgvQuestionList.Columns[14].HeaderText = "正确答案";
            //dgvQuestionList.Columns[15].HeaderText = "创建时间";
            //dgvQuestionList.Columns[15].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dgvQuestionList.Columns["TestQuestionId"].Visible = false;
            dgvQuestionList.Columns["StTypeId"].Visible = false;
            dgvQuestionList.Columns["StType"].Visible = false;
            dgvQuestionList.Columns["StTypeSupplyId"].Visible = false;
            dgvQuestionList.Columns["StLevelId"].Visible = false;
            dgvQuestionList.Columns["ResourceId"].Visible = false;
            dgvQuestionList.Columns["UserId"].Visible = false;
            dgvQuestionList.Columns["CreateTime"].Visible = false;
            dgvQuestionList.EnableHeadersVisualStyles = false;
            dgvQuestionList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvQuestionList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridViewButtonColumn dgvButtonEdit = new DataGridViewButtonColumn();// 设置列名
            dgvButtonEdit.Name = "ManageEdit";
            dgvButtonEdit.HeaderText = "修改试题";// 设置列标题
            dgvButtonEdit.UseColumnTextForButtonValue = true;
            dgvButtonEdit.Text = "修改";// 设置按钮标题
            dgvButtonEdit.Width = 20;
            dgvButtonEdit.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
            DataGridViewButtonColumn dgvButtonDel = new DataGridViewButtonColumn();// 设置列名
            dgvButtonDel.Name = "ManageDel";
            dgvButtonDel.HeaderText = "删除试题";// 设置列标题
            dgvButtonDel.UseColumnTextForButtonValue = true;
            dgvButtonDel.Text = "删除";// 设置按钮标题
            dgvButtonDel.Width = 20;
            dgvButtonDel.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
            // 向DataGridView追加
            dgvQuestionList.Columns.Insert(dgvQuestionList.Columns.Count, dgvButtonEdit);
            dgvQuestionList.Columns.Insert(dgvQuestionList.Columns.Count, dgvButtonDel);
        }

        //添加行号
        private void dgvCourseList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvQuestionList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvQuestionList.RowHeadersDefaultCellStyle.Font, rectangle, dgvQuestionList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //单元格点击事件
        private void dgvCourseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dgvQuestionList.Columns[e.ColumnIndex].Name == "ManageEdit")
                {
                    Guid questionId = new Guid(dgvQuestionList.Rows[e.RowIndex].Cells["TestQuestionId"].Value.ToString());
                    Guid UserId = questionBll.MatchingUser(questionId);
                    if (Program.mf.LoginUser.UserId == UserId)
                    {
                        AddTestQuestionForm AddWindow = new AddTestQuestionForm();
                        AddWindow.PARM = 0;
                        AddWindow.testquestionId = new Guid(dgvQuestionList.Rows[e.RowIndex].Cells["TestQuestionId"].Value.ToString());
                        //AddWindow.tqlf = this;
                        AddWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("操作无法完成，非本人创建的试题不允许修改！", "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (dgvQuestionList.Columns[e.ColumnIndex].Name == "ManageDel")
                {
                    Guid questionId = new Guid(dgvQuestionList.Rows[e.RowIndex].Cells["TestQuestionId"].Value.ToString());
                    Guid UserId = questionBll.MatchingUser(questionId);
                    if (Program.mf.LoginUser.UserId == UserId)
                    {
                        Guid questionid = new Guid(dgvQuestionList.Rows[e.RowIndex].Cells["TestQuestionId"].Value.ToString());//删除需要的id
                        DelQuestion(questionid);
                    }
                    else
                    {
                        MessageBox.Show("操作无法完成，非本人创建的试题不允许删除！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        //删除试题
        private void DelQuestion(Guid questionId)
        {

            DialogResult remindBox = MessageBox.Show("确实要删除此试题吗？", "删除试题", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (remindBox == DialogResult.OK)
            {
                questionBll = new QuestionBLL();
                TestQuestion testquestion = null;

                if (questionBll.DeleteQuestion(questionId, out testquestion))
                {
                    MessageBox.Show("试题删除成功！");
                    ShowQuestionList();//删除后按照之前的查询条件刷新列表
                }
                else
                {
                    MessageBox.Show("操作无法完成，因为试题已被使用，不允许删除！", "试题已被使用", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //系统ComboBox联动工种ComboBox
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

        //工种ComboBox联动科目ComboBox
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
        }

        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQuestionList();
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

        private void dgvQuestionList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvQuestionList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvQuestionList.RowHeadersDefaultCellStyle.Font, rectangle, dgvQuestionList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }        
    }
}
