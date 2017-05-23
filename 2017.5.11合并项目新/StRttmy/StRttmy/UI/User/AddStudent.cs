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
    public partial class AddStudent : Form
    {
        public StudentFrom ulf = null;
        public delegate void FreshData();
        private StudentRepository sr;
        public AddStudent()
        {
            InitializeComponent();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            AddSt();
        }
        private void AddSt() 
        {
            sr = new StudentRepository();
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("登录名不能为空。");
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
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("登录密码不能为空。");
                textBox5.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("学员名字不能为空。");
                textBox6.Focus();
                return;
            }
            if (sr.Exists(System.Int32.Parse(textBox2.Text)))
            {
                MessageBox.Show("此学员工号已经存在，请使用其他学员工号。");
                textBox2.Focus();
                return;
            }
            Student student = new Student();
            student.StudentId = Guid.NewGuid();
            student.LoginName = textBox1.Text;
            student.Password = Common.TextUtility.Sha256(textBox5.Text);
            student.Name = textBox6.Text;
            student.WorkNumber = System.Int32.Parse(textBox2.Text);
            student.Telephone = textBox3.Text;
            student.WorkingUnit = textBox4.Text;
            if (sr.Add(student))
            {
               
                ClearControl();
                if (ulf != null)
                {
                    FreshData fd = new FreshData(ulf.StudengList);
                    fd();
                }
            }
            else
            {
                MessageBox.Show("添加学生失败。");
                textBox1.Focus();
            }
        }
        private void ClearControl()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
