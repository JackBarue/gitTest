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
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class AddPaperForm : Form
    {
        public TestPapersListForm tplf = null;
        public NewEditQuestionForm neqf = null;
        public int PARM = 0x000;
        public Guid courseId = Guid.Empty;
        public Guid paperid;
        private ComboBoxShow comboxShow = null;
        private Guid testpaperId=Guid.NewGuid();
        private List<StType> typeList = null;
        private List<StType> typeofworkList = null;
        private List<StType> subjecttypeList = null;
        private IList<StType> TypeList = null;
        private List<StTypeSupply> typesupplyList = null;
        private List<StLevel> leveltypeList = null;
        private delegate void FreshData();
        private PaperBLL paperBLL = null;        
        private CourseBLL courseBLL = null;
        private DateTime oldCreateTime;

        public AddPaperForm()
        {
            InitializeComponent();
        }

        private void AddPaperForm_Load(object sender, EventArgs e)
        {
            WinLoadJudgement();
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

        private void cmbGenerate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbGenerate.SelectedIndex == 0)
            {
                //MSQuestionForm msqf = new MSQuestionForm();
                //msqf.testpaperId = testpaperId;
                //msqf.ShowDialog();
                NewAddPaperQuestionForm napq = new NewAddPaperQuestionForm();
                napq.testpaperId = testpaperId;
                napq.ShowDialog();
            }
            else
            {
                ATQuestionForm atf = new ATQuestionForm();
                atf.testpaperId = testpaperId;
                atf.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPaper();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddPaper()
        {
            TestPaper newTestPaper = new TestPaper();
            #region 新增试卷
            if (PARM == 0)
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
                if (string.IsNullOrEmpty(txtPaperName.Text))
                {
                    MessageBox.Show("操作失败，试卷名称不允许为空，请填写试卷名称！", "填写试卷名称", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTestTime.Text))
                {
                    MessageBox.Show("操作失败，考试时间不允许为空，请填写考试时间！", "填写考试时间", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                newTestPaper.TestPaperId = testpaperId;
                Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                newTestPaper.StTypeId = sttypeid;//科目
                Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                newTestPaper.StTypeSupplyId = typesupplyid;//类别
                Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                newTestPaper.StLevelId = stlevelid;//等级
                newTestPaper.TestName = txtPaperName.Text;
                newTestPaper.UserId = Program.mf.LoginUser.UserId;//创建教师
                newTestPaper.TestTime = Convert.ToInt32(txtTestTime.Text);
                newTestPaper.CreateTime = DateTime.Now;
                paperBLL = new PaperBLL();
                TestPaper testQ = new TestPaper();
                testQ = paperBLL.MatchingPaper(txtPaperName.Text, newTestPaper.TestPaperId);
                if (testQ != null)
                {
                    MessageBox.Show("操作失败，试卷已存在，请检查试卷或修改试卷名称后保存！", "试卷已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (paperBLL.AddPaper(newTestPaper))
                {
                    if (tplf != null)
                    {
                        MessageBox.Show("新建试卷成功！");
                        FreshData freshData = new FreshData(tplf.ShowPapersList);
                        freshData();
                        ClearAll();
                        testpaperId = Guid.NewGuid();
                    }
                }
                else
                {
                    MessageBox.Show("新建试卷失败！");
                }
            }
            #endregion
            #region 课件生成试卷
            else if (PARM == 1)
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
                if (string.IsNullOrEmpty(txtPaperName.Text))
                {
                    MessageBox.Show("操作失败，试卷名称不允许为空，请填写试卷名称！", "填写试卷名称", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTestTime.Text))
                {
                    MessageBox.Show("操作失败，考试时间不允许为空，请填写考试时间！", "填写考试时间", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                newTestPaper.TestPaperId = testpaperId;
                Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                newTestPaper.StTypeId = sttypeid;//科目
                Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                newTestPaper.StTypeSupplyId = typesupplyid;//类别
                Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                newTestPaper.StLevelId = stlevelid;//等级
                newTestPaper.TestName = txtPaperName.Text;
                newTestPaper.UserId = Program.mf.LoginUser.UserId;//创建教师
                newTestPaper.TestTime = Convert.ToInt32(txtTestTime.Text);
                newTestPaper.CreateTime = DateTime.Now;
                paperBLL = new PaperBLL();
                TestPaper testQ = new TestPaper();
                testQ = paperBLL.MatchingPaper(txtPaperName.Text, newTestPaper.TestPaperId);
                if (testQ != null)
                {
                    MessageBox.Show("操作失败，试卷已存在，请检查试卷或修改试卷名称后保存！", "试卷已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (paperBLL.CheckTestQuestionResources(courseId))
                {
                    if (paperBLL.AddPaper(newTestPaper) && paperBLL.AddCoursewarePapers(newTestPaper.TestPaperId, courseId))
                    {
                        MessageBox.Show("生成试卷成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("生成试卷失败！");
                    }
                }
                else
                {
                    MessageBox.Show("操作失败，您所选择的课件素材中没有相关联的试题，请您将素材关联试题后再试！", "生成试卷失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
            #region 派生试卷
            if (PARM == 2)
            {
                if (string.IsNullOrEmpty(txtPaperName.Text))
                {
                    MessageBox.Show("操作失败，试卷名称不允许为空，请填写试卷名称！", "填写试卷名称", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTestTime.Text))
                {
                    MessageBox.Show("操作失败，考试时间不允许为空，请填写考试时间！", "填写考试时间", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                newTestPaper.TestPaperId = testpaperId;
                Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                newTestPaper.StTypeId = sttypeid;//科目
                Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                newTestPaper.StTypeSupplyId = typesupplyid;//类别
                Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                newTestPaper.StLevelId = stlevelid;//等级
                newTestPaper.TestName = txtPaperName.Text;
                newTestPaper.UserId = Program.mf.LoginUser.UserId;//创建教师
                newTestPaper.TestTime = Convert.ToInt32(txtTestTime.Text);
                newTestPaper.CreateTime = DateTime.Now;
                paperBLL = new PaperBLL();
                TestPaper testQ = new TestPaper();
                testQ = paperBLL.MatchingPaper(txtPaperName.Text, newTestPaper.TestPaperId);
                if (testQ != null)
                {
                    MessageBox.Show("操作失败，试卷已存在，请检查试卷或修改试卷名称后保存！", "试卷已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                List<ExamPaper> newEP = new List<ExamPaper>();
                newEP = paperBLL.FindExamPaer(paperid);
                if (paperBLL.AddPaper(newTestPaper) && paperBLL.DerivedExampaper(newEP, testpaperId))
                {
                    if (tplf != null)
                    {
                        MessageBox.Show("派生试卷成功！");
                        FreshData freshData = new FreshData(tplf.ShowPapersList);
                        freshData();                        
                    }
                }
                else
                {
                    MessageBox.Show("派生试卷失败！");
                }

            }
            #endregion
            #region 试卷信息修改
            if (PARM == 3)
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
                if (string.IsNullOrEmpty(txtPaperName.Text))
                {
                    MessageBox.Show("操作失败，试卷名称不允许为空，请填写试卷名称！", "填写试卷名称", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTestTime.Text))
                {
                    MessageBox.Show("操作失败，考试时间不允许为空，请填写考试时间！", "填写考试时间", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                newTestPaper.TestPaperId = paperid;
                Guid sttypeid = new Guid(cmbSubject.SelectedValue.ToString());
                newTestPaper.StTypeId = sttypeid;//科目
                Guid typesupplyid = new Guid(cmbGenre.SelectedValue.ToString());
                newTestPaper.StTypeSupplyId = typesupplyid;//类别
                Guid stlevelid = new Guid(cmbLevel.SelectedValue.ToString());
                newTestPaper.StLevelId = stlevelid;//等级
                newTestPaper.TestName = txtPaperName.Text;
                newTestPaper.UserId = Program.mf.LoginUser.UserId;//创建教师
                newTestPaper.TestTime = Convert.ToInt32(txtTestTime.Text);
                newTestPaper.CreateTime = oldCreateTime;
                paperBLL = new PaperBLL();
                TestPaper testP = new TestPaper();
                testP = paperBLL.MatchingPaper(txtPaperName.Text, newTestPaper.TestPaperId);
                if (testP != null)
                {
                    MessageBox.Show("操作失败，试卷已存在，请检查试卷或修改试卷名称后保存！", "试卷已存在", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPaperName.Focus();
                    return;
                }
                if (paperBLL.UpdatePaper(newTestPaper))
                {
                    if (neqf != null)
                    {
                        MessageBox.Show("修改试卷信息成功！");
                        FreshData freshData = new FreshData(neqf.ShowQIPList);
                        freshData();
                        testpaperId = Guid.NewGuid();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("修改试卷信息失败！");
                }
            }
            #endregion
        }

        private void ClearAll()
        {
            txtPaperName.Clear();
            txtTestTime.Clear();
            cmbSystem.SelectedIndex = 0;
            cmbTypeofWork.SelectedIndex = 0;
            cmbSubject.SelectedIndex = 0;
            cmbGenre.SelectedIndex = 0;
            cmbLevel.SelectedIndex = 0;
            cmbGenerate.SelectedIndex = 0;
        }

        private void WinLoadJudgement()
        {
            #region 新增试卷
            if (PARM == 0)
            {
                #region 试卷生成ComboBox
                cmbGenerate.DataSource = GeneratePaperSelectItem.TypeSelectList;
                cmbGenerate.DisplayMember = "Text";
                cmbGenerate.ValueMember = "Value";
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
            }
            #endregion
            #region 课件生成试卷
            if (PARM == 1)
            {
                label9.Visible = false;
                cmbGenerate.Visible = false;
                label14.Visible = false;
                StTypeRepository st = new StTypeRepository();
                StLevelRepository sl = new StLevelRepository();
                StTypeSupplyRepository sts = new StTypeSupplyRepository();
                List<StType> sd = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd.Insert(0, ins);
                cmbSystem.DisplayMember = "Name";
                cmbSystem.ValueMember = "StTypeId";
                cmbSystem.DataSource = sd;

                List<StTypeSupply> cr = sts.StTypeSupplyList().ToList();
                StTypeSupply insts = new StTypeSupply();
                insts.StTypeSupplyId = Guid.Empty;
                insts.StTypeName = "全部";
                cr.Insert(0, insts);
                cmbGenre.DisplayMember = "StTypeName";
                cmbGenre.ValueMember = "StTypeSupplyId";
                cmbGenre.DataSource = cr;

                List<StLevel> fr = sl.StLevelList().ToList();
                StLevel insl = new StLevel();
                insl.StLevelId = Guid.Empty;
                insl.StLevelName = "全部";
                fr.Insert(0, insl);
                cmbLevel.DisplayMember = "StLevelName";
                cmbLevel.ValueMember = "StLevelId";
                cmbLevel.DataSource = fr;

                Model.Courseware oldCourseware = new Model.Courseware();
                courseBLL = new CourseBLL();
                oldCourseware = courseBLL.GetCourse(courseId);
                cmbSystem.SelectedValue = st.StType(st.StType(oldCourseware.StType.Fid).Fid).StTypeId;
                cmbTypeofWork.SelectedValue = st.StType(oldCourseware.StType.Fid).StTypeId;
                cmbSubject.SelectedValue = oldCourseware.StType.StTypeId;
                cmbGenre.SelectedValue = oldCourseware.StTypeSupply.StTypeSupplyId;
                cmbLevel.SelectedValue = oldCourseware.StLevel.StLevelId;
                txtPaperName.Text = courseBLL.FindCourseware(courseId).Name + "试卷";
                this.Text = "生成试卷";
                btnAdd.Text = "确定";
            }
            #endregion
            #region 派生试卷
            if (PARM == 2)
            {
                label9.Visible = false;
                cmbGenerate.Visible = false;
                label14.Visible = false;
                paperBLL = new PaperBLL();
                TestPaper oldPaper = new TestPaper();
                oldPaper=paperBLL.GetPaper(paperid);

                #region 系统ComboBox
                Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
                comboxShow = new ComboBoxShow();
                TypeList = comboxShow.ThreeTypecmbShow(gid);
                cmbSystem.DataSource = TypeList;
                cmbSystem.DisplayMember = "Name";
                cmbSystem.ValueMember = "StTypeId";
                string SysTypeName = comboxShow.pSystemcmbShow(paperid);
                cmbSystem.Text = SysTypeName;
                #endregion
                #region 工种ComboBox
                StType sysitem = cmbSystem.SelectedItem as StType;
                comboxShow = new ComboBoxShow();
                typeList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
                cmbTypeofWork.DataSource = typeList;
                cmbTypeofWork.DisplayMember = "Name";
                cmbTypeofWork.ValueMember = "StTypeId";
                string TypeofWorkName = comboxShow.pTypeofWorkcmbShow(paperid);
                cmbTypeofWork.Text = TypeofWorkName;
                #endregion
                #region 科目ComboBox
                StType subitem = cmbTypeofWork.SelectedItem as StType;
                comboxShow = new ComboBoxShow();
                typeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
                cmbSubject.DataSource = typeList;
                cmbSubject.DisplayMember = "Name";
                cmbSubject.ValueMember = "StTypeId";
                string SubjectName = comboxShow.pSubjectcmbShow(paperid);
                cmbSubject.Text = SubjectName;
                #endregion
                #region 类别ComboBox
                comboxShow = new ComboBoxShow();
                typesupplyList = comboxShow.GenrecmbShow();
                cmbGenre.DataSource = typesupplyList;
                cmbGenre.DisplayMember = "StTypeName";
                cmbGenre.ValueMember = "StTypeSupplyId";
                string genreName = comboxShow.pGenrecmbShow(paperid);
                cmbGenre.Text = genreName;
                #endregion
                #region 等级ComboBox
                comboxShow = new ComboBoxShow();
                leveltypeList = comboxShow.LevelcmbShow();
                cmbLevel.DataSource = leveltypeList;
                cmbLevel.DisplayMember = "StLevelName";
                cmbLevel.ValueMember = "StLevelId";
                string levelName = comboxShow.pLevelcmbShow(paperid);
                cmbLevel.Text = levelName;
                #endregion
                txtPaperName.Text = oldPaper.TestName + "派生试题";
                
                txtTestTime.Text = oldPaper.TestTime.ToString();
                this.Text = "派生试卷";
                btnAdd.Text = "确定";
            }
            #endregion
            #region 试卷信息修改
            if (PARM == 3)
            {
                label9.Visible = false;
                cmbGenerate.Visible = false;
                label14.Visible = false;
                paperBLL = new PaperBLL();
                TestPaper oldPaper = new TestPaper();
                oldPaper = paperBLL.GetPaper(paperid);

                #region 系统ComboBox
                Guid gid = new Guid("510d3f7e-496c-45a0-9370-57a27b4a3cee");
                comboxShow = new ComboBoxShow();
                TypeList = comboxShow.ThreeTypecmbShow(gid);
                cmbSystem.DataSource = TypeList;
                cmbSystem.DisplayMember = "Name";
                cmbSystem.ValueMember = "StTypeId";
                string SysTypeName = comboxShow.pSystemcmbShow(paperid);
                cmbSystem.Text = SysTypeName;
                #endregion
                #region 工种ComboBox
                StType sysitem = cmbSystem.SelectedItem as StType;
                comboxShow = new ComboBoxShow();
                typeList = comboxShow.ThreeTypecmbShow(sysitem.StTypeId);
                cmbTypeofWork.DataSource = typeList;
                cmbTypeofWork.DisplayMember = "Name";
                cmbTypeofWork.ValueMember = "StTypeId";
                string TypeofWorkName = comboxShow.pTypeofWorkcmbShow(paperid);
                cmbTypeofWork.Text = TypeofWorkName;
                #endregion
                #region 科目ComboBox
                StType subitem = cmbTypeofWork.SelectedItem as StType;
                comboxShow = new ComboBoxShow();
                typeList = comboxShow.ThreeTypecmbShow(subitem.StTypeId);
                cmbSubject.DataSource = typeList;
                cmbSubject.DisplayMember = "Name";
                cmbSubject.ValueMember = "StTypeId";
                string SubjectName = comboxShow.pSubjectcmbShow(paperid);
                cmbSubject.Text = SubjectName;
                #endregion
                #region 类别ComboBox
                comboxShow = new ComboBoxShow();
                typesupplyList = comboxShow.GenrecmbShow();
                cmbGenre.DataSource = typesupplyList;
                cmbGenre.DisplayMember = "StTypeName";
                cmbGenre.ValueMember = "StTypeSupplyId";
                string genreName = comboxShow.pGenrecmbShow(paperid);
                cmbGenre.Text = genreName;
                #endregion
                #region 等级ComboBox
                comboxShow = new ComboBoxShow();
                leveltypeList = comboxShow.LevelcmbShow();
                cmbLevel.DataSource = leveltypeList;
                cmbLevel.DisplayMember = "StLevelName";
                cmbLevel.ValueMember = "StLevelId";
                string levelName = comboxShow.pLevelcmbShow(paperid);
                cmbLevel.Text = levelName;
                #endregion
                txtPaperName.Text = oldPaper.TestName;
                oldCreateTime = (DateTime)oldPaper.CreateTime;
                txtTestTime.Text = oldPaper.TestTime.ToString();
                this.Text = "修改试卷信息";
                btnAdd.Text = "修改"; 
            }
            #endregion
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

        private void cmbGenerate_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.DrawString(GeneratePaperSelectItem.TypeSelectList[e.Index].Text, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip1.Show(GeneratePaperSelectItem.TypeSelectList[e.Index].Text, cmbGenerate, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            }

            e.DrawFocusRectangle();
        }

        private void cmbGenerate_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbGenerate);
        }
    }
}
