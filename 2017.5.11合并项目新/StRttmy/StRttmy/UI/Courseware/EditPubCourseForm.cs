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
    public partial class EditPubCourseForm : Form
    {
        private CourseBLL cb = null;
        public Guid coursewareId = Guid.Empty;
        public PubCourseListForm pclf = null;
        public delegate void FreshData(string keyword, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype);
        private CoursewareRepository dc;
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;

        public EditPubCourseForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void EditPubCourseForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
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
            ShowCourse();
        }

        //窗体激活
        private void EditPubCourseForm_Activated(object sender, EventArgs e)
        {
            txtName.Focus();
            this.AcceptButton = btnEdit;//窗体中按下回车启动btnEdit按钮
        }

        //初始化
        private void ShowCourse()
        {
            if (coursewareId != Guid.Empty)
            {
                Model.Courseware oldCourseware = new Model.Courseware();
                cb = new CourseBLL();
                oldCourseware = cb.GetCourse(coursewareId);

                cmbType.SelectedValue = st.StType(st.StType(oldCourseware.StType.Fid).Fid).StTypeId;
                comboBox1.SelectedValue = st.StType(oldCourseware.StType.Fid).StTypeId;
                comboBox4.SelectedValue = oldCourseware.StType.StTypeId;
                comboBox2.SelectedValue = oldCourseware.StTypeSupply.StTypeSupplyId;
                comboBox3.SelectedValue = oldCourseware.StLevel.StLevelId;

                txtName.Text = oldCourseware.Name;
                txtKeyword.Text = oldCourseware.Keyword;
                txtDescription.Text = oldCourseware.Description;
            }
            else
            {
                MessageBox.Show("此课件不存在。");
                Close();
            }
        }

        //修改课件
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditCourse();
        }

        //修改课件表单提交
        private void EditCourse()
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
                if (tempc != null && tempc.CoursewareId != coursewareId)
                {
                    MessageBox.Show("课件名称已存在。");
                    txtName.Focus();
                    return;
                }

                if (coursewareId != Guid.Empty)
                {
                    Model.Courseware newCourseware = new Model.Courseware();
                    newCourseware.CoursewareId = coursewareId;
                    newCourseware.StTypeSupplyId = categorystr;
                    newCourseware.StLevelId = levelstr;
                    newCourseware.Name = txtName.Text;
                    newCourseware.Keyword = txtKeyword.Text;
                    newCourseware.Description = txtDescription.Text;
                    newCourseware.StTypeId = subjects;
                    
                    //if (cmbType.SelectedValue != null)
                    //{
                    //    //newCourseware.Type = (CoursewareType)cmbType.SelectedValue;
                    //}
                    //else
                    //{
                    //    Courseware oldCourseware = new Courseware();
                    //    oldCourseware = cb.GetCourse(coursewareId);
                    //   // newCourseware.Type = oldCourseware.Type;
                    //}

                    if (cb.EditPubCourse(newCourseware))
                    {
                        MessageBox.Show("修改课件成功。");
                        if (pclf != null)
                        {
                            FreshData fd = new FreshData(pclf.ShowCourseList);
                            fd("", Guid.Parse("b6c51980-f0c2-4ce1-90d5-d8fc726e0e3b"), categorystr, levelstr, subjects, TypeOfWork, SystemType);
                        }
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("修改课件失败。");
                        txtName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("此课件不存在。");
                    Close();
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
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
