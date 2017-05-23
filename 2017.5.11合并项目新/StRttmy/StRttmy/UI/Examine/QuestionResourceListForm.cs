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
    public partial class QuestionResourceListForm : Form
    {
        private ResourceBLL rb = null;
        private QuestionBLL qusetionBll = null;
        DataGridViewCheckBoxColumn dgvResourceCheckBoxs = new DataGridViewCheckBoxColumn();
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        public Guid testquestionId = Guid.Empty;
        public int PARM = 0X00;

        public QuestionResourceListForm()
        {
            InitializeComponent();
        }

        private void QuestionResourceListForm_Load(object sender, EventArgs e)
        {
            if (PARM == 1 || PARM == 2 || PARM == 3)
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
                ShowResourceList();
            }
            if (PARM == 0 || PARM == 4 || PARM == 5)
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
                ShowBindingResourceList();
                qusetionBll = new QuestionBLL();
                var bindings = qusetionBll.BindingResource(testquestionId);
                for (int i = 0; i < dgvResourceList.Rows.Count; i++)
                {
                    foreach (var item in bindings)
                    {
                        Guid dgvresourceId = (Guid)dgvResourceList.Rows[i].Cells["ResourceId"].Value;
                        if (dgvresourceId == item.ResourceId)
                        {
                            dgvResourceList.Rows[i].Cells[0].Value = 1;
                        }
                    }
                }
            }
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
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                ShowBindingResourceList();
            }
            if (PARM == 1 || PARM == 2 || PARM == 3)
            {
                ShowResourceList();
            }
        }

        private void cmbSystem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                ShowBindingResourceList();
            }
            if (PARM == 1 || PARM == 2 || PARM == 3)
            {
                ShowResourceList();
            }
        }

        private void cmbTypeofWork_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                ShowBindingResourceList();
            }
            if (PARM == 1 || PARM == 2 || PARM == 3)
            {
                ShowResourceList();
            }
        }

        private void cmbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                ShowBindingResourceList();
            }
            if (PARM == 1 || PARM == 2 || PARM == 3)
            {
                ShowResourceList();
            }
        }

        private void cmbGenre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                ShowBindingResourceList();
            }
            if (PARM == 1 || PARM == 2 || PARM == 3)
            {
                ShowResourceList();
            }
        }

        private void cmbLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                ShowBindingResourceList();
            }
            if (PARM == 1 || PARM == 2 || PARM == 3)
            {
                ShowResourceList();
            }
        }

        private void dgvResourceList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvResourceList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvResourceList.RowHeadersDefaultCellStyle.Font, rectangle, dgvResourceList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvResourceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {

                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvResourceList.Rows[e.RowIndex].Cells[0];
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

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            SetAllRowChecked();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void ShowResourceList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvResourceList.DataSource = null;
                dgvResourceList.Columns.Clear();
                rb = new ResourceBLL();
                IList<Resource> resourceFindList = new List<Resource>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                resourceFindList = rb.ResourceList(keyword, sysid, workid, subjectid, genreid, levelid);
                dgvResourceList.DataSource = resourceFindList;
                //隔行变色
                dgvResourceList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvResourceList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                ResourceSetDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        private void ShowOtherResourceList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvResourceList.DataSource = null;
                dgvResourceList.Columns.Clear();
                qusetionBll = new QuestionBLL();
                IList<Resource> resourceOtherList = new List<Resource>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                resourceOtherList = qusetionBll.otherResource(testquestionId, keyword, sysid, workid, subjectid, genreid, levelid);
                dgvResourceList.DataSource = resourceOtherList;
                //隔行变色
                dgvResourceList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvResourceList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                ResourceSetDgv();
            }
        }

        private void ShowBindingResourceList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvResourceList.DataSource = null;
                dgvResourceList.Columns.Clear();
                qusetionBll = new QuestionBLL();
                IList<Resource> resourceOtherList = new List<Resource>();
                string keyword = txtKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                resourceOtherList = qusetionBll.ShowBindingResource(testquestionId, keyword, sysid, workid, subjectid, genreid, levelid);
                dgvResourceList.DataSource = resourceOtherList;
                //隔行变色
                dgvResourceList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvResourceList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                ResourceSetDgv();
            }
        }

        private void ResourceSetDgv()
        {
            dgvResourceList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvResourceList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvResourceList.Columns[0].HeaderText = "素材ID";
            dgvResourceList.Columns[3].HeaderText = "素材标题";
            dgvResourceList.Columns[4].HeaderText = "素材关键字";
            dgvResourceList.Columns["StLevel"].Visible = false;
            dgvResourceList.Columns["StLevelId"].Visible = false;
            dgvResourceList.Columns["StType"].Visible = false;
            dgvResourceList.Columns["StTypeId"].Visible = false;
            dgvResourceList.Columns["StTypeSupply"].Visible = false;
            dgvResourceList.Columns["StTypeSupplyId"].Visible = false;
            dgvResourceList.Columns["ResourceId"].Visible = false;
            dgvResourceList.Columns["Level"].Visible = false;
            dgvResourceList.Columns["ContentFile"].Visible = false;
            dgvResourceList.Columns["TextFile"].Visible = false;
            dgvResourceList.Columns["SoundFile"].Visible = false;
            dgvResourceList.Columns["UserId"].Visible = false;
            dgvResourceList.Columns["User"].Visible = false;
            dgvResourceList.Columns["CreateTime"].Visible = false;
            dgvResourceList.Columns.Insert(0, dgvResourceCheckBoxs);
            dgvResourceCheckBoxs.Width = 35;
            dgvResourceCheckBoxs.HeaderText = "选择";
            dgvResourceList.EnableHeadersVisualStyles = false;
            dgvResourceList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvResourceList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void SetAllRowChecked()
        {
            int count = 0;
            if (chkCheckAll.Checked)
            {
                count = dgvResourceList.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvResourceList.Rows[i].Cells[0];
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
                count = dgvResourceList.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvResourceList.Rows[i].Cells[0];
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

        private void Save()
        {
            #region 修改素材
            if (PARM == 0)
            {
                qusetionBll = new QuestionBLL();
                List<TestQuestionResource> tqrList = new List<TestQuestionResource>();
                int selectChecked = 0;
                for (int i = 0; i < dgvResourceList.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvResourceList.Rows[i].Cells[0];
                    if (checkCell.Value == null)
                    {
                        selectChecked++;
                    }
                }
                if (selectChecked >= dgvResourceList.Rows.Count)
                {
                    MessageBox.Show("请选择素材！");
                    return;
                }
                foreach (DataGridViewRow item in dgvResourceList.Rows)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)item.Cells[0];
                    bool flag = Convert.ToBoolean(checkCell.Value);
                    if (flag == true)
                    {
                        TestQuestionResource newtqr = new TestQuestionResource();
                        newtqr.QuestionResourceId = Guid.NewGuid();
                        newtqr.TestQuestionId = testquestionId;
                        newtqr.ResourceId = new Guid(item.Cells[1].Value.ToString());
                        tqrList.Add(newtqr);
                    }
                }
                if (qusetionBll.UpdateQuestionResource(tqrList, testquestionId, Guid.Empty))
                {
                    MessageBox.Show("素材修改成功！");
                    ShowBindingResourceList();
                    qusetionBll = new QuestionBLL();
                    var bindings = qusetionBll.BindingResource(testquestionId);
                    for (int i = 0; i < dgvResourceList.Rows.Count; i++)
                    {
                        foreach (var item in bindings)
                        {
                            Guid dgvresourceId = (Guid)dgvResourceList.Rows[i].Cells["ResourceId"].Value;
                            if (dgvresourceId == item.ResourceId)
                            {
                                dgvResourceList.Rows[i].Cells[0].Value = 1;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("素材修改失败！");
                }
            }
            #endregion
            #region 新增素材
            else if (PARM == 1)
            {
                qusetionBll = new QuestionBLL();
                List<TestQuestionResource> tqrList = new List<TestQuestionResource>();
                int selectChecked = 0;
                for (int i = 0; i < dgvResourceList.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvResourceList.Rows[i].Cells[0];
                    //bool flag = Convert.ToBoolean(checkCell.Value);
                    if (checkCell.Value == null)
                    {
                        selectChecked++;
                    }
                }
                if (selectChecked >= dgvResourceList.Rows.Count)
                {
                    MessageBox.Show("请选择素材！");
                    return;
                }
                foreach (DataGridViewRow item in dgvResourceList.Rows)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)item.Cells[0];
                    bool flag = Convert.ToBoolean(checkCell.Value);
                    if (flag == true)
                    {
                        TestQuestionResource newtqr = new TestQuestionResource();
                        newtqr.QuestionResourceId = Guid.NewGuid();
                        newtqr.TestQuestionId = testquestionId;
                        newtqr.ResourceId = new Guid(item.Cells[1].Value.ToString());
                        tqrList.Add(newtqr);
                    }
                }
                if (qusetionBll.AddQuestionResource(tqrList))
                {
                    MessageBox.Show("素材添加成功！");
                    ShowOtherResourceList();
                }
                else
                {
                    MessageBox.Show("素材添加失败！");
                }
            }
            #endregion
        }

        #region 下拉列表显示悬浮提示框
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

        private void dgvResourceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UI.DetailResourceContentForm drcf = new DetailResourceContentForm();//查看内容
            drcf.resourceId = (Guid)dgvResourceList.Rows[e.RowIndex].Cells["ResourceId"].Value;
            drcf.Show();
        }
    }
}
