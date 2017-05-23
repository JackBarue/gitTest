using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI.Help
{
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        //窗体加载
        private void AboutUs_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\Files\gsjs.jpg");
        }

        //公司介绍
        private void tSB1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\Files\gsjs.jpg");
        }

        //产品特点
        private void tSB2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\Files\cptd.jpg");
        }

        //产品应用
        private void tSB3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\Files\cpyy.jpg");
        }

    }
}
