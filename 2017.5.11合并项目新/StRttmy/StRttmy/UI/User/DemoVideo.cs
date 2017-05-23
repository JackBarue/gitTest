using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI
{
    public partial class DemoVideo : Form
    {
        public DemoVideo()
        {
            InitializeComponent();
        }

        private void DemoVideo_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL ="";
        }
    }
}
