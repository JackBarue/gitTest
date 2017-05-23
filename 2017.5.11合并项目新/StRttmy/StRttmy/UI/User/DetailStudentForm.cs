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
    public partial class DetailStudentForm : Form
    {
        public Guid upstudent = Guid.Empty;
        private StudentRepository sr;
        public delegate void FreshData();
        public StudentFrom uplf = null;
        public DetailStudentForm()
        {
            InitializeComponent();
            sr = new StudentRepository();
        }

      

        private void DetailStudentForm_Load(object sender, EventArgs e)
        {
            ShowStudent();
        }
        //初始化
        private void ShowStudent()
        {
            if (upstudent != Guid.Empty)
            {
                Student st = new Student();
                st = sr.students(upstudent);
                textBox1.Text = st.Name;
                textBox2.Text = st.WorkNumber.ToString();
                textBox3.Text = st.Telephone;
                textBox4.Text = st.WorkingUnit;
                 label7.Text = st.LoginName;
                textBox6.Text = "";
            }
            else
            {
                MessageBox.Show("此用户不存在。");
                Close();
            }
        }
        private void EditStudent()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("学员名字不能为空。");
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("学员工号不能为空。");
                textBox2.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("手机号不能为空。");
                textBox3.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("工作单位不能为空。");
                textBox4.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("登录密码不能为空。");
                textBox4.Focus();
                return;
            }
            if (upstudent != Guid.Empty)
            {
                Student newStudent = new Student();
                newStudent.StudentId = upstudent;
                newStudent.Name = textBox1.Text;
                newStudent.WorkNumber = System.Int32.Parse(textBox2.Text);
                newStudent.Telephone = textBox3.Text;
                newStudent.WorkingUnit = textBox4.Text;
                newStudent.LoginName = label7.Text;
                newStudent.Password = Common.TextUtility.Sha256(textBox6.Text);
                if (sr.Update(newStudent))
                {
                    MessageBox.Show("修改成功");
                    if (uplf != null)
                    {
                        FreshData fd = new FreshData(uplf.StudengList);
                        fd();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("修改失败");
                    textBox1.Focus();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditStudent();
        }

    }
}
