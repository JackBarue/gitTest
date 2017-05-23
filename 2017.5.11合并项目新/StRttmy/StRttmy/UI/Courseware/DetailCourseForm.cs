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
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class DetailCourseForm : Form
    {
        private CourseBLL cb = null;
        public Guid coursewareId = Guid.Empty;
        private StTypeRepository st;
        public DetailCourseForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void DetailCourseForm_Load(object sender, EventArgs e)
        {

            ShowCourse();
        }

        //初始化
        private void ShowCourse()
        {
            if (coursewareId != Guid.Empty)
            {

                st = new StTypeRepository();
                Model.Courseware oldCourseware = new Model.Courseware();
                cb = new CourseBLL();
                oldCourseware = cb.GetCourse(coursewareId);

                txtType.Text = oldCourseware.CoursewareLevel.Name;
                textBox1.Text = st.StType(st.StType(oldCourseware.StType.Fid).Fid).Name;
                textBox2.Text = st.StType(oldCourseware.StType.Fid).Name;
                textBox3.Text = oldCourseware.StTypeSupply.StTypeName;
                textBox4.Text = oldCourseware.StLevel.StLevelName;
                textBox5.Text = oldCourseware.StType.Name;
                txtName.Text = oldCourseware.Name;
                txtKeyword.Text = oldCourseware.Keyword;
                txtDescription.Text = oldCourseware.Description;
            }
            else
            {
                MessageBox.Show("此课件不存在。");
                Close();
            }
        }

     
    }
}
