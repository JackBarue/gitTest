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
    public partial class ATQuestionForm : Form
    {
        public Guid testpaperId;
        public QuestionBLL questionBll = null;
        public PaperBLL paperBll = null;
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;        
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private delegate void FreshData();
        private int num = 0;
        private double score = 0;
        private List<TestQuestion> testquestionList;
        private Guid sysid = Guid.Empty;
        private Guid workid = Guid.Empty;
        private Guid genreid = Guid.Empty;
        private Guid levelid = Guid.Empty;
        private Guid subjectid = Guid.Empty;

        public ATQuestionForm()
        {
            InitializeComponent();
        }

        private void ATQuestionForm_Load(object sender, EventArgs e)
        {
            WindowShow();
        }

        private void WindowShow()
        {
            #region 系统ComboBox
            Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
            comboxShow = new ComboBoxShow();
            typeList = comboxShow.ThreeTypecmbShow(gid);
            cmbSystem.DataSource = typeList;
            cmbSystem.DisplayMember = "Name";
            cmbSystem.ValueMember = "StTypeId";
            #endregion

            #region 类别ComboBox
            comboxShow = new ComboBoxShow();
            typesupplyList = comboxShow.GenrecmbShow();
            cmbGenre.DataSource = typesupplyList;
            cmbGenre.DisplayMember = "StTypeName";
            cmbGenre.ValueMember = "StTypeSupplyId";
            #endregion

            #region 等级ComboBox
            comboxShow = new ComboBoxShow();
            leveltypeList = comboxShow.LevelcmbShow();
            cmbLevel.DataSource = leveltypeList;
            cmbLevel.DisplayMember = "StLevelName";
            cmbLevel.ValueMember = "StLevelId";
            #endregion
        }

        private void cmbSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            StType sysitem = cmbSystem.SelectedItem as StType;
            comboxShow = new ComboBoxShow();
            typeofworkList = new List<StType>();
            typeofworkList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
            cmbTypeofWork.DataSource = typeofworkList;
            cmbTypeofWork.DisplayMember = "Name";
            cmbTypeofWork.ValueMember = "StTypeId";
        }

        private void cmbTypeofWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            StType subitem = cmbTypeofWork.SelectedItem as StType;
            comboxShow = new ComboBoxShow();
            subjecttypeList = new List<StType>();
            subjecttypeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
            cmbSubject.DataSource = subjecttypeList;
            cmbSubject.DisplayMember = "Name";
            cmbSubject.ValueMember = "StTypeId";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int chkCount = 0;
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    if ((c as CheckBox).Checked == false)
                    {
                        chkCount++;
                    }
                }
            }
            if (chkCount > 3)
            {
                MessageBox.Show("请选择至少一种题型进行自动生成！");
                return;
            }
            Add();
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Add()
        {
            testquestionList = new List<TestQuestion>();
            #region 系统
            if (new Guid(cmbSystem.SelectedValue.ToString()) != Guid.Empty)
            {
                sysid = new Guid(cmbSystem.SelectedValue.ToString());
                //选择题
                if (chkChoice.Checked == true)
                {
                    Choice();
                }
                //判断题
                if (chkJudge.Checked == true)
                {
                    Judge();
                }
                //简答题
                if (chkSAQ.Checked == true)
                {
                    SAQ();
                }
                //论述题
                if (chkEssay.Checked == true)
                {
                    Essay();
                }
            }
            #endregion
            #region 工种
            else if (new Guid(cmbTypeofWork.SelectedValue.ToString()) != Guid.Empty)
            {
                workid = new Guid(cmbTypeofWork.SelectedValue.ToString());
                //选择题
                if (chkChoice.Checked == true)
                {
                    Choice();
                }
                //判断题
                if (chkJudge.Checked == true)
                {
                    Judge();
                }
                //简答题
                if (chkSAQ.Checked == true)
                {
                    SAQ();
                }
                //论述题
                if (chkEssay.Checked == true)
                {
                    Essay();
                }
            }
            #endregion
            #region 类别
            else if (new Guid(cmbGenre.SelectedValue.ToString()) != Guid.Empty)
            {
                genreid = new Guid(cmbGenre.SelectedValue.ToString());
                //选择题
                if (chkChoice.Checked == true)
                {
                    Choice();
                }
                //判断题
                if (chkJudge.Checked == true)
                {
                    Judge();
                }
                //简答题
                if (chkSAQ.Checked == true)
                {
                    SAQ();
                }
                //论述题
                if (chkEssay.Checked == true)
                {
                    Essay();
                }
            }
            #endregion
            #region 等级
            else if (new Guid(cmbLevel.SelectedValue.ToString()) != Guid.Empty)
            {
                levelid = new Guid(cmbLevel.SelectedValue.ToString());
                //选择题
                if (chkChoice.Checked == true)
                {
                    Choice();
                }
                //判断题
                if (chkJudge.Checked == true)
                {
                    Judge();
                }
                //简答题
                if (chkSAQ.Checked == true)
                {
                    SAQ();
                }
                //论述题
                if (chkEssay.Checked == true)
                {
                    Essay();
                }
            }
            #endregion
            #region 科目
            else if (new Guid(cmbSubject.SelectedValue.ToString()) != Guid.Empty)
            {
                subjectid = new Guid(cmbSubject.SelectedValue.ToString());
                //选择题
                if (chkChoice.Checked == true)
                {
                    Choice();
                }
                //判断题
                if (chkJudge.Checked == true)
                {
                    Judge();
                }
                //简答题
                if (chkSAQ.Checked == true)
                {
                    SAQ();
                }
                //论述题
                if (chkEssay.Checked == true)
                {
                    Essay();
                }
            }
            #endregion
            #region 其他
            else
            {
                //选择题
                if (chkChoice.Checked == true)
                {
                    Choice();
                }
                //判断题
                if (chkJudge.Checked == true)
                {
                    Judge();
                }
                //简答题
                if (chkSAQ.Checked == true)
                {
                    SAQ();
                }
                //论述题
                if (chkEssay.Checked == true)
                {
                    Essay();
                }
            }
            #endregion
            if (paperBll.ATAddExamPaper(testquestionList, testpaperId))
            {
                MessageBox.Show("试题选择成功！");
            }
            else
            {
                MessageBox.Show("试题选择失败！");
            }
        }

        /// <summary>
        /// 选择题方法
        /// </summary>
        private void Choice()
        {
            paperBll = new PaperBLL();
            if (!string.IsNullOrEmpty(txtChoiceCount.Text) && !string.IsNullOrEmpty(txtChoiceScore.Text))
            {
                num = Convert.ToInt32(txtChoiceCount.Text);
                score = Convert.ToDouble(txtChoiceScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 1, num, score));
            }
            else if (!string.IsNullOrEmpty(txtChoiceCount.Text))
            {
                num = Convert.ToInt32(txtChoiceCount.Text);
                score = 0;
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 1, num, score));
            }
            else if (!string.IsNullOrEmpty(txtChoiceScore.Text))
            {
                num = 0;
                score = Convert.ToDouble(txtChoiceScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 1, num, score));
            }
        }
        /// <summary>
        /// 判断题方法
        /// </summary>
        private void Judge()
        {
            paperBll = new PaperBLL();
            if (!string.IsNullOrEmpty(txtJudgeCount.Text) && !string.IsNullOrEmpty(txtJudgeScore.Text))
            {
                num = Convert.ToInt32(txtJudgeCount.Text);
                score = Convert.ToDouble(txtJudgeScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 2, num, score));
            }
            else if (!string.IsNullOrEmpty(txtJudgeCount.Text))
            {
                num = Convert.ToInt32(txtJudgeCount.Text);
                score = 0;
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 2, num, score));
            }
            else if (!string.IsNullOrEmpty(txtJudgeScore.Text))
            {
                num = 0;
                score = Convert.ToDouble(txtJudgeScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 2, num, score));
            }
        }
        /// <summary>
        /// 简答题方法
        /// </summary>
        private void SAQ()
        {
            paperBll = new PaperBLL();
            if (!string.IsNullOrEmpty(txtSAQCount.Text) && !string.IsNullOrEmpty(txtSAQScore.Text))
            {
                num = Convert.ToInt32(txtSAQCount.Text);
                score = Convert.ToDouble(txtSAQScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 3, num, score));
            }
            else if (!string.IsNullOrEmpty(txtSAQCount.Text))
            {
                num = Convert.ToInt32(txtSAQCount.Text);
                score = 0;
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 3, num, score));
            }
            else if (!string.IsNullOrEmpty(txtSAQScore.Text))
            {
                num = 0;
                score = Convert.ToDouble(txtSAQScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 3, num, score));
            }
        }
        /// <summary>
        /// 论述题方法
        /// </summary>
        private void Essay()
        {
            paperBll = new PaperBLL();
            if (!string.IsNullOrEmpty(txtEssayCount.Text) && !string.IsNullOrEmpty(txtEssayScore.Text))
            {
                num = Convert.ToInt32(txtEssayCount.Text);
                score = Convert.ToDouble(txtEssayScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 4, num, score));
            }
            else if (!string.IsNullOrEmpty(txtEssayCount.Text))
            {
                num = Convert.ToInt32(txtEssayCount.Text);
                score = 0;
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 4, num, score));
            }
            else if (!string.IsNullOrEmpty(txtEssayScore.Text))
            {
                num = 0;
                score = Convert.ToDouble(txtEssayScore.Text);
                testquestionList.AddRange(paperBll.GetTestQuestions(sysid, workid, genreid, levelid, subjectid, 4, num, score));
            }
        }

        private void cmbSystem_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(typeList[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(typeList[e.Index].Name, cmbSystem, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbSystem_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbSystem);
        }

        private void cmbTypeofWork_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(typeofworkList[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(typeofworkList[e.Index].Name, cmbTypeofWork, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbTypeofWork_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbTypeofWork);
        }

        private void cmbGenre_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(typesupplyList[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(typesupplyList[e.Index].StTypeName, cmbGenre, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbGenre_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbGenre);
        }

        private void cmbLevel_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(leveltypeList[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(leveltypeList[e.Index].StLevelName, cmbLevel, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbLevel_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbLevel);
        }

        private void cmbSubject_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(subjecttypeList[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(subjecttypeList[e.Index].Name, cmbSubject, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbSubject_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbSubject);
        }
    }
}
