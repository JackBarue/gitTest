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
    public partial class PromptDrag : Form
    {
        CourseResourceForm CF;
        public PromptDrag()
        {
            InitializeComponent();
        }

        public PromptDrag(Form f)
        {
            this.CF = (CourseResourceForm)f;
            InitializeComponent();
            this.Width = f.Width;
           // this.Height = f.Height - 185;
            this.DoubleBuffered = true;//双缓存处理
            this.Owner = f;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Owner.Location.X, this.Owner.Location.Y);
        }
        /// <summary>
        /// 点击边框给予操作提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请先退出白板才能使用窗口拖动放大功能！");
        }
       /// <summary>
       /// 关闭父级窗口委托
       /// </summary>
       /// <param name="f"></param>
        public delegate void CloseWin(Form f);
        public CloseWin CW;
        private void label1_Click(object sender, EventArgs e)
        {
            CloseWin CW = new CloseWin(CF.Close_win);
            CW(CF);
        }
    }
}
