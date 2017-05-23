using StRttmy.BLL;
using StRttmy.Model;
using StRttmy.Repository;
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
    public partial class AddAlltType : Form
    {
        public AddAlltType()
        {
            InitializeComponent();
        }
        public StType subjects = null;
        public StType TypeOfWork = null;
        public StType SystemType = null;
        public StLevel levelstr = null;
        public StTypeSupply categorystr = null;
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        private void AddAlltType_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            if (SystemType != null)
            {
                textBox1.Text = SystemType.Name;
            }
            if (TypeOfWork != null)
            {
                textBox2.Text = TypeOfWork.Name;
            }
            if (subjects != null)
            {
                textBox3.Text = subjects.Name;
                textBox6.Text = subjects.StTime.ToString();
                textBox7.Text = subjects.Purpose;
                richTextBox1.Text = subjects.Content;
            }
            if (levelstr != null)
            {
                textBox4.Text = levelstr.StLevelName;
            }
            if (categorystr != null)
            {
                textBox5.Text = categorystr.StTypeName;
            }
        }

        private void TypeControl() 
        {
            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();

            Guid typeofwork = Guid.Empty;
            Guid subjectss = Guid.Empty;
         if (!string.IsNullOrEmpty(textBox1.Text))
            {
                //if (st.Exists(textBox1.Text))
                //{
                //    MessageBox.Show("此系统已存在。");
                //    textBox1.Focus();
                //    return;
                //}
                StType sttype = new StType();
                sttype.StTypeId = typeofwork = Guid.NewGuid();
                sttype.Fid = Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee");
                sttype.UserId = Guid.Empty;
                sttype.Name = textBox1.Text;//Exists
                sttype.ReferenceType = 0;
                sttype.CreateTime = System.DateTime.Now;
                st.Add(sttype);
            }

            if (!string.IsNullOrEmpty(textBox2.Text))
            {               
                StType sttype1 = new StType();
                sttype1.StTypeId = subjectss = Guid.NewGuid();
                sttype1.Fid = typeofwork;
                sttype1.UserId = Guid.Empty;
                sttype1.Name = textBox2.Text;//Exists
                sttype1.ReferenceType = 0;
                sttype1.CreateTime = System.DateTime.Now;
                st.Add(sttype1);
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {              
                StType sttype2 = new StType();
                sttype2.StTypeId  = Guid.NewGuid();
                sttype2.Fid = subjectss;
                sttype2.UserId = Program.mf.LoginUser.UserId;
                sttype2.Name = textBox3.Text;//Exists
                sttype2.ReferenceType = 0;
                sttype2.CreateTime = System.DateTime.Now;
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    sttype2.StTime = Convert.ToDouble(textBox6.Text);
                }
                if (!string.IsNullOrEmpty(textBox7.Text))
                {
                    sttype2.Purpose = textBox7.Text;
                }
                if (!string.IsNullOrEmpty(richTextBox1.Text))
                {
                    sttype2.Content = richTextBox1.Text;
                }
                st.Add(sttype2);
            }

           


            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                if (sl.Exists(textBox4.Text))
                {
                    MessageBox.Show("此类型已存在。");
                    textBox4.Focus();
                    return;
                }
                StLevel stlevel = new StLevel();
                stlevel.StLevelId = Guid.NewGuid();
                stlevel.StLevelName = textBox4.Text;
                stlevel.UserId = Program.mf.LoginUser.UserId;
                stlevel.CreateTime = System.DateTime.Now;                
                sl.Add(stlevel);
            }

            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                if (sts.Exists(textBox5.Text))
                {
                    MessageBox.Show("此类型已存在。");
                    textBox5.Focus();
                    return;
                }
                StTypeSupply sttypesupply = new StTypeSupply();
                sttypesupply.StTypeSupplyId = Guid.NewGuid();
                sttypesupply.StTypeName = textBox5.Text;
                sttypesupply.UserId = Program.mf.LoginUser.UserId;
                sttypesupply.CreateTime = System.DateTime.Now;
                sts.Add(sttypesupply);
            }
           
        }

        private void TypeEditor()
        {
            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();
            StTypeBLL stb = new StTypeBLL();
            if (SystemType != null)
            {
                Guid typeofworkFid = Guid.Empty;
                Guid subjectsFid = Guid.Empty;
                StType stsystem = new StType();
                stsystem.StTypeId = typeofworkFid = SystemType.StTypeId;
                stsystem.Name = textBox1.Text;
                stsystem.Fid = SystemType.Fid;
                stsystem.ReferenceType = SystemType.ReferenceType;
                stsystem.UserId = Guid.Empty;
                stsystem.CreateTime = SystemType.CreateTime;
                st.Update(stsystem);

                if (TypeOfWork != null)
                {
                    StType typeofwork = new StType();
                    typeofwork.StTypeId = subjectsFid = TypeOfWork.StTypeId;
                    typeofwork.Name = textBox2.Text;
                    typeofwork.Fid = TypeOfWork.Fid;
                    typeofwork.ReferenceType = TypeOfWork.ReferenceType;
                    typeofwork.UserId = Guid.Empty;
                    st.Update(typeofwork);
                }
                else if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    StType typeofwork = new StType();
                    typeofwork.StTypeId = subjectsFid = Guid.NewGuid();
                    typeofwork.Name = textBox2.Text;
                    typeofwork.Fid = typeofworkFid;
                    typeofwork.ReferenceType = 0;
                    typeofwork.UserId = Guid.Empty;
                    typeofwork.CreateTime = System.DateTime.Now;
                    if (string.IsNullOrEmpty(textBox3.Text))
                    {
                        MessageBox.Show("科目不能为空");
                        return;
                    }
                    else
                    {
                        st.Add(typeofwork);
                    }
                }


                if (subjects != null)
                {
                    StType subjectss = new StType();
                    subjectss.StTypeId = subjects.StTypeId;
                    subjectss.Name = textBox3.Text;
                    subjectss.Fid = subjects.Fid;
                    subjectss.ReferenceType = subjects.ReferenceType;
                    subjectss.UserId = Program.mf.LoginUser.UserId;
                    if (subjects.StTime != null)
                    {
                        subjectss.StTime = Convert.ToDouble(textBox6.Text);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(textBox6.Text))
                        {
                            subjectss.StTime = Convert.ToDouble(textBox6.Text);
                        }
                    }
                    if (subjects.Purpose != null)
                    {
                        subjectss.Purpose = textBox7.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(textBox7.Text))
                        {
                            subjectss.Purpose = textBox7.Text;
                        }
                    }
                    if (subjects.Content != null)
                    {
                        subjectss.Content = richTextBox1.Text;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(richTextBox1.Text))
                        {
                            subjectss.Content = richTextBox1.Text;
                        }
                    }
                    st.Update(subjectss);
                }
                else
                {
                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        StType subjectss = new StType();
                        subjectss.StTypeId = Guid.NewGuid();
                        subjectss.Name = textBox3.Text;
                        subjectss.Fid = subjectsFid;
                        subjectss.ReferenceType = 0;
                        subjectss.UserId = Program.mf.LoginUser.UserId;
                        subjectss.CreateTime = System.DateTime.Now;
                        if (!string.IsNullOrEmpty(textBox6.Text))
                        {
                            subjectss.StTime = Convert.ToDouble(textBox6.Text);
                        }
                        if (!string.IsNullOrEmpty(textBox7.Text))
                        {
                            subjectss.Purpose = textBox7.Text;
                        }
                        if (!string.IsNullOrEmpty(richTextBox1.Text))
                        {
                            subjectss.Content = richTextBox1.Text;
                        }
                        if (string.IsNullOrEmpty(textBox2.Text))
                        {
                            MessageBox.Show("工种不能为空");
                            return;
                        }
                        else
                        {
                            st.Add(subjectss);
                        }
                    }
                }
                //if (string.IsNullOrEmpty(textBox3.Text) || subjects == null || string.IsNullOrEmpty(textBox2.Text) || TypeOfWork == null)
                //{
                //    MessageBox.Show("科目与工种不能为空");
                //    return;
                //}            
            }
           
           
            if (levelstr != null)
            {
                StLevel leveltr = new StLevel();
                leveltr.StLevelId = levelstr.StLevelId;
                leveltr.StLevelName = textBox4.Text;
                leveltr.UserId = Program.mf.LoginUser.UserId;
                sl.Update(leveltr);
            }
            if (categorystr != null)
            {
                StTypeSupply categoriestr = new StTypeSupply();
                categoriestr.StTypeSupplyId = categorystr.StTypeSupplyId;
                categoriestr.StTypeName = textBox5.Text;
                categoriestr.UserId = Program.mf.LoginUser.UserId;
                sts.Update(categoriestr);
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (SystemType != null || TypeOfWork != null || subjects != null || levelstr != null || categorystr != null)
            {
                TypeEditor();
            }
            else
            {
                TypeControl();
            }
        }
    }
}
