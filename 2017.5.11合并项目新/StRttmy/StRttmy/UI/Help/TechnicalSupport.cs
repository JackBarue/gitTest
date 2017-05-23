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
    public partial class TechnicalSupport : Form
    {
        public TechnicalSupport()
        {
            InitializeComponent();
        }

        //窗体加载
        private void TechnicalSupport_Load(object sender, EventArgs e)
        {
            //pictureBox1.ImageLocation = Application.StartupPath + @"\Files\TechnicalSupport1.jpg";
            webBrowser1.Navigate(Application.StartupPath + @"\Files\TechnicalSupport1.jpg");
        }
    }
}
