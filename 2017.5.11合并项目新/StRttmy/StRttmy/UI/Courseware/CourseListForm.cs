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
    public partial class CourseListForm : Form
    {
        private CourseBLL cb = null;
        private CoursewareRepository dc;
        private StTypeRepository st;
        private CoursewareLevelRepository cl;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        
        public CourseListForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<CoursewareLevel> er = null;
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void CourseListForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7,72,110);
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
            ShowCourseList(txtKeyword.Text, cmbTypestr, categorystr, levelstr, subjects, TypeOfWork, SystemType);
            txtKeyword.Focus();
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowCourseList(txtKeyword.Text, cmbTypestr, categorystr, levelstr, subjects, TypeOfWork, SystemType);
        }

        //查询方法
        private void ShowCourseList(string keyword, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (Program.mf.LoginUser != null)
            {
                dgvCourseList.DataSource = null;
                dgvCourseList.Columns.Clear();
                IList<Model.Courseware> Coursewares = null;
                cb = new CourseBLL();
                Coursewares = cb.CoursewareList(keyword, Guid.Empty, type, category, level, subjects, typeofwork, systemtype);
                //foreach (Model.Courseware a in Coursewares)
                //{
                //    //a.Keyword = st.StType(st.StType(a.StType.Fid).Fid).Name + st.StType(a.StType.Fid).Name + a.StType.Name + a.StTypeSupply.StTypeName + a.StLevel.StLevelName;
                //}
                this.dgvCourseList.DataSource = Coursewares;//列表绑定数据
                SetDgv(Coursewares.Count());
                //foreach (Model.Courseware a in Coursewares)
                //{
                //    a.Keyword = st.StType(st.StType(a.StType.Fid).Fid).Name + st.StType(a.StType.Fid).Name + a.StType.Name + a.StTypeSupply.StTypeName + a.StLevel.StLevelName;
                //    Panel panel = new Panel();
                //    panel.Width = this.Width * 1 / 4 -15;
                //    panel.Height = 160;                   
                //    Label lab = new Label();
                //    lab.Text = a.Name;
                //    lab.ForeColor = Color.White;
                //    lab.Location = new System.Drawing.Point(30, 10); 
                //    panel.Controls.Add(lab);
                //    Label labc = new Label();
                //    labc.Width = this.Width * 1 / 4 -75;
                //    labc.Height = 80;
                //    labc.Location = new System.Drawing.Point(30,40); 
                //    labc.Text = a.Description;
                //    labc.BackColor = Color.White;
                //    panel.Controls.Add(labc);
                //    Label labjoin = new Label();
                //    labjoin.Location = new System.Drawing.Point(150, 130); 
                //    labjoin.Text = "学习>>";
                //    labjoin.Cursor = System.Windows.Forms.Cursors.Hand;
                //    labjoin.Tag = a.CoursewareId;
                //    labjoin.ForeColor = Color.White;
                //    labjoin.Width = 45;
                //    labjoin.Click += new System.EventHandler(this.labjoin_Click);
                //    panel.Controls.Add(labjoin);
                //    Label labteach = new Label();
                //    labteach.Cursor = System.Windows.Forms.Cursors.Hand;
                //    labteach.Tag = a.CoursewareId;
                //    labteach.Location = new System.Drawing.Point(220, 130);                  
                //    labteach.Text = "教学>>";
                //    labteach.Width = 45;
                //    labteach.ForeColor = Color.White;
                //    labteach.Click += new System.EventHandler(this.labteach_Click);
                //    panel.Controls.Add(labteach);
                //    panel.BackColor = Color.FromArgb(109,184,213);
                //    flowLayoutPanel1.Controls.Add(panel);                                      
                //}
                //this.dgvCourseList.DataSource = Coursewares;
                    
                ////隔行变色
                //this.dgvCourseList.RowsDefaultCellStyle.BackColor = Color.FromArgb(232, 219, 210);//单元格颜色(淡灰色)
                //this.dgvCourseList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 240, 236);//奇数单元格颜色(米黄色)
                ////this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                //SetDgv(Coursewares.Count());
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }
        private void labjoin_Click(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            UI.CourseResourceForm crf = new CourseResourceForm();//查看内容
            crf.coursewareId =(Guid)lab.Tag;
            crf.ShowDialog();
        }

        private void labteach_Click(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            UI.ChooseClassFrom crf = new ChooseClassFrom();//查看内容
            crf.coursewareId = (Guid)lab.Tag;
            crf.ShowDialog();
        }
        //列表表头和操作按钮赋值及属性值
        private void SetDgv(int ListLenght)
        {
            dgvCourseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvCourseList.Columns[0].HeaderText = "课件ID";
            dgvCourseList.Columns[1].HeaderText = "课件类型";
            dgvCourseList.Columns[1].Width = 100;
            dgvCourseList.Columns[2].HeaderText = "课件级别";
            dgvCourseList.Columns[3].HeaderText = "课件关键字";
            dgvCourseList.Columns[3].Width = 320;
            dgvCourseList.Columns[4].HeaderText = "课件名称";
            dgvCourseList.Columns[4].Width = ListLenght > 23 ? 300 : 320;
            dgvCourseList.Columns[5].HeaderText = "课件类型";
            dgvCourseList.Columns[5].Width = 80;
            dgvCourseList.Columns[6].HeaderText = "创建人Id";
            dgvCourseList.Columns[7].HeaderText = "创建人";
            dgvCourseList.Columns[8].HeaderText = "创建时间";
            dgvCourseList.Columns[9].HeaderText = "课件名称";
            dgvCourseList.Columns[10].HeaderText = "课件名称";
            dgvCourseList.Columns[9].Width = 20;
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
            dgvCourseList.Columns[9].HeaderCell.Style.BackColor = Color.FromArgb(23, 111, 161);
            dgvCourseList.Columns[9].HeaderCell.Style.ForeColor = Color.White;
            dgvCourseList.Columns[10].HeaderCell.Style.BackColor = Color.FromArgb(92, 156, 194);
            dgvCourseList.Columns[10].HeaderCell.Style.ForeColor = Color.White;
            dgvCourseList.Columns[11].HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvCourseList.Columns[9].DefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvCourseList.Columns[9].DefaultCellStyle.ForeColor = Color.White;
            dgvCourseList.Columns[10].DefaultCellStyle.BackColor = Color.FromArgb(139, 187, 207);
            dgvCourseList.Columns[11].DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);

          
            if (Program.mf.LoginUser!= null)
            {
                //button1.Hide();
                if (Program.mf.LoginUser.UserType == UserType.教师)
                {
                    DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
                    dgvButtonColEdit.Name = "ManageDetail";
                    // 设置列标题
                    dgvButtonColEdit.HeaderText = "预览";
                    // 设置按钮标题
                    dgvButtonColEdit.UseColumnTextForButtonValue = true;
                    dgvButtonColEdit.Text = "预览";
                    dgvButtonColEdit.Width = 82;
                    dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
                    dgvButtonColEdit.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
                    dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    DataGridViewButtonColumn dgvButtonColContent = new DataGridViewButtonColumn();
                    // 设置列名
                    dgvButtonColContent.Name = "ManageContent";
                    // 设置列标题
                    dgvButtonColContent.HeaderText = "教学";
                    // 设置按钮标题
                    dgvButtonColContent.UseColumnTextForButtonValue = true;
                    dgvButtonColContent.Text = "教学";
                    dgvButtonColContent.Width = 82;
                    dgvButtonColContent.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
                    dgvButtonColContent.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
                    dgvButtonColContent.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColEdit);
                    dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColContent);   
                }
                if (Program.mf.LoginUser.UserType == UserType.学员)
                {
                    DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
                    dgvButtonColEdit.Name = "ManageDetail";
                    // 设置列标题
                    dgvButtonColEdit.HeaderText = "学习";
                    // 设置按钮标题
                    dgvButtonColEdit.UseColumnTextForButtonValue = true;
                    dgvButtonColEdit.Text = "学习";
                    dgvButtonColEdit.Width = 82;
                    dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
                    dgvButtonColEdit.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
                    dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColEdit);
                }
              }
            // 向DataGridView追加
           
           
        }

        //添加行号
        private void dgvCourseList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvCourseList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvCourseList.RowHeadersDefaultCellStyle.Font, rectangle, dgvCourseList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //单元格点击
        private void dgvCourseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageDetail")
                {
                    //UI.DetailCourseForm dcf = new DetailCourseForm();//查看信息
                    //dcf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    //dcf.ShowDialog();

                    UI.CourseResourceForm crf = new CourseResourceForm();//查看内容
                    crf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    crf.ShowDialog();
                }

                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageContent")
                {
                    UI.ChooseClassFrom crf = new ChooseClassFrom();//查看内容
                    crf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    crf.ShowDialog();
                }
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
    }
}
