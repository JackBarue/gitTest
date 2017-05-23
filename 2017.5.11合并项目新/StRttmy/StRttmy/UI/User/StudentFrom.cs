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
    public partial class StudentFrom : Form
    {
        private StudentRepository sr;
        public StudentFrom()
        {
            InitializeComponent();
            
        }
      
        private void StudentFrom_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            StudengList();
        }
        public void StudengList() 
        {
            if (Program.mf.LoginUser != null)
            {
                sr = new StudentRepository();
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                IList<Student> studengts = null;
                studengts = sr.studentsList();
                dataGridView1.DataSource = studengts;
                dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridView1.Columns[0].HeaderText = " 学员ID";
                dataGridView1.Columns[1].HeaderText = " 登录名";
                dataGridView1.Columns[2].HeaderText = "登录密码";
                dataGridView1.Columns[3].HeaderText = "学员名字";
                dataGridView1.Columns[4].HeaderText = "学员工号";
                dataGridView1.Columns[5].HeaderText = "手机号";
                dataGridView1.Columns[6].HeaderText = "工作单位";
                dataGridView1.Columns["StudentId"].Visible = false;
                dataGridView1.Columns["LoginName"].Visible = false;
                dataGridView1.Columns["Password"].Visible = false;
                dataGridView1.Columns["CreateTime"].Visible = false;
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
                dgvButtonColEdit.Width = 50;
                dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
                dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                DataGridViewButtonColumn dgvButtonColDel = new DataGridViewButtonColumn();
                // 设置列名
                dgvButtonColDel.Name = "ManageDel";
                // 设置列标题
                dgvButtonColDel.HeaderText = "删除";
                // 设置按钮标题
                dgvButtonColDel.UseColumnTextForButtonValue = true;
                dgvButtonColDel.Text = "删除";
                dgvButtonColDel.Width = 68;
                dgvButtonColDel.HeaderCell.Style.BackColor = Color.FromArgb(97, 147, 170);
                dgvButtonColDel.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // 向DataGridView追加
                dataGridView1.Columns.Insert(dataGridView1.Columns.Count, dgvButtonColEdit);
                dataGridView1.Columns.Insert(dataGridView1.Columns.Count, dgvButtonColDel);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle, dataGridView1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "ManageDetail")
                {
                    DetailStudentForm dcf = new DetailStudentForm();//修改
                    dcf.upstudent = (Guid)dataGridView1.Rows[e.RowIndex].Cells["StudentId"].Value;
                    dcf.uplf = this;
                    dcf.ShowDialog();
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "ManageDel")
                {
                    Guid studentId = (Guid)dataGridView1.Rows[e.RowIndex].Cells["StudentId"].Value;//删除需要的id
                    DelCourse(studentId);
                }
            }
        }
        private ClassStudentRepository csr;
        //删除
        private void DelCourse(Guid studentId)
        {
            sr = new StudentRepository();
            csr = new ClassStudentRepository();
            DialogResult dr = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                if (csr.Exists(studentId))
                {
                    MessageBox.Show("该学员无法删除");
                }
                else
                {
                   
                    if ( sr.Delete(studentId))
                    {
                        MessageBox.Show("学员已删除。");
                        StudengList();
                    }
                    else
                    {
                        MessageBox.Show("学员删除失败。");
                    }
                }
               
            }
        }
        //添加学员
        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent ads = new AddStudent();
            ads.ulf = this;
            ads.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sr = new StudentRepository();
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            IList<Student> FindStudent = null;
            FindStudent = sr.FindStudentsList(textBox1.Text);
            dataGridView1.DataSource = FindStudent;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].HeaderText = " 学员ID";
            dataGridView1.Columns[1].HeaderText = " 登录名";
            dataGridView1.Columns[2].HeaderText = "登录密码";
            dataGridView1.Columns[3].HeaderText = "学员名字";
            dataGridView1.Columns[4].HeaderText = "学员工号";
            dataGridView1.Columns[5].HeaderText = "手机号";
            dataGridView1.Columns[6].HeaderText = "工作单位";
            dataGridView1.Columns["StudentId"].Visible = false;
            dataGridView1.Columns["LoginName"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;

            DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
            dgvButtonColEdit.Name = "ManageDetail";
            // 设置列标题
            dgvButtonColEdit.HeaderText = "修改";
            // 设置按钮标题
            dgvButtonColEdit.UseColumnTextForButtonValue = true;
            dgvButtonColEdit.Text = "修改";
            dgvButtonColEdit.Width = 50;
            dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewButtonColumn dgvButtonColDel = new DataGridViewButtonColumn();
            // 设置列名
            dgvButtonColDel.Name = "ManageDel";
            // 设置列标题
            dgvButtonColDel.HeaderText = "删除";
            // 设置按钮标题
            dgvButtonColDel.UseColumnTextForButtonValue = true;
            dgvButtonColDel.Text = "删除";
            dgvButtonColDel.Width = 68;
            dgvButtonColDel.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // 向DataGridView追加
            dataGridView1.Columns.Insert(dataGridView1.Columns.Count, dgvButtonColEdit);
            dataGridView1.Columns.Insert(dataGridView1.Columns.Count, dgvButtonColDel);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {           
            textBox1.SelectAll();
            textBox1.ForeColor = Color.FromArgb(100, 0, 0, 0);           
        }

      

    }
}
