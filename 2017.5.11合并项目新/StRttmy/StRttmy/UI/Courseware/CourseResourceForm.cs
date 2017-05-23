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
using System.Xml;
using System.IO;
using StRttmy.Common;
using StRttmy.Repository;
using System.Globalization;


namespace StRttmy.UI
{
    public partial class CourseResourceForm : Form
    {
        private CourseBLL cb = null;

        private WebBrowser contentWebBrowser;//主显示内容为htm文件时，用此控件显示
        private PictureBox pb;
        private string Mp3Path = "";//用于声音开关的判断,如果媒体文件因规则要求被静音,则声音开关不对它进行控制
        public string MainPath = "";//同Mp3Path的用途一样
        private string TextPath = "";//用于和上两条共同判断重复播放同一个素材的处理
        private bool isMute = false;//不静音
        //private int BrowserType = 0;//contentWebBrowser的加载状态:0未播放过,1加载中,2加载完成
        public Guid coursewareId = Guid.Empty;
        public List<Guid> classIds = new List<Guid>();
        public DateTime startime = DateTime.MinValue;
        public CourseResourceForm()           
        {
            InitializeComponent();
            contentWebBrowser = new WebBrowser();
            //this.contentWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(contentWebBrowser_DocumentCompleted);// 点击U3D加载完成前禁用其它控件控件绑定。
            pb = new PictureBox();
        }

        //窗体加载
        private void CourseResourceForm_Load(object sender, EventArgs e)
        {
            timer3.Start();
            panel6.BackColor = Color.FromArgb(2, 103, 155);
            treeView1.BackColor = Color.FromArgb(162, 211, 226);
            button2.Image = StRttmy.Properties.Resources.FullScreen;
            this.splitContainer2.Panel1.Controls.Clear();
            this.splitContainer2.Panel1.Controls.Add(Swf);
            Swf.Dock = System.Windows.Forms.DockStyle.Fill;
            Swf.Movie = Application.StartupPath + @"\Files\studyeasy.swf";
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Files\jm-DT.jpg");
            axWindowsMediaPlayer1.Hide();
            BindCourse();
            button5.Visible = false;//声音重播默认关闭
            ColorSlider.Enabled = false;//视频进度条禁用
            toolTip1.SetToolTip(this.button4, "停止");
            toolTip1.SetToolTip(this.button3, "播放/暂停");
            toolTip1.SetToolTip(this.button5, "声音重放");
            toolTip1.SetToolTip(this.Sound_btn, "声音开关");
            toolTip1.SetToolTip(this.button2, "放大/缩小");
            toolTip1.SetToolTip(this.button1, "白板开关");
            //颜色选项下拉框默认值
            comboBox2.Text = "颜色";
            //颜色添加
            ComboBoxItem cbi1 = new ComboBoxItem();
            cbi1.Text = "黑色";
            cbi1.Value = Color.Black;
            comboBox2.Items.Add(cbi1);
            ComboBoxItem cbi2 = new ComboBoxItem();
            cbi2.Text = "红色";
            cbi2.Value = Color.Red;
            comboBox2.Items.Add(cbi2);
            ComboBoxItem cbi3 = new ComboBoxItem();
            cbi3.Text = "蓝色";
            cbi3.Value = Color.Blue;
            comboBox2.Items.Add(cbi3);
            ComboBoxItem cbi4 = new ComboBoxItem();
            cbi4.Text = "黄色";
            cbi4.Value = Color.Yellow;
            comboBox2.Items.Add(cbi4);
            ComboBoxItem cbi5 = new ComboBoxItem();
            cbi5.Text = "绿色";
            cbi5.Value = Color.Green;
            comboBox2.Items.Add(cbi5);
            ComboBoxItem cbi6 = new ComboBoxItem();
            cbi6.Text = "紫色";
            cbi6.Value = Color.Purple;
            comboBox2.Items.Add(cbi6);
            //画笔粗细默认值
            comboBox1.Text = "粗细";
        }

        //清空各种控件内容
        private void CloseContent()
        {
            //axWindowsMediaPlayer2.Invalidate();
            axWindowsMediaPlayer2.close();
            axWindowsMediaPlayer1.Invalidate();
            Swf.Movie = "";
            Swf.Update();
            Swf.Refresh();
            pb.Invalidate();
            contentWebBrowser.Invalidate();
            txtWebBrowser.Invalidate();
        }

        //释放各种控件的资源
        private void DisposeContent()
        {
            axWindowsMediaPlayer2.close();
            axWindowsMediaPlayer1.close();
            if (Swf != null)
                Swf.Dispose();
            if (pb != null)
                pb.Dispose();
            if (contentWebBrowser != null)
                contentWebBrowser.Dispose();
            if (txtWebBrowser != null)
                txtWebBrowser.Dispose();
        }

        //treeView1初始化数据
        private void BindCourse()
        {
            if (coursewareId != Guid.Empty)
            {
                Model.Courseware course = new Model.Courseware();
                List<CoursewareResource> crs = new List<CoursewareResource>();
                cb = new CourseBLL();
                course = cb.GetCourse(coursewareId);
                crs = course.coursewareResources.ToList<CoursewareResource>();
                //lblName.Text = course.Name;
                this.Text = course.Name;
                //  把数据绑定到treeView
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc = StRttmy.Common.XmlMenuUtility.EntitiesToXmlMenuFile(crs);
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                BindXmlToTreeView(xmlDoc.DocumentElement, treeView1.Nodes);
                treeView1.EndUpdate();
            }
            else
            {
                MessageBox.Show("此课件不存在。");
                Close();
            }
        }

        //把数据绑定到treeView1
        private void BindXmlToTreeView(XmlNode father, TreeNodeCollection treeNodes)
        {
            String LabelText = "", MainUrlText = "", TextUrl = "", Mp3Url = "";
            foreach (XmlAttribute xa in father.Attributes)
            {
                if (xa.Name == "Label")
                {
                    LabelText = father.Attributes["Label"].Value;
                }
                if (xa.Name == "MainUrl")
                {
                    MainUrlText = father.Attributes["MainUrl"].Value;
                }
                if (xa.Name == "TextUrl")
                {
                    TextUrl = father.Attributes["TextUrl"].Value;
                }
                if (xa.Name == "Mp3Url")
                {
                    Mp3Url = father.Attributes["Mp3Url"].Value;
                }
            }
            TreeNode treeNode = new TreeNode(); ;
            if (LabelText != "")
            {
                treeNode = treeNodes.Add(LabelText);
                CourseResourceTag tag = new CourseResourceTag();

                if (MainUrlText != "")
                {
                    tag.MainUrl = Application.StartupPath + @"\" + MainUrlText;
                }
                if (TextUrl != "")
                {
                    tag.TextUrl = Application.StartupPath + @"\" + TextUrl;
                }
                if (Mp3Url != "")
                {
                    tag.Mp3Url = Application.StartupPath + @"\" + Mp3Url;
                }
                treeNode.Tag = tag;
            }
            foreach (XmlNode child in father.ChildNodes)
            {
                BindXmlToTreeView(child, treeNode.Nodes);
            }
        }

        //课件素材点击
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CourseResourceTag tag = new CourseResourceTag();
                tag = (CourseResourceTag)e.Node.Tag;
                if (tag.MainUrl != null)
                {
                    CloseContent();
                    this.panel1.Controls.Clear();//清空文字区域
                    txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;//填充文字控件
                    axWindowsMediaPlayer1.URL = "";//清空单独声音控件
                    Validation s = new Validation();
                    string file_name = tag.MainUrl.Substring(tag.MainUrl.LastIndexOf("\\") + 1);
                    if (true)//s.validation_file(file_name)
                    {
                        //  主内容链接
                        if (!string.IsNullOrEmpty(tag.MainUrl))
                        {
                            string BeforeMainPath = MainPath;
                            string BeforeMp3Path = Mp3Path;
                            string BeforeTextPath = TextPath;
                            MainPath = tag.MainUrl;//点击获取到主内容名称给声音控制提供参数
                            Mp3Path = tag.Mp3Url;
                            TextPath = tag.TextUrl;
                            //if (BrowserType == 1)
                            //{
                            //    return;
                            //}
                            
                            //char kzmfgf = '.';
                            string kzm = MainPath.Substring(MainPath.LastIndexOf(".") + 1).ToLower();
                            //图片播放
                            if (kzm== "jpg" || kzm == "gif" || kzm == "png")
                            {
                                this.splitContainer2.Panel1.Controls.Clear();
                                pb.ImageLocation = tag.MainUrl;
                                pb.SizeMode = PictureBoxSizeMode.Zoom;
                                this.splitContainer2.Panel1.Controls.Add(pb);
                                pb.Dock = System.Windows.Forms.DockStyle.Fill;
                            }
                            //html(unity3d)、htm播放
                            if (kzm == "htm" || kzm== "html" || kzm == "unity3d")
                            {
                                //treeView1.Enabled = false;// 点击U3D加载完成前禁用其它控件。
                                U3Dloading();
                                this.splitContainer2.Panel1.Controls.Clear();
                                if (kzm == "unity3d")
                                {
                                    contentWebBrowser.Navigate(TextUtility.Ecode(tag.MainUrl));
                                    //BrowserType = 1;
                                }
                                else
                                {
                                    contentWebBrowser.Navigate(tag.MainUrl);
                                }
                                this.splitContainer2.Panel1.Controls.Add(contentWebBrowser);
                                contentWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                            }
                            //flash播放
                            if (kzm == "swf")
                            {
                                Swf.Movie = tag.MainUrl;
                                if (Swf.ReadyState != 4)
                                {
                                    Application.DoEvents();
                                }
                                this.splitContainer2.Panel1.Controls.Clear();
                                this.splitContainer2.Panel1.Controls.Add(Swf);
                                Swf.Dock = System.Windows.Forms.DockStyle.Fill;
                            }
                            //MP4播放
                            if (kzm == "mp4")
                            {
                                string mp4Url = TextUtility.MP4Decode(tag.MainUrl);//解密后的新路径
                                this.splitContainer2.Panel1.Controls.Clear();
                                axWindowsMediaPlayer2.URL = mp4Url;
                                this.splitContainer2.Panel1.Controls.Add(axWindowsMediaPlayer2);
                               axWindowsMediaPlayer2.Dock = System.Windows.Forms.DockStyle.Fill;
                            }
                        }
                        //  文字链接
                        if (!string.IsNullOrEmpty(tag.TextUrl))
                        {
                            string TextStr = Path.GetFileName(tag.TextUrl);
                            string hzm = TextStr.Substring(TextStr.LastIndexOf(".") + 1).ToLower();
                            if (hzm == "htm" || hzm == "html")
                            {
                                this.panel1.Height = splitContainer2.Panel2.Height - panel6.Height;
                                this.panel1.Controls.Clear();
                                this.panel1.Controls.Add(txtWebBrowser);
                                txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                                txtWebBrowser.Navigate(tag.TextUrl);
                            }
                            else
                            {
                                MessageBox.Show("未知的文件格式！");
                            }
                        }
                        //  声音链接
                        if (!string.IsNullOrEmpty(tag.Mp3Url))
                        {
                            string Mp3Str = Path.GetFileName(tag.Mp3Url);
                            string hzm = Mp3Str.Substring(Mp3Str.LastIndexOf(".") + 1).ToLower();
                            if (hzm == "mp3")
                            {
                                axWindowsMediaPlayer1.URL = tag.Mp3Url;
                            }
                            else
                            {
                                MessageBox.Show("未知的文件格式！");
                            }
                        }
                        if (Mp3Path != "" && MainPath != "")
                        {
                            SoundSwitch();
                        }
                    }
                }
            }           
        }

        //U3D加载状态
        //private void contentWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    if (contentWebBrowser.ReadyState == WebBrowserReadyState.Complete)
        //    {
        //        treeView1.Enabled = true;
        //    }
        //    else
        //    {
        //        contentWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(contentWebBrowser_DocumentCompleted);
        //    }
        //}

        #region 点击u3d禁止其余操作
        /// <summary>
        /// 点击u3d后锁定事件
        /// </summary>
        public void Lock_unity()
        {
            splitContainer1.Panel1.Enabled = false;
            panel6.Enabled = false;
        }
        /// <summary>
        /// 接收u3d反馈的参数方法
        /// </summary>
        /// <param name="message"></param>
        public void getContext(string message)
        {
            if (message == "activation")
            {              
                splitContainer1.Panel1.Enabled = true;
                panel6.Enabled = true;
                timer5.Stop();
            }
        }
        /// <summary>
        /// U3D点击禁止其它操作主方法
        /// </summary>
        private void U3Dloading()
        {
            if (MainPath != "")
            {
                string u3dFile = Path.GetFileName(MainPath);
                char kzmfgf = '.';
                string kzm = u3dFile.Substring(u3dFile.LastIndexOf(".") + 1).ToLower();
                if (kzm == "html" || kzm == "unity3d")
                {
                    Lock_unity();// 点击U3D加载完成前禁用其它控件。                   
                    u3dFile = u3dFile.Substring(0, u3dFile.LastIndexOf("."));
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\Resources\ContentFiles\" + u3dFile + ".unity3d");
                    long filesize = fi.Length;
                    double filesize_01 = filesize / 1024;
                    double filesize_02 = filesize_01 / 10240;
                    int IntegerSize = (int)Math.Round(filesize_02);
                    timer5.Interval = IntegerSize * 2000 + 3000;
                    timer5.Start();
                }
            }
        }
        /// <summary>
        /// 配合U3D点击禁止事件time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer5_Tick(object sender, EventArgs e)
        {        
            splitContainer1.Panel1.Enabled = true;
            panel6.Enabled = true;
            timer5.Stop();
        }

        #endregion



        //声音开关
        private void Sound_btn_Click(object sender, EventArgs e)
        {
            if (Mp3Path != "" && MainPath != "")
            {
                isMute = isMute ? false : true;
                SoundSwitch();
            }
        }

        //开关公用方法
        private void SoundSwitch()
        {
            string tempMp3path = Path.GetFileName(Mp3Path);
            string tempMainPath = Path.GetFileName(MainPath);
            //string SoundText = isMute ? @"\Files\YL2.png" : @"\Files\YL.png";
            Sound_btn.Image = isMute ? StRttmy.Properties.Resources.sound_ban : StRttmy.Properties.Resources.sound;
            axWindowsMediaPlayer1.settings.mute = isMute;//axWindowsMediaPlayer1开关音量
            //if (tempMainPath.Split('.')[1] == "mp4" && tempMp3path != "specificempty001.mp3")//当媒体文件是mp4声音文件又不是默认的空声音时,mp4静音
            //{
            //    axWindowsMediaPlayer2.settings.mute = true;//axWindowsMediaPlayer2静音 
            //    button5.Visible = true;
            //}
            //else
            //{
               
            //}
            axWindowsMediaPlayer2.settings.mute = isMute;//axWindowsMediaPlayer2开关音量
            if (tempMainPath.Substring(tempMainPath.LastIndexOf(".") + 1).ToLower() != "mp4" && tempMp3path != "specificempty001.mp3")
                button5.Visible = true;
            else
                button5.Visible = false;//声音重播显示

        }
        long eh = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            eh = eh + 1;
        }
        //关闭窗体
        public ChooseClassFrom ulf = null;
        public delegate void FreshData();
        private void CourseResourceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (startime != DateTime.MinValue)
            {
                DialogResult dr = MessageBox.Show("教学时间:" + startime.ToString() + "至" + System.DateTime.Now.ToString(), "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    e.Cancel = false;
                    timer3.Stop();
                    //本地时间转毫秒数
                    DateTime TheDate = startime;
                    DateTime d1 = new DateTime(1970, 1, 1);
                    DateTime d2 = TheDate.ToUniversalTime();
                    TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
                    long ms = (long)ts.TotalMilliseconds;  //返回 1970 年 1 月 1 日至今的毫秒数

                    //毫秒数转本地时间               
                    long ss = eh * 1000 + ms;
                    DateTime dt2 = new DateTime(1970, 1, 1);
                    dt2 = dt2.AddMilliseconds(ss);
                    DateTime dtemp = DateTime.Now;
                    dt2 = dt2.Add(dtemp - dtemp.ToUniversalTime());//加上本地时间和国际时间的时差 
                    foreach (Guid a in classIds)
                    {
                        Teaching addteach = new Teaching();
                        addteach.TeachingId = Guid.NewGuid();
                        addteach.StClassId = a;
                        addteach.CoursewareId = coursewareId;
                        addteach.Startime = startime;
                        addteach.Endtime = dt2;
                        addteach.UserId = Program.mf.LoginUser.UserId;
                        TeachingRepository tc = new TeachingRepository();
                        if (tc.Add(addteach))
                        {
                            if (ulf != null)
                            {
                                FreshData fd = new FreshData(ulf.closewindow);
                                fd();
                            }
                        }
                        else
                        {
                            MessageBox.Show("教学未记录");
                        }
                    }

                    DisposeContent();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                DisposeContent();
            }
        }

        //播放状态改变事件
        private void axWindowsMediaPlayer2_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //播放结束和停止播放时播放状态改变3次--1.wmppsMediaEnded播放结束 2.wmppsTransitioning过度 3.wmppsStopped播放停止
            if (axWindowsMediaPlayer2.playState.ToString() == "wmppsStopped")
            {
                //MP4播放结束和停止播放时画面停留在开始的第一帧
                axWindowsMediaPlayer2.Ctlcontrols.play();
                axWindowsMediaPlayer2.Ctlcontrols.pause();
                ColorSlider.Enabled = false;
                timer1.Stop();
                ColorSlider.Value = 0;
            }
            if (axWindowsMediaPlayer2.playState.ToString() == "wmppsPlaying")
            {
                timer1.Start();
                ColorSlider.Enabled = true;
                button3.Image = StRttmy.Properties.Resources.pause_MouseDown;
            }
            if (axWindowsMediaPlayer2.playState.ToString() == "wmppsPaused")
            {
                button3.Image = StRttmy.Properties.Resources.play;
            }
        }

        //播放暂停按钮
        private void button3_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer2.playState.ToString() == "wmppsPlaying")
            {
                axWindowsMediaPlayer2.Ctlcontrols.pause();
            }
            else if (axWindowsMediaPlayer2.playState.ToString() == "wmppsPaused")
            {
                axWindowsMediaPlayer2.Ctlcontrols.play();
            }
        }

        //停止按钮
        private void button4_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double total = 0.0d;
            if (axWindowsMediaPlayer2.currentMedia != null)
            {
                total = Math.Ceiling(axWindowsMediaPlayer2.currentMedia.duration);
                ColorSlider.Maximum = (int)axWindowsMediaPlayer2.currentMedia.duration;//設定撥放位置調整Bar最大值                    
            }
            double cur = Math.Floor(this.axWindowsMediaPlayer2.Ctlcontrols.currentPosition);
            if (total != 0)
            {
                DateTime dt = DateTime.Parse(DateTime.Now.ToString("00:00:00")).AddMilliseconds(cur * 1000);
                DateTime dt1 = DateTime.Parse(DateTime.Now.ToString("00:00:00")).AddMilliseconds(total * 1000);
                label1.Text = dt.ToString("mmm:ss") + "/" + dt1.ToString("mmm:ss");
                ColorSlider.Value = (int)cur;
            }
        }

        //播放器绑定滚动条
        private void ColorSlider_Scroll(object sender, ScrollEventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.currentPosition = ColorSlider.Value;
        }

        //声音重播
        private void button5_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        //白板开关
        Mapping dr;
        private bool DrawOnOrOff = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (DrawOnOrOff)
            {
                dr = new Mapping(this);
                dr.Show();
                DrawOnOrOff = false;
                button1.Image = StRttmy.Properties.Resources.EditorBoard_close;
                lockframe = true;//锁定窗口调整
                //白板选项显示
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                button6.Visible = true;
                button2.Enabled = false;//禁用菜单栏隐藏         
            }
            else
            {
                dr.df.Close();
                dr.pd.Close();
                dr.Close();
                DrawOnOrOff = true;
                button1.Image = StRttmy.Properties.Resources.EditorBoard_open;
                lockframe = false;//打开窗口调整
                //白板选项隐藏
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                button6.Visible = false;
                //画笔选项重置
                comboBox2.Text = "颜色";
                comboBox1.Text = "粗细";
                button2.Enabled = true;//启用菜单栏隐藏
            }
        }

        #region 最大化屏幕格局不变
        private void CourseResourceForm_Resize(object sender, EventArgs e)
        {
            //屏幕最大化
            if (this.WindowState == FormWindowState.Maximized)
            {
                splitContainer2.SplitterDistance = Convert.ToInt32(this.Height * 0.8);
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                splitContainer2.SplitterDistance = Convert.ToInt32(this.Height * 0.66);
            }
            splitContainer1.SplitterDistance = treeView1.Width;
            panel1.Height = splitContainer2.Panel2.Height - panel6.Height;
            if (!fullScreen)
            {
                splitContainer2.SplitterDistance = splitContainer2.Height - 44;
                panel1.Height = 0;
            }
            ColorSlider.Width = panel6.Width - 117;//进度条自适应
        }
        #endregion

        //禁止双击全屏
        private void axWindowsMediaPlayer2_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            if (axWindowsMediaPlayer2.fullScreen == true)
            {
                axWindowsMediaPlayer2.fullScreen = false;
            }
        }

        //禁止拖动窗口
        bool lockframe = false;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (lockframe)
            {
                if (m.Msg != 0x0112 && m.WParam != (IntPtr)0xF012)
                {
                    base.WndProc(ref m);
                }
            }
            else
                base.WndProc(ref m);
        }

        //隐藏文字与菜单
        private bool fullScreen = true;
        private void button2_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = fullScreen;
            //屏幕最大化 
            if (fullScreen)
            {
                splitContainer2.SplitterDistance = splitContainer2.Height - 44;
                panel1.Height = 0;
                fullScreen = false;
                button1.Enabled = false;
            }
            else
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    splitContainer2.SplitterDistance = Convert.ToInt32(this.Height * 0.7);
                }
                if (this.WindowState == FormWindowState.Maximized)
                {
                    splitContainer2.SplitterDistance = Convert.ToInt32(this.Height * 0.8); ;
                }
                splitContainer1.SplitterDistance = treeView1.Width;
                panel1.Height = splitContainer2.Panel2.Height - panel6.Height;
                fullScreen = true;
                button1.Enabled = true;
            }
            ColorSlider.Width = panel6.Width - 117;//进度条自适应          
        }

        //画笔粗细改变
        public delegate void DrwaEventHandler(float cx);
        public DrwaEventHandler DrwaCx;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrwaCx += dr.DrawSize;
            DrwaCx(Convert.ToSingle(comboBox1.Text));
        }

        #region 画笔颜色改变
        public class ComboBoxItem
        {
            private string _text = null;
            private object _value = null;
            public string Text { get { return this._text; } set { this._text = value; } }
            public object Value { get { return this._value; } set { this._value = value; } }
            public override string ToString()
            {
                return this._text;
            }
        }

        public delegate void ColorEventHandler(Color cc);
        public ColorEventHandler ColorCg;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = ((ComboBoxItem)comboBox2.SelectedItem).Text;
            object vlaue01 = ((ComboBoxItem)comboBox2.SelectedItem).Value;
            ColorCg += dr.Color_changed;
            ColorCg((Color)vlaue01);
        }
        #endregion
        //擦除按钮
        public delegate void ClearEventHandler();
        public ClearEventHandler Clear_rub;
        private void button6_Click(object sender, EventArgs e)
        {
            ClearEventHandler Clear_rub = new ClearEventHandler(dr.Clear_rb);
            Clear_rub();
        }

        /// <summary>
        /// 白板打开时关闭窗口功能
        /// </summary>
        /// <param name="f"> 本窗口</param>
        public void Close_win(Form f)
        {
            f.Close();
        }

        #region 播放控制条显示/隐藏

        private int tokens = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (tokens == 1 || tokens == 3)
            //{
            //    panel6.Visible = false;
            //    tokens = 0;
            //    timer2.Stop();
            //}    
        }

        //鼠标移入显示播放控制区
        private void splitContainer3_Panel1_MouseEnter(object sender, EventArgs e)
        {
            //timer2.Stop();
            //if (tokens == 0)
            //{
            //    panel6.Visible = true;
            //    tokens = 1;
            //}
        }

        private void panel6_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //  tokens = 3;
        }

        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            //  timer2.Start();
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //   tokens = 2;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            //  tokens = 1;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            // tokens = 2;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            //  tokens = 1;
        }

        private void ColorSlider_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //  tokens = 2;
        }

        private void ColorSlider_MouseLeave(object sender, EventArgs e)
        {
            // tokens = 1;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //  tokens = 2;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            // tokens = 1;
        }

        private void Sound_btn_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //   tokens = 2;
        }

        private void Sound_btn_MouseLeave(object sender, EventArgs e)
        {
            //  tokens = 1;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            // timer2.Stop();
            //  tokens = 2;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            // tokens = 1;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            // tokens = 2;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            //tokens = 1;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            // timer2.Stop();
            //  tokens = 2;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //  tokens = 1;
        }

        private void comboBox2_MouseEnter(object sender, EventArgs e)
        {
            // timer2.Stop();
            //  tokens = 2;
        }

        private void comboBox2_MouseLeave(object sender, EventArgs e)
        {
            // tokens = 1;
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //  tokens = 2;
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            //  tokens = 1;
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            //  timer2.Stop();
            //  tokens = 2;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            //  tokens = 1;
        }
        #endregion

       

       

    }
}