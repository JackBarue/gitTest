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
using System.Windows.Forms.DataVisualization.Charting;

namespace StRttmy.UI.Diagram
{
    public partial class ResultsReport : Form
    {
        private CoursewareRepository cr;
        private TeachingRepository tcr;
        private PaperRepository pr;

        public ResultsReport()
        {
            InitializeComponent();
        }

        private void ResultsReport_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = System.DateTime.Now;
            QueryNumber(new DateTime(2017, 3, 7), System.DateTime.Now);
        }

        private List<DiagramCourseware> al = null;
        private DiagramCourseware diagramcourseware;
        private void QueryNumber(DateTime star, DateTime end)
        {
            chart1.DataSource = null;
            chart1.Series[0].Points.Clear();
            cr = new CoursewareRepository();
            al = new List<DiagramCourseware>();
            List<Model.Courseware> coursewareAll = cr.CoursewareAll(star, end).ToList();

            diagramcourseware = new DiagramCourseware();
            diagramcourseware.TeachName = "课件制作数量";
            diagramcourseware.StTime = coursewareAll.Where(g => g.CoursewareLevelId == Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c")).Count();
            al.Add(diagramcourseware);

            tcr = new TeachingRepository();
            List<Teaching> teachingAll = tcr.TeachingAll(star, end).ToList();
            long satrtime = 0;
            long endTimen = 0;
            foreach (Teaching d in teachingAll)
            {
                DateTime d1 = (DateTime)d.Startime;
                satrtime = d1.Ticks + satrtime;
            }
            foreach (Teaching c in teachingAll)
            {
                DateTime c1 = (DateTime)c.Endtime;
                endTimen = c1.Ticks + endTimen;
            }
            long j = (endTimen - satrtime) / 60000;
            double duration = (double)(j / 600000);
            diagramcourseware = new DiagramCourseware();
            diagramcourseware.TeachName = "教学时长";
            diagramcourseware.StTime = duration;
            al.Add(diagramcourseware);
            int nu = 0;
            foreach (Teaching f in teachingAll)
            {
                nu = f.StClass.ClassStudents.Count() + nu;
            }
            diagramcourseware = new DiagramCourseware();
            diagramcourseware.TeachName = "培训人数";
            diagramcourseware.StTime = nu;
            al.Add(diagramcourseware);

            pr = new PaperRepository();
            List<StudentPaperListShowClass> splsc = pr.StudentPaperPOP(star, end).ToList();
            int stuno = 0;
            foreach (StudentPaperListShowClass item in splsc)
            {
                stuno = splsc.Where(c => c.StudentExamPaperId == item.StudentExamPaperId).Count();
            }
            diagramcourseware = new DiagramCourseware();
            diagramcourseware.TeachName = "合格人数";
            diagramcourseware.StTime = stuno;
            al.Add(diagramcourseware);

            chart1.DataSource = al;
            chart1.ChartAreas[0].AxisY.Interval = 1;//设置刻度间隔为5%
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;//不显示网格线
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.Series[0].XValueMember = "TeachName";
            chart1.Series[0].YValueMembers = "StTime";
            chart1.DataBind();
            chart1.Series[0].Color = ColorTranslator.FromHtml("#1874CD");
            chart1.Series[0].Points[0].Color = ColorTranslator.FromHtml("#1874CD");

            chart1.Series[1].Color = ColorTranslator.FromHtml("#FFA500");
            chart1.Series[0].Points[1].Color = ColorTranslator.FromHtml("#FFA500");

            chart1.Series[2].Color = ColorTranslator.FromHtml("#CD3700");
            chart1.Series[0].Points[2].Color = ColorTranslator.FromHtml("#CD3700");

            chart1.Series[3].Color = ColorTranslator.FromHtml("#104E8B");
            chart1.Series[0].Points[3].Color = ColorTranslator.FromHtml("#104E8B");
        }
    }
}
