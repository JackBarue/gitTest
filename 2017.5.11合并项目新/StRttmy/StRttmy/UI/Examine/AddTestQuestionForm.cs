using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.Model;
using StRttmy.BLL;
using StRttmy.Common;

namespace StRttmy.UI
{
    public partial class AddTestQuestionForm : Form
    {
        private QuestionBLL questionBll = null;
        private PaperBLL paperBll = null;
        public Guid testquestionId = Guid.Empty;
        public Guid resourceId = Guid.Empty;
        public Guid newtestquestionId = Guid.NewGuid();
        public Guid paperId = Guid.Empty;
        public int PARM = 0X00;
        public int pageCurrent = 0;
        public string oldkeyword = "";
        public Guid oldsysid = Guid.Empty;
        public Guid oldworkid = Guid.Empty;
        public Guid oldgenreid = Guid.Empty;
        public Guid oldlevelid = Guid.Empty;
        public Guid oldsubjectid = Guid.Empty;
        public NewEditQuestionForm neqf = null;
        public NewTestQuestionListForm ntqlf = null;
        public NewAddResourceQuestionForm narqf = null;
        private ComboBoxShow comboxShow = null;
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private IList<StType> TypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private delegate void FreshQData(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent);
        private delegate void FreshPData();
        private delegate void FreshRQData(string keyword, Guid sysid, Guid workid, Guid genreid, Guid levelid, Guid subjectid, int pageSize, int pageCurrent, Guid resourceid);
        private DateTime oldCreateTime;

        public AddTestQuestionForm()
        {
            InitializeComponent();
        }
        //窗体加载
        private void AddTestQuestionForm_Load(object sender, EventArgs e)
        {
            WinLoadJudgement();
        }

        //新建试题
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTestQuestion();
        }

        //选择正确答案
        private void cmbCorrect_MouseClick(object sender, MouseEventArgs e)
        {
            List<string> correctList = new List<string>();
            int cmbIndex = 4;
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    string txtTag = "" + c.Tag;
                    if (!string.IsNullOrEmpty(txtTag) && !string.IsNullOrEmpty(c.Text))
                    {
                        cmbIndex--;
                        correctList.Add(c.Tag.ToString());
                        correctList.Sort();
                    }
                }
            }
            cmbCorrect.DataSource = correctList;
        }

        //系统ComboBox联动工种ComboBox
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

        //工种ComboBox联动科目ComboBox
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

        //表单提交
        private void AddTestQuestion()
        {
            #region 修改
            if (PARM == 0)
            {
                if (Program.mf.LoginUser != null)
                {
                    if (cmbSystem.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个系统！");
                        cmbSystem.Focus();
                        return;
                    }
                    if (cmbTypeofWork.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个工种！");
                        cmbTypeofWork.Focus();
                        return;
                    }
                    if (cmbSubject.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个科目！");
                        cmbSubject.Focus();
                        return;
                    }
                    if (Convert.ToInt32(cmbQuestionType.SelectedValue) < 0)
                    {
                        MessageBox.Show("操作失败，试题类型不允许为空，请选择试题类型！", "选择试题类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbQuestionType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuestion.Text))
                    {
                        MessageBox.Show("操作失败，试题不允许为空，请录入试题！", "录入试题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }

                    // 试题录入答案计算
                    int txtCount = 0;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag))
                            {
                                if (string.IsNullOrEmpty(c.Text))
                                {
                                    txtCount++;
                                }
                            }
                        }
                    }

                    if (cmbQuestionType.SelectedIndex == 1 || cmbQuestionType.SelectedIndex == 2)
                    {
                        if (txtCount > 2)
                        {
                            MessageBox.Show("操作失败，请录入至少2个答案！","录入答案",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }

                        if (cmbCorrect.SelectedIndex < 0)
                        {
                            MessageBox.Show("操作失败，请选择正确答案！","选择答案",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            cmbCorrect.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtScore.Text))
                    {
                        MessageBox.Show("操作失败，请录入本题分数！","录入分数",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtScore.Focus();
                        return;
                    }
                    //试题信息
                    TestQuestion newTestQuestion = new TestQuestion();
                    newTestQuestion.TestQuestionId = testquestionId;//试题Id
                    newTestQuestion.Question = txtQuestion.Text;//试题
                    newTestQuestion.QuestionType = (TestQuestionType)cmbQuestionType.SelectedValue;//类型
                    Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                    newTestQuestion.StTypeId = sttypeid;//科目
                    Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                    newTestQuestion.StTypeSupplyId = typesupplyid;//类别
                    Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                    newTestQuestion.StLevelId = stlevelid;//等级
                    newTestQuestion.UserId = Program.mf.LoginUser.UserId;//创建教师
                    newTestQuestion.Score = Convert.ToDouble(txtScore.Text);//分数                
                    newTestQuestion.AnswerA = txtAnswerA.Text;//答案
                    newTestQuestion.AnswerB = txtAnswerB.Text;
                    newTestQuestion.AnswerC = txtAnswerC.Text;
                    newTestQuestion.AnswerD = txtAnswerD.Text;
                    newTestQuestion.Correct = cmbCorrect.Text;//正确答案
                    newTestQuestion.CreateTime = oldCreateTime;
                    questionBll = new QuestionBLL();
                    TestQuestion testQ = new TestQuestion();
                    testQ = questionBll.MatchingQuestion(txtQuestion.Text, newTestQuestion.TestQuestionId);
                    if (testQ != null)
                    {
                        MessageBox.Show("操作失败，试题已存在，请检查试题或修改题目后保存！","试题已存在",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }
                    if (questionBll.UpdateQuestion(newTestQuestion))
                    {
                        MessageBox.Show("修改试题成功！");
                        if (ntqlf != null)
                        {
                            FreshQData freshData = new FreshQData(ntqlf.ShowQuestionList);
                            freshData(oldkeyword, oldsysid, oldworkid, oldgenreid, oldlevelid, oldsubjectid, 8, pageCurrent);
                        }
                        //if (tqlf != null)
                        //{
                        //    FreshData freshData = new FreshData(tqlf.ShowQuestionList);
                        //    freshData();
                        //}
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("修改试题失败！");
                    }
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
            }
            #endregion

            #region 新增
            else if (PARM == 1)
            {
                if (Program.mf.LoginUser != null)
                {
                    if (cmbSystem.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个系统！");
                        cmbSystem.Focus();
                        return;
                    }
                    if (cmbTypeofWork.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个工种！");
                        cmbTypeofWork.Focus();
                        return;
                    }
                    if (cmbSubject.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个科目！");
                        cmbSubject.Focus();
                        return;
                    }
                    if (Convert.ToInt32(cmbQuestionType.SelectedValue) < 0)
                    {
                        MessageBox.Show("操作失败，试题类型不允许为空，请选择试题类型！", "选择试题类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbQuestionType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuestion.Text))
                    {
                        MessageBox.Show("操作失败，试题不允许为空，请录入试题！", "录入试题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }

                    // 试题录入答案计算
                    int txtCount = 0;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag))
                            {
                                if (string.IsNullOrEmpty(c.Text))
                                {
                                    txtCount++;
                                }
                            }
                        }
                    }

                    if (cmbQuestionType.SelectedIndex == 1 || cmbQuestionType.SelectedIndex == 2)
                    {
                        if (txtCount > 2)
                        {
                            MessageBox.Show("操作失败，请录入至少2个答案！","录入答案",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }

                        if (cmbCorrect.SelectedIndex < 0)
                        {
                            MessageBox.Show("操作失败，请选择正确答案！","选择答案",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            cmbCorrect.Focus();
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(txtScore.Text))
                    {
                        MessageBox.Show("操作失败，请录入本题分数！","录入分数",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtScore.Focus();
                        return;
                    }
                    TestQuestion newTestQuestion = new TestQuestion();
                    newTestQuestion.TestQuestionId = newtestquestionId; //试题Id
                    questionBll = new QuestionBLL();
                    TestQuestion testQ = new TestQuestion();
                    testQ = questionBll.MatchingQuestion(txtQuestion.Text, newTestQuestion.TestQuestionId);
                    if (testQ != null)
                    {
                        MessageBox.Show("操作失败，试题已存在，请检查试题或修改题目后保存！","试题已存在",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }
                    //试题信息
                    newTestQuestion.Question = txtQuestion.Text;//试题
                    newTestQuestion.QuestionType = (TestQuestionType)cmbQuestionType.SelectedIndex;//类型
                    Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                    newTestQuestion.StTypeId = sttypeid;//科目
                    Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                    newTestQuestion.StTypeSupplyId = typesupplyid;//类别
                    Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                    newTestQuestion.StLevelId = stlevelid;//等级
                    newTestQuestion.UserId = Program.mf.LoginUser.UserId;//创建教师
                    newTestQuestion.Score = Convert.ToDouble(txtScore.Text);//分数                
                    newTestQuestion.AnswerA = txtAnswerA.Text;//答案
                    newTestQuestion.AnswerB = txtAnswerB.Text;
                    newTestQuestion.AnswerC = txtAnswerC.Text;
                    newTestQuestion.AnswerD = txtAnswerD.Text;
                    newTestQuestion.Correct = cmbCorrect.Text;//正确答案
                    newTestQuestion.CreateTime = DateTime.Now;

                    if (questionBll.AddQuestion(newTestQuestion))
                    {
                        if (ntqlf != null)
                        {
                            MessageBox.Show("新建试题成功！");
                            ClearAll();
                            FreshQData freshData = new FreshQData(ntqlf.ShowQuestionList);
                            freshData(oldkeyword, oldsysid, oldworkid, oldgenreid, oldlevelid, oldsubjectid, 8, pageCurrent);
                        }
                        //if (tqlf != null)
                        //{
                        //    MessageBox.Show("新建试题成功！");
                        //    ClearAll();
                        //    FreshData freshData = new FreshData(tqlf.ShowQuestionList);
                        //    freshData();
                        //}
                    }
                    else
                    {
                        MessageBox.Show("新建试题失败！");
                    }
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
            }
            #endregion

            #region 素材关联试题新增
            else if (PARM == 2)
            {
                if (Program.mf.LoginUser != null)
                {
                    if (cmbSystem.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个系统！");
                        cmbSystem.Focus();
                        return;
                    }
                    if (cmbTypeofWork.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个工种！");
                        cmbTypeofWork.Focus();
                        return;
                    }
                    if (cmbSubject.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个科目！");
                        cmbSubject.Focus();
                        return;
                    }
                    if (Convert.ToInt32(cmbQuestionType.SelectedValue) < 0)
                    {
                        MessageBox.Show("操作失败，试题类型不允许为空，请选择试题类型！", "选择试题类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbQuestionType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuestion.Text))
                    {
                        MessageBox.Show("操作失败，试题不允许为空，请录入试题！", "录入试题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }

                    // 试题录入答案计算
                    int txtCount = 0;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag))
                            {
                                if (string.IsNullOrEmpty(c.Text))
                                {
                                    txtCount++;
                                }
                            }
                        }
                    }

                    if (cmbQuestionType.SelectedIndex == 1 || cmbQuestionType.SelectedIndex == 2)
                    {
                        if (txtCount > 2)
                        {
                            MessageBox.Show("操作失败，请录入至少2个答案！","录入答案",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }

                        if (cmbCorrect.SelectedIndex < 0)
                        {
                            MessageBox.Show("操作失败，请选择正确答案！","选择答案",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            cmbCorrect.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtScore.Text))
                    {
                        MessageBox.Show("操作失败，请录入本题分数！","录入分数",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtScore.Focus();
                        return;
                    }
                    TestQuestion newTestQuestion = new TestQuestion();
                    newTestQuestion.TestQuestionId = newtestquestionId; //试题Id
                    questionBll = new QuestionBLL();
                    TestQuestion testQ = new TestQuestion();
                    testQ = questionBll.MatchingQuestion(txtQuestion.Text, newTestQuestion.TestQuestionId);
                    if (testQ != null)
                    {
                        MessageBox.Show("操作失败，试题已存在，请检查试题或修改题目后保存！","试题已存在",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }
                    //试题信息
                    newTestQuestion.Question = txtQuestion.Text;//试题
                    newTestQuestion.QuestionType = (TestQuestionType)cmbQuestionType.SelectedIndex;//类型
                    Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                    newTestQuestion.StTypeId = sttypeid;//科目
                    Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                    newTestQuestion.StTypeSupplyId = typesupplyid;//类别
                    Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                    newTestQuestion.StLevelId = stlevelid;//等级
                    newTestQuestion.UserId = Program.mf.LoginUser.UserId;//创建教师
                    newTestQuestion.Score = Convert.ToDouble(txtScore.Text);//分数                
                    newTestQuestion.AnswerA = txtAnswerA.Text;//答案
                    newTestQuestion.AnswerB = txtAnswerB.Text;
                    newTestQuestion.AnswerC = txtAnswerC.Text;
                    newTestQuestion.AnswerD = txtAnswerD.Text;
                    newTestQuestion.Correct = cmbCorrect.Text;//正确答案
                    newTestQuestion.CreateTime = DateTime.Now;

                    List<TestQuestionResource> tqrList = new List<TestQuestionResource>();
                    TestQuestionResource newtqr = new TestQuestionResource();
                    newtqr.QuestionResourceId = Guid.NewGuid();
                    newtqr.ResourceId = resourceId;
                    newtqr.TestQuestionId = newTestQuestion.TestQuestionId;
                    tqrList.Add(newtqr);

                    if (questionBll.AddQuestion(newTestQuestion) && questionBll.AddQuestionResource(tqrList))
                    {
                        MessageBox.Show("新建试题成功！");
                        ClearAll();
                        if (narqf != null)
                        {
                            FreshRQData freshdata = new FreshRQData(narqf.ShowQIRList);
                            freshdata(oldkeyword, oldsysid, oldworkid, oldgenreid, oldlevelid, oldsubjectid, 8, pageCurrent, resourceId);
                        }
                    }
                    else
                    {
                        MessageBox.Show("新建试题失败！");
                    }
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
            }
            #endregion

            #region 素材关联试题修改
            if (PARM == 5)
            {
                if (Program.mf.LoginUser != null)
                {
                    if (cmbSystem.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个系统！");
                        cmbSystem.Focus();
                        return;
                    }
                    if (cmbTypeofWork.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个工种！");
                        cmbTypeofWork.Focus();
                        return;
                    }
                    if (cmbSubject.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个科目！");
                        cmbSubject.Focus();
                        return;
                    }
                    if (Convert.ToInt32(cmbQuestionType.SelectedValue) < 0)
                    {
                        MessageBox.Show("操作失败，试题类型不允许为空，请选择试题类型！", "选择试题类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbQuestionType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuestion.Text))
                    {
                        MessageBox.Show("操作失败，试题不允许为空，请录入试题！", "录入试题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }

                    // 试题录入答案计算
                    int txtCount = 0;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag))
                            {
                                if (string.IsNullOrEmpty(c.Text))
                                {
                                    txtCount++;
                                }
                            }
                        }
                    }

                    if (cmbQuestionType.SelectedIndex == 1 || cmbQuestionType.SelectedIndex == 2)
                    {
                        if (txtCount > 2)
                        {
                            MessageBox.Show("操作失败，请录入至少2个答案！", "录入答案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (cmbCorrect.SelectedIndex < 0)
                        {
                            MessageBox.Show("操作失败，请选择正确答案！", "选择答案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbCorrect.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtScore.Text))
                    {
                        MessageBox.Show("操作失败，请录入本题分数！", "录入分数", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtScore.Focus();
                        return;
                    }
                    //试题信息
                    TestQuestion newTestQuestion = new TestQuestion();
                    newTestQuestion.TestQuestionId = testquestionId;//试题Id
                    newTestQuestion.Question = txtQuestion.Text;//试题
                    newTestQuestion.QuestionType = (TestQuestionType)cmbQuestionType.SelectedValue;//类型
                    Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                    newTestQuestion.StTypeId = sttypeid;//科目
                    Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                    newTestQuestion.StTypeSupplyId = typesupplyid;//类别
                    Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                    newTestQuestion.StLevelId = stlevelid;//等级
                    newTestQuestion.UserId = Program.mf.LoginUser.UserId;//创建教师
                    newTestQuestion.Score = Convert.ToDouble(txtScore.Text);//分数                
                    newTestQuestion.AnswerA = txtAnswerA.Text;//答案
                    newTestQuestion.AnswerB = txtAnswerB.Text;
                    newTestQuestion.AnswerC = txtAnswerC.Text;
                    newTestQuestion.AnswerD = txtAnswerD.Text;
                    newTestQuestion.Correct = cmbCorrect.Text;//正确答案
                    newTestQuestion.CreateTime = oldCreateTime;
                    questionBll = new QuestionBLL();
                    TestQuestion testQ = new TestQuestion();
                    testQ = questionBll.MatchingQuestion(txtQuestion.Text, newTestQuestion.TestQuestionId);
                    if (testQ != null)
                    {
                        MessageBox.Show("操作失败，试题已存在，请检查试题或修改题目后保存！", "试题已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }
                    if (questionBll.UpdateQuestion(newTestQuestion))
                    {
                        MessageBox.Show("修改试题成功！");
                        if (narqf != null)
                        {
                            FreshRQData freshdata = new FreshRQData(narqf.ShowQIRList);
                            freshdata(oldkeyword, oldsysid, oldworkid, oldgenreid, oldlevelid, oldsubjectid, 8, pageCurrent, resourceId);
                            this.Close();
                        }
                        //if (tqlf != null)
                        //{
                        //    FreshData freshData = new FreshData(tqlf.ShowQuestionList);
                        //    freshData();
                        //}
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("修改试题失败！");
                    }
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
            }
            #endregion

            #region 试卷管理新增
            else if (PARM == 3)
            {
                if (Program.mf.LoginUser != null)
                {
                    if (cmbSystem.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个系统！");
                        cmbSystem.Focus();
                        return;
                    }
                    if (cmbTypeofWork.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个工种！");
                        cmbTypeofWork.Focus();
                        return;
                    }
                    if (cmbSubject.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个科目！");
                        cmbSubject.Focus();
                        return;
                    }
                    if (Convert.ToInt32(cmbQuestionType.SelectedValue) < 0)
                    {
                        MessageBox.Show("操作失败，试题类型不允许为空，请选择试题类型！", "选择试题类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbQuestionType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuestion.Text))
                    {
                        MessageBox.Show("操作失败，试题不允许为空，请录入试题！", "录入试题", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }

                    // 试题录入答案计算
                    int txtCount = 0;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag))
                            {
                                if (string.IsNullOrEmpty(c.Text))
                                {
                                    txtCount++;
                                }
                            }
                        }
                    }

                    if (cmbQuestionType.SelectedIndex == 1 || cmbQuestionType.SelectedIndex == 2)
                    {
                        if (txtCount > 2)
                        {
                            MessageBox.Show("操作失败，请录入至少2个答案！", "录入答案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (cmbCorrect.SelectedIndex < 0)
                        {
                            MessageBox.Show("操作失败，请选择正确答案！", "选择答案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbCorrect.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtScore.Text))
                    {
                        MessageBox.Show("操作失败，请录入本题分数！", "录入分数", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtScore.Focus();
                        return;
                    }
                    TestQuestion newTestQuestion = new TestQuestion();
                    newTestQuestion.TestQuestionId = newtestquestionId; //试题Id
                    questionBll = new QuestionBLL();
                    TestQuestion testQ = new TestQuestion();
                    testQ = questionBll.MatchingQuestion(txtQuestion.Text, newTestQuestion.TestQuestionId);
                    if (testQ != null)
                    {
                        MessageBox.Show("操作失败，试题已存在，请检查试题或修改题目后保存！", "试题已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }
                    //试题信息
                    newTestQuestion.Question = txtQuestion.Text;//试题
                    newTestQuestion.QuestionType = (TestQuestionType)cmbQuestionType.SelectedIndex;//类型
                    Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                    newTestQuestion.StTypeId = sttypeid;//科目
                    Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                    newTestQuestion.StTypeSupplyId = typesupplyid;//类别
                    Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                    newTestQuestion.StLevelId = stlevelid;//等级
                    newTestQuestion.UserId = Program.mf.LoginUser.UserId;//创建教师
                    newTestQuestion.Score = Convert.ToDouble(txtScore.Text);//分数                
                    newTestQuestion.AnswerA = txtAnswerA.Text;//答案
                    newTestQuestion.AnswerB = txtAnswerB.Text;
                    newTestQuestion.AnswerC = txtAnswerC.Text;
                    newTestQuestion.AnswerD = txtAnswerD.Text;
                    newTestQuestion.Correct = cmbCorrect.Text;//正确答案
                    newTestQuestion.CreateTime = DateTime.Now;

                    List<ExamPaper> epList = new List<ExamPaper>();
                    ExamPaper newexam = new ExamPaper();
                    newexam.ExamPaperId = Guid.NewGuid();
                    newexam.TestPaperId = paperId;
                    newexam.TestQuestionId = newTestQuestion.TestQuestionId;
                    epList.Add(newexam);
                    paperBll = new PaperBLL();
                    if (questionBll.AddQuestion(newTestQuestion) && paperBll.MSAddExamPaper(epList))
                    {
                        MessageBox.Show("新建试题成功！");
                        ClearAll();
                        if (neqf != null)
                        {
                            FreshPData freshData = new FreshPData(neqf.ShowQIPList);
                            freshData();
                        }
                    }
                    else
                    {
                        MessageBox.Show("新建试题失败！");
                    }
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
            }
            #endregion

            #region 试卷管理修改
            if (PARM == 4)
            {
                if (Program.mf.LoginUser != null)
                {
                    if (cmbSystem.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个系统！");
                        cmbSystem.Focus();
                        return;
                    }
                    if (cmbTypeofWork.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个工种！");
                        cmbTypeofWork.Focus();
                        return;
                    }
                    if (cmbSubject.SelectedIndex == 0)
                    {
                        MessageBox.Show("请选择一个科目！");
                        cmbSubject.Focus();
                        return;
                    }
                    if (Convert.ToInt32(cmbQuestionType.SelectedValue) < 0)
                    {
                        MessageBox.Show("操作失败，试题类型不允许为空，请选择试题类型！", "选择试题类型", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbQuestionType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuestion.Text))
                    {
                        MessageBox.Show("操作失败，试题不允许为空，请录入试题！","录入试题",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }

                    // 试题录入答案计算
                    int txtCount = 0;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag))
                            {
                                if (string.IsNullOrEmpty(c.Text))
                                {
                                    txtCount++;
                                }
                            }
                        }
                    }

                    if (cmbQuestionType.SelectedIndex == 1 || cmbQuestionType.SelectedIndex == 2)
                    {
                        if (txtCount > 2)
                        {
                            MessageBox.Show("操作失败，请录入至少2个答案！", "录入答案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (cmbCorrect.SelectedIndex < 0)
                        {
                            MessageBox.Show("操作失败，请选择正确答案！", "选择答案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbCorrect.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtScore.Text))
                    {
                        MessageBox.Show("操作失败，请录入本题分数！", "录入分数", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtScore.Focus();
                        return;
                    }
                    //试题信息
                    TestQuestion newTestQuestion = new TestQuestion();
                    newTestQuestion.TestQuestionId = testquestionId;//试题Id
                    newTestQuestion.Question = txtQuestion.Text;//试题
                    newTestQuestion.QuestionType = (TestQuestionType)cmbQuestionType.SelectedValue;//类型
                    Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                    newTestQuestion.StTypeId = sttypeid;//科目
                    Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                    newTestQuestion.StTypeSupplyId = typesupplyid;//类别
                    Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                    newTestQuestion.StLevelId = stlevelid;//等级
                    newTestQuestion.UserId = Program.mf.LoginUser.UserId;//创建教师
                    newTestQuestion.Score = Convert.ToDouble(txtScore.Text);//分数                
                    newTestQuestion.AnswerA = txtAnswerA.Text;//答案
                    newTestQuestion.AnswerB = txtAnswerB.Text;
                    newTestQuestion.AnswerC = txtAnswerC.Text;
                    newTestQuestion.AnswerD = txtAnswerD.Text;
                    newTestQuestion.Correct = cmbCorrect.Text;//正确答案
                    newTestQuestion.CreateTime = oldCreateTime;
                    questionBll = new QuestionBLL();
                    TestQuestion testQ = new TestQuestion();
                    testQ = questionBll.MatchingQuestion(txtQuestion.Text, newTestQuestion.TestQuestionId);
                    if (testQ != null)
                    {
                        MessageBox.Show("操作失败，试题已存在，请检查试题或修改题目后保存！", "试题已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuestion.Focus();
                        return;
                    }
                    if (questionBll.UpdateQuestion(newTestQuestion))
                    {
                        MessageBox.Show("修改试题成功！");
                        if (neqf != null)
                        {
                            FreshPData freshData = new FreshPData(neqf.ShowQIPList);
                            freshData();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("修改试题失败！");
                    }
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
            }
            #endregion

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体初始化方法
        /// </summary>
        private void WinLoadJudgement()
        {
            if (PARM == 0)
            {
                #region 修改界面初始化
                if (testquestionId != Guid.Empty)
                {
                    this.Text = "修改试题";
                    TestQuestion oldQuestion = new TestQuestion();
                    questionBll = new QuestionBLL();
                    oldQuestion = questionBll.GetQuestion(testquestionId);

                    #region 试题类型ComboBox
                    cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
                    cmbQuestionType.DisplayMember = "Text";
                    cmbQuestionType.ValueMember = "Value";
                    cmbQuestionType.Text = oldQuestion.QuestionType.ToString();
                    #endregion

                    #region 系统ComboBox
                    Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
                    comboxShow = new ComboBoxShow();
                    TypeList = comboxShow.ThreeTypecmbShow(gid);
                    cmbSystem.DataSource = TypeList;
                    cmbSystem.DisplayMember = "Name";
                    cmbSystem.ValueMember = "StTypeId";
                    string SysTypeName = comboxShow.SystemcmbShow(testquestionId);
                    cmbSystem.Text = SysTypeName;
                    #endregion

                    #region 工种ComboBox
                    StType sysitem = cmbSystem.SelectedItem as StType;
                    comboxShow = new ComboBoxShow();
                    typeList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
                    cmbTypeofWork.DataSource = typeList;
                    cmbTypeofWork.DisplayMember = "Name";
                    cmbTypeofWork.ValueMember = "StTypeId";

                    string TypeofWorkName = comboxShow.TypeofWorkcmbShow(testquestionId);
                    cmbTypeofWork.Text = TypeofWorkName;
                    #endregion

                    #region 科目ComboBox
                    StType subitem = cmbTypeofWork.SelectedItem as StType;
                    comboxShow = new ComboBoxShow();
                    typeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
                    cmbSubject.DataSource = typeList;
                    cmbSubject.DisplayMember = "Name";
                    cmbSubject.ValueMember = "StTypeId";

                    string SubjectName = comboxShow.SubjectcmbShow(testquestionId);
                    cmbSubject.Text = SubjectName;
                    #endregion

                    #region 类别ComboBox
                    comboxShow = new ComboBoxShow();
                    typesupplyList = comboxShow.GenrecmbShow();
                    cmbGenre.DataSource = typesupplyList;
                    cmbGenre.DisplayMember = "StTypeName";
                    cmbGenre.ValueMember = "StTypeSupplyId";

                    string genreName = comboxShow.GenrecmbShow(testquestionId);
                    cmbGenre.Text = genreName;
                    #endregion

                    #region 等级ComboBox
                    comboxShow = new ComboBoxShow();
                    leveltypeList = comboxShow.LevelcmbShow();
                    cmbLevel.DataSource = leveltypeList;
                    cmbLevel.DisplayMember = "StLevelName";
                    cmbLevel.ValueMember = "StLevelId";

                    string levelName = comboxShow.LevelcmbShow(testquestionId);
                    cmbLevel.Text = levelName;
                    #endregion
                    txtQuestion.Text = oldQuestion.Question;
                    txtAnswerA.Text = oldQuestion.AnswerA;
                    txtAnswerB.Text = oldQuestion.AnswerB;
                    txtAnswerC.Text = oldQuestion.AnswerC;
                    txtAnswerD.Text = oldQuestion.AnswerD;
                    txtScore.Text = oldQuestion.Score.ToString();
                    oldCreateTime = (DateTime)oldQuestion.CreateTime;
                    #region 正确答案列表计算绑定
                    List<string> correctList = new List<string>();
                    int cmbIndex = 4;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag) && !string.IsNullOrEmpty(c.Text))
                            {
                                cmbIndex--;
                                correctList.Add(c.Tag.ToString());
                                correctList.Sort();
                            }
                        }
                    }

                    cmbCorrect.DataSource = correctList;
                    cmbCorrect.Text = oldQuestion.Correct;
                    #endregion
                    btnAdd.Text = "修改";
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                #endregion
            }
            else if (PARM == 1)
            {
                #region 新增界面初始化
                if (Program.mf.LoginUser != null)
                {
                    this.Text = "新建试题";

                    #region 试题类型ComboBox
                    cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
                    cmbQuestionType.DisplayMember = "Text";
                    cmbQuestionType.ValueMember = "Value";
                    #endregion

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

                    btnResource.Visible = true;
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                #endregion
            }
            else if (PARM == 2)
            {
                #region 素材新增界面初始化
                if (Program.mf.LoginUser != null)
                {
                    this.Text = "新建试题";

                    #region 试题类型ComboBox
                    cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
                    cmbQuestionType.DisplayMember = "Text";
                    cmbQuestionType.ValueMember = "Value";
                    #endregion

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

                    btnResource.Visible = false;
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                #endregion
            }

            else if (PARM == 5)
            {
                #region 素材关联试题修改
                if (Program.mf.LoginUser != null)
                {
                    this.Text = "修改试题";
                    TestQuestion oldQuestion = new TestQuestion();
                    questionBll = new QuestionBLL();
                    oldQuestion = questionBll.GetQuestion(testquestionId);

                    #region 试题类型ComboBox
                    cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
                    cmbQuestionType.DisplayMember = "Text";
                    cmbQuestionType.ValueMember = "Value";
                    cmbQuestionType.Text = oldQuestion.QuestionType.ToString();
                    #endregion

                    #region 系统ComboBox
                    Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
                    comboxShow = new ComboBoxShow();
                    TypeList = comboxShow.ThreeTypecmbShow(gid);
                    cmbSystem.DataSource = TypeList;
                    cmbSystem.DisplayMember = "Name";
                    cmbSystem.ValueMember = "StTypeId";
                    string SysTypeName = comboxShow.SystemcmbShow(testquestionId);
                    cmbSystem.Text = SysTypeName;
                    #endregion

                    #region 工种ComboBox
                    StType sysitem = cmbSystem.SelectedItem as StType;
                    comboxShow = new ComboBoxShow();
                    typeList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
                    cmbTypeofWork.DataSource = typeList;
                    cmbTypeofWork.DisplayMember = "Name";
                    cmbTypeofWork.ValueMember = "StTypeId";

                    string TypeofWorkName = comboxShow.TypeofWorkcmbShow(testquestionId);
                    cmbTypeofWork.Text = TypeofWorkName;
                    #endregion

                    #region 科目ComboBox
                    StType subitem = cmbTypeofWork.SelectedItem as StType;
                    comboxShow = new ComboBoxShow();
                    typeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
                    cmbSubject.DataSource = typeList;
                    cmbSubject.DisplayMember = "Name";
                    cmbSubject.ValueMember = "StTypeId";

                    string SubjectName = comboxShow.SubjectcmbShow(testquestionId);
                    cmbSubject.Text = SubjectName;
                    #endregion

                    #region 类别ComboBox
                    comboxShow = new ComboBoxShow();
                    typesupplyList = comboxShow.GenrecmbShow();
                    cmbGenre.DataSource = typesupplyList;
                    cmbGenre.DisplayMember = "StTypeName";
                    cmbGenre.ValueMember = "StTypeSupplyId";

                    string genreName = comboxShow.GenrecmbShow(testquestionId);
                    cmbGenre.Text = genreName;
                    #endregion

                    #region 等级ComboBox
                    comboxShow = new ComboBoxShow();
                    leveltypeList = comboxShow.LevelcmbShow();
                    cmbLevel.DataSource = leveltypeList;
                    cmbLevel.DisplayMember = "StLevelName";
                    cmbLevel.ValueMember = "StLevelId";

                    string levelName = comboxShow.LevelcmbShow(testquestionId);
                    cmbLevel.Text = levelName;
                    #endregion
                    txtQuestion.Text = oldQuestion.Question;
                    txtAnswerA.Text = oldQuestion.AnswerA;
                    txtAnswerB.Text = oldQuestion.AnswerB;
                    txtAnswerC.Text = oldQuestion.AnswerC;
                    txtAnswerD.Text = oldQuestion.AnswerD;
                    txtScore.Text = oldQuestion.Score.ToString();
                    oldCreateTime = (DateTime)oldQuestion.CreateTime;
                    #region 正确答案列表计算绑定
                    List<string> correctList = new List<string>();
                    int cmbIndex = 4;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag) && !string.IsNullOrEmpty(c.Text))
                            {
                                cmbIndex--;
                                correctList.Add(c.Tag.ToString());
                                correctList.Sort();
                            }
                        }
                    }

                    cmbCorrect.DataSource = correctList;
                    cmbCorrect.Text = oldQuestion.Correct;
                    #endregion
                    btnAdd.Text = "修改";

                    btnResource.Visible = true;
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                #endregion
            }
            else if (PARM == 3)
            {
                #region 试卷管理界面新增初始化
                if (Program.mf.LoginUser != null)
                {
                    this.Text = "新建试题";

                    #region 试题类型ComboBox
                    cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
                    cmbQuestionType.DisplayMember = "Text";
                    cmbQuestionType.ValueMember = "Value";
                    #endregion

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

                    btnResource.Visible = true;
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                #endregion
            }
            else if (PARM == 4)
            {
                #region 试卷管理界面修改初始化
                if (testquestionId != Guid.Empty)
                {
                    this.Text = "修改试题";
                    TestQuestion oldQuestion = new TestQuestion();
                    questionBll = new QuestionBLL();
                    oldQuestion = questionBll.GetQuestion(testquestionId);

                    #region 试题类型ComboBox
                    cmbQuestionType.DataSource = QuestionSelectItem.TypeSelectList;
                    cmbQuestionType.DisplayMember = "Text";
                    cmbQuestionType.ValueMember = "Value";
                    cmbQuestionType.Text = oldQuestion.QuestionType.ToString();
                    #endregion

                    #region 系统ComboBox
                    Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
                    comboxShow = new ComboBoxShow();
                    TypeList = comboxShow.ThreeTypecmbShow(gid);
                    cmbSystem.DataSource = TypeList;
                    cmbSystem.DisplayMember = "Name";
                    cmbSystem.ValueMember = "StTypeId";
                    string SysTypeName = comboxShow.SystemcmbShow(testquestionId);
                    cmbSystem.Text = SysTypeName;
                    #endregion

                    #region 工种ComboBox
                    StType sysitem = cmbSystem.SelectedItem as StType;
                    comboxShow = new ComboBoxShow();
                    typeList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
                    cmbTypeofWork.DataSource = typeList;
                    cmbTypeofWork.DisplayMember = "Name";
                    cmbTypeofWork.ValueMember = "StTypeId";

                    string TypeofWorkName = comboxShow.TypeofWorkcmbShow(testquestionId);
                    cmbTypeofWork.Text = TypeofWorkName;
                    #endregion

                    #region 科目ComboBox
                    StType subitem = cmbTypeofWork.SelectedItem as StType;
                    comboxShow = new ComboBoxShow();
                    typeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
                    cmbSubject.DataSource = typeList;
                    cmbSubject.DisplayMember = "Name";
                    cmbSubject.ValueMember = "StTypeId";

                    string SubjectName = comboxShow.SubjectcmbShow(testquestionId);
                    cmbSubject.Text = SubjectName;
                    #endregion

                    #region 类别ComboBox
                    comboxShow = new ComboBoxShow();
                    typesupplyList = comboxShow.GenrecmbShow();
                    cmbGenre.DataSource = typesupplyList;
                    cmbGenre.DisplayMember = "StTypeName";
                    cmbGenre.ValueMember = "StTypeSupplyId";

                    string genreName = comboxShow.GenrecmbShow(testquestionId);
                    cmbGenre.Text = genreName;
                    #endregion

                    #region 等级ComboBox
                    comboxShow = new ComboBoxShow();
                    leveltypeList = comboxShow.LevelcmbShow();
                    cmbLevel.DataSource = leveltypeList;
                    cmbLevel.DisplayMember = "StLevelName";
                    cmbLevel.ValueMember = "StLevelId";

                    string levelName = comboxShow.LevelcmbShow(testquestionId);
                    cmbLevel.Text = levelName;
                    #endregion
                    txtQuestion.Text = oldQuestion.Question;
                    txtAnswerA.Text = oldQuestion.AnswerA;
                    txtAnswerB.Text = oldQuestion.AnswerB;
                    txtAnswerC.Text = oldQuestion.AnswerC;
                    txtAnswerD.Text = oldQuestion.AnswerD;
                    txtScore.Text = oldQuestion.Score.ToString();
                    oldCreateTime = (DateTime)oldQuestion.CreateTime;
                    #region 正确答案列表计算绑定
                    List<string> correctList = new List<string>();
                    int cmbIndex = 4;
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox)
                        {
                            string txtTag = "" + c.Tag;
                            if (!string.IsNullOrEmpty(txtTag) && !string.IsNullOrEmpty(c.Text))
                            {
                                cmbIndex--;
                                correctList.Add(c.Tag.ToString());
                                correctList.Sort();
                            }
                        }
                    }

                    cmbCorrect.DataSource = correctList;
                    cmbCorrect.Text = oldQuestion.Correct;
                    #endregion
                    btnAdd.Text = "修改";
                }
                else
                {
                    MessageBox.Show("当前登录用户失效，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                #endregion
            }
        }

        private void ClearAll()
        {
            cmbSystem.SelectedIndex = 0;
            cmbTypeofWork.SelectedIndex = 0;
            cmbSubject.SelectedIndex = 0;
            cmbGenre.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            cmbCorrect.DataSource = null;
            txtQuestion.Clear();
            txtAnswerA.Clear();
            txtAnswerB.Clear();
            txtAnswerC.Clear();
            txtAnswerD.Clear();
            txtScore.Clear();
            newtestquestionId = Guid.NewGuid();
        }

        private void btnResource_Click(object sender, EventArgs e)
        {
            QuestionResourceListForm qrlf = new QuestionResourceListForm();
            if (PARM == 0 || PARM == 4 || PARM == 5)
            {
                qrlf.PARM = PARM;
                qrlf.testquestionId = testquestionId;
                qrlf.ShowDialog();
            }
            if (PARM == 1 || PARM == 3)
            {
                qrlf.PARM = PARM;
                qrlf.testquestionId = newtestquestionId;
                qrlf.ShowDialog();
            }
            if (PARM == 2)
            {
                NewAddResourceQuestionForm narq = new NewAddResourceQuestionForm();
                narq.resourceId = resourceId;
                narq.ShowDialog();
            }
        }

        private void cmbQuestionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbQuestionType.SelectedIndex == 0 || cmbQuestionType.SelectedIndex == 1)
            {
                this.Size = new Size(589, 463);
                label8.Visible = true;
                txtAnswerA.Visible = true;
                label10.Visible = true;
                txtAnswerB.Visible = true;
                label11.Visible = true;
                txtAnswerC.Visible = true;
                label12.Visible = true;
                txtAnswerD.Visible = true;
                label9.Visible = true;
                cmbCorrect.Visible = true;
                label19.Visible = true;

                label8.Location = new Point(25, 222);
                txtAnswerA.Location = new Point(85, 219);
                label10.Location = new Point(25, 254);
                txtAnswerB.Location = new Point(85, 251);
                label11.Location = new Point(25, 286); ;
                txtAnswerC.Location = new Point(85, 283);
                label12.Location = new Point(25, 318);
                txtAnswerD.Location = new Point(85, 315);
                label9.Location = new Point(25, 352);
                cmbCorrect.Location = new Point(85, 349);
                label19.Location = new Point(171, 352);
                label13.Location = new Point(221, 352);
                txtScore.Location = new Point(281, 349);
                label20.Location = new Point(337, 352);
                btnResource.Location = new Point(367, 347);
                btnAdd.Location = new Point(170, 391);
                btnReturn.Location = new Point(367, 391);
            }
            else if (cmbQuestionType.SelectedIndex == 2)
            {
                this.Size = new Size(589, 393);
                label8.Visible = true;
                txtAnswerA.Visible = true;
                label10.Visible = true;
                txtAnswerB.Visible = true;
                label11.Visible = false;
                txtAnswerC.Visible = false;
                label12.Visible = false;
                txtAnswerD.Visible = false;
                label9.Visible = true;
                cmbCorrect.Visible = true;
                label19.Visible = true;


                label9.Location = new Point(25, 286);
                cmbCorrect.Location = new Point(85, 283);
                label19.Location = new Point(171, 286);
                label13.Location = new Point(221, 286);
                txtScore.Location = new Point(281, 283);
                label20.Location = new Point(337, 286);
                btnResource.Location = new Point(367, 281);
                btnAdd.Location = new Point(170, 325);
                btnReturn.Location = new Point(367, 325);
            }

            else if (cmbQuestionType.SelectedIndex == 3 || cmbQuestionType.SelectedIndex == 4)
            {
                this.Size = new Size(589, 332);
                label8.Visible = false;
                txtAnswerA.Visible = false;
                label10.Visible = false;
                txtAnswerB.Visible = false;
                label11.Visible = false;
                txtAnswerC.Visible = false;
                label12.Visible = false;
                txtAnswerD.Visible = false;
                label9.Visible = false;
                cmbCorrect.Visible = false;
                label19.Visible = false;

                label13.Location = new Point(25, 222);
                txtScore.Location = new Point(85, 219);
                label20.Location = new Point(141, 222);
                btnResource.Location = new Point(171, 217);
                btnAdd.Location = new Point(170, 261);
                btnReturn.Location = new Point(367, 261);
            }
        }

        #region 下拉列表悬浮提示框
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
        #endregion
    }
}
