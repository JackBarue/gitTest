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
    public partial class DetailResourceForm : Form
    {
        private ResourceBLL rb = null;
        public Guid resourceId = Guid.Empty;
        private StTypeRepository st;
        public DetailResourceForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void DetailResourceForm_Load(object sender, EventArgs e)
        {
            ShowResource();
        }   

        //初始化
        private void ShowResource()
        {
            if (resourceId != Guid.Empty)
            {
                st = new StTypeRepository();
                Resource oldResource = new Resource();
                rb = new ResourceBLL();
                oldResource = rb.GetResource(resourceId);
                txtResourceType.Text = st.StType(st.StType(oldResource.StType.Fid).Fid).Name;
                textBox1.Text = st.StType(oldResource.StType.Fid).Name;
                textBox2.Text = oldResource.StTypeSupply.StTypeName;
                textBox3.Text = oldResource.StLevel.StLevelName;
                textBox4.Text = oldResource.StType.Name;
                txtTitle.Text = oldResource.Title;
                txtKeyword.Text = oldResource.Keyword;
                txtContentFile.Text = oldResource.ContentFile;
                txtSoundFile.Text = oldResource.SoundFile;
                txtTextFile.Text = oldResource.TextFile;
            }
            else
            {
                MessageBox.Show("此素材不存在。");
                Close();
            }
        }

    }
}
