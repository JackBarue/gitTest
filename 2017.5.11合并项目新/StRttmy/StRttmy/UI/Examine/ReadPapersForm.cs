using StRttmy.BLL;
using StRttmy.Common;
using StRttmy.Model;
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
    public partial class ReadPapersForm : Form
    {
        private PaperBLL paperBll;
        private QuestionBLL questionBLL;
        private int count;

        public ReadPapersForm()
        {
            InitializeComponent();
        }

        private void ReadPapersForm_Load(object sender, EventArgs e)
        {
            ShowReadPapersList();
        }

        public void ShowReadPapersList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvStudentPaperList.DataSource = null;
                dgvStudentPaperList.Columns.Clear();
                paperBll = new PaperBLL();
                IList<ReadPapersClass> stydentpaperList = new List<ReadPapersClass>();
                string keyword = txtKeyword.Text;
                if(keyword==string.Format(@"试卷\班级名\学员姓名"))
                {
                    keyword="";
                    stydentpaperList = paperBll.ReadStudentPaper(keyword, Program.mf.LoginUser.UserId);
                }
                else
                {
                    stydentpaperList = paperBll.ReadStudentPaper(keyword, Program.mf.LoginUser.UserId);
                }
                dgvStudentPaperList.DataSource = stydentpaperList;
                //隔行变色
                dgvStudentPaperList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvStudentPaperList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        public void SetDgv()
        {
            dgvStudentPaperList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvStudentPaperList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvStudentPaperList.Columns[0].HeaderText = "学生试卷Id"; 
            dgvStudentPaperList.Columns[1].HeaderText = "试卷名称";
            dgvStudentPaperList.Columns[2].HeaderText = "班级名称";
            dgvStudentPaperList.Columns[3].HeaderText = "学生姓名";
            dgvStudentPaperList.Columns[4].HeaderText = "试卷状态";
            dgvStudentPaperList.Columns["StudentExamPaperId"].Visible = false;
            dgvStudentPaperList.EnableHeadersVisualStyles = false;
            dgvStudentPaperList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvStudentPaperList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        public void SetAllQuestionId(Guid paperId)
        {
            int i = 0;
            int index = 0;
            questionBLL = new QuestionBLL();
            foreach (var item in questionBLL.Question(paperId))
            {
                QuestionHelp.AllQuestionId[i] = (Guid)item.TestQuestionId;
                QuestionHelp.SelectQuestionId[index] = QuestionHelp.AllQuestionId[i];
                index++;
                i++;
            }

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowReadPapersList();
        }

        private void txtKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Text = "";
            txtKeyword.ForeColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void dgvStudentPaperList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                paperBll = new PaperBLL();
                TestStateType state = paperBll.SearchState(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestState;
                if (state == TestStateType.未批卷)
                {
                    NewTestingForm ntf = new NewTestingForm();
                    ntf.judge = 3;
                    ntf.rpf = this;
                    ntf.testpaperId = paperBll.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestPaperId;
                    ntf.studentid = paperBll.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).StudentId;
                    ntf.ShowDialog();
                }
                if (state == TestStateType.已批卷)
                {
                    NewTestingForm ntf = new NewTestingForm();
                    ntf.judge = 4;
                    ntf.rpf = this;
                    ntf.testpaperId = paperBll.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).TestPaperId;
                    ntf.studentid = paperBll.MatchingStuPaper(new Guid(dgvStudentPaperList.Rows[e.RowIndex].Cells["StudentExamPaperId"].Value.ToString())).StudentId;
                    ntf.ShowDialog();
                }
            }
        }
    }
}
