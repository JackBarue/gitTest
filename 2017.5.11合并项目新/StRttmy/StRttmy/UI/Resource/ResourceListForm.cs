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
    public partial class ResourceListForm : Form
    {
        private ResourceBLL rb = null;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        private StTypeRepository st;
        public ResourceListForm()
        {
            InitializeComponent();
        }

       
        private void ResourceListForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7, 72, 110);
            dgvResourceList.BackgroundColor = Color.FromArgb(72, 161, 201);
            BrowseWindow();
        }

        //窗体加载
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        public void BrowseWindow()
        {

            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();

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
            ShowResourceList(txtKeyword.Text, categorystr, levelstr, subjects, TypeOfWork, SystemType);
        }

        //窗体激活
        private void ResourceListForm_Activated(object sender, EventArgs e)
        {
            txtKeyword.Focus();
        }

        //初始化
        public void ShowResourceList(string keyword, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            StTypeRepository st = new StTypeRepository();
            if (Program.mf.LoginUser != null)
            {
                dgvResourceList.DataSource = null;
                dgvResourceList.Columns.Clear();
                IList<ResourceListClass> resources = null;
                
                rb = new ResourceBLL();
                resources = rb.ResourceListShow(keyword, category, level, subjects, typeofwork, systemtype);
                //foreach (Resource a in resources)
                //{
                //    a.SoundFile = st.StType(st.StType(a.StType.Fid).Fid).Name + st.StType(a.StType.Fid).Name + a.StType.Name + a.StTypeSupply.StTypeName + a.StLevel.StLevelName;                    
                //}
                this.dgvResourceList.DataSource = resources;
                //隔行变色
                //this.dgvResourceList.RowsDefaultCellStyle.BackColor = Color.FromArgb(232, 219, 210);//单元格颜色(淡灰色)
                //this.dgvResourceList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 240, 236);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv(resources.Count());
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowResourceList(txtKeyword.Text, categorystr, levelstr, subjects, TypeOfWork, SystemType);
            txtKeyword.Focus();
        }

        //列表表头和操作按钮赋值及属性值
        private void SetDgv(int ListLenght)
        {
            dgvResourceList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满dgvResourceList
            dgvResourceList.Columns[0].HeaderText = "素材ID";
            dgvResourceList.Columns[1].HeaderText = "素材类型";
            dgvResourceList.Columns[1].Width = 165;
            dgvResourceList.Columns[2].HeaderText = "素材类型";
            dgvResourceList.Columns[2].Width = 165;
            dgvResourceList.Columns[4].HeaderText = "素材标题";
            dgvResourceList.Columns[4].Width = 30;
            //dgvResourceList.Columns[3].Width = ListLenght > 23 ? 120 : 130;
            dgvResourceList.Columns[5].HeaderText = "素材关键字";
            //dgvResourceList.Columns[4].Width = 420;
            dgvResourceList.Columns[7].HeaderText = "媒体文件";
            dgvResourceList.Columns[6].HeaderText = "文字文件";
            dgvResourceList.Columns[3].HeaderText = "声音文件";
            dgvResourceList.Columns[12].HeaderText = "创建时间";
            //dgvResourceList.Columns["StLevel"].Visible = false;
            dgvResourceList.Columns["StLevelId"].Visible = false;
            //dgvResourceList.Columns["StType"].Visible = false;
            dgvResourceList.Columns["StTypeId"].Visible = false;
            //dgvResourceList.Columns["StTypeSupply"].Visible = false;
            dgvResourceList.Columns["StTypeSupplyId"].Visible = false;
            dgvResourceList.Columns["ResourceId"].Visible = false;
            dgvResourceList.Columns["Level"].Visible = false;
            dgvResourceList.Columns["ContentFile"].Visible = false;
            dgvResourceList.Columns["TextFile"].Visible = false;
            dgvResourceList.Columns["SoundFile"].Visible = false;
            dgvResourceList.Columns["UserId"].Visible = false;
            dgvResourceList.Columns["ResourceStyle"].Visible = false;
            //dgvResourceList.Columns["User"].Visible = false;
            dgvResourceList.Columns["CreateTime"].Visible = false;

            dgvResourceList.EnableHeadersVisualStyles = false;
            dgvResourceList.Columns[2].HeaderCell.Style.BackColor = Color.FromArgb(23, 111, 161);
            dgvResourceList.Columns[2].HeaderCell.Style.ForeColor = Color.White;
            dgvResourceList.Columns[4].HeaderCell.Style.BackColor = Color.FromArgb(92, 156, 194);
            dgvResourceList.Columns[4].HeaderCell.Style.ForeColor = Color.White;
            dgvResourceList.Columns[5].HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvResourceList.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvResourceList.Columns[2].DefaultCellStyle.ForeColor = Color.White;
            dgvResourceList.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(139, 187, 207);
            dgvResourceList.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);

            DataGridViewButtonColumn dgvButtonNewQuestions = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonNewQuestions.Name = "ManageNewQuestions";
            // 设置列标题
            dgvButtonNewQuestions.HeaderText = "关联试题";
            // 设置按钮标题
            dgvButtonNewQuestions.UseColumnTextForButtonValue = true;
            dgvButtonNewQuestions.Text = "关联试题";
            dgvButtonNewQuestions.Width = 100;
            dgvButtonNewQuestions.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonNewQuestions.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonNewQuestions.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonDerived = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonDerived.Name = "ManageDerived";
            // 设置列标题
            dgvButtonDerived.HeaderText = "派生";
            // 设置按钮标题
            dgvButtonDerived.UseColumnTextForButtonValue = true;
            dgvButtonDerived.Text = "派生";
            dgvButtonDerived.Width = 100;
            dgvButtonDerived.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonDerived.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonDerived.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonColEdit.Name = "ManageDetail";
            // 设置列标题
            dgvButtonColEdit.HeaderText = "查看信息";
            // 设置按钮标题
            dgvButtonColEdit.UseColumnTextForButtonValue = true;
            dgvButtonColEdit.Text = "查看信息";
            dgvButtonColEdit.Width = 100;
            dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEdit.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            DataGridViewButtonColumn dgvButtonColDel = new DataGridViewButtonColumn();
            // 设置列名
            dgvButtonColDel.Name = "ManageContent";
            // 设置列标题
            dgvButtonColDel.HeaderText = "查看内容";
            // 设置按钮标题
            dgvButtonColDel.UseColumnTextForButtonValue = true;
            dgvButtonColDel.Text = "查看内容";
            dgvButtonColDel.Width = 100;
            dgvButtonColDel.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColDel.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColDel.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // 向DataGridView追加
            dgvResourceList.Columns.Insert(dgvResourceList.Columns.Count, dgvButtonNewQuestions);
            dgvResourceList.Columns.Insert(dgvResourceList.Columns.Count, dgvButtonDerived);
            dgvResourceList.Columns.Insert(dgvResourceList.Columns.Count, dgvButtonColEdit);
            dgvResourceList.Columns.Insert(dgvResourceList.Columns.Count, dgvButtonColDel);
        }

        //添加行号
        private void dgvResourceList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvResourceList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvResourceList.RowHeadersDefaultCellStyle.Font, rectangle, dgvResourceList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //单元格点击事件
        private void dgvResourceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dgvResourceList.Columns[e.ColumnIndex].Name == "ManageNewQuestions")//素材关联试题
                {
                    NewAddResourceQuestionForm narqf = new NewAddResourceQuestionForm();
                    narqf.resourceId = (Guid)dgvResourceList.Rows[e.RowIndex].Cells["ResourceId"].Value;
                    narqf.ShowDialog();
                    //AddTestQuestionForm atf = new AddTestQuestionForm();
                    //atf.resourceId = (Guid)dgvResourceList.Rows[e.RowIndex].Cells["ResourceId"].Value;
                    //atf.PARM = 2;
                    //atf.ShowDialog();
                }
                if (dgvResourceList.Columns[e.ColumnIndex].Name == "ManageDerived")
                {
                    DerivedResourceForm dref = new DerivedResourceForm();
                    dref.Recrlf = this;
                    dref.MainPath = dgvResourceList.Rows[e.RowIndex].Cells["ContentFile"].Value.ToString();
                    dref.ShowDialog();
                }
                if (dgvResourceList.Columns[e.ColumnIndex].Name == "ManageDetail")
                {
                    UI.DetailResourceForm drf = new DetailResourceForm();//查看信息
                    drf.resourceId = (Guid)dgvResourceList.Rows[e.RowIndex].Cells["ResourceId"].Value;
                    drf.ShowDialog();
                }

                if (dgvResourceList.Columns[e.ColumnIndex].Name == "ManageContent")
                {
                    UI.DetailResourceContentForm drcf = new DetailResourceContentForm();//查看内容
                    drcf.resourceId = (Guid)dgvResourceList.Rows[e.RowIndex].Cells["ResourceId"].Value;
                    drcf.ShowDialog();
                }
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

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
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
            // 绘制背景
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
