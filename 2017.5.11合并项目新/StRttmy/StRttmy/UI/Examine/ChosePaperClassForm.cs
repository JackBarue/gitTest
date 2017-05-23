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
    public partial class ChosePaperClassForm : Form
    {
        public Guid testpaperId;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private ComboBoxShow comboxShow = null;
        private delegate void FreshData();       
        private PaperBLL paperBll = null;
        DataGridViewDisableCheckBoxColumn dgvPaperRadioButton = new DataGridViewDisableCheckBoxColumn();
        DataGridViewCheckBoxColumn dgvClassCheckBoxs = new DataGridViewCheckBoxColumn();

        public ChosePaperClassForm()
        {
            InitializeComponent();
        }

        private void ChosePaperClassForm_Load(object sender, EventArgs e)
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
            ShowPapersList();
            ShowClassList();
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

        private void dgvPapersList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvPapersList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvPapersList.RowHeadersDefaultCellStyle.Font, rectangle, dgvPapersList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvClassList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvClassList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvClassList.RowHeadersDefaultCellStyle.Font, rectangle, dgvClassList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgvPapersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                DataGridViewColumn column = dgvPapersList.Columns[e.ColumnIndex];
                if (column is DataGridViewCheckBoxColumn)
                {
                    DataGridViewDisableCheckBoxCell checkcell = dgvPapersList.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewDisableCheckBoxCell;
                    dgvPapersList.Rows[e.ColumnIndex].Cells["checkbox"].Value = SelectedStatus.NoSelected;
                    if (!checkcell.Enabled)
                    {
                        return;
                    }
                    checkcell.Value = SelectedStatus.NoSelected;
                    if ((SelectedStatus)checkcell.Value == SelectedStatus.NoSelected)
                    {
                        checkcell.Value = SelectedStatus.Selected;
                        SetRadioButtonValue(checkcell);

                        dgvClassList.DataSource = null;
                        dgvClassList.Columns.Clear();
                        paperBll = new PaperBLL();
                        IList<StClass> classFindList = new List<StClass>();
                        string keyword = txtClassKeyword.Text;
                        testpaperId = new Guid(dgvPapersList.Rows[e.RowIndex].Cells[1].Value.ToString());
                        classFindList = paperBll.SearchOtherClass(keyword, testpaperId);
                        dgvClassList.DataSource = classFindList;
                        ClassSetDgv();
                    }
                }      
            }
                 
        }

        private void btnPaperQuery_Click(object sender, EventArgs e)
        {
            ShowPapersList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dgvPapersList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkcell = (DataGridViewCheckBoxCell)this.dgvPapersList.Rows[i].Cells[0];
                //Boolean flag = Convert.ToBoolean(checkcell.Value);
                if (checkcell.Value == null)
                {
                    count++;
                }
            }
            if (count >= dgvPapersList.Rows.Count)
            {
                MessageBox.Show("请选择一套试题！");
                return;
            }
            count = 1;
            int dgvClasscount = 1;
            foreach (DataGridViewRow item in dgvClassList.Rows)
            {
                dgvClasscount = dgvClassList.Rows.Count;
                DataGridViewCheckBoxCell checkClassCell = (DataGridViewCheckBoxCell)item.Cells[0];
                //bool flag = Convert.ToBoolean(checkClassCell.Value);
                if (checkClassCell.Value == null)
                {
                    count++;
                }
            }
            if (count > dgvClasscount)
            {
                MessageBox.Show("请至少选择一个班级！");
                return;
            }
            AddClassExampaper();
        }

        private void dgvClassList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //获取控件的值
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)this.dgvClassList.Rows[e.RowIndex].Cells[0];
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

        private void AddClassExampaper()
        {
            List<ClassExamPaper> classpaperList=new List<ClassExamPaper>();
            for (int i = 0; i < dgvPapersList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkcell = (DataGridViewCheckBoxCell)this.dgvPapersList.Rows[i].Cells[0];
                //Boolean paperflag = Convert.ToBoolean(checkcell.Value);
                if ((SelectedStatus)checkcell.Value == SelectedStatus.Selected)
                {                    
                    foreach (DataGridViewRow item in dgvClassList.Rows)
                    {
                        DataGridViewCheckBoxCell checkClassCell = (DataGridViewCheckBoxCell)item.Cells[0];
                        bool classflag = Convert.ToBoolean(checkClassCell.Value);
                        if (classflag == true)
                        {
                            ClassExamPaper classexampaper = new ClassExamPaper();
                            classexampaper.ClassExamPaperId = Guid.NewGuid();
                            classexampaper.TestPaperId = testpaperId;
                            classexampaper.StClassId = new Guid(item.Cells[1].Value.ToString());
                            classpaperList.Add(classexampaper);
                        }
                    }
                }
            }
            if (paperBll.AddClassExampaper(classpaperList))
            {
                MessageBox.Show("试卷及参考班级选择成功！");
                ShowOtherClassList();
            }
            else
            {
                MessageBox.Show("试卷及参考班级选择失败！");
            }
        }

        public void ShowPapersList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvPapersList.DataSource = null;
                dgvPapersList.Columns.Clear();
                paperBll = new PaperBLL();
                IList<TestPaper> paperFindList = new List<TestPaper>();
                string keyword = txtPaperKeyword.Text;
                Guid sysid = new Guid(cmbSystem.SelectedValue.ToString());
                Guid workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                Guid genreid = new Guid(cmbGenre.SelectedValue.ToString());
                Guid levelid = new Guid(cmbLevel.SelectedValue.ToString());
                Guid subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                Guid userid = Program.mf.LoginUser.UserId;
                paperFindList = paperBll.SearchPaperClass(keyword, sysid, workid, genreid, levelid, subjectid, userid);
                dgvPapersList.DataSource = paperFindList;
                //隔行变色
                dgvPapersList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvPapersList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                PaperSetDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        public void PaperSetDgv()
        {
            dgvPapersList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPapersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvPapersList.Columns[1].HeaderText = "试卷Id";            
            dgvPapersList.Columns[1].HeaderText = "试卷名称";
            dgvPapersList.Columns[1].Width = 140;
            dgvPapersList.Columns[7].HeaderText = "考试时间(分钟)";
            dgvPapersList.Columns[7].Width = 100;
            dgvPapersList.Columns["TestPaperId"].Visible = false;
            dgvPapersList.Columns["StTypeId"].Visible = false;
            dgvPapersList.Columns["StTypeSupplyId"].Visible = false;
            dgvPapersList.Columns["StLevelId"].Visible = false;
            dgvPapersList.Columns["User"].Visible = false;
            dgvPapersList.Columns["UserId"].Visible = false;
            dgvPapersList.Columns["CreateTime"].Visible = false;
            dgvPapersList.Columns.Insert(0, dgvPaperRadioButton);
            dgvPaperRadioButton.Width = 35;
            dgvPaperRadioButton.HeaderText = "选择";
            dgvPaperRadioButton.Name = "checkbox";
            dgvPaperRadioButton.DataPropertyName = "Selected";
            dgvPaperRadioButton.FalseValue = "NoSelected";
            dgvPaperRadioButton.IndeterminateValue = "Indeterminate";
            dgvPaperRadioButton.TrueValue = "Selected";
            dgvPapersList.EnableHeadersVisualStyles = false;
            dgvPapersList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvPapersList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        public void ClassSetDgv()
        {
            dgvClassList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvClassList.Columns[1].HeaderText = "班级Id";
            dgvClassList.Columns[1].HeaderText = "班级名称";
            dgvClassList.Columns[1].Width = 100;

            dgvClassList.Columns["StClassId"].Visible = false;
            dgvClassList.Columns["ClassState"].Visible = false;
            dgvClassList.Columns["User"].Visible = false;
            dgvClassList.Columns["UserId"].Visible = false;
            dgvClassList.Columns["CreateTime"].Visible = false;
            dgvClassList.Columns["ClassStudents"].Visible = false;
            dgvClassList.Columns.Insert(0, dgvClassCheckBoxs);
            dgvClassCheckBoxs.Width = 35;
            dgvClassCheckBoxs.HeaderText = "选择";
            dgvClassList.EnableHeadersVisualStyles = false;
            dgvClassList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvClassList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        public void ShowClassList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvClassList.DataSource = null;
                dgvClassList.Columns.Clear();
                paperBll = new PaperBLL();
                IList<StClass> classFindList = new List<StClass>();
                string keyword = txtClassKeyword.Text;
                classFindList = paperBll.SearchClass(keyword);
                dgvClassList.DataSource = classFindList;
                //隔行变色
                dgvClassList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvClassList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                ClassSetDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        public void ShowOtherClassList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvClassList.DataSource = null;
                dgvClassList.Columns.Clear();
                paperBll = new PaperBLL();
                IList<StClass> classFindList = new List<StClass>();
                string keyword = txtClassKeyword.Text;
                for (int i = 0; i < dgvPapersList.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell checkcell = (DataGridViewCheckBoxCell)this.dgvPapersList.Rows[i].Cells[0];
                    //Boolean paperflag = Convert.ToBoolean(checkcell.Value);
                    if ((SelectedStatus)checkcell.Value == SelectedStatus.Selected)
                    {
                        testpaperId = new Guid(dgvPapersList.Rows[i].Cells[1].Value.ToString());
                    }
                }
                classFindList = paperBll.SearchOtherClass(keyword,testpaperId);
                dgvClassList.DataSource = classFindList;
                //隔行变色
                this.dgvClassList.RowsDefaultCellStyle.BackColor = Color.FromArgb(232, 219, 210);//单元格颜色(淡灰色)
                this.dgvClassList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 240, 236);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                ClassSetDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        private void SetAllRowChecked()
        {
            int count = 0;
            if (chkCheckAll.Checked)
            {
                count = dgvClassList.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvClassList.Rows[i].Cells[0];
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
                count = dgvClassList.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvClassList.Rows[i].Cells[0];
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

        private void SetRadioButtonValue(DataGridViewDisableCheckBoxCell cell)
        {
            SelectedStatus status = (SelectedStatus)cell.Value;
            if (status == SelectedStatus.Selected)
            {
                status = SelectedStatus.NoSelected;
            }
            else
            {
                status = SelectedStatus.Selected;
            }
            for (int i = 0; i < dgvPapersList.Rows.Count; i++)
            {
                DataGridViewDisableCheckBoxCell cel = dgvPapersList.Rows[i].Cells["checkbox"] as DataGridViewDisableCheckBoxCell;
                if (!cel.Equals(cell))
                {
                    cel.Value = status;
                }
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

        private void btnClassQuery_Click(object sender, EventArgs e)
        {
            ShowClassList();
        }
    }
}
