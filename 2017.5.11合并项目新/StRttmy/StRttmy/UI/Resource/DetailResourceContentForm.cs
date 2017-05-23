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
using System.IO;
using StRttmy.Common;

namespace StRttmy.UI
{
    public partial class DetailResourceContentForm : Form
    {
        //  主显示内容为htm文件时，用此控件显示
        private WebBrowser contentWebBrowser;
        private PictureBox pb;
        private ResourceBLL rb = null;
        private Resource oldResource = null;
        public Guid resourceId = Guid.Empty;
        private bool isMute = false;//不静音
        private string Mp3Path = "";//用于声音开关的判断,如果媒体文件因规则要求被静音,则声音开关不对它进行控制
        public string MainPath = "";//同mp3path的用途一样

        public DetailResourceContentForm()
        {
            InitializeComponent();
            pb = new PictureBox();
            contentWebBrowser = new WebBrowser();
        }

        //窗体加载
        private void DetailResourceContentForm_Load(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(2,103,155);
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Files\jm-2_DT.jpg");
            button5.Visible = false;//声音重播默认关闭
            ColorSlider.Enabled = false;//视频进度条禁用
            ShowResourceContent();
            toolTip1.SetToolTip(this.button4, "停止");
            toolTip1.SetToolTip(this.button3, "播放/暂停");
            toolTip1.SetToolTip(this.button5, "声音重放");
            toolTip1.SetToolTip(this.button1, "声音开关");
        }

        //清空各种控件内容
        private void CloseContent()
        {
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
        //屏幕大小切换

        private void DetailResourceContentForm_Resize(object sender, EventArgs e)
        {
            //屏幕最大化
            if (this.WindowState == FormWindowState.Maximized)
            {
                splitContainer1.SplitterDistance = Convert.ToInt32(this.Height * 0.8);
                ColorSlider.Width = panel6.Width - 217;//进度条自适应
                splitContainer2.SplitterDistance = 33;
            }
            else
            {
                //屏幕正常大小
                if (this.WindowState == FormWindowState.Normal)
                {
                    splitContainer1.SplitterDistance = Convert.ToInt32(this.Height * 0.68);
                    ColorSlider.Width = panel6.Width - 217;//进度条自适应

                }
            }
        }
        //禁止双击全屏
        private void axWindowsMediaPlayer2_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            if (axWindowsMediaPlayer2.fullScreen == true)
            {
                axWindowsMediaPlayer2.fullScreen = false;
            }
        }

        //初始化
        private void ShowResourceContent()
        {
            CloseContent();
            this.splitContainer2.Panel2.Controls.Clear();//清空文字区域
            txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;//填充文字控件
            axWindowsMediaPlayer1.URL = "";//清空单独声音控件
            Validation s = new Validation();          
            if (resourceId != Guid.Empty)
            {
                oldResource = new Resource();
                rb = new ResourceBLL();
                oldResource = rb.GetResource(resourceId);
                this.Text = oldResource.Title;
                //  媒体内容
                if (true)//s.validation_file(Path.GetFileName(oldResource.ContentFile))
                {
                    if (!string.IsNullOrEmpty(oldResource.ContentFile))
                    {
                        MainPath = Path.GetFileName(oldResource.ContentFile);//点击获取到主内容名称给声音控制提供参数
                        char kzmfgf = '.';
                        string[] kzm = oldResource.ContentFile.Split(kzmfgf);
                        //图片播放
                        if (kzm[1].ToLower() == "jpg" || kzm[1].ToLower() == "gif" || kzm[1].ToLower() == "png")
                        {
                            this.splitContainer1.Panel1.Controls.Clear();
                            pb.ImageLocation = Application.StartupPath + @"\Resources\ContentFiles\" + oldResource.ContentFile;
                            pb.SizeMode = PictureBoxSizeMode.Zoom;
                            this.splitContainer1.Panel1.Controls.Add(pb);
                            pb.Dock = System.Windows.Forms.DockStyle.Fill;
                        }
                        //html(unity3d)、htm播放
                        if (kzm[1].ToLower() == "htm" || kzm[1].ToLower() == "html" || kzm[1].ToLower() == "unity3d")
                        {
                            this.splitContainer1.Panel1.Controls.Clear();
                            if (kzm[1].ToLower() == "unity3d")
                                contentWebBrowser.Navigate(TextUtility.Ecode(oldResource.ContentFile));
                            else
                                contentWebBrowser.Navigate(Application.StartupPath + @"\Resources\ContentFiles\" + oldResource.ContentFile);
                            this.splitContainer1.Panel1.Controls.Add(contentWebBrowser);
                            contentWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                        }
                        //flash播放
                        if (kzm[1].ToLower() == "swf")
                        {
                            Swf.Movie = Application.StartupPath + @"\Resources\ContentFiles\" + oldResource.ContentFile;
                            if (Swf.ReadyState != 4)
                            {
                                Application.DoEvents();
                            }
                            this.splitContainer1.Panel1.Controls.Clear();
                            this.splitContainer1.Panel1.Controls.Add(Swf);
                            Swf.Dock = System.Windows.Forms.DockStyle.Fill;
                        }
                        //MP4播放
                        if (kzm[1].ToLower() == "mp4")
                        {
                            this.splitContainer1.Panel1.Controls.Clear();
                            string mp4Url = TextUtility.MP4Decode(Application.StartupPath + @"\Resources\ContentFiles\" + oldResource.ContentFile);//解密后的新路径
                            axWindowsMediaPlayer2.URL = mp4Url;
                            this.splitContainer1.Panel1.Controls.Add(axWindowsMediaPlayer2);
                            axWindowsMediaPlayer2.Dock = System.Windows.Forms.DockStyle.Fill;
                        }
                    }
                //  文字内容
                if (!string.IsNullOrEmpty(oldResource.TextFile))
                {
                    char hzmfgf = '.';
                    string[] hzm = oldResource.TextFile.Split(hzmfgf);
                    if (hzm[1].ToLower() == "htm" || hzm[1].ToLower() == "html")
                    {
                        this.splitContainer2.Panel2.Controls.Clear();
                        this.splitContainer2.Panel2.Controls.Add(txtWebBrowser);
                        txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                        txtWebBrowser.Navigate(Application.StartupPath + @"\Resources\TextFiles\" + oldResource.TextFile);
                    }
                }
                //  声音内容
                if (!string.IsNullOrEmpty(oldResource.SoundFile))
                {
                    char shzmfgf = '.';
                    string[] hzm = oldResource.SoundFile.Split(shzmfgf);
                    if (hzm[1].ToLower() == "mp3")
                    {
                        axWindowsMediaPlayer1.URL = Application.StartupPath + @"\Resources\SoundFiles\" + oldResource.SoundFile;
                        Mp3Path = oldResource.SoundFile;
                    }
                }
                if (!string.IsNullOrEmpty(oldResource.ContentFile))
                {
                    SoundSwitch();
                }
            }              
            }
            else
            {
                MessageBox.Show("此素材不存在。");
                Close();
            }
        }

        //声音开关
     

        private void button1_Click(object sender, EventArgs e)
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
            button1.Image = isMute ? StRttmy.Properties.Resources.sound_ban : StRttmy.Properties.Resources.sound;
            axWindowsMediaPlayer1.settings.mute = isMute;//axWindowsMediaPlayer1开关音量
            if (tempMainPath.Split('.')[1] == "mp4" && tempMp3path != "specificempty001.mp3")//当媒体文件是mp4声音文件又不是默认的空声音时,mp4静音
            {
                axWindowsMediaPlayer2.settings.mute = true;//axWindowsMediaPlayer2静音 
                button5.Visible = true;
            }
            else
            {
                axWindowsMediaPlayer2.settings.mute = isMute;//axWindowsMediaPlayer2开关音量
                if (tempMainPath.Split('.')[1] != "mp4" && tempMp3path != "specificempty001.mp3")
                    button5.Visible = true;
                else
                    button5.Visible = false;//声音重播显示
            }      
        }

        //关闭窗体
        private void DetailResourceContentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeContent();
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

        #region 播放控制条显示/隐藏

        private int tokens = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (tokens == 1 || tokens == 3)
            {
                panel6.Visible = false;
                tokens = 0;
                timer2.Stop();
            }   
        }

        //鼠标移入显示播放控制区
        private void splitContainer2_Panel1_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            if (tokens == 0)
            {
                panel6.Visible = true;
                tokens = 1;
            }
        }

        private void panel6_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 3;
            ColorSlider.Width = panel6.Width - 217;//进度条自适应
        }

        private void panel6_MouseLeave(object sender, EventArgs e)
        {
            timer2.Start();
            ColorSlider.Width = panel6.Width - 217;//进度条自适应
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }

        private void ColorSlider_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void ColorSlider_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }

      
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }

         #endregion


       
    }
}
