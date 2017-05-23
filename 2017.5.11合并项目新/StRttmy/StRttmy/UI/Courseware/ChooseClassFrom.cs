using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI
{
    public partial class ChooseClassFrom : Form
    {
        public Guid coursewareId = Guid.Empty;
        private Guid ClassId = Guid.Empty;
        private StClassRepository sc;
        private CoursewareRepository cw;
        private StTypeRepository st;
        public ChooseClassFrom()
        {
            InitializeComponent();
            sc = new StClassRepository();
        }


        private CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
        private string format = "yyyy MM dd HH:mm ";
        private void ChooseClassFrom_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            //char kzmfgf = '.';
            //string[] kzm = DateTime.Now.ToString(format, cultureInfo).Split(kzmfgf);
            //textBox1.Text = kzm[0];
            //textBox2.Text = kzm[1];
            //textBox3.Text = kzm[2];
            //textBox4.Text = kzm[3];
            //textBox5.Text = kzm[4];
            label8.Text = DateTime.Now.ToString(format, cultureInfo);
            cw = new CoursewareRepository();
            st = new StTypeRepository();
            Model.Courseware subjectsId = cw.Find(coursewareId);
            label4.Text = subjectsId.Name;
            StType subjects = st.StType(subjectsId.StTypeId);
            label5.Text = subjects.StTime.ToString();
            richTextBox2.Text = subjects.Purpose;
            richTextBox1.Text = subjects.Content;
            List<String> _className = new List<string>();
            IList<StClass> ClassNames = null;
            ClassNames = sc.ClassList();
            //comboBox1.DataSource = _className;
            this.dataGridView1.DataSource = ClassNames;//列表绑定数据
            //隔行变色
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(232, 219, 210);//单元格颜色(淡灰色)
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 240, 236);//奇数单元格颜色(米黄色)
            //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
            SetDgv(ClassNames.Count());
        }

        
        //列表表头和操作按钮赋值及属性值
        private void SetDgv(int ListLenght)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dataGridView1.Columns[1].HeaderText = "班级名称";
            dataGridView1.Columns["StClassId"].Visible = false;
            dataGridView1.Columns["ClassState"].Visible = false;
            dataGridView1.Columns["User"].Visible = false;
            dataGridView1.Columns["UserId"].Visible = false;
            dataGridView1.Columns["ClassStudents"].Visible = false;
            dataGridView1.Columns["CreateTime"].Visible = false;
            DataGridViewCheckBoxColumn dtCheck = new DataGridViewCheckBoxColumn();
            dtCheck.DataPropertyName = "check";
            dtCheck.Name = "check";
            dtCheck.HeaderText = "";
            dataGridView1.Columns.Add(dtCheck);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text != "" || textBox1.Text != null)
            //{
            if (ClassIds.Count() > 0)
            {
                if (coursewareId != Guid.Empty)
                {
                    //MessageBox.Show(DateTime.ParseExact(label8.Text, format, cultureInfo) + "教学开始");
                    DialogResult dr = MessageBox.Show(DateTime.ParseExact(label8.Text, format, cultureInfo) + "教学开始", "提示", MessageBoxButtons.OKCancel);
                      if (dr == DialogResult.OK)
                      {
                          UI.CourseResourceForm crf = new CourseResourceForm();//查看内容
                          crf.coursewareId = coursewareId;
                          crf.classIds = ClassIds;
                          // string stringValue = textBox1.Text + "." + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text + "." + textBox5.Text; // 得到日期字符串
                          DateTime datetime = DateTime.ParseExact(label8.Text, format, cultureInfo); // 将字符串转换成日期
                          crf.startime = datetime;
                          crf.ulf = this;
                          crf.ShowDialog();
                      }
                }
            }
            else
            {
                MessageBox.Show("请先选择班级");
            }
            //}
            //else
            //{
            //    MessageBox.Show("开始时间不能为空");
            //}
        }
        List<Guid> ClassIds = new List<Guid>();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "check")
                {
                    ClassIds.Add((Guid)dataGridView1.Rows[e.RowIndex].Cells["StClassId"].Value);
                }
            }
        }
        public void closewindow()
        {
            this.Close();
        }
    }
}
