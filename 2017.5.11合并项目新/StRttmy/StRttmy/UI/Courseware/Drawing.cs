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
    public partial class Drawing : Form
    {
        public Drawing()
        {
            InitializeComponent();
        }
        public Drawing(Form f)
        {

            InitializeComponent();
            this.Width = f.Width - 240;
            this.Height = f.Height - 185;
            this.DoubleBuffered = true;//双缓存处理
            this.Owner = f;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Owner.Location.X + 247, this.Owner.Location.Y + 30);
        }

        private void Drawing_Load(object sender, EventArgs e)
        {
            //去边框
            FormBorderStyle = FormBorderStyle.None;
        }
    }
}
