using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.Model;
using StRttmy.BLL;
using System.IO;
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class DerivedResourceForm : Form
    {
        public string MainPath = "";
        private ResourceBLL rb = null;
        public EditCourseResourceForm ecrlf = null;
        public delegate void FreshData(string keyword, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype);

        public ResourceListForm Recrlf = null;
        public delegate void FresResourcehData();
        public string SoundStr = "";
        public string TextStr = "";
        private ResourceRepository dc;
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;

        public DerivedResourceForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void AddResourceForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            dc = new ResourceRepository();
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
            txtContentFile.Text = Path.GetFileName(MainPath);
            btnContentFile.Enabled = false;
        }

        //窗体激活
        private void AddResourceForm_Activated(object sender, EventArgs e)
        {
            txtTitle.Focus();
            this.AcceptButton = btnAdd;//窗体中按下回车启动btnAdd按钮
        }

        //提交
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddResource();
        }

        //提交方法
        private void AddResource()
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
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox.Show("素材标题不能为空值。");
                    txtTitle.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtKeyword.Text))
                {
                    MessageBox.Show("素材关键字不能为空值。");
                    txtKeyword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtContentFile.Text))
                {
                    MessageBox.Show("媒体文件不能为空值。");
                    txtContentFile.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSoundFile.Text))
                {
                    MessageBox.Show("声音文件不能为空值。");
                    txtSoundFile.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTextFile.Text))
                {
                    MessageBox.Show("文字文件不能为空值。");
                    txtTextFile.Focus();
                    return;
                }
                rb = new ResourceBLL();
                Resource newResource = new Resource();
               // newResource.Type = (ResourceType)cmbResourceType.SelectedValue;
                newResource.ResourceId = Guid.NewGuid();
                newResource.StLevelId = levelstr;
                newResource.StTypeSupplyId = categorystr;
                newResource.StTypeId = subjects;
                newResource.Title = txtTitle.Text;
                newResource.Keyword = txtKeyword.Text;
                newResource.ContentFile = Path.GetFileName(txtContentFile.Text);
                newResource.SoundFile = Path.GetFileName(txtSoundFile.Text);
                newResource.TextFile = Path.GetFileName(txtTextFile.Text);
                newResource.UserId = Program.mf.LoginUser.UserId;
                string soundFilePath = Application.StartupPath + @"\Resources\SoundFiles\" + Path.GetFileName(SoundStr);
                if (!File.Exists(soundFilePath))
                    File.Copy(SoundStr, soundFilePath, true);
                string textFilePath = Application.StartupPath + @"\Resources\TextFiles\" + Path.GetFileName(TextStr);
                if (!File.Exists(textFilePath))
                    File.Copy(TextStr, textFilePath, true);  
                if (rb.AddCusResource(newResource))
                {
                    ClearControl();
                    this.Close();
                    if (ecrlf != null)
                    {
                        FreshData fd = new FreshData(ecrlf.ShowResourceList);//委托刷新素材列表
                        fd("", Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty);
                    }

                    if(Recrlf != null)
                    {
                        FresResourcehData frd = new FresResourcehData(Recrlf.BrowseWindow);
                        frd();
                    }

                }
                else
                {
                    MessageBox.Show("创建素材失败。");
                    txtTitle.Focus();
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //清空表单信息
        private void ClearControl()
        {
            txtTitle.Clear();
            txtKeyword.Clear();
            txtContentFile.Clear();
            txtSoundFile.Clear();
            txtTextFile.Clear();
            txtTitle.Focus();
        }

        //选择声音文件
        private void btnSoundFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "支持文件(*.mp3)|*.mp3";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSoundFile.Text = Path.GetFileName(openFileDialog1.FileName);
                SoundStr = openFileDialog1.FileName;
            }
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                long soundMaxFilesize = 15728640;//15mb
                if (fi.Length > soundMaxFilesize)
                {
                    MessageBox.Show("请选择小于" + soundMaxFilesize / 1024 / 1024 + "MB的文件。");
                    txtSoundFile.Clear();
                }
                rb = new ResourceBLL();
                if (rb.SoundFileResourceIs(Path.GetFileName(txtSoundFile.Text)))
                {
                    MessageBox.Show("文件已经存在，请修改此文件名后再试。");
                    txtSoundFile.Clear();
                }
            }
        }

        //选择文字文件
        private void btnTextFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "支持文件(*.html,*.htm)|*.html;*.htm";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTextFile.Text = Path.GetFileName(openFileDialog1.FileName);
                TextStr = openFileDialog1.FileName;
            }
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                long textMaxFilesize = 15728640;//15mb
                if (fi.Length > textMaxFilesize)
                {
                    MessageBox.Show("请选择小于" + textMaxFilesize / 1024 / 1024 + "MB的文件。");
                    txtTextFile.Clear();
                }
                rb = new ResourceBLL();
                if (rb.TextFileResourceIs(Path.GetFileName(txtTextFile.Text)))
                {
                    MessageBox.Show("文件已经存在，请修改此文件名后再试。");
                    txtTextFile.Clear();
                }
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
