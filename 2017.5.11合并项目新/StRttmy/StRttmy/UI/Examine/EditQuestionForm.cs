using StRttmy.BLL;
using StRttmy.Common;
using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI
{
    public partial class EditQuestionForm : Form
    {
        public TestPapersListForm tplf = null;        
        private QuestionBLL questionBll = null;
        private PaperBLL paperBll = null;
        public List<StType> typeList = null;
        public List<StTypeSupply> typesupplyList = null;
        public List<StLevel> leveltypeList = null;
        public Guid testpaperId;
        DataGridViewCheckBoxColumn dgvCheckBoxs = new DataGridViewCheckBoxColumn();

        public EditQuestionForm()
        {
            InitializeComponent();
        }

        private void EditQuestionForm_Load(object sender, EventArgs e)
        {
            #region 试题类型ComboBox
            cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
            cmbQuestionType.DisplayMember = "Text";
            cmbQuestionType.ValueMember = "Value";
            #endregion
            ShowQIPList();
        }

        private void cmbQuestionType_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowQIPList();
        }

        private void cmbQuestionType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ShowQIPList();
        }

        private void dgvEPList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvEPList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvEPList.RowHeadersDefaultCellStyle.Font, rectangle, dgvEPList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            int count = 0;
            count = dgvEPList.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvEPList.Rows[i].Cells[0];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == false) //查找被选择的数据行
                {
                    checkCell.Value = true;
                }
                else
                {
                    continue;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int count = 0;
            count = dgvEPList.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgvEPList.Rows[i].Cells[0];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true) //查找被选择的数据行
                {
                    checkCell.Value = false;
                }
                else
                {
                    continue;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MSQuestionForm msqf=new MSQuestionForm();
            msqf.testpaperId = testpaperId;
            msqf.ShowDialog();
            ShowQIPList();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dgvEPList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkcell = (DataGridViewCheckBoxCell)this.dgvEPList.Rows[i].Cells[0];
                //Boolean flag = Convert.ToBoolean(checkcell.Value);
                if (checkcell.Value == null)
                {
                    count++;
                }
            }
            if (count >=dgvEPList.Rows.Count)
            {
                MessageBox.Show("请选择需要删除的试题！");
                return;
            }
            paperBll = new PaperBLL();
            List<ExamPaper> epList = new List<ExamPaper>();
            foreach (DataGridViewRow item in dgvEPList.Rows)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)item.Cells[0];
                bool flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    ExamPaper newexampaper = new ExamPaper();
                    newexampaper.ExamPaperId = Guid.NewGuid();
                    newexampaper.TestPaperId = testpaperId;
                    newexampaper.TestQuestionId = new Guid(item.Cells[1].Value.ToString());
                    epList.Add(newexampaper);
                }
            }
            paperBll.DeleteExamQuestion(epList);
            MessageBox.Show("试题删除成功！");
            ShowQIPList();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("试卷修改成功！");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowQIPList()
        {
            if (Program.mf.LoginUser != null)
            {
                dgvEPList.DataSource = null;
                dgvEPList.Columns.Clear();
                questionBll = new QuestionBLL();
                IList<TestQuestion> questionFindList = new List<TestQuestion>();
                int questiontype;
                questiontype = Convert.ToInt32(cmbQuestionType.SelectedValue.ToString());
                questionFindList = questionBll.QuestioninPaperList(testpaperId, questiontype);
                dgvEPList.DataSource = questionFindList;
                //隔行变色
                dgvEPList.RowsDefaultCellStyle.BackColor = Color.FromArgb(174, 207, 222);//单元格颜色(淡灰色)
                dgvEPList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(198, 225, 234);//奇数单元格颜色(米黄色)
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

        private void SetDgv()
        {
            dgvEPList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEPList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvEPList.Columns[0].HeaderText = "试题Id";
            dgvEPList.Columns[1].HeaderText = "试题";
            dgvEPList.Columns[2].HeaderText = "试题类型";
            dgvEPList.Columns[10].HeaderText = "答案A";
            dgvEPList.Columns[11].HeaderText = "答案B";
            dgvEPList.Columns[12].HeaderText = "答案C";
            dgvEPList.Columns[13].HeaderText = "答案D";
            dgvEPList.Columns[9].HeaderText = "本题分数";
            dgvEPList.Columns[14].HeaderText = "正确答案";
            dgvEPList.Columns["TestQuestionId"].Visible = false;
            dgvEPList.Columns["StTypeId"].Visible = false;
            dgvEPList.Columns["StType"].Visible = false;
            dgvEPList.Columns["StTypeSupplyId"].Visible = false;
            dgvEPList.Columns["StLevelId"].Visible = false;
            dgvEPList.Columns["ResourceId"].Visible = false;
            dgvEPList.Columns["UserId"].Visible = false;
            dgvEPList.Columns["CreateTime"].Visible = false;
            dgvEPList.Columns.Insert(0, dgvCheckBoxs);
            dgvCheckBoxs.HeaderText = "选择";
            dgvEPList.EnableHeadersVisualStyles = false; 
            dgvEPList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvEPList.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void cmbQuestionType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(QuestionSelectItem.TypeSelectList[e.Index].Text, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(QuestionSelectItem.TypeSelectList[e.Index].Text, cmbQuestionType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbQuestionType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbQuestionType);
        }        
    }
}
