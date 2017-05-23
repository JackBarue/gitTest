using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.Model;
using StRttmy.BLL;
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class AddCusCourseForm : Form
    {
        private CourseBLL cb = null;
        public CusCourseListForm cclf = null;
        private CoursewareRepository dc;
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        public AddCusCourseForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void AddCusCourseForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52,152,186);
            //cmbType.DataSource = CourseSelectItem.TypeSelectList;
            //cmbType.DisplayMember = "Text";
            //cmbType.ValueMember = "Value";
            dc = new CoursewareRepository();
            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();

            sd = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
            StType ins = new StType();
            ins.StTypeId = Guid.Empty;
            ins.Name = "全部";
            sd.Insert(0, ins);
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "StTypeId";
            cmbType.DataSource = sd;

            cr = sts.StTypeSupplyList().ToList();
            StTypeSupply insts = new StTypeSupply();
            insts.StTypeSupplyId = Guid.Empty;
            insts.StTypeName = "全部";
            cr.Insert(0, insts);
            comboBox2.DisplayMember = "StTypeName";
            comboBox2.ValueMember = "StTypeSupplyId";
            comboBox2.DataSource = cr;

            fr = sl.StLevelList().ToList();
            StLevel insl = new StLevel();
            insl.StLevelId = Guid.Empty;
            insl.StLevelName = "全部";
            fr.Insert(0, insl);
            comboBox3.DisplayMember = "StLevelName";
            comboBox3.ValueMember = "StLevelId";
            comboBox3.DataSource = fr;
            txtKeyword.Focus();
        }

        //窗体激活
        private void AddCusCourseForm_Activated(object sender, EventArgs e)
        {
            txtName.Focus();//登录名输入框获得焦点
            this.AcceptButton = btnAdd;//窗体中按下回车启动btnAdd按钮
        }

        //新建课件
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCourse();
        }

        //表单提交
        public delegate void AddShow(Guid id);
        private Guid coursewareId = Guid.Empty;
        public delegate void FreshData(string keyword, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype);
        private void AddCourse()
        {
            if (Program.mf.LoginUser != null)
            {
                if (subjects == Guid.Empty)
                {
                    MessageBox.Show("课件科目不能为空");
                    cmbType.BackColor = Color.Red;
                    comboBox1.BackColor = Color.Red;
                    comboBox4.BackColor = Color.Red;
                    return;
                }
                if (categorystr == Guid.Empty)
                {
                    MessageBox.Show("课件类别不能为空");
                    comboBox2.BackColor = Color.Red;
                    return;
                }
                if (levelstr == Guid.Empty)
                {
                    MessageBox.Show("课件等级不能为空");
                    comboBox3.BackColor = Color.Red;
                    return;
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("课件名称不能为空值。");
                    txtName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtKeyword.Text))
                {
                    MessageBox.Show("课件关键字不能为空值。");
                    txtKeyword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageBox.Show("课件简介不能为空值。");
                    txtDescription.Focus();
                    return;
                }

                cb = new CourseBLL();
                Model.Courseware tempc = new Model.Courseware();
                tempc = cb.GetCourseByName(txtName.Text);
                if (tempc != null)
                {
                    MessageBox.Show("课件名称已存在。");
                    txtName.Focus();
                    return;
                }

                Model.Courseware newCourseware = new Model.Courseware();
                newCourseware.CoursewareId = coursewareId = Guid.NewGuid();
                newCourseware.CoursewareLevelId = Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c");
                newCourseware.StLevelId = levelstr;
                newCourseware.StTypeSupplyId = categorystr;
                newCourseware.StTypeId = subjects;
                newCourseware.Name = txtName.Text;
                newCourseware.Keyword = txtKeyword.Text;
                newCourseware.Description = txtDescription.Text;
                newCourseware.UserId = Program.mf.LoginUser.UserId;
                //newResource.CreateTime = System.DateTime.Now;

                //  给courseware的课件素材添加内容
                //  根据课件名称生成根节点
                CoursewareResource cr0 = new CoursewareResource();
               // cr0.CoursewareResourcesId = Guid.NewGuid();
                cr0.id = Guid.NewGuid().ToString();
                ////cr0.pId = "";
                cr0.pId = Guid.NewGuid().ToString();
                cr0.name = newCourseware.Name;
                cr0.MainUrl = "";
                cr0.TextUrl = "";
                cr0.Mp3Url = "";

                newCourseware.coursewareResources = new List<CoursewareResource>();
                newCourseware.coursewareResources.Add(cr0);

                if (cb.AddCusCourse(newCourseware))//保存课件到数据库
                {
                    ClearControl();
                    if (cclf != null)
                    {
                        FreshData fd = new FreshData(cclf.ShowCourseList);
                        fd("", Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"), Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty);
                        AddShow ads = new AddShow(cclf.AddResource);
                        ads(coursewareId);
                        this.Close();
                    }                 
                }
                else
                {
                    MessageBox.Show("新建课件失败。");
                    txtName.Focus();
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }
        

        //初始化表单
        private void ClearControl()
        {
            txtName.Clear();
            txtKeyword.Clear();
            txtDescription.Clear();
            txtName.Focus();
        }

        
        private Guid SystemType = Guid.Empty;
        private List<StType> sd1 = null;
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)cmbType.SelectedValue != Guid.Empty)
            {
                SystemType = (Guid)cmbType.SelectedValue;
                sd1 = st.StTypeList(SystemType);
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd1.Insert(0, ins);
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "StTypeId";
                comboBox1.DataSource = sd1;
            }
            else
            {

                SystemType = Guid.Empty;

                comboBox1.ResetText();
                comboBox4.ResetText();
                comboBox1.SelectedText = "全部";
                TypeOfWork = Guid.Empty;
                comboBox4.SelectedText = "全部";
                subjects = Guid.Empty;
                comboBox1.DataSource = null;
                comboBox4.DataSource = null;
            }

        }
        private Guid TypeOfWork = Guid.Empty;
        private List<StType> sd2 = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {        
            if (comboBox1.SelectedText != "全部")
            {
                if ((Guid)comboBox1.SelectedValue != Guid.Empty)
                {
                    TypeOfWork = (Guid)comboBox1.SelectedValue;
                    sd2 = st.StTypeList(TypeOfWork);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd2.Insert(0, ins);
                    comboBox4.DisplayMember = "Name";
                    comboBox4.ValueMember = "StTypeId";
                    comboBox4.DataSource = sd2;
                }
                else
                {
                    TypeOfWork = Guid.Empty;
                    subjects = Guid.Empty;
                    comboBox4.ResetText();
                    comboBox4.SelectedText = "全部";
                    comboBox4.DataSource = null;
                }
            }
            else
            {

                TypeOfWork = Guid.Empty;
                comboBox4.ResetText();
                comboBox4.SelectedText = "全部";
                comboBox4.DataSource = null;
            }
        }

        private Guid subjects = Guid.Empty;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox4.SelectedText != "全部")
            {
                if ((Guid)comboBox4.SelectedValue != Guid.Empty)
                {
                    subjects = (Guid)comboBox4.SelectedValue;
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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox2.SelectedValue != Guid.Empty)
            {
                categorystr = (Guid)comboBox2.SelectedValue;
            }
            else
            {
                categorystr = Guid.Empty;
            }
        }

        private Guid levelstr = Guid.Empty;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox3.SelectedValue != Guid.Empty)
            {
                levelstr = (Guid)comboBox3.SelectedValue;
            }
            else
            {
                levelstr = Guid.Empty;
            }
        }

        private void AddCusCourseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void AddCusCourseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void AddCusCourseForm_Deactivate(object sender, EventArgs e)
        {
            
        }

        private void cmbType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd[e.Index].Name, cmbType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbType);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd1[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd1[e.Index].Name, comboBox1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
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
            e.Graphics.DrawString(cr[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(cr[e.Index].StTypeName, comboBox2, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox2);
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(fr[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(fr[e.Index].StLevelName, comboBox3, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
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
            e.Graphics.DrawString(sd2[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd2[e.Index].Name, comboBox4, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox4);
        }
    }
}
