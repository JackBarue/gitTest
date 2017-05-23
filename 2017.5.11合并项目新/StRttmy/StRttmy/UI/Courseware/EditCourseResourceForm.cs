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
using StRttmy.Common;
using System.IO;
using System.Threading;
using StRttmy.Repository;

namespace StRttmy.UI
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class EditCourseResourceForm : Form
    {
        private ResourceBLL rb = null;
        private PictureBox pb;
        private CourseBLL cb = null;
        public Guid coursewareId = Guid.Empty;
        private WebBrowser contentWebBrowser;//主显示内容为htm文件时，用此控件显示
        private string Mp3Path = "";//用于声音开关的判断,如果媒体文件因规则要求被静音,则声音开关不对它进行控制
        public string MainPath = "";//1.用于派生新素材时需要的判断条件 2.同Mp3Path的用途一样
        private string TextPath = "";//用于和上两条共同判断重复播放同一个素材的处理
        //private int NowPlayArea = 0;//用于判断3个区域播放同一素材时的处理,1左边课件,2右边素材,3右边课件素材
        //private double LastTime = 0.0;//用于判断连续点击播放的限制
        private bool isMute = false;//不静音
        private bool tv1 = false;//treeView1是否被激活
        private bool isHaveSave = false;//是否需要保存
        private TreeNode TargetNode;//通过目的地坐标获取目的地节点
        private int treeViewArea = 0;//判断是treeView1拖放到treeView1还是tvCourse拖放到treeView1


        private ResourceRepository dc;
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;

        public EditCourseResourceForm()
        {
            InitializeComponent();
            pb = new PictureBox();
            contentWebBrowser = new WebBrowser();
           // this.contentWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(contentWebBrowser_DocumentCompleted);// 点击U3D加载完成前禁用其它控件控件绑定。
            this.contentWebBrowser.ObjectForScripting = this;
            
        }

        //窗体加载
        private void EditCourseResourceForm_Load(object sender, EventArgs e)
        {
            treeView1.BackColor = Color.FromArgb(162,211,226);
            panel6.BackColor = Color.FromArgb(2, 103, 155);
            dgvResourceList.BackgroundColor = Color.FromArgb(162, 211, 226);
            dgvCourseList.BackgroundColor = Color.FromArgb(162, 211, 226);
            SetResType();
            SetCourseType();
            this.splitContainer2.Panel1.Controls.Clear();
            this.splitContainer2.Panel1.Controls.Add(Swf);
            Swf.Dock = System.Windows.Forms.DockStyle.Fill;
            Swf.Movie = Application.StartupPath + @"\Files\studyeasy.swf";
            this.tvCourse.AllowDrop = true;
            this.treeView1.AllowDrop = true;
            this.treeView1.LabelEdit = true;
            this.treeView1.Enabled = true;
            axWindowsMediaPlayer1.Hide();
            BindCourse();
            //axWindowsMediaPlayer2.uiMode = "none";
            button5.Visible = false;//声音重播默认关闭
            ColorSlider.Enabled = false;//视频进度条禁用
            toolTip1.SetToolTip(this.button4, "停止");
            toolTip1.SetToolTip(this.button3, "播放/暂停");
            toolTip1.SetToolTip(this.button5, "声音重放");
            toolTip1.SetToolTip(this.button6, "声音开关");
           
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

       

        #region 最大化屏幕格局不变


        private void EditCourseResourceForm_Resize(object sender, EventArgs e)
        {
            //屏幕最大化
            if (this.WindowState == FormWindowState.Maximized)
            {
                splitContainer1.Width = this.Width - panel7.Width - 10;
                splitContainer2.SplitterDistance = 830;
                ColorSlider.Width = panel6.Width - 217;//进度条自适应
                splitContainer3.SplitterDistance = 33;
            }
            else
            {
                //屏幕正常大小
                if (this.WindowState == FormWindowState.Normal)
                {
                    splitContainer1.Width = this.Width - panel7.Width - 10;
                    splitContainer2.SplitterDistance = 496;
                    ColorSlider.Width = panel6.Width - 217;//进度条自适应
                }
            }
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

      #region 当前课件


        //左边treeView1初始化数据
        private void BindCourse()
        {
            if (coursewareId != Guid.Empty)
            {
                Model.Courseware course = new Model.Courseware();
                List<CoursewareResource> crs = new List<CoursewareResource>();
                cb = new CourseBLL();
                course = cb.GetCourse(coursewareId);
                crs = course.coursewareResources.ToList<CoursewareResource>();
               // lblName.Text = course.Name;
                this.Text = course.Name;
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

        //把数据绑定到treeView1(左边)
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
            TreeNode treeNode = new TreeNode();
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

        //左边课件素材点击
        
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //this.treeView1.Enabled = false;//禁止对树菜单操作 
            if (e.Button == MouseButtons.Left) 
            {
                CourseResourceTag tag = new CourseResourceTag();
                tag = (CourseResourceTag)e.Node.Tag;
                MainPath = "";
                if (tag.MainUrl != null)
                {
                    CloseContent();
                    this.splitContainer3.Panel2.Controls.Clear();//清空文字区域
                    txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;//填充文字控件
                    axWindowsMediaPlayer1.URL = "";//清空单独声音控件
                    Validation s = new Validation();
                    string file_name = tag.MainUrl.Substring(tag.MainUrl.LastIndexOf("\\") + 1);
                    if (true)//s.validation_file(file_name)
                    {
                        //  主内容链接
                        if (!string.IsNullOrEmpty(tag.MainUrl))
                        {
                            //string BeforeMainPath = MainPath;
                            //string BeforeMp3Path = Mp3Path;
                            //string BeforeTextPath = TextPath;
                            MainPath = tag.MainUrl;//左边课件素材点击获取到主内容名称给创建新素材和声音控制提供参数
                            Mp3Path = tag.Mp3Url;
                            TextPath = tag.TextUrl;
                            //double NowTime = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                            //double ts = NowTime - LastTime;
                            //if (LastTime != 0.0 && ts < 3000.0)
                            //{
                            //    MainPath = BeforeMainPath;
                            //    Mp3Path = BeforeMp3Path;
                            //    TextPath = BeforeTextPath;
                            //    MessageBox.Show("请勿连续播放,请稍后再试!");
                            //    return;
                            //}
                            //if (Path.GetFileName(BeforeMainPath) == Path.GetFileName(tag.MainUrl) && Path.GetFileName(BeforeMp3Path) == Path.GetFileName(tag.Mp3Url) && Path.GetFileName(BeforeTextPath) == Path.GetFileName(tag.TextUrl))
                            //{
                            //    if (NowPlayArea == 1)
                            //    {
                            //        MessageBox.Show("正在播放当前内容,请勿重复播放！");
                            //    }
                            //    return;
                            //}
                            //else
                            //{
                            //    NowPlayArea = 1;//当播放的3个文件都不相同时才切换播放区域
                            //}
                            string kzm = MainPath.Substring(MainPath.LastIndexOf(".") + 1).ToLower();
                            //图片播放
                            if (kzm == "jpg" || kzm == "gif" || kzm == "png")
                            {
                                this.splitContainer2.Panel1.Controls.Clear();
                                pb.ImageLocation = tag.MainUrl;
                                pb.SizeMode = PictureBoxSizeMode.Zoom;
                                this.splitContainer2.Panel1.Controls.Add(pb);
                                pb.Dock = System.Windows.Forms.DockStyle.Fill;
                            }
                            //html(unity3d)、htm播放
                            if (kzm == "htm" || kzm == "html" || kzm == "unity3d")
                            {
                                // Lock_unity(); // 点击U3D加载完成前禁用其它控件。
                                U3Dloading();
                                this.splitContainer2.Panel1.Controls.Clear();
                                if (kzm == "unity3d")
                                    contentWebBrowser.Navigate(TextUtility.Ecode(tag.MainUrl));
                                else
                                    contentWebBrowser.Navigate(tag.MainUrl);
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
                                this.splitContainer3.Panel2.Controls.Clear();
                                this.splitContainer3.Panel2.Controls.Add(txtWebBrowser);
                                txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                                txtWebBrowser.Navigate(tag.TextUrl);
                            }
                            else
                            {
                                MessageBox.Show("未知的文字文件格式！");
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
                                MessageBox.Show("未知的声音文件格式！");
                            }
                            //LastTime = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
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
       // private void contentWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
       // {

            //if (contentWebBrowser.ReadyState == WebBrowserReadyState.Complete)
            //{
            //    panel7.Enabled = true;
            //    splitContainer1.Panel1.Enabled = true;
            //    panel6.Enabled = true;
            //}
            //else
            //{
            //    contentWebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(contentWebBrowser_DocumentCompleted);
            //}


        // }
        #region 点击u3d禁止其余操作
        /// <summary>
        /// 点击u3d后锁定事件
        /// </summary>
        public void Lock_unity()
        {
            panel7.Enabled = false;
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
                panel7.Enabled = true;
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
                string[] kzm = u3dFile.Split(kzmfgf);
                if (kzm[1].ToLower() == "html" || kzm[1].ToLower() == "unity3d")
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
            panel7.Enabled = true;
            splitContainer1.Panel1.Enabled = true;
            panel6.Enabled = true;
            timer5.Stop();
        }

      #endregion

        
        //声音开关
        private void button6_Click(object sender, EventArgs e)
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
            button6.Image = isMute ? StRttmy.Properties.Resources.sound_ban : StRttmy.Properties.Resources.sound;
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

        //关闭窗体
        private void EditCourseResourceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isHaveSave)
            {
                DialogResult dr = MessageBox.Show("有未保存的操作,是否保存?", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    checkSave();
                }
            }
            DisposeContent();
        }
        
        #endregion

        #region 素材

        //右边素材列表初始化数据
        public void ShowResourceList(string keyword, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (Program.mf.LoginUser != null)
            {
                dgvResourceList.DataSource = null;
                dgvResourceList.Columns.Clear();
                IList<Resource> resources = null;
                rb = new ResourceBLL();
                resources = rb.AllResourceList(keyword,Guid.Empty, category, level, subjects, typeofwork, systemtype);
                this.dgvResourceList.DataSource = resources;
                SetResDgv();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //右边素材列表表头和属性值
        private void SetResDgv()
        {
            dgvResourceList.ColumnHeadersVisible = false;
            dgvResourceList.RowHeadersVisible = false;
            dgvResourceList.Columns[0].HeaderText = "素材ID";
            dgvResourceList.Columns[1].HeaderText = "素材类型";
            dgvResourceList.Columns[2].HeaderText = "素材级别";
            dgvResourceList.Columns[3].HeaderText = "素材标题";
            dgvResourceList.Columns[4].HeaderText = "素材关键字";
            dgvResourceList.Columns[5].HeaderText = "媒体文件";
            dgvResourceList.Columns[6].HeaderText = "文字文件";
            dgvResourceList.Columns[7].HeaderText = "声音文件";
            dgvResourceList.Columns[8].HeaderText = "创建人Id";
            dgvResourceList.Columns[9].HeaderText = "创建人";
            dgvResourceList.Columns[10].HeaderText = "创建时间";
            dgvResourceList.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvResourceList.Columns["StLevel"].Visible = false;
            dgvResourceList.Columns["StLevelId"].Visible = false;
            dgvResourceList.Columns["StType"].Visible = false;
            dgvResourceList.Columns["StTypeId"].Visible = false;
            dgvResourceList.Columns["StTypeSupply"].Visible = false;
            dgvResourceList.Columns["StTypeSupplyId"].Visible = false;
            dgvResourceList.Columns["ResourceId"].Visible = false;
           // dgvResourceList.Columns["Type"].Visible = false;
            dgvResourceList.Columns["Level"].Visible = false;
            dgvResourceList.Columns["Keyword"].Visible = false;
            dgvResourceList.Columns["ContentFile"].Visible = false;
            dgvResourceList.Columns["TextFile"].Visible = false;
            dgvResourceList.Columns["SoundFile"].Visible = false;
            dgvResourceList.Columns["UserId"].Visible = false;
            dgvResourceList.Columns["User"].Visible = false;
            dgvResourceList.Columns["CreateTime"].Visible = false;
            dgvResourceList.MultiSelect = true;//允许多选行
            dgvResourceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//选中整行中的某一个单元格即选中整行
            //DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
            //dgvButtonColEdit.Name = "ManageDetail";
            //// 设置列标题
            //dgvButtonColEdit.HeaderText = "查看信息";
            //// 设置按钮标题
            //dgvButtonColEdit.UseColumnTextForButtonValue = true;
            //dgvButtonColEdit.Text = "查看信息";
            //dgvButtonColEdit.Width = 76;

            //DataGridViewButtonColumn dgvButtonColDel = new DataGridViewButtonColumn();
            //// 设置列名
            //dgvButtonColDel.Name = "ManageContent";
            //// 设置列标题
            //dgvButtonColDel.HeaderText = "查看内容";
            //// 设置按钮标题
            //dgvButtonColDel.UseColumnTextForButtonValue = true;
            //dgvButtonColDel.Text = "查看内容";
            //dgvButtonColDel.Width = 76;

            //// 向DataGridView追加
            //dgvResourceList.Columns.Insert(dgvResourceList.Columns.Count, dgvButtonColEdit);
            //dgvResourceList.Columns.Insert(dgvResourceList.Columns.Count, dgvButtonColDel);
        }

        //右边素材列表查询下来框赋值
        private List<StType> sd1 = null;
        private List<StTypeSupply> cr1 = null;
        private List<StLevel> fr1 = null;
        private void SetResType()
        {
            dc = new ResourceRepository();
            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();


            sd1 = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
            StType ins = new StType();
            ins.StTypeId = Guid.Empty;
            ins.Name = "全部";
            sd1.Insert(0, ins);
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "StTypeId";
            cmbType.DataSource = sd1;

            cr1 = sts.StTypeSupplyList().ToList();
            StTypeSupply insts = new StTypeSupply();
            insts.StTypeSupplyId = Guid.Empty;
            insts.StTypeName = "全部";
            cr1.Insert(0, insts);
            comboBox2.DisplayMember = "StTypeName";
            comboBox2.ValueMember = "StTypeSupplyId";
            comboBox2.DataSource = cr1;

            fr1 = sl.StLevelList().ToList();
            StLevel insl = new StLevel();
            insl.StLevelId = Guid.Empty;
            insl.StLevelName = "全部";
            fr1.Insert(0, insl);
            comboBox3.DisplayMember = "StLevelName";
            comboBox3.ValueMember = "StLevelId";
            comboBox3.DataSource = fr1;
            ShowResourceList(txtResKeyword.Text, categorystr, levelstr, subjects, TypeOfWork, SystemType);
        }

        #region 下拉选框
        private Guid SystemType = Guid.Empty;
        private List<StType> sd2 = null;
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)cmbType.SelectedValue != Guid.Empty)
            {
                SystemType = (Guid)cmbType.SelectedValue;
                sd2 = st.StTypeList(SystemType);
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd2.Insert(0, ins);
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "StTypeId";
                comboBox1.DataSource = sd2;
            }
            else
            {

                SystemType = Guid.Empty;

                comboBox1.ResetText();
                comboBox4.ResetText();
                comboBox1.SelectedText = "全部";
                TypeOfWork = Guid.Empty;
                comboBox4.SelectedText = "全部";
                subjects = Guid.Empty;
                comboBox1.DataSource = null;
                comboBox4.DataSource = null;
            }

        }
        private Guid TypeOfWork = Guid.Empty;
        private List<StType> sd3 = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedText != "全部")
            {
                if ((Guid)comboBox1.SelectedValue != Guid.Empty)
                {
                    TypeOfWork = (Guid)comboBox1.SelectedValue;
                    sd3 = st.StTypeList(TypeOfWork);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd3.Insert(0, ins);
                    comboBox4.DisplayMember = "Name";
                    comboBox4.ValueMember = "StTypeId";
                    comboBox4.DataSource = sd3;
                }
                else
                {
                    TypeOfWork = Guid.Empty;
                    subjects = Guid.Empty;
                    comboBox4.ResetText();
                    comboBox4.SelectedText = "全部";
                    comboBox4.DataSource = null;
                }
            }
            else
            {

                TypeOfWork = Guid.Empty;
                comboBox4.ResetText();
                comboBox4.SelectedText = "全部";
                comboBox4.DataSource = null;
            }
        }

        private Guid subjects = Guid.Empty;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox4.SelectedText != "全部")
            {
                if ((Guid)comboBox4.SelectedValue != Guid.Empty)
                {
                    subjects = (Guid)comboBox4.SelectedValue;
                }
                else
                {
                    subjects = Guid.Empty;
                }
            }
            else
            {
                subjects = Guid.Empty;
            }
        }


        private Guid categorystr = Guid.Empty;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox2.SelectedValue != Guid.Empty)
            {
                categorystr = (Guid)comboBox2.SelectedValue;
            }
            else
            {
                categorystr = Guid.Empty;
            }
        }

        private Guid levelstr = Guid.Empty;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox3.SelectedValue != Guid.Empty)
            {
                levelstr = (Guid)comboBox3.SelectedValue;
            }
            else
            {
                levelstr = Guid.Empty;
            }
        }
        #region 下拉菜单悬浮显示
        private void cmbType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd1[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(sd1[e.Index].Name, cmbType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(cmbType);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd2[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(sd2[e.Index].Name, comboBox1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox1);
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(cr1[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(cr1[e.Index].StTypeName, comboBox2, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox2);
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(fr1[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(fr1[e.Index].StLevelName, comboBox3, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox3);
        }

        private void comboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd3[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(sd3[e.Index].Name, comboBox4, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox4);
        }
        #endregion


        #endregion

        //右边素材列表查询
        private void btnResourceQuery_Click(object sender, EventArgs e)
        {
              ShowResourceList(txtResKeyword.Text, categorystr, levelstr, subjects, TypeOfWork, SystemType);
        }

        //右边素材列表单元格点击
        private void dgvResourceList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string contentFile, textFile, soundFile, BeforeMainPath;
                contentFile = dgvResourceList.Rows[e.RowIndex].Cells["ContentFile"].Value.ToString();
                BeforeMainPath = MainPath;
                MainPath = Application.StartupPath + @"\Resources\ContentFiles\" + contentFile;//素材列表单元格点击获取到主内容名称给派生新素材和声音控制提供参数
                textFile = dgvResourceList.Rows[e.RowIndex].Cells["TextFile"].Value.ToString();
                soundFile = dgvResourceList.Rows[e.RowIndex].Cells["SoundFile"].Value.ToString();
                PlayResource(contentFile, textFile, soundFile, BeforeMainPath);
                U3Dloading();
            }
        }
      

        //右边素材列表单元格双击
        private void dgvResourceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MainPath = AddTreeNode(e.RowIndex);
        }

        //右边素材列表点击后播放
        
        private void PlayResource(string contentFile, string textFile, string soundFile, string BeforeMainPath)
        {
            CloseContent();
            this.splitContainer3.Panel2.Controls.Clear();//清空文字区域
            txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;//填充文字控件
            axWindowsMediaPlayer1.URL = "";//清空单独声音控件
            Validation s = new Validation();
            //string file_name = tag.MainUrl.Substring(tag.MainUrl.LastIndexOf("\\") + 1);
            if (true)//s.validation_file(contentFile)
            {
                //  媒体内容
                if (!string.IsNullOrEmpty(contentFile))
                {
                    string BeforeMp3Path = Mp3Path;
                    string BeforeTextPath = TextPath;
                    Mp3Path = Application.StartupPath + @"\Resources\SoundFiles\" + soundFile;
                    TextPath = Application.StartupPath + @"\Resources\TextFiles\" + textFile;
                    //double NowTime = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                    //double ts = NowTime - LastTime;
                    //if (LastTime != 0.0 && ts < 3000.0)
                    //{
                    //    MainPath = BeforeMainPath;
                    //    Mp3Path = BeforeMp3Path;
                    //    TextPath = BeforeTextPath;
                    //    MessageBox.Show("请勿连续播放,请稍后再试!");
                    //    return;
                    //}
                    //if (Path.GetFileName(BeforeMainPath) == contentFile && Path.GetFileName(BeforeMp3Path) == soundFile && Path.GetFileName(BeforeTextPath) == textFile)
                    //{
                    //    if (NowPlayArea == 2)
                    //    {
                    //        MessageBox.Show("正在播放当前内容,请勿重复播放！");
                    //    }
                    //    return;
                    //}
                    //else
                    //{
                    //    NowPlayArea = 2;//当播放的3个文件都不相同时才切换播放区域
                    //}             
                    char kzmfgf = '.';
                    string[] kzm = contentFile.Split(kzmfgf);
                    //图片播放
                    if (kzm[1].ToLower() == "jpg" || kzm[1].ToLower() == "gif" || kzm[1].ToLower() == "png")
                    {
                        this.splitContainer2.Panel1.Controls.Clear();
                        pb.ImageLocation = Application.StartupPath + @"\Resources\ContentFiles\" + contentFile;
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        this.splitContainer2.Panel1.Controls.Add(pb);
                        pb.Dock = System.Windows.Forms.DockStyle.Fill;
                    }
                    //html(unity3d)、htm播放
                    if (kzm[1].ToLower() == "htm" || kzm[1].ToLower() == "html" || kzm[1].ToLower() == "unity3d")
                    {

                        // timer3.Start();
                        this.splitContainer2.Panel1.Controls.Clear();
                        if (kzm[1].ToLower() == "unity3d")
                            contentWebBrowser.Navigate(TextUtility.Ecode(contentFile));
                        else
                            contentWebBrowser.Navigate(Application.StartupPath + @"\Resources\ContentFiles\" + contentFile);
                        this.splitContainer2.Panel1.Controls.Add(contentWebBrowser);
                        contentWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                    }
                    //flash播放
                    if (kzm[1].ToLower() == "swf")
                    {
                        Swf.Movie = Application.StartupPath + @"\Resources\ContentFiles\" + contentFile;
                        if (Swf.ReadyState != 4)
                        {
                            Application.DoEvents();
                        }
                        this.splitContainer2.Panel1.Controls.Clear();
                        this.splitContainer2.Panel1.Controls.Add(Swf);
                        Swf.Dock = System.Windows.Forms.DockStyle.Fill;
                    }
                    //MP4播放
                    if (kzm[1].ToLower() == "mp4")
                    {
                        string mp4Url = TextUtility.MP4Decode(Application.StartupPath + @"\Resources\ContentFiles\" + contentFile);//解密后的新路径
                        axWindowsMediaPlayer2.URL = mp4Url;
                        this.splitContainer2.Panel1.Controls.Clear();
                        this.splitContainer2.Panel1.Controls.Add(axWindowsMediaPlayer2);
                        axWindowsMediaPlayer2.Dock = System.Windows.Forms.DockStyle.Fill;

                    }
                }
                //  文字内容
                if (!string.IsNullOrEmpty(textFile))
                {
                    char hzmfgf = '.';
                    string[] hzm = textFile.Split(hzmfgf);
                    if (hzm[1].ToLower() == "htm" || hzm[1].ToLower() == "html")
                    {
                        this.splitContainer3.Panel2.Controls.Clear();
                        this.splitContainer3.Panel2.Controls.Add(txtWebBrowser);
                        txtWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                        txtWebBrowser.Navigate(Application.StartupPath + @"\Resources\TextFiles\" + textFile);
                    }
                }
                //  声音内容
                if (!string.IsNullOrEmpty(soundFile))
                {
                    char shzmfgf = '.';
                    string[] hzm = soundFile.Split(shzmfgf);
                    if (hzm[1].ToLower() == "mp3")
                    {
                        axWindowsMediaPlayer1.URL = Application.StartupPath + @"\Resources\SoundFiles\" + soundFile;
                    }
                }
                if (Mp3Path != "" && MainPath != "")
                {
                    SoundSwitch();
                }
            }
        }

        //添加
        private void button1_Click(object sender, EventArgs e)
        {
            int[] selIndexes = dgvResourceList.SelectedRows.OfType<DataGridViewRow>().Select(x => x.Index).OrderBy(x => x).ToArray();//多选行的行号数组
            if (selIndexes.Length > 0)
            {
                foreach (int n in selIndexes)
                {
                    AddTreeNode(n);
                }
            }
            else {
                MessageBox.Show("还未选中需要添加的素材。");
            }            
        }

        //添加空节点
        private void button2_Click(object sender, EventArgs e)
        {
            AddTreeNode(-1);
        }

        //添加ztree数据方法
        private string AddTreeNode(int rowIndex)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = rowIndex == -1 ? "空节点" : dgvResourceList.Rows[rowIndex].Cells["Title"].Value.ToString();
            CourseResourceTag tag = new CourseResourceTag();
            string contentStr = rowIndex == -1 ? "" : dgvResourceList.Rows[rowIndex].Cells["ContentFile"].Value.ToString();
            tag.MainUrl = rowIndex == -1 ? "" : Application.StartupPath + @"\Resources\ContentFiles\" + contentStr;
            tag.Mp3Url = rowIndex == -1 ? "" : Application.StartupPath + @"\Resources\SoundFiles\" + dgvResourceList.Rows[rowIndex].Cells["SoundFile"].Value.ToString();
            tag.TextUrl = rowIndex == -1 ? "" : Application.StartupPath + @"\Resources\TextFiles\" + dgvResourceList.Rows[rowIndex].Cells["TextFile"].Value.ToString();
            treeNode.Tag = tag;
            this.treeView1.Nodes[0].Nodes.Add(treeNode);
            this.treeView1.SelectedNode = treeNode;
            this.treeView1.Select();
            isHaveSave = true;
            return contentStr;
        }

        //派生新素材
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (MainPath == "")
            {
                MessageBox.Show("请选择一个素材。");
                return;
            }
            DerivedResourceForm drf = new DerivedResourceForm();
            drf.ecrlf = this;
            drf.MainPath = MainPath;
            drf.ShowDialog();
        }

        #endregion        

        #region 课件素材

        //右边课件列表初始化数据
        private void ShowCourseList(string keyword, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (Program.mf.LoginUser != null)
            {
                dgvCourseList.DataSource = null;
                dgvCourseList.Columns.Clear();
                IList<Model.Courseware> Coursewares = null;
                cb = new CourseBLL();
                Coursewares = cb.CusCoursewareList(keyword,Guid.Empty, type, category, level, subjects, typeofwork, systemtype);
                this.dgvCourseList.DataSource = Coursewares;
                SetCourseDgv();
                tvCourse.Nodes.Clear();
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //右边课件列表表头和属性值
        private void SetCourseDgv()
        {
            dgvCourseList.ColumnHeadersVisible = false;
            dgvCourseList.RowHeadersVisible = false;
            dgvCourseList.Columns[0].HeaderText = "课件ID";
            dgvCourseList.Columns[1].HeaderText = "课件类型";
            dgvCourseList.Columns[2].HeaderText = "课件级别";
            dgvCourseList.Columns[3].HeaderText = "课件关键字";
            dgvCourseList.Columns[4].HeaderText = "课件名称";
            dgvCourseList.Columns[5].HeaderText = "课件简介";
            dgvCourseList.Columns[6].HeaderText = "创建人Id";
            dgvCourseList.Columns[7].HeaderText = "创建人";
            dgvCourseList.Columns[8].HeaderText = "创建时间";
            dgvCourseList.Columns[9].HeaderText = "课件素材";
            dgvCourseList.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCourseList.Columns["StLevel"].Visible = false;
            dgvCourseList.Columns["StLevelId"].Visible = false;
            dgvCourseList.Columns["StType"].Visible = false;
            dgvCourseList.Columns["StTypeId"].Visible = false;
            dgvCourseList.Columns["StTypeSupply"].Visible = false;
            dgvCourseList.Columns["StTypeSupplyId"].Visible = false;
            dgvCourseList.Columns["CoursewareLevel"].Visible = false;
            dgvCourseList.Columns["CoursewareLevelId"].Visible = false;
            dgvCourseList.Columns["CoursewareId"].Visible = false;
            //dgvCourseList.Columns["Type"].Visible = false;
            //dgvCourseList.Columns["Level"].Visible = false;
            dgvCourseList.Columns["Keyword"].Visible = false;
            dgvCourseList.Columns["Description"].Visible = false;
            dgvCourseList.Columns["UserId"].Visible = false;
            dgvCourseList.Columns["User"].Visible = false;
            dgvCourseList.Columns["CreateTime"].Visible = false;
            dgvCourseList.Columns["coursewareResources"].Visible = false;
            //DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();                // 设置列名
            //dgvButtonColEdit.Name = "ManageDetail";
            //// 设置列标题
            //dgvButtonColEdit.HeaderText = "查看信息";
            //// 设置按钮标题
            //dgvButtonColEdit.UseColumnTextForButtonValue = true;
            //dgvButtonColEdit.Text = "查看信息";
            //dgvButtonColEdit.Width = 76;

            //DataGridViewButtonColumn dgvButtonColContent = new DataGridViewButtonColumn();
            //// 设置列名
            //dgvButtonColContent.Name = "ManageContent";
            //// 设置列标题
            //dgvButtonColContent.HeaderText = "查看内容";
            //// 设置按钮标题
            //dgvButtonColContent.UseColumnTextForButtonValue = true;
            //dgvButtonColContent.Text = "查看内容";
            //dgvButtonColContent.Width = 76;

            //// 向DataGridView追加
            //dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColEdit);
            //dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColContent);
        }

        //右边课件列表查询类型
        private CoursewareRepository dcr;
        private CoursewareLevelRepository cl;
        private List<CoursewareLevel> er1 = null;
        private List<StType> sd4 = null;
        private List<StTypeSupply> cr2 = null;
        private List<StLevel> fr2 = null; 
        private void SetCourseType()
        {
            dcr = new CoursewareRepository();
            st = new StTypeRepository();
            cl = new CoursewareLevelRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();
            er1 = new List<CoursewareLevel>();
            IList<CoursewareLevel> cler = cl.CoursewareLevelsList();
            foreach (CoursewareLevel a in cler)
            {
                if (a.CoursewareLevelId != Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"))
                {
                    er1.Add(a);
                }
            }
            CoursewareLevel iner = new CoursewareLevel();
            iner.CoursewareLevelId = Guid.Empty;
            iner.Name = "全部";
            er1.Insert(0, iner);
            comboBox6.DisplayMember = "Name";
            comboBox6.ValueMember = "CoursewareLevelId";
            comboBox6.DataSource = er1;

            sd4 = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
            StType ins = new StType();
            ins.StTypeId = Guid.Empty;
            ins.Name = "全部";
            sd4.Insert(0, ins);
            comboBox5.DisplayMember = "Name";
            comboBox5.ValueMember = "StTypeId";
            comboBox5.DataSource = sd4;

            cr2 = sts.StTypeSupplyList().ToList();
            StTypeSupply insts = new StTypeSupply();
            insts.StTypeSupplyId = Guid.Empty;
            insts.StTypeName = "全部";
            cr2.Insert(0, insts);
            comboBox7.DisplayMember = "StTypeName";
            comboBox7.ValueMember = "StTypeSupplyId";
            comboBox7.DataSource = cr2;

            fr2 = sl.StLevelList().ToList();
            StLevel insl = new StLevel();
            insl.StLevelId = Guid.Empty;
            insl.StLevelName = "全部";
            fr2.Insert(0, insl);
            comboBox10.DisplayMember = "StLevelName";
            comboBox10.ValueMember = "StLevelId";
            comboBox10.DataSource = fr2;
            ShowCourseList(txtCourseKeyword.Text, cmbTypestr1, categorystr1, levelstr1, subjects1, TypeOfWork1, SystemType1);           
        }

        #region 课件类型选择框

        private Guid cmbTypestr1 = Guid.Empty;
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox6.SelectedValue != Guid.Empty)
            {
                cmbTypestr1 = (Guid)comboBox6.SelectedValue;
            }
            else
            {
                cmbTypestr1 = Guid.Empty;
            }
        }



        private Guid SystemType1 = Guid.Empty;
        private List<StType> sd5 = null;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox5.SelectedValue != Guid.Empty)
            {
                SystemType1 = (Guid)comboBox5.SelectedValue;
                sd5 = st.StTypeList(SystemType1);
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd5.Insert(0, ins);
                comboBox8.DisplayMember = "Name";
                comboBox8.ValueMember = "StTypeId";
                comboBox8.DataSource = sd5;
            }
            else
            {

                SystemType = Guid.Empty;

                comboBox8.ResetText();
                comboBox9.ResetText();               
                TypeOfWork1 = Guid.Empty;                
                subjects1 = Guid.Empty;
                comboBox8.DataSource = null;
                comboBox9.DataSource = null;
                comboBox8.Text = "全部";
                comboBox9.Text = "全部";
            }
        }

 

        private Guid TypeOfWork1 = Guid.Empty;
        private List<StType> sd6 = null;
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.Text != "全部" && comboBox8.Text != "")
            {
                if ((Guid)comboBox8.SelectedValue != Guid.Empty)
                {
                    TypeOfWork1 = (Guid)comboBox8.SelectedValue;
                    sd6 = st.StTypeList(TypeOfWork1);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd6.Insert(0, ins);
                    comboBox9.DisplayMember = "Name";
                    comboBox9.ValueMember = "StTypeId";
                    comboBox9.DataSource = sd6;
                }
                else
                {
                    TypeOfWork1 = Guid.Empty;
                    subjects1 = Guid.Empty;
                    comboBox9.ResetText();
                    comboBox9.Text = "全部";
                    comboBox9.DataSource = null;
                }
            }
            else
            {

                TypeOfWork1 = Guid.Empty;
                comboBox9.ResetText();
                comboBox9.DataSource = null;
                comboBox9.Text = "全部";
            }

        }

        private Guid subjects1 = Guid.Empty;
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text != "全部" && comboBox9.Text != "")
            {
                if ((Guid)comboBox9.SelectedValue != Guid.Empty)
                {
                    subjects1 = (Guid)comboBox9.SelectedValue;
                }
                else
                {
                    subjects1 = Guid.Empty;
                }
            }
            else
            {
                subjects1 = Guid.Empty;
            }

        }


        private Guid categorystr1 = Guid.Empty;
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox7.SelectedValue != Guid.Empty)
            {
                categorystr1 = (Guid)comboBox7.SelectedValue;
            }
            else
            {
                categorystr1 = Guid.Empty;
            }
        }

        private Guid levelstr1 = Guid.Empty;
        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox10.SelectedValue != Guid.Empty)
            {
                levelstr1 = (Guid)comboBox10.SelectedValue;
            }
            else
            {
                levelstr1 = Guid.Empty;
            }
        }
        #region 下拉框悬浮提示
        private void comboBox6_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(er1[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(er1[e.Index].Name, comboBox6, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox6_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox6);
        }

        private void comboBox5_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd4[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(sd4[e.Index].Name, comboBox5, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox5_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox5);
        }

        private void comboBox8_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd5[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(sd5[e.Index].Name, comboBox8, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox8_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox8);
        }

        private void comboBox7_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(cr2[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(cr2[e.Index].StTypeName, comboBox7, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox7_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox7);
        }

        private void comboBox10_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(fr2[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(fr2[e.Index].StLevelName, comboBox10, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox10_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox10);
        }

        private void comboBox9_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd6[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip2.Show(sd6[e.Index].Name, comboBox9, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox9_DropDownClosed(object sender, EventArgs e)
        {
            toolTip2.Hide(comboBox9);
        }

        #endregion

        #endregion
        //右边课件列表查询
        private void btnCourseQuery_Click(object sender, EventArgs e)
        {
            ShowCourseList(txtResKeyword.Text, cmbTypestr1, categorystr1, levelstr1, subjects1, TypeOfWork1, SystemType1);
        }

        //右边课件列表课件单元格点击
        private void dgvCourseList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                Guid courseId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                Model.Courseware course = new Model.Courseware();
                List<CoursewareResource> crs = new List<CoursewareResource>();
                cb = new CourseBLL();
                course = cb.GetCourse(courseId);
                crs = course.coursewareResources.ToList<CoursewareResource>();
                //  把数据绑定到treeView
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc = StRttmy.Common.XmlMenuUtility.EntitiesToXmlMenuFile(crs);
                tvCourse.BeginUpdate();
                tvCourse.Nodes.Clear();
                BindXmlToTreeView(xmlDoc.DocumentElement, tvCourse.Nodes);
                tvCourse.EndUpdate();               
            }
        }

        //右边课件列表素材单元格点击
        private void tvCourse_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                CourseResourceTag tag = new CourseResourceTag();
                tag = (CourseResourceTag)e.Node.Tag;
                if (tag.MainUrl != null)
                {
                    CloseContent();
                    this.splitContainer3.Panel2.Controls.Clear();//清空文字区域
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
                            MainPath = tag.MainUrl;//课件列表素材单元格点击获取到主内容名称给创建新素材和声音控制提供参数
                            Mp3Path = tag.Mp3Url;
                            TextPath = tag.TextUrl;
                            //double NowTime = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                            //double ts = NowTime - LastTime;
                            //if (LastTime != 0.0 && ts < 3000.0)
                            //{
                            //    MainPath = BeforeMainPath;
                            //    Mp3Path = BeforeMp3Path;
                            //    TextPath = BeforeTextPath;
                            //    MessageBox.Show("请勿连续播放,请稍后再试!");
                            //    return;
                            //}
                            //if (Path.GetFileName(BeforeMainPath) == Path.GetFileName(tag.MainUrl) && Path.GetFileName(BeforeMp3Path) == Path.GetFileName(tag.Mp3Url) && Path.GetFileName(BeforeTextPath) == Path.GetFileName(tag.TextUrl))
                            //{
                            //    if (NowPlayArea == 3)
                            //    {
                            //        MessageBox.Show("正在播放当前内容,请勿重复播放！");
                            //    }
                            //    return;
                            //}
                            //else
                            //{
                            //    NowPlayArea = 3;//当播放的3个文件都不相同时才切换播放区域
                            //}
                            string kzm = MainPath.Substring(MainPath.LastIndexOf(".") + 1).ToLower();
                            //图片播放
                            if (kzm == "jpg" || kzm == "gif" || kzm == "png")
                            {
                                this.splitContainer2.Panel1.Controls.Clear();
                                pb.ImageLocation = tag.MainUrl;
                                pb.SizeMode = PictureBoxSizeMode.Zoom;
                                this.splitContainer2.Panel1.Controls.Add(pb);
                                pb.Dock = System.Windows.Forms.DockStyle.Fill;
                            }
                            //html(unity3d)、htm播放
                            if (kzm == "htm" || kzm == "html" || kzm == "unity3d")
                            {
                                // Lock_unity();// 点击U3D加载完成前禁用其它控件。
                                U3Dloading();
                                this.splitContainer2.Panel1.Controls.Clear();
                                if (kzm == "unity3d")
                                    contentWebBrowser.Navigate(TextUtility.Ecode(tag.MainUrl));
                                else
                                    contentWebBrowser.Navigate(tag.MainUrl);
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
                                this.splitContainer3.Panel2.Controls.Clear();
                                this.splitContainer3.Panel2.Controls.Add(txtWebBrowser);
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

        #region 节点控制

        //右边tvCourse树组件中按下鼠标
        private void tvCourse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            { 
                tv1 = false;
            }
        }

        //tvCourse开始拖动
        private void tvCourse_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        //在tvCourse内完成拖放
        private void tvCourse_DragDrop(object sender, DragEventArgs e)
        {
            //暂时没有业务逻辑
        }

        //将某项拖动进入tvCourse工作区
        private void tvCourse_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
        }

        //将某项拖动进入treeView1工作区
        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (!tv1)//从非treeView1拖放到treeView1
            {
                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else//从treeView1拖放到treeView1
            {
                if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        //在treeView1内完成拖放
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));//拖放目的地坐标
            Point MenuPoint = ((TreeView)(sender)).PointToClient(new Point(e.X + 20, e.Y));//拖放时弹出选择菜单的位置
            TargetNode = ((TreeView)(sender)).GetNodeAt(pt);//通过目的地坐标获取目的地节点//this.treeView1.GetNodeAt(pt)      
            TreeNode MoveNode;//拖动的节点
            //MoveNode = (TreeNode)(e.Data.GetData("System.Windows.Forms.TreeNode"));//单选拖动的节点
            if (!tv1)//从非treeView1拖放到treeView1
            {
                treeViewArea = 2;
                foreach (TreeNode tn in tvCourse.SelectedNodes)
                {
                    MoveNode = tn;//拖动的节点
                    if (TargetNode == null || MoveNode == null || TargetNode.TreeView == MoveNode.TreeView || !e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false))//检测是否是一个标准的拖放操作
                        return;
                }
            }
            else//从treeView1拖放到treeView1
            {
                treeViewArea = 1;
                foreach (TreeNode tn in treeView1.SelectedNodes)
                {
                    MoveNode = tn;//拖动的节点
                    if (TargetNode == null || MoveNode == null || TargetNode.Text == MoveNode.Text || MoveNode.Level == 0)//检测是否是一个标准的拖放操作
                        return;
                }
            }
            toolStripMenuItem1.Enabled = TargetNode.Level == 0 ? false : true;//当拖放目的地为顶级节点时"节点插入"禁用
            contextMenuStrip2.Show(this, MenuPoint);//拖放方式选择菜单
        }

        //节点插入
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditNodes(1);
        }

        //建立层级关系
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            EditNodes(2);
            treeView1.Select();
            treeView1.SelectedNode = (TreeNode)TargetNode.Clone();
            TargetNode.Expand();
        }

        //多选节点拖拉公用部分
        private void EditNodes(int type)
        {
            DeselectNodes();//删除多选节点时样式的取消操作
            if (treeViewArea == 1)
            {
                MultiSelectTreeView.IsUse = false;//删除多选节点时MultiSelectTreeView类中调用DeselectNodes()时TreeView获取不到报错,false时MultiSelectTreeView类中将不再调用DeselectNodes()方法
                for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
                {
                    if (type == 1)
                        TargetNode.Parent.Nodes.Insert(TargetNode.Index, (TreeNode)((TreeNode)treeView1.SelectedNodes[i]).Clone());//把拖动的节点添加到目的地节点的父级节点下的指定下标-1处(即目的地节点前面)              
                    else
                        TargetNode.Nodes.Insert(TargetNode.Nodes.Count, (TreeNode)((TreeNode)treeView1.SelectedNodes[i]).Clone());//把拖动的节点添加到目的地节点下的末尾处//TargetNode.Nodes.Add(NewMoveNode);
                    ((TreeNode)treeView1.SelectedNodes[i]).Nodes.Clear();
                    ((TreeNode)treeView1.SelectedNodes[i]).Remove();
                }
            }
            if (treeViewArea == 2)
            {
                for (int i = 0; i < tvCourse.SelectedNodes.Count; i++)
                {
                    if (type == 1)
                        TargetNode.Parent.Nodes.Insert(TargetNode.Index, (TreeNode)((TreeNode)tvCourse.SelectedNodes[i]).Clone());//把拖动的节点添加到目的地节点的父级节点下的指定下标-1处(即目的地节点前面)              
                    else
                        TargetNode.Nodes.Insert(TargetNode.Nodes.Count, (TreeNode)((TreeNode)tvCourse.SelectedNodes[i]).Clone());//把拖动的节点添加到目的地节点下的末尾处//TargetNode.Nodes.Add(NewMoveNode);
                }
            }
            MultiSelectTreeView.IsUse = true;//删除多选节点时MultiSelectTreeView类中调用DeselectNodes()时TreeView获取不到报错,true时MultiSelectTreeView类中将正常调用DeselectNodes()方法
            isHaveSave = true;
        }

        //删除多选节点时样式的取消操作(仅删除多选节点时使用,正常的多选选中和多选取消MultiSelectTreeView类会自动触发事件)
        private void DeselectNodes()
        {
            if (treeView1.SelectedNodes.Count != 0)
            {
                TreeNode node1 = (TreeNode)treeView1.SelectedNodes[0];
                Color backColor = node1.TreeView.BackColor;
                Color foreColor = node1.TreeView.ForeColor;
                foreach (TreeNode n in treeView1.SelectedNodes)
                {
                    n.BackColor = backColor;
                    n.ForeColor = foreColor;
                }
            }
            if (tvCourse.SelectedNodes.Count != 0)
            {
                TreeNode node1 = (TreeNode)TargetNode;
                Color backColor = node1.TreeView.BackColor;
                Color foreColor = node1.TreeView.ForeColor;
                foreach (TreeNode n in tvCourse.SelectedNodes)
                {
                    n.BackColor = backColor;
                    n.ForeColor = foreColor;
                }
            }
        }

        //treeView1开始拖动
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (!tv1)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    DoDragDrop(e.Item, DragDropEffects.Copy);
                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    DoDragDrop(e.Item, DragDropEffects.Move);
                }
            }
        }

        //treeView1树组件中按下鼠标
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.GetNodeAt(e.X, e.Y);
            if (node != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(this, new Point(e.X + 20, e.Y));//右键菜单
                    if (treeView1.SelectedNodes.Count < 2)//调和手感优化
                        treeView1.SelectedNode = node;
                    修改节点ToolStripMenuItem.Enabled = treeView1.SelectedNodes.Count > 1 ? false : true;//当多选节点时"修改节点"禁用
                    删除节点ToolStripMenuItem.Enabled = treeView1.SelectedNode.Level == 0 ? false : true;//单选选中节点是顶级节点时"删除节点"禁用
                    foreach (TreeNode tn in treeView1.SelectedNodes)
                    {
                        if (tn.Level == 0)
                            删除节点ToolStripMenuItem.Enabled = false;//检测多选时是否含有顶级节点时"修改节点"和"删除节点"都禁用
                    }
                }
                else
                {
                    //treeView1.SelectedNode = node;
                    tv1 = true;
                }
            }
        }

        //treeView1树组件中释放鼠标
        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            tv1 = false;
        }

        //treeView1控件区离开事件
        private void treeView1_Leave(object sender, EventArgs e)
        {
            //暂时没有业务逻辑
        }

        //删除
        private void DelNode(TreeView tv)
        {
            DeselectNodes();//删除多选节点时样式的取消操作
            MultiSelectTreeView.IsUse = false;//删除多选节点时MultiSelectTreeView类中调用DeselectNodes()时TreeView获取不到报错,false时MultiSelectTreeView类中将不再调用DeselectNodes()方法
            for (int i = 0; i < treeView1.SelectedNodes.Count; i++)
            {
                ((TreeNode)treeView1.SelectedNodes[i]).Nodes.Clear();
                ((TreeNode)treeView1.SelectedNodes[i]).Remove();
            }
            MultiSelectTreeView.IsUse = true;//删除多选节点时MultiSelectTreeView类中调用DeselectNodes()时TreeView获取不到报错,true时MultiSelectTreeView类中将正常调用DeselectNodes()方法
            isHaveSave = true;

            //    if (tv.SelectedNode.Nodes.Count > 0)//单选删除判断当前节点有子节点时
            //    {
            //        //MessageBox.Show("请先删除此节点中的子节点！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        DialogResult dr = MessageBox.Show("需要删除的节点下还有其他节点,是否继续?", "提示", MessageBoxButtons.OKCancel);
            //        if (dr == DialogResult.OK)
            //        {
            //            tv.SelectedNode.Nodes.Clear();
            //        }
            //    }
            //    tv.SelectedNode.Remove();
            //}
        }

        //修改
        private void EditNode(TreeView tv)
        {
            if (!tv.LabelEdit)
            {
                tv.LabelEdit = true;
            }
            tv.SelectedNode.BeginEdit();
            isHaveSave = true;
        }

        private void 修改节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNode(this.treeView1);
        }

        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelNode(this.treeView1);
        }

        #endregion

        #region treeView数据转换为CourseResource数据

        private XmlDocument TvToXmlDoc(TreeView tv)
        {
            XmlDocument doc = new XmlDocument();

            //  id,pid值

            for (int i = 0; i < tv.Nodes.Count; i++)
            {
                TreeNode child = tv.Nodes[i];
                XmlElement newEle = doc.CreateElement("menu");                

                if (child.Text != "")
                {
                    string str1 = Application.StartupPath + @"\";
                    newEle.SetAttribute("Label", child.Text);                    

                    if (child.Tag != null)
                    {
                        CourseResourceTag tag = new CourseResourceTag();
                        tag = (CourseResourceTag)child.Tag;

                        string id = Guid.NewGuid().ToString();
                        string pId = Guid.NewGuid().ToString();
                        string CoursewareResourcesId = Guid.NewGuid().ToString();
                        ((CourseResourceTag)child.Tag).id = id;
                        newEle.SetAttribute("id", id);
                        newEle.SetAttribute("pId", pId);
                        newEle.SetAttribute("CoursewareResourcesId", CoursewareResourcesId);
                        if (!string.IsNullOrEmpty(tag.MainUrl))
                        {
                            string mainUrlStr = tag.MainUrl.Substring(str1.Length, tag.MainUrl.Length - str1.Length);
                            newEle.SetAttribute("MainUrl", mainUrlStr);
                        }
                        else
                        {
                            newEle.SetAttribute("MainUrl", ""); 
                        }
                        if (!string.IsNullOrEmpty(tag.TextUrl))
                        {
                            string textUrlStr = tag.TextUrl.Substring(str1.Length, tag.TextUrl.Length - str1.Length);
                            newEle.SetAttribute("TextUrl", textUrlStr);
                        }
                        else
                        {
                            newEle.SetAttribute("TextUrl", "");
                        }
                        if (!string.IsNullOrEmpty(tag.Mp3Url))
                        {
                            string mp3UrlStr = tag.Mp3Url.Substring(str1.Length, tag.Mp3Url.Length - str1.Length);
                            newEle.SetAttribute("Mp3Url", mp3UrlStr);
                        }
                        else
                        {
                            newEle.SetAttribute("Mp3Url", "");
                        }
                    }                    
                }

                doc.AppendChild(newEle);
                if (child.Nodes.Count > 0)
                {
                    GetTreeNodeInfo(child, newEle);
                }
            }
            return doc;
        }

        private void GetTreeNodeInfo(TreeNode node, XmlElement ele)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                TreeNode child = node.Nodes[i];
                XmlElement newEle = ele.OwnerDocument.CreateElement("menu");

                if (child.Text != "")
                {
                    string str1 = Application.StartupPath + @"\";
                    newEle.SetAttribute("Label", child.Text);

                    if (child.Tag != null)
                    {
                        CourseResourceTag tag = new CourseResourceTag();
                        tag = (CourseResourceTag)child.Tag;

                        string id = Guid.NewGuid().ToString();
                        ((CourseResourceTag)child.Tag).id = id;
                        newEle.SetAttribute("id", id);

                        //  获取父层id作为pid
                        //TreeNode parent = node.Nodes[i].Parent;
                        TreeNode parent = child.Parent;
                        CourseResourceTag parentTag = new CourseResourceTag();
                        parentTag = (CourseResourceTag)parent.Tag;
                        string pid = parentTag.id;
                        newEle.SetAttribute("pId", pid);

                        if (!string.IsNullOrEmpty(tag.MainUrl))
                        {
                            string mainUrlStr = tag.MainUrl.Substring(str1.Length, tag.MainUrl.Length - str1.Length);
                            newEle.SetAttribute("MainUrl", mainUrlStr);
                        }
                        else
                        {
                            newEle.SetAttribute("MainUrl", "");
                        }
                        if (!string.IsNullOrEmpty(tag.TextUrl))
                        {
                            string textUrlStr = tag.TextUrl.Substring(str1.Length, tag.TextUrl.Length - str1.Length);
                            newEle.SetAttribute("TextUrl", textUrlStr);
                        }
                        else
                        {
                            newEle.SetAttribute("TextUrl", "");
                        }
                        if (!string.IsNullOrEmpty(tag.Mp3Url))
                        {
                            string mp3UrlStr = tag.Mp3Url.Substring(str1.Length, tag.Mp3Url.Length - str1.Length);
                            newEle.SetAttribute("Mp3Url", mp3UrlStr);
                        }
                        else
                        {
                            newEle.SetAttribute("Mp3Url", "");
                        }
                    }                    
                }

                ele.AppendChild(newEle);
                if (child.Nodes.Count > 0)
                {
                    GetTreeNodeInfo(child, newEle);
                }
            }
        }      

        #endregion

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            checkSave();
        }

        //保存方法
        private void checkSave() {
            List<CoursewareResource> crs = XmlMenuUtility.XmlDocToEntityList(TvToXmlDoc(treeView1));
            foreach(CoursewareResource a in crs)
            {
                a.CoursewareId = coursewareId;
                //a.CoursewareResourcesId = Guid.NewGuid();
            }
            if (coursewareId != Guid.Empty)
            {
                cb = new CourseBLL();
                if (cb.SaveCourseResources(crs, coursewareId))
                {
                    DialogResult dr = MessageBox.Show("修改课件成功。是否生成试卷？", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        AddPaperForm apf = new AddPaperForm();
                        apf.PARM = 1;
                        apf.courseId = coursewareId;
                        apf.ShowDialog();
                    }
                    //MessageBox.Show("修改课件成功。");
                    isHaveSave = false;
                    //Close();
                }
                else
                {
                    MessageBox.Show("修改课件失败。");
                }
            }
            else
            {
                MessageBox.Show("此课件不存在。");
                Close();
            }
        }
        #endregion    

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
      
       //播放时间获取
        private void timer1_Tick_1(object sender, EventArgs e)
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
                label1.Text = dt.ToString("mmm:ss")+"/"+ dt1.ToString("mmm:ss");
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
        private void splitContainer3_Panel1_MouseEnter(object sender, EventArgs e)
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

        //鼠标离开播放区
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
        private void button6_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            tokens = 2;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            tokens = 1;
        }
 #endregion

     
     
       


    }
}

