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
    public partial class StClassFrom : Form
    {
        private StudentRepository sr;
        private StClassRepository sc;
        private ClassStudentRepository csr;
        public StClassFrom()
        {
            InitializeComponent();
            sr = new StudentRepository();
            sc = new StClassRepository();
            csr = new ClassStudentRepository();
        }

        private string ClassType = "";
        private void StClassFrom_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            panel2.BackColor = Color.FromArgb(52, 152, 186);
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;

            StClassList();

        }

        public void StClassList() 
        {
            if (Program.mf.LoginUser != null)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                IList<StClass> stclass = null;
                stclass = sc.ClassList();
                dataGridView1.DataSource = stclass;
                dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(174,207,222);//单元格颜色(淡灰色)
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198,225,234);//奇数单元格颜色(米黄色)
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns[0].HeaderText = " 学员ID";
                dataGridView1.Columns[1].HeaderText = "班级名字";
                dataGridView1.Columns[2].HeaderText = "班级状态";
                dataGridView1.Columns[3].HeaderText = "手机号";
                dataGridView1.Columns[4].HeaderText = "工作单位";
                dataGridView1.Columns["StClassId"].Visible = false;
                dataGridView1.Columns["UserId"].Visible = false;
                dataGridView1.Columns["CreateTime"].Visible = false;
                dataGridView1.Columns["User"].Visible = false;
                dataGridView1.Columns["ClassStudents"].Visible = false;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
                dgvButtonColEdit.Name = "ManageDetail";
                // 设置列标题
                dgvButtonColEdit.HeaderText = "修改";
                // 设置按钮标题
                dgvButtonColEdit.UseColumnTextForButtonValue = true;
                dgvButtonColEdit.Text = "修改";
                dgvButtonColEdit.Width = 35;
                dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
                dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
               
                DataGridViewButtonColumn dgvButtonColPub = new DataGridViewButtonColumn();
                // 设置列名
                dgvButtonColPub.Name = "ManagePub";
                // 设置列标题
                dgvButtonColPub.HeaderText = "派生";
                // 设置按钮标题
                dgvButtonColPub.UseColumnTextForButtonValue = true;
                dgvButtonColPub.Text = "派生";
                dgvButtonColPub.Width = 35;
                dgvButtonColPub.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
                dgvButtonColPub.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                dataGridView1.Columns.Insert(dataGridView1.Columns.Count, dgvButtonColEdit);
                dataGridView1.Columns.Insert(dataGridView1.Columns.Count, dgvButtonColPub);
            }
        }


        private List<Student> studengname = null;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
             string classname = (dataGridView1.Rows[e.RowIndex].Cells["ClassName"].Value.ToString());
             StClass studentIds = sc.ClassNumber(classname);
            // List<Student> studengname = new List<Student>();
             studengname = new List<Student>();
             IList<ClassStudent> studentnames = csr.students(studentIds.StClassId);
             foreach (ClassStudent a in studentnames)
             {
                studengname.Add(sr.students(a.StudentId));
             }
             dataGridView2.DataSource = null;
             dataGridView2.Columns.Clear();
             dataGridView2.DataSource = studengname;
             dataGridView2.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
             dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
             dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

             dataGridView2.Columns[0].HeaderText = " 学员ID";
             dataGridView2.Columns[1].HeaderText = "学员名字";
             dataGridView2.Columns[2].HeaderText = "学员工号";
             dataGridView2.Columns[3].HeaderText = "手机号";
             dataGridView2.Columns[4].HeaderText = "工作单位";
             dataGridView2.Columns["StudentId"].Visible = false;
             dataGridView2.Columns["CreateTime"].Visible = false;
             dataGridView2.Columns["Telephone"].Visible = false;
             dataGridView2.Columns["WorkingUnit"].Visible = false;
             dataGridView2.EnableHeadersVisualStyles = false;
             dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97,147,170);
             dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
             ClassType = dataGridView1.Rows[e.RowIndex].Cells["ClassState"].Value.ToString();
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ManageDetail")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["ClassState"].Value.ToString() == "启用")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["ClassState"].Value = "禁用";
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["ClassState"].Value = "启用";
                    }
                    if (sc.UpdateStype((Guid)dataGridView1.Rows[e.RowIndex].Cells["StClassId"].Value))
                    {
                        MessageBox.Show("班级状态已修改");
                    }
                    else
                    {
                        MessageBox.Show("班级状态修改失败");
                    }
                  
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ManagePub")
                {
                    if (studengname != null)
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["ClassState"].Value.ToString() == "启用")
                        {
                            AddClassForm cf = new AddClassForm();
                            cf.ulf = this;
                            cf.students = studengname;
                            cf.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("班级已禁用，若需引用请先修改班级状态");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择班级");
                    }
                }

              
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddClassForm cf = new AddClassForm();
            cf.ulf = this;
            cf.ShowDialog();
        }

      

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle, dataGridView1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView2.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView2.RowHeadersDefaultCellStyle.Font, rectangle, dataGridView2.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

    }
}
