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

namespace StRttmy.UI.Diagram
{
    public partial class DiagramAnalysis : Form
    {

        private CoursewareRepository cr;
        private TeachingRepository tcr;
        private PaperRepository pr;
        private List<DiagramCourseware> al = null;
        private List<DiagramPOP> pop = null;

        public DiagramAnalysis()
        {
            InitializeComponent();
        }

        private void DiagramAnalysis_Load(object sender, EventArgs e)
        {
            CoursewareData();
        }

        private int tabClick = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabClick == 0)
            {
                CoursewareNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 1)
            {
                POPNumeber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 2)
            {
               // TeachNumber(dateTimePicker1.Value, dateTimePicker2.Value);
            }
            if (tabClick == 3)
            {
              //  TeachDuration(dateTimePicker1.Value, dateTimePicker2.Value);
            }
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                CoursewareData();
                tabClick = 0;
            }
            if (e.TabPage == tabPage2)
            {
                PercentofPass();
                tabClick = 1;
            }
            //if (e.TabPage == tabPage3)
            //{
            //   // TeachData();
            //    tabClick = 2;
            //}
            //if (e.TabPage == tabPage4)
            //{
            //   // TeachDurationData();
            //    tabClick = 3;
            //}
        }
        #region 课件使用分布
        private void CoursewareData()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            CoursewareNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart1.ChartAreas[0].AxisY.Interval = 1;//设置刻度间隔为5%
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.Series["课件使用次数"].XValueMember = "TeachName";
            chart1.Series["课件使用次数"].YValueMembers = "StNumber";
        }

        private void CoursewareNumber(DateTime star, DateTime end)
        {
            chart1.DataSource = null;
            chart1.Series["课件使用次数"].Points.Clear();
            al = new List<DiagramCourseware>();
            tcr = new TeachingRepository();
            cr = new CoursewareRepository();
            List<Teaching> teachingAll = tcr.TeachingAll(star, end).ToList();
            List<Teaching> nonDuplicateList2 = teachingAll.Where((x, i) => teachingAll.FindIndex(z => z.CoursewareId == x.CoursewareId) == i).ToList();//Lambda表达式去重  
            foreach (Teaching a in nonDuplicateList2)
            {
                DiagramCourseware diagramcourseware = new DiagramCourseware();
                diagramcourseware.TeachName =cr.Find(a.CoursewareId).Name;
                diagramcourseware.StNumber = teachingAll.Where(f => f.CoursewareId == a.CoursewareId).Count();
                al.Add(diagramcourseware);

            }
            chart1.DataSource = al;
        }
        #endregion

        #region 考试合格率
        private void PercentofPass()
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            POPNumeber(new DateTime(2017, 3, 7), System.DateTime.Now);
            chart2.ChartAreas[0].AxisY.Interval = 5;//设置刻度间隔为5%
           
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.Series["错误率"].XValueMember = "Name";
            chart2.Series["错误率"].YValueMembers = "POPNumber";
        }

        private void POPNumeber(DateTime star, DateTime end)
        {
            chart2.DataSource = null;
            chart2.Series["错误率"].Points.Clear();
            pop = new List<DiagramPOP>();
            pr = new PaperRepository();
            List<StudentPaperListShowClass> stuPOP = pr.StudentPaper(star, end).ToList();
            List<StudentPaperListShowClass> ListPOP = stuPOP.Where((x, i) => stuPOP.FindIndex(z => z.StudentExamPaperId == x.StudentExamPaperId) == i).ToList();//Lambda表达式去重  
            foreach (StudentPaperListShowClass a in ListPOP)
            {
                DiagramPOP diagrampop = new DiagramPOP();
                diagrampop.Name = a.Name;
                diagrampop.POPNumber = stuPOP.Where(f => f.StudentExamPaperId == a.StudentExamPaperId).Count();
                pop.Add(diagrampop);
            }
            chart2.DataSource = pop;
        }
        #endregion
    }  
}
