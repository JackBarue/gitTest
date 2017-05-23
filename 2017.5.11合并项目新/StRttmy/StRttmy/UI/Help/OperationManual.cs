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
    public partial class OperationManual : Form
    {
        public OperationManual()
        {
            InitializeComponent();
        }

        //窗体加载
        private void OperationManual_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Application.StartupPath + @"\Files\OperationManual1.html");
        }
    }
}
