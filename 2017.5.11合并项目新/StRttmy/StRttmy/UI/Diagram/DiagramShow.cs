using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI.Diagram
{
    public partial class DiagramShow : Form
    {
        private CoursewareRepository cr;
        private ResourceRepository rr;
        private TeachingRepository tcr;
        private QuestionRepository qr;
        private PaperRepository pr;
        private UserRepository ur;
        private List<DiagramCourseware> al = null;
        private List<DiagramTestQuestion> tq = null;

        private List<DiagramShowClass> diashow = null;
        private DiagramCourseware diagramcourseware;

        public DiagramShow()
        {
            InitializeComponent();
        }
        
        private void DiagramShow_Load(object sender, EventArgs e)
        {
            CoursewareMake();
        }
        private int tabClick = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabClick == 0)
            {
                QueryNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 1)
            {
                ResourceNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 2)
            {
                TeachNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 3)
            {
                TeachDuration(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 4)
            {
                TestQuestionNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }

            if (tabClick == 5)
            {
                TestPaperNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           // QueryNumber(dateTimePicker1.Value, dateTimePicker2.Value);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                CoursewareMake();
                tabClick = 0;
            }
            if (e.TabPage == tabPage2)
            {
                ResourceMake();
                tabClick = 1;
            }
            if (e.TabPage == tabPage3)
            {
                TeachData();
                tabClick = 2;
            }
            if (e.TabPage == tabPage4)
            {
                TeachDurationData();
                tabClick = 3;
            }
            if (e.TabPage == tabPage5)
            {
                TestQuestionMake();
                tabClick = 4;
            }

            if (e.TabPage == tabPage6)
            {
                TestPaperMake();
                tabClick = 5;
            }

        }

        private void ChartShow(DateTime star, DateTime end)
        {
            chart1.DataSource = null;
            chart1.Series[0].Points.Clear();
            cr = new CoursewareRepository();
            diashow = new List<DiagramShowClass>();
            List<Model.Courseware> coursewareAll = cr.CoursewareAll(star, end).ToList();
            diagramcourseware = new DiagramCourseware();
            diagramcourseware.TeachName = "课件制作数量";
            diagramcourseware.StTime = coursewareAll.Where(g => g.CoursewareLevelId == Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c")).Count();
            al.Add(diagramcourseware);
        }

        #region 课件制作
        private void CoursewareMake()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            QueryNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart1.ChartAreas[0].AxisY.Interval = 1;//设置刻度间隔为5%
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.Series["制作课件数量"].XValueMember = "TeachName";
            chart1.Series["制作课件数量"].YValueMembers = "StNumber";
        }

        private void QueryNumber(DateTime star, DateTime end)
        {
            chart1.DataSource = null;
            chart1.Series["制作课件数量"].Points.Clear();
            al = new List<DiagramCourseware>();
            cr = new CoursewareRepository();
            List<Model.Courseware> coursewareAll = cr.CoursewareAll(star, end).ToList();
            coursewareAll = coursewareAll.Where(G => G.CoursewareLevelId == Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c")).ToList();
            List<Model.Courseware> nonDuplicateList2 = coursewareAll.Where((x, i) => coursewareAll.FindIndex(z => z.UserId == x.UserId) == i).ToList();//Lambda表达式去重  
            foreach (Model.Courseware a in nonDuplicateList2)
            {
                DiagramCourseware diagramcourseware = new DiagramCourseware();
                diagramcourseware.TeachName = a.User.UserName;
                diagramcourseware.StNumber = coursewareAll.Where(f => f.UserId == a.UserId).Count();
                al.Add(diagramcourseware);

            }
            chart1.DataSource = al;
        }

        #endregion

        #region 素材制作
        private void ResourceMake()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            ResourceNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.Series["素材制作数量"].XValueMember = "TeachName";
            chart2.Series["素材制作数量"].YValueMembers = "StNumber";
        }


        private void ResourceNumber(DateTime star, DateTime end)
        {
            chart2.DataSource = null;
            chart2.Series["素材制作数量"].Points.Clear();
            al = new List<DiagramCourseware>();
            rr = new  ResourceRepository();
            List<Resource> resourceAll = rr.ResourceAll(star, end).ToList();//Guid.Parse("6223f38c-c9c0-4ca7-a35f-8687124b3d88")
            resourceAll = resourceAll.Where(K => K.Level == ResourceLevel.自编).ToList();
            List<Resource> nonDuplicateList2 = resourceAll.Where((x, i) => resourceAll.FindIndex(z => z.UserId == x.UserId) == i).ToList();//Lambda表达式去重  
            foreach (Resource a in nonDuplicateList2)
            {
                DiagramCourseware diagramcourseware = new DiagramCourseware();
                diagramcourseware.TeachName = a.User.UserName;
                diagramcourseware.StNumber = resourceAll.Where(f => f.UserId == a.UserId).Count();
                al.Add(diagramcourseware);

            }
            chart2.DataSource = al;
        }
        #endregion

        #region 教学次数

        private void TeachData()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            TeachNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart3.ChartAreas[0].AxisY.Interval = 1;//设置刻度间隔为5%
            chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart3.Series["教学次数"].XValueMember = "TeachName";
            chart3.Series["教学次数"].YValueMembers = "StNumber";
        }

        private void TeachNumber(DateTime star, DateTime end)
        {
            chart3.DataSource = null;
            chart3.Series["教学次数"].Points.Clear();
            al = new List<DiagramCourseware>();
            tcr = new TeachingRepository();
            ur = new UserRepository();
            List<Teaching> teachingAll = tcr.TeachingAll(star, end).ToList();
            List<Teaching> nonDuplicateList2 = teachingAll.Where((x, i) => teachingAll.FindIndex(z => z.UserId == x.UserId) == i).ToList();//Lambda表达式去重  
            foreach (Teaching a in nonDuplicateList2)
            {
                DiagramCourseware diagramcourseware = new DiagramCourseware();
                diagramcourseware.TeachName =ur.Find(a.UserId).UserName;
                diagramcourseware.StNumber = teachingAll.Where(f => f.UserId == a.UserId).Count();
                al.Add(diagramcourseware);

            }
            chart3.DataSource = al;
        }

        #endregion

        #region 教学时长

        private void TeachDurationData()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            TeachDuration(new DateTime(2017, 3, 7), System.DateTime.Now);
            //chart4.ChartAreas[0].AxisY.Interval = 1;//设置刻度间隔为5%
            chart4.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart4.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart4.Series["教学时长"].XValueMember = "TeachName";
            chart4.Series["教学时长"].YValueMembers = "StTime";
        }

        private void TeachDuration(DateTime star, DateTime end)
        {
            chart4.DataSource = null;
            chart4.Series["教学时长"].Points.Clear();
            al = new List<DiagramCourseware>();
            tcr = new TeachingRepository();
            ur = new UserRepository();
            List<Teaching> teachingAll = tcr.TeachingAll(star, end).ToList();
            List<Teaching> nonDuplicateList2 = teachingAll.Where((x, i) => teachingAll.FindIndex(z => z.UserId == x.UserId) == i).ToList();//Lambda表达式去重  
            foreach (Teaching a in nonDuplicateList2)
            {
                DiagramCourseware diagramcourseware = new DiagramCourseware();
                diagramcourseware.TeachName = ur.Find(a.UserId).UserName;
                long satrtime = 0;
                long endTimen = 0;
                foreach (Teaching d in teachingAll.Where(f => f.UserId == a.UserId))
                {
                    DateTime d1 = (DateTime)d.Startime;
                    satrtime = d1.Ticks + satrtime;
                }
                foreach (Teaching c in teachingAll.Where(f => f.UserId == a.UserId))
                {
                    DateTime c1 = (DateTime)c.Endtime;
                    endTimen = c1.Ticks + endTimen;
                }
                 long j = (endTimen - satrtime) / 60000;
                 double duration = (double)(j / 600000);
                 diagramcourseware.StTime = duration;// teachingAll.Select(f => f.Startime) - teachingAll.Select(f => f.Endtime).Sum();
                al.Add(diagramcourseware);

            }
            chart4.DataSource = al;
        }

        #endregion

        #region 试题制作
        private void TestQuestionMake()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            TestQuestionNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart5.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart5.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.Series["制作课件数量"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bubble;
            chart5.Series["试题制作数量"].XValueMember = "TQTeachName";
            chart5.Series["试题制作数量"].YValueMembers = "TQNumber";
        }

        private void TestQuestionNumber(DateTime star, DateTime end)
        {
            chart5.DataSource = null;
            chart5.Series["试题制作数量"].Points.Clear();
            tq = new List<DiagramTestQuestion>();
            qr = new QuestionRepository();
            List<TestQuestionUserClass> tqAll = qr.TestQuestionAll(star, end).ToList();
            List<TestQuestionUserClass> tqList = tqAll.Where((x, i) => tqAll.FindIndex(z => z.UserId == x.UserId) == i).ToList();
            foreach (TestQuestionUserClass a in tqList)
            {
                DiagramTestQuestion diagramtestquestion = new DiagramTestQuestion();
                diagramtestquestion.TQTeachName = a.UserName;
                diagramtestquestion.TQNumber = tqAll.Where(f => f.UserId == a.UserId).Count();
                tq.Add(diagramtestquestion);
            }
            chart5.DataSource = tq;
        }
        #endregion

        #region 试卷制作
        private void TestPaperMake()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            TestPaperNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart6.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart6.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart6.Series["试卷制作数量"].XValueMember = "TQTeachName";
            chart6.Series["试卷制作数量"].YValueMembers = "TQNumber";
        }

        private void TestPaperNumber(DateTime star, DateTime end)
        {
            chart6.DataSource = null;
            chart6.Series["试卷制作数量"].Points.Clear();
            tq = new List<DiagramTestQuestion>();
            pr = new PaperRepository();
            List<TestPaper> tpAll = pr.TestPaperAll(star, end).ToList();
            List<TestPaper> tpList = tpAll.Where((x, i) => tpAll.FindIndex(z => z.UserId == x.UserId) == i).ToList();
            foreach (TestPaper a in tpList)
            {
                DiagramTestQuestion diagramtestquestion = new DiagramTestQuestion();
                diagramtestquestion.TQTeachName = a.User.UserName;
                diagramtestquestion.TQNumber = tpAll.Where(f => f.UserId == a.UserId).Count();
                tq.Add(diagramtestquestion);
            }
            chart6.DataSource = tq;
        }

        #endregion
    }
}
