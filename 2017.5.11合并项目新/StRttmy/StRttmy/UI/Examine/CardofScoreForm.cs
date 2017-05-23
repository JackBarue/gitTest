using StRttmy.BLL;
using StRttmy.Common;
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
    public partial class CardofScoreForm : Form
    {
        private QuestionBLL questionBLL;
        private StudentRepository studentBLL;
        private PaperBLL paperBLL;
        public StudentPaperDetailForm spdf = null;
        public delegate void FreshData(int a);
        private int count;
        public int x = 0;
        public int y = 0;
        public int spdfHeight = 0;

        public CardofScoreForm()
        {
            InitializeComponent();
        }

        private void btnAnswerIndex_Click(object sender, EventArgs e)
        {
            //int index = Convert.ToInt32(((Button)sender).Tag);
            
            //spdf.index = index;
            //FreshData ads = new FreshData(spdf.Testload);
            //ads(index);
            //spdf.Show();
        
        }

        private void ControlShow()
        {
            questionBLL = new QuestionBLL();
            int btnIndex = 0;
            int btnNo = 1;
            int btnNumber = 0;
            int btnRow = 0;

            int lblIndex = 0;
            int lblNumber = 0;
            int lblRow = 0;
            count = questionBLL.Question(QuestionHelp.paperId).Count;
            if (count <= 30)
            {
                this.Size = new Size(561, 390);

                foreach (var item in questionBLL.Question(QuestionHelp.paperId))
                {
                    Button button = new Button();
                    if (btnNumber % 5 == 0 && btnNumber != 0)
                    {
                        btnRow++;
                    }
                    button.Name = item.TestQuestionId.ToString();
                    button.Size = new Size(35, 23);
                    button.Tag = btnIndex;
                    button.Text = btnNo.ToString();
                    button.Location = new Point(20 + btnNumber % 5 * 106, 53 + btnRow * 50);
                    button.Click += new System.EventHandler(this.btnAnswerIndex_Click);
                    btnIndex++;
                    btnNo++;
                    btnNumber++;

                    Label label = new Label();
                    if (lblNumber % 5 == 0 && lblNumber != 0)
                    {
                        lblRow++;
                    }
                    label.Size = new Size(41, 12);
                    label.Tag = lblIndex;
                    label.Location = new Point(61 + lblNumber % 5 * 106, 58 + lblRow * 50);
                    lblIndex++;
                    lblNumber++;
                    this.Controls.Add(button);
                    this.Controls.Add(label);
                }
            }
            else
            {
                this.Size = new Size(1080, 540);
                foreach (var item in questionBLL.Question(QuestionHelp.paperId))
                {
                    Button button = new Button();
                    if (btnNumber % 10 == 0 && btnNumber != 0)
                    {
                        btnRow++;
                    }
                    button.Name = item.TestQuestionId.ToString();
                    button.Size = new Size(35, 23);
                    button.Tag = btnIndex;
                    button.Text = btnNo.ToString();
                    button.Location = new Point(20 + btnNumber % 10 * 106, 53 + btnRow * 50);
                    button.Click += new System.EventHandler(this.btnAnswerIndex_Click);
                    btnIndex++;
                    btnNo++;
                    btnNumber++;

                    Label label = new Label();
                    if (lblNumber % 10 == 0 && lblNumber != 0)
                    {
                        lblRow++;
                    }
                    label.Size = new Size(41, 12);
                    label.Tag = lblIndex;
                    label.Location = new Point(61 + lblNumber % 10 * 106, 58 + lblRow * 50);
                    lblIndex++;
                    lblNumber++;
                    this.Controls.Add(button);
                    this.Controls.Add(label);
                }
            }
        }

        private void CardofScoreForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(x, spdfHeight+80);
            ControlShow();
            int index = 0;
            foreach (Control item in this.Controls)
            {
                if (item is Label)
                {
                    index = Convert.ToInt32(item.Tag);
                    if (index != -1)
                    {
                        questionBLL = new QuestionBLL();
                        string questiontype = questionBLL.Find(QuestionHelp.SelectQuestionId[index]).QuestionType.ToString();
                        
                        if ((questiontype == "简答题" || questiontype == "论述题") && QuestionHelp.ScoreInfo[index] == "未评分")
                        {
                            item.Text = "未评分";
                        }
                        else
                        {
                            item.Text = QuestionHelp.Score[index].ToString() + "分";
                        }
                    }
                }
            }
        }
    }
}
