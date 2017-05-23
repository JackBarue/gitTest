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
    
    public partial class AddClassForm : Form
    {
        private StudentRepository sr;
        public StClassFrom ulf = null;
        public delegate void FreshData();
        public List<Student> students = null;
        public AddClassForm()
        {
            InitializeComponent();
            sr = new StudentRepository();
        }

        private void AddClassForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            checkedListBox1.BackColor = Color.FromArgb(198, 225, 234);
            checkedListBox2.BackColor = Color.FromArgb(198, 225, 234);
            checkedListBox1.ForeColor = Color.White;
            checkedListBox2.ForeColor = Color.White;
            if (students == null)
            {
                Students();
            }
            else
            {
                Students();
                ReferenceStudents();
            }
            
        }
        private void Students() 
        {
            IList<Student> studentname = sr.studentsList();
            foreach (Student a in studentname)
            {
                checkedListBox1.Items.Add(a.Name + "(" + a.WorkNumber + ")");
                
            }
        }

        private void ReferenceStudents()
        {
            foreach (Student a in students)
            {
                checkedListBox2.Items.Add(a.Name + "(" + a.WorkNumber + ")");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择 左边 的项目！");
                return;
            }
            List<string> Addname = new List<string>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    Addname.Add(checkedListBox1.Items[i].ToString());                    
                }
            }

            for (int j = 0; j < Addname.Count; j++) 
            {
                checkedListBox2.Items.Add(Addname[j]);
                checkedListBox1.Items.Remove(Addname[j]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkedListBox2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择 右边 的项目！");
                return;
            }
            List<string> Removename = new List<string>();
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    Removename.Add(checkedListBox2.Items[i].ToString());
                }
            }

            for (int j = 0; j < Removename.Count; j++)
            {
                checkedListBox1.Items.Add(Removename[j]);
                checkedListBox2.Items.Remove(Removename[j]);
            }
           
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex != -1)
            {
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }
        }
        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (checkedListBox2.SelectedIndex != -1)
            {               
                checkedListBox2.SetItemChecked(checkedListBox2.SelectedIndex, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("班级名字不能为空");
                textBox1.Focus();
                return;
            }
            if (checkedListBox2.Items.Count <= 0)  
            {
                MessageBox.Show("学员不能为空。");
                textBox1.Focus();
                return;
            }
            StClassRepository sc = new StClassRepository();
            StudentRepository st = new StudentRepository();
            ClassStudentRepository cs = new ClassStudentRepository();
            if (sc.Exists(textBox1.Text))
            {
                MessageBox.Show("班级已存在请更换班级名称");
                textBox1.Focus();
                return;
            }
            Guid classId = Guid.Empty;
            StClass newclass = new StClass();
            newclass.ClassName = textBox1.Text;
            newclass.StClassId = classId = Guid.NewGuid();
            newclass.UserId = Program.mf.LoginUser.UserId;
            newclass.ClassState = "启用";
            newclass.CreateTime = System.DateTime.Now;
            if (sc.Add(newclass))
            {
                if (classId != Guid.Empty)
                {
                   
                    char kzmfgf = ')';                   
                    foreach(string a in checkedListBox2.Items)
                    {
                        string[] kzm = a.Split(kzmfgf);
                        int wn =System.Int32.Parse(kzm[0].Substring(kzm[0].LastIndexOf("(")+1));
                        if (wn > 0) 
                        {
                            ClassStudent classstudent = new ClassStudent();
                            classstudent.ClassStudentId = Guid.NewGuid();
                            classstudent.StClassId = classId;
                            classstudent.StudentId = st.FindStudentId(wn);
                            if (!cs.Add(classstudent))
                            {
                                MessageBox.Show("添加用户出错");
                            }
                        }
                    }
                }
                ClearControl();
                if (ulf != null)
                {
                    FreshData fd = new FreshData(ulf. StClassList);
                    fd();
                    this.Close();
                }

            }
            
        }
        private void ClearControl()
        {
            textBox1.Clear();
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
        }
    }
}
