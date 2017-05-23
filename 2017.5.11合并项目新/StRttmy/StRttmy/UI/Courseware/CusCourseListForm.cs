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
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class CusCourseListForm : Form
    {
        private CourseBLL cb = null;
        private CoursewareRepository dc;
        private StTypeRepository st;
        private CoursewareLevelRepository cl;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        public CusCourseListForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<CoursewareLevel> er = null;
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void CusCourseListForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7, 72, 110);
            dgvCourseList.BackgroundColor = Color.FromArgb(72, 161, 201);
            dc = new CoursewareRepository();
            st = new StTypeRepository();
            cl = new CoursewareLevelRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();
            er = new List<CoursewareLevel>();
            IList<CoursewareLevel> cler = cl.CoursewareLevelsList();
            foreach (CoursewareLevel a in cler)
            {
                if (a.CoursewareLevelId != Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"))
                {
                    er.Add(a);
                }
            }
            CoursewareLevel iner = new CoursewareLevel();
            iner.CoursewareLevelId = Guid.Empty;
            iner.Name = "全部";
            er.Insert(0, iner);
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "CoursewareLevelId";
            cmbType.DataSource = er;

            sd = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
            StType ins = new StType();
            ins.StTypeId = Guid.Empty;
            ins.Name = "全部";
            sd.Insert(0, ins);
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "StTypeId";
            comboBox1.DataSource = sd;

            cr = sts.StTypeSupplyList().ToList();
            StTypeSupply insts = new StTypeSupply();
            insts.StTypeSupplyId = Guid.Empty;
            insts.StTypeName = "全部";
            cr.Insert(0, insts);
            comboBox3.DisplayMember = "StTypeName";
            comboBox3.ValueMember = "StTypeSupplyId";
            comboBox3.DataSource = cr;

            fr = sl.StLevelList().ToList();
            StLevel insl = new StLevel();
            insl.StLevelId = Guid.Empty;
            insl.StLevelName = "全部";
            fr.Insert(0, insl);
            comboBox4.DisplayMember = "StLevelName";
            comboBox4.ValueMember = "StLevelId";
            comboBox4.DataSource = fr;
            ShowCourseList(txtKeyword.Text, Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"), categorystr, levelstr, subjects, TypeOfWork, SystemType);
            txtKeyword.Focus();
        }

        //新建课件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCusCourseForm af = new AddCusCourseForm();
            af.cclf = this;            
            af.ShowDialog();
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowCourseList(txtKeyword.Text, Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"), categorystr, levelstr, subjects, TypeOfWork, SystemType);
        }

        //查询方法(只查是自己创建的自定义课件)
       // List<string> StTypeName = new List<string>();
        public void ShowCourseList(string keyword, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (Program.mf.LoginUser != null)
            {
               
                dgvCourseList.DataSource = null;
                dgvCourseList.Columns.Clear();
                IList<Model.Courseware> Coursewares = null;
                cb = new CourseBLL();
                Coursewares = cb.CusCoursewareList(keyword, Program.mf.LoginUser.UserId, type, category, level, subjects, typeofwork, systemtype);
                //foreach (Model.Courseware a in Coursewares)
                //{
                //  a.Keyword = st.StType(st.StType(a.StType.Fid).Fid).Name + st.StType(a.StType.Fid).Name + a.StType.Name + a.StTypeSupply.StTypeName + a.StLevel.StLevelName;
                //}
                this.dgvCourseList.DataSource = Coursewares;//列表绑定数据
                //隔行变色
                //this.dgvCourseList.RowsDefaultCellStyle.BackColor = Color.FromArgb(232, 219, 210);//单元格颜色(淡灰色)
                //this.dgvCourseList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 240, 236);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv(Coursewares.Count());
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //列表表头和操作按钮赋值及属性值
        private void SetDgv(int ListLenght)
        {
            dgvCourseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvCourseList.Columns[0].HeaderText = "课件ID";
            dgvCourseList.Columns[1].HeaderText = "课件类型";
            dgvCourseList.Columns[2].HeaderText = "课件级别";
            dgvCourseList.Columns[3].HeaderText = "课件类型";
            dgvCourseList.Columns[3].Width = 100;
            dgvCourseList.Columns[4].HeaderText = "课件名称";
            dgvCourseList.Columns[4].Width = ListLenght > 23 ? 260 : 280; ;
            dgvCourseList.Columns[5].HeaderText = "课件类型";
            dgvCourseList.Columns[5].Width =100;
            dgvCourseList.Columns[6].HeaderText = "课件关键字";
            dgvCourseList.Columns[7].HeaderText = "创建人";
            dgvCourseList.Columns[8].HeaderText = "课件关键字";
            dgvCourseList.Columns[9].HeaderText = "课件名称";
            dgvCourseList.Columns[9].Width = 20;
            dgvCourseList.Columns[10].HeaderText = "课件类型";
            dgvCourseList.Columns[11].HeaderText = "课件简介";
            dgvCourseList.Columns["CoursewareId"].Visible = false;
            dgvCourseList.Columns["CoursewareLevel"].Visible = false;
            dgvCourseList.Columns["CoursewareLevelId"].Visible = false;
            dgvCourseList.Columns["StLevel"].Visible = false;
            dgvCourseList.Columns["StLevelId"].Visible = false;
            dgvCourseList.Columns["StType"].Visible = false;
            dgvCourseList.Columns["StTypeId"].Visible = false;
            dgvCourseList.Columns["StTypeSupply"].Visible = false;
            dgvCourseList.Columns["StTypeSupplyId"].Visible = false;
            dgvCourseList.Columns["UserId"].Visible = false;
            dgvCourseList.Columns["User"].Visible = false;
            dgvCourseList.Columns["Keyword"].Visible = false;
            dgvCourseList.Columns["CreateTime"].Visible = false;
            dgvCourseList.Columns["coursewareResources"].Visible = false;
            dgvCourseList.EnableHeadersVisualStyles = false;
            dgvCourseList.Columns[9].HeaderCell.Style.BackColor = Color.FromArgb(23,111,161);
            dgvCourseList.Columns[9].HeaderCell.Style.ForeColor = Color.White;
            dgvCourseList.Columns[10].HeaderCell.Style.BackColor = Color.FromArgb(92, 156, 194);
            dgvCourseList.Columns[10].HeaderCell.Style.ForeColor = Color.White;
            dgvCourseList.Columns[11].HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvCourseList.Columns[9].DefaultCellStyle.BackColor = Color.FromArgb(97,147,170);
            dgvCourseList.Columns[9].DefaultCellStyle.ForeColor = Color.White;
            dgvCourseList.Columns[10].DefaultCellStyle.BackColor = Color.FromArgb(139,187,207);
            dgvCourseList.Columns[11].DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            //DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn(); //添加列
            //acCode.Name = "acCode";
            //acCode.DataPropertyName = "acCode";
            //acCode.HeaderText = "A/C Code";
            //acCode.DisplayIndex = 0;
            ////acCode.Tag = StTypeName;
            ////acCode.= StTypeName;
            //dgvCourseList.Columns.Add(acCode);
            

            DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonColEdit.Name = "ManageEdit";
            // 设置列标题
            dgvButtonColEdit.HeaderText = "修改信息";
            // 设置按钮标题
            dgvButtonColEdit.UseColumnTextForButtonValue = true;
            dgvButtonColEdit.Text = "修改信息";
            dgvButtonColEdit.Width = 77;
            dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEdit.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonColEditRes = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonColEditRes.Name = "ManageEditRes";
            // 设置列标题
            dgvButtonColEditRes.HeaderText = "修改素材";
            // 设置按钮标题
            dgvButtonColEditRes.UseColumnTextForButtonValue = true;
            dgvButtonColEditRes.Text = "修改素材";
            dgvButtonColEditRes.Width = 77;
            dgvButtonColEditRes.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEditRes.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEditRes.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonPaper = new DataGridViewButtonColumn();

            dgvButtonPaper.Name = "ManageEditPapers";
            // 设置列标题
            dgvButtonPaper.HeaderText = "生成试卷";
            // 设置按钮标题
            dgvButtonPaper.UseColumnTextForButtonValue = true;
            dgvButtonPaper.Text = "生成试卷";
            dgvButtonPaper.Width = 77;
            dgvButtonPaper.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonPaper.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonPaper.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonColDel = new DataGridViewButtonColumn();//dgvButtonColDel

            // 设置列名
            dgvButtonColDel.Name = "ManageDel";
            // 设置列标题
            dgvButtonColDel.HeaderText = "删除";
            // 设置按钮标题
            dgvButtonColDel.UseColumnTextForButtonValue = true;
            dgvButtonColDel.Text = "删除";
            dgvButtonColDel.Width = 68;
            dgvButtonColDel.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColDel.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColDel.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonColPub = new DataGridViewButtonColumn();
            // 设置列名
            dgvButtonColPub.Name = "ManagePub";
            // 设置列标题
            dgvButtonColPub.HeaderText = "共享";
            // 设置按钮标题
            dgvButtonColPub.UseColumnTextForButtonValue = true;
            dgvButtonColPub.Text = "共享";
            dgvButtonColPub.Width = 68;
            dgvButtonColPub.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColPub.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColPub.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // 向DataGridView追加
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColEdit);
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColEditRes);//dgvButtonPaper
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonPaper);
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColDel);
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColPub);
        }

        //添加行号
        private void dgvCourseList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvCourseList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvCourseList.RowHeadersDefaultCellStyle.Font, rectangle, dgvCourseList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //单元格点击事件
        private void dgvCourseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageEdit")
                {
                    UI.EditCusCourseForm ccf = new EditCusCourseForm();//修改课件信息
                    ccf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    ccf.cclf = this;
                    ccf.ShowDialog();
                }

                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageEditRes")
                {
                    UI.EditCourseResourceForm ecrf = new EditCourseResourceForm();//修改素材
                    ecrf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    ecrf.ShowDialog();
                }

                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageEditPapers")
                {
                    AddPaperForm apf = new AddPaperForm();
                    apf.PARM = 1;
                    apf.courseId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    apf.ShowDialog();
                }

                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageDel")
                {
                    Guid resourceId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;//删除需要的id
                    DelCourse(resourceId);
                }

                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManagePub")
                {
                    Guid resourceId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;//发布需要的id
                    PublishCusCourse(resourceId);
                }
            }
        }

        //删除
        private void DelCourse(Guid coursewareId)
        {
            DialogResult dr = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                cb = new CourseBLL();
                if (cb.DelCourse(coursewareId))
                {
                    MessageBox.Show("课件已删除。");
                    ShowCourseList(txtKeyword.Text, Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"), categorystr, levelstr, subjects, TypeOfWork, SystemType);//删除后按照之前的查询条件刷新列表
                    txtKeyword.Focus();
                }
                else
                {
                    MessageBox.Show("课件删除失败。");
                }
            }
        }

        //发布
        private void PublishCusCourse(Guid coursewareId)
        {
            cb = new CourseBLL();
            if (cb.PublishCusCourse(coursewareId))
            {
                MessageBox.Show("发布成功。");
                ShowCourseList(txtKeyword.Text, Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"), categorystr, levelstr, subjects, TypeOfWork, SystemType);//发布后按照之前的查询条件刷新列表
                txtKeyword.Focus();
            }
            else
            {
                MessageBox.Show("发布失败。");
            }
        }

        private Guid cmbTypestr = Guid.Empty;
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)cmbType.SelectedValue != Guid.Empty)
            {
                cmbTypestr = (Guid)cmbType.SelectedValue;
            }
            else
            {
                cmbTypestr = Guid.Empty;
            }
        }
        private Guid SystemType = Guid.Empty;
        private List<StType> sd1 = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox1.SelectedValue != Guid.Empty)
            {
                SystemType = (Guid)comboBox1.SelectedValue;
                sd1 = st.StTypeList(SystemType);
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd1.Insert(0, ins);
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "StTypeId";
                comboBox2.DataSource = sd1;
            }
            else
            {

                SystemType = Guid.Empty;

                comboBox2.ResetText();
                comboBox5.ResetText();
                comboBox2.SelectedText = "全部";
                TypeOfWork = Guid.Empty;
                comboBox5.SelectedText = "全部";
                subjects = Guid.Empty;
                comboBox2.DataSource = null;
                comboBox5.DataSource = null;
            }
        }
        private Guid TypeOfWork = Guid.Empty;
        private List<StType> sd2 = null;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedText != "全部")
            {
                if ((Guid)comboBox2.SelectedValue != Guid.Empty)
                {
                    TypeOfWork = (Guid)comboBox2.SelectedValue;
                    sd2 = st.StTypeList(TypeOfWork);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd2.Insert(0, ins);
                    comboBox5.DisplayMember = "Name";
                    comboBox5.ValueMember = "StTypeId";
                    comboBox5.DataSource = sd2;
                }
                else
                {
                    TypeOfWork = Guid.Empty;
                    subjects = Guid.Empty;
                    comboBox5.ResetText();
                    comboBox5.SelectedText = "全部";
                    comboBox5.DataSource = null;
                }
            }
            else
            {

                TypeOfWork = Guid.Empty;
                comboBox5.ResetText();
                comboBox5.SelectedText = "全部";
                comboBox5.DataSource = null;
            }

        }

        private Guid subjects = Guid.Empty;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedText != "全部")
            {
                if ((Guid)comboBox5.SelectedValue != Guid.Empty)
                {
                    subjects = (Guid)comboBox5.SelectedValue;
                }
                else
                {
                    subjects = Guid.Empty;
                }
            }
            else
            {
                subjects = Guid.Empty;
            }

        }


        private Guid categorystr = Guid.Empty;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox3.SelectedValue != Guid.Empty)
            {
                categorystr = (Guid)comboBox3.SelectedValue;
            }
            else
            {
                categorystr = Guid.Empty;
            }
        }

        private Guid levelstr = Guid.Empty;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox4.SelectedValue != Guid.Empty)
            {
                levelstr = (Guid)comboBox4.SelectedValue;
            }
            else
            {
                levelstr = Guid.Empty;
            }
        }
        public void AddResource(Guid  id) 
        {
            EditCourseResourceForm erf = new EditCourseResourceForm();
            erf.coursewareId = id;
            erf.MdiParent = this.MdiParent;
            erf.ShowDialog();
        }

        #region 下拉选框注解
        private void cmbType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(er[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(er[e.Index].Name, cmbType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbType);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd[e.Index].Name, comboBox1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox1);
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd1[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd1[e.Index].Name, comboBox2, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox2);
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(cr[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(cr[e.Index].StTypeName, comboBox3, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox3);
        }

        private void comboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(fr[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(fr[e.Index].StLevelName, comboBox4, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox4);
        }

        private void comboBox5_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd2[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd2[e.Index].Name, comboBox5, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox5_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox5);
        }
        #endregion
    }
}
