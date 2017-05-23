using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI
{
    public partial class Mapping : Form
    {
        public Drawing df;//透明窗体不穿透鼠标
        public PromptDrag pd;
        private bool startdraw = false;//是否开始画图
        private Graphics gs;//画版
        //private Pen pen;//画笔
        private Point startpt;//画图起点
        private Pen p = new Pen(Color.Black, 1);



        float PenWidth
        {
            set { p.Width = value; }
        }

        Color DrawColor
        {
            get { return p.Color; }
            set { p.Color = value; }
        }
        public Mapping()
        {
            InitializeComponent();
        }

        public Mapping(Form f)
        {
            InitializeComponent();
            //白板宽高随父级改变
            this.Width = f.Width - 240;
            this.Height = f.Height - 185;

            //WindowState = FormWindowState.Maximized;//本窗体最大化
            TransparencyKey = BackColor;//背景透明(鼠标穿透)
            DoubleBuffered = true;//双缓存处理

            df = new Drawing(f);//不穿透鼠标透明窗体
            //设置不穿透鼠标透明窗体画板鼠标事件为本显示画图窗体鼠标事件进行同步
            df.MouseDown += mapping_MouseDown;//鼠标按下事件
            df.MouseMove += mapping_MouseMove;//鼠标移动事件
            df.MouseUp += mapping_MouseUp;//鼠标弹起事件

            //不穿透鼠标透明窗体参数设置如下
            //df.WindowState = FormWindowState.Maximized;//最大化
            df.Opacity = 0.1;//背景透明不穿透鼠标
            df.TopMost = true;//让不穿透鼠标透明窗体画板为最上层
            //df.DoubleBuffered = true;//双缓存处理
            df.Show();//显示
            pd = new PromptDrag(f);
            pd.Opacity = 0.1;
            pd.TopMost = true;
            pd.Show();

            gs = CreateGraphics();//创建窗体画板
            //comboBox1.SelectedIndex = 0;
            this.Owner = f;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(this.Owner.Location.X + 247, this.Owner.Location.Y + 30);//出现位置定位

            //将线帽样式设为圆线帽，否则笔宽变宽时会出现明显的缺口  
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;
        }

        private void mapping_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startdraw = true;//开始画图
                startpt = e.Location;
            }
        }

        private void mapping_MouseMove(object sender, MouseEventArgs e)
        {
            if (startdraw)
            {
                gs.SmoothingMode = SmoothingMode.AntiAlias; //抗锯齿
                gs.DrawLine(p, startpt, e.Location);
                startpt = e.Location;

            }
        }

        private void mapping_MouseUp(object sender, MouseEventArgs e)
        {
            startdraw = false;//结束画图
        }

        private void Mapping_Load(object sender, EventArgs e)
        {
            //去除边框
            FormBorderStyle = FormBorderStyle.None;

        }
        //清空画板
        public void Clear_rb() 
        {
            gs.Clear(BackColor);
        }
        //画笔粗细
        public void DrawSize(float cx)
        {
            PenWidth = cx;
        }
        //画笔颜色
        public void Color_changed(Color cc) 
        {
            DrawColor = cc;
        }
    }
}
