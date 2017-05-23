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
using StPublicUtility;
using StRttmy.Common;
using System.Xml;
//using StRttmy.UI.UpdateMaterial;
using System.Security.Cryptography;
using StRttmy.UI.Help;
using StRttmy.UI.Courseware;
using StRttmy.UI.Diagram;

namespace StRttmy.UI
{
    public partial class MainForm : Form
    {
        public User LoginUser = null;
        public Student sLogin = null;
        int i = Screen.PrimaryScreen.Bounds.Width;//显示器当前使用的分辨率宽
        int j = Screen.PrimaryScreen.Bounds.Height;//显示器当前使用的分辨率高
        //int wid= Screen.PrimaryScreen.WorkingArea.Width;//显示器实际分辨率宽
        //int hei = Screen.PrimaryScreen.WorkingArea.Height;//显示器实际分辨率高
        private static UMHCONTROLLib.IRCUMHDog dog = new UMHCONTROLLib.RCUMHDogClass();
        public string EncryptDecryptKey = "!$study%";//密匙
        private byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };
        public MainForm()
        {

            InitializeComponent();
        }

        //窗体加载
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text += "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();//程序版本号,如有修改时需要手动,项目-项目属性-程序集信息-程序集版本号
            TextUtility.CheckDelFile();
            // pictureBox1.Hide();
            toolStrip1.Visible = false;
            toolStripButton2.Visible = false;
            button1.Enabled = true;
            toolStrip1.BackColor = Color.FromArgb(194, 221, 233);
            toolStrip2.BackColor = Color.FromArgb(194, 221, 233);
            axWindowsMediaPlayer1.URL = "";// Application.StartupPath + @"\Files\Intr.MP4";            
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.FromArgb(0, 90, 143);
            label5.ForeColor = Color.White;
            toolStripLabel3.ForeColor = Color.FromArgb(36, 170, 217);
            toolStripLabel2.ForeColor = Color.FromArgb(36, 170, 217);
            //label1.Text = i + "*" + j + "|" + wid + "*" + hei + "|";
            //TextUtility.ChangeRes(1280,720);//软件全屏,修改用户的分辨率

            //if (ChcekCode())
            //{
            //    CheckCodeForm();
            //}
            //else 
            //{
            //    button1.Enabled = false;
            //    toolStripButton1.Visible = false;
            //    toolStripButton2.Visible = true;
            //}
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

            //LoginForm lf = new LoginForm(setDataGridView);
            //lf.ShowDialog();          
            toolStrip1.BringToFront();

        }

        #region 验证狗的字符串与日志文件是否匹配
        //private bool Detection() 
        //{ 
        //    if (ReadDate() == ReaddogString())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 获取狗的字符串
        ///// </summary>
        ///// <returns></returns>
        //private string ReaddogString()
        //{
        //    int retCode;
        //    string tmpstr = "0";

        //    dog.m_addr = 65;
        //    dog.m_bytes = 14;
        //    dog.m_command = 2;
        //    retCode = dog.OperateDog();

        //    if (retCode == 0)
        //    {
        //        tmpstr = dog.Memdata;
        //        return tmpstr;
        //    }
        //    return tmpstr;
        //}
        ///// <summary>
        ///// 读取日志
        ///// </summary>
        ///// <param name="XmlPath">读取日志地址</param>
        ///// <returns>返回日志集合</returns>
        //private string ReadDate()
        //{
        //    try
        //    {
        //        string time = System.Windows.Forms.Application.StartupPath + "\\Log.xml";
        //        XmlDocument xmlDocument = new XmlDocument();
        //        xmlDocument.Load(time);
        //        XmlNode topM1 = xmlDocument.FirstChild.NextSibling;
        //        double c = 0.0001;
        //        double d = 0.0001;
        //        foreach(XmlNode a in topM1)
        //        {                    
        //            string b;
        //            b = DecryptStr(a.InnerText, EncryptDecryptKey).Substring(0, 1);
        //            if( b =="D")
        //            {
        //                c = c + 0.0001;

        //            }
        //            if (b == "T")
        //            {
        //                d = d + 0.0001;
        //            }                   
        //        }
        //        return "D" + c.ToString() + "T" + d.ToString();
        //    }
        //    catch
        //    {
        //        MessageBox.Show("缺少必要的运行文件，请与管理员！");
        //        return null;
        //    }
        //}
        //public void CloseDetection() 
        //{
        //    if (!Detection())
        //    {
        //        this.Close();
        //    }
        //    else
        //    {
        //        tsddbCourseManage.Visible = true;
        //        tsddbResourceManage.Visible = true;
        //        tsddbUserManage.Visible = true;
        //        用户管理ToolStripMenuItem.Visible = true;
        //    }
        //}
        ///// <summary>
        ///// 解密字符串
        ///// </summary>
        ///// <param name="decryptString"></param>
        ///// <param name="decryptKey"></param>
        ///// <returns></returns>

        //public string DecryptStr(string decryptString, string decryptKey)
        //{
        //    try
        //    {
        //        byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
        //        byte[] rgbIV = Keys;
        //        byte[] inputByteArray = Convert.FromBase64String(decryptString);
        //        DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
        //        MemoryStream mStream = new MemoryStream();
        //        CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        //        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        //        cStream.FlushFinalBlock();
        //        return Encoding.UTF8.GetString(mStream.ToArray());
        //    }
        //    catch
        //    {
        //        return decryptString;
        //    }
        //}
        #endregion

        #region 识别码/注册码

        //检测产品注册情况
        private bool ChcekCode()
        {
            //MachineInfo f = new MachineInfo();
            //string PathXml = Application.StartupPath + @"\SerialNumber.xml";
            //bool IsOk = false;
            //if (!System.IO.File.Exists(PathXml))//没有找到SerialNumber
            //{
            //    IsOk = false;
            //}
            //else
            //{
            //    try
            //    {
            //        XmlDocument Doc = new XmlDocument();
            //        Doc.Load(PathXml);
            //        XmlNode node = Doc.SelectSingleNode("/User/Code");
            //        if (node == null)//没有找到Code根节点
            //        {
            //            System.IO.File.Delete(PathXml);
            //            IsOk = false;
            //        }
            //        else
            //        {
            //            if (node.InnerText == "" || node.InnerText == null)//Code为空
            //            {
            //                System.IO.File.Delete(PathXml);
            //                IsOk = false;
            //            }
            //            else
            //            {
            //                string CodeStr = CheckDecode(node.InnerText, 2);
            //                if (CodeStr != null && f.GetMacByNetworkInterface() == CodeStr)//验证通过放行
            //                {
            //                    IsOk = true;
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)//SerialNumber内容不完整
            //    {
            //        if (ex.Message != null)
            //        {
            //            System.IO.File.Delete(PathXml);
            //            IsOk = false;
            //            return IsOk;
            //        }
            //    }
            //}
            return true;
        }

        //检测识别/注册码解密(CodeType表示识别码解密时1/注册码解密时2)
        private string CheckDecode(string comAfterStr, int CodeType)
        {
            string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (comAfterStr.Length == 44 || comAfterStr.Length == 50)
            {
                comAfterStr = comAfterStr.Length == 50 ? comAfterStr.Replace("-", "") : comAfterStr;//有"-"的话要去掉
                int baInt = Fixeds.IndexOf(comAfterStr.Substring(0, 1));
                int aInt = Fixeds.IndexOf(comAfterStr.Substring(1, 1));
                int bInt = baInt - aInt;
                string cStr = comAfterStr.Substring(2);//取第3个到最后的字符串
                string DecodeStr = "";
                for (int i = 0; i < cStr.Length + 1; i++)
                {
                    if (i % 7 == 0 && i != 0)
                        DecodeStr += RulesDecode(cStr.Substring(i < 7 ? 0 : i - 7, 7), aInt, bInt) + ",";//解密并按每7个字符串加","来分割开
                }
                var DecodeArr = DecodeStr.Split(',');
                string resolveStr = DecodeArr[0] + DecodeArr[2] + DecodeArr[5] + DecodeArr[4].Substring(0, 3);//24位Mac
                double allTime = Convert.ToDouble(DecodeArr[1] + DecodeArr[3].Substring(0, 6));//时间,解密时暂时用不到
                string OldCodeType = DecodeArr[3].Substring(6);//表示识别码解密时1/注册码解密时2
                if (CodeType == 2 && OldCodeType == "1")//解析注册码的地方放入了识别码
                    return null;
                if (CodeType == 1 && OldCodeType == "2")//解析识别码的地方放入了注册码
                    return null;
                string MacStr = "";
                for (int i = 0; i < resolveStr.Length + 1; i++)//把24位Mac还原成12位
                {
                    if (i % 2 == 0 && i != 0)
                    {
                        string aStr = resolveStr.Substring(i < 2 ? 0 : i - 2, 2);//循环中按顺序每次取一个两位数的字符串
                        if (aStr == "00")
                            MacStr += aStr.Substring(0, 1);//当字符串是"00"时取"0"
                        if (aStr.Substring(0, 1) != "0" && aStr.Substring(1) == "0")
                            MacStr += aStr.Substring(0, 1);//当字符串的第一个字符串不是"0"但第二个字符串是"0"时取第一个字符串
                        if (aStr.Substring(1) != "0")//当字符串的第二个字符串不是"0"时,按规则取出对应的字母
                        {
                            var FixedsArr = Fixeds.ToArray();
                            if (isNumberic(aStr))
                            {
                                int aIndex = System.Int32.Parse(aStr) - 1;
                                MacStr += FixedsArr[aIndex > 9 && aIndex < 20 ? aIndex - 1 : aIndex >= 20 ? aIndex - 2 : aIndex].ToString().ToLower();//按规则还原成字母
                            }
                            else
                                return null;
                        }
                    }
                }
                return MacStr;
            }
            else
                return null;
        }

        //识别/注册码解密规则
        private string RulesDecode(string Str, int NumB, int NumT)
        {
            string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var StrArray1 = Str.Substring(0, 3).ToCharArray();
            var StrArray2 = Str.Substring(3).ToCharArray();
            string allStr = "";
            allStr += StrArray2[0].ToString() + StrArray1[0].ToString();
            allStr += StrArray2[1].ToString() + StrArray1[1].ToString();
            allStr += StrArray2[2].ToString() + StrArray1[2].ToString();
            allStr += StrArray2[3].ToString();
            var allStrArray = allStr.ToCharArray();
            string decodeStr = "";
            for (int i = 0; i < allStrArray.Length; i++)
            {
                int aInt = -1;
                if (isNumberic(allStrArray[i].ToString()))
                    aInt = System.Int32.Parse(allStrArray[i].ToString());
                else
                {
                    var FixedArray = NumT > 0 && NumT < 4 ? Fixeds.Substring(0, 10).ToCharArray() : NumT > 3 && NumT < 7 ? Fixeds.Substring(8, 10).ToCharArray() : Fixeds.Substring(16).ToCharArray();
                    string FixedArrayStr = "";
                    foreach (char a in FixedArray)
                        FixedArrayStr += a.ToString();
                    aInt = FixedArrayStr.IndexOf(allStrArray[i].ToString().ToUpper());
                }
                int X = (aInt - NumB) < 0 ? (10 + aInt) - NumB : aInt - NumB;
                decodeStr += X.ToString();
            }
            return decodeStr;
        }

        //验证是否是数字
        private bool isNumberic(string intStr)
        {
            int result = -1;
            try
            {
                result = Convert.ToInt32(intStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        //启动登录控件
        private void button1_Click(object sender, EventArgs e)
        {
            if (LoginUser == null)
            {
                toolStripButton1_Click(sender, e);
            }
        }

        //注册产品
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RegistForm rf = new RegistForm(CheckCodeForm);
            rf.ShowDialog();
        }

        //注册产品成功后的委托方法
        public void CheckCodeForm()
        {
            button1.Enabled = true;
            this.AcceptButton = button1;//窗体中按下回车启动button1按钮
            toolStripButton1.Visible = true;
            toolStripButton2.Visible = false;
        }

        //登录
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //LoginForm lf = new LoginForm(setDataGridView);
            //lf.ShowDialog();
            panel1.BringToFront();
            //LoginUser = new User();
            //LoginUser.UserId = 4;
            //LoginUser.LoginName = "t1";
            //LoginUser.Password = "1";
            //LoginUser.UserName = "王老师";
            //LoginUser.WorkingUnit = "昆明学易";
            //LoginUser.CreateTime = DateTime.Now;
            //LoginUser.UserType = UserType.教师;
            //Program.mf.Text = LoginUser.UserName + "，欢迎使用---轨道交通教学课件管理系统";

            if (LoginUser != null)
            {
                SetShowMenuItem();//登录成功后更新权限菜单
                axWindowsMediaPlayer1.close();
                axWindowsMediaPlayer1.Hide();
                pictureBox1.ImageLocation = Application.StartupPath + @"\Files\HomeP.jpg";
                pictureBox1.Show();
                //if (!Detection())
                //{
                //if (LoginUser.UserType == UserType.管理人员 || LoginUser.UserType == UserType.系统管理员)
                //{
                //    tsddbCourseManage.Visible = false;
                //    tsddbResourceManage.Visible = false;
                //    tsddbUserManage.Visible = false;
                //    用户管理ToolStripMenuItem.Visible = false;
                //   // UpdateMaterialPackage up = new UpdateMaterialPackage();
                //    //up.ControlBox = false;
                //    //up.ShowDialog();
                
                //}
                //else
                //{
                //    MessageBox.Show("缺少对应的更新包，或者非对应的加密狗");
                //    this.Close();
                //}
                //}
            }
        }
        #region  登录
        //登录确认
        private void btnLogin_Click(object sender, EventArgs e)
        {           
            Login();   
        }
        private UserBLL ub = null;
        private void Login()
        {

            if (string.IsNullOrEmpty(txtLoginName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("登录名或密码不能为空值。");
                txtLoginName.Focus();
                return;
            }
            UserLogin login = new UserLogin();
            login.LoginName = txtLoginName.Text;
            login.Password = txtPassword.Text;

            ub = new UserBLL();
            User curUser = new User();
            Student curstudent = new Student();
            if (ub.Login(login, out curUser))
            {
                LoginUser = curUser;//委托返回给主窗体需要的curUser
                //Program.mf.Text = curUser.UserName + "，欢迎使用---轨道交通多媒体课件资源编制管理系统";
                SetShowMenuItem();//初始化权限菜单
                pictureBox1.ImageLocation = Application.StartupPath + @"\Files\denglujiemian.jpg";
                panel1.SendToBack();
                toolStrip2.SendToBack(); 
                toolStripLabel2.Text = curUser.UserName;
                //this.Close();
            }
            else
            {

                if (ub.sLogin(login, out curstudent))
                {
                    User curUser1 = new User();
                    curUser1.UserId = curstudent.StudentId;
                    curUser1.UserName = curstudent.Name;
                    curUser1.LoginName = curstudent.LoginName;
                    curUser1.Password = curstudent.Password;
                    curUser1.UserType = UserType.学员;
                    LoginUser = curUser1;//委托返回给主窗体需要的curUser

                   // Program.mf.Text = curUser1.UserName + "，欢迎使用---轨道交通多媒体课件资源编制管理系统";
                    SetShowMenuItem();//初始化权限菜单
                    pictureBox1.ImageLocation = Application.StartupPath + @"\Files\denglujiemian.jpg";
                    panel1.SendToBack();
                    toolStrip2.SendToBack();                 
                    toolStripLabel2.Text = curUser1.UserName;
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("登录失败！");
                    txtLoginName.Focus();//登录名输入框获得焦点                   
                }

            }
        }

        #endregion

        /// <summary>
        /// 加密文件改写为加密状态
        /// </summary>
        private void ChangeSwfSec()
        {
            //string str;
            //str = StSec.EncryptStr("Studyeasy.cn", StSec.EncryptDecryptKey);
            //StSec.WriteXml(str);
        }

        //关闭主窗体
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ChangeSwfSec();
            //TextUtility.FuYuan(i,j);
            TextUtility.CheckDelFile();
            Application.Exit();
        }
        //禁止双击全屏
        private void axWindowsMediaPlayer1_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            if (axWindowsMediaPlayer1.fullScreen == true)
            {
                axWindowsMediaPlayer1.fullScreen = false;
            }
        }

        //注销
        private void tsbUnRegister_Click(object sender, EventArgs e)
        {
            UnRegister();
            pictureBox1.ImageLocation = Application.StartupPath + @"\Files\jiemian.jpg";
        }

        //注销方法
        public void UnRegister()
        {
            LoginUser = null;
            Program.mf.Text = "轨道交通多媒体课件资源编制管理系统";
            toolStrip2.BringToFront();
            SetShowMenuItem();//注销后更新权限菜单
            txtLoginName.Text = "";
            txtPassword.Text = "";
        }

        //委托方法
        public void setDataGridView(User u)
        {
            if (u != null)
            {
                LoginUser = u;
            }
        }

        //权限控制
        public void SetShowMenuItem()
        {
            toolStrip1.Show();
            toolStrip2.Show();
            tsddbCourseManage.Visible = false;
            tsddbResourceManage.Visible = false;
            tsddbUserManage.Visible = false;
            toolStripDropDownButton1.Visible = false;
            if (LoginUser != null)
            {
                //button1.Hide();
                toolStrip2.Hide();
                if (LoginUser.UserType == UserType.学员)
                {
                    tsddbCourseManage.Visible = false;
                    tsddbResourceManage.Visible = false;
                    tsddbUserManage.Visible = false;
                    toolStripDropDownButton2.Visible = false;
                    toolStripDropDownButton1.Visible = true;
                    自定义素材管理ToolStripMenuItem.Visible = false;
                    自定义课件管理ToolStripMenuItem.Visible = false;
                    发布课件管理ToolStripMenuItem.Visible = false;
                    用户管理ToolStripMenuItem.Visible = false;
                    题库管理ToolStripMenuItem.Visible = false;
                    试卷管理ToolStripMenuItem.Visible = false;
                    考试ToolStripMenuItem.Visible = true;
                    批卷ToolStripMenuItem.Visible = false;
                    toolStripButton3.Visible = false;
                    toolStripDropDownButton3.Visible = false;
                    toolStripButton4.Visible = true;
                }
                if (LoginUser.UserType == UserType.教师)
                {
                    tsddbCourseManage.Visible = true;
                    tsddbResourceManage.Visible = true;
                    tsddbUserManage.Visible = true;
                    toolStripDropDownButton2.Visible = false;
                    toolStripDropDownButton1.Visible = true;
                    自定义素材管理ToolStripMenuItem.Visible = true;
                    自定义课件管理ToolStripMenuItem.Visible = true;
                    发布课件管理ToolStripMenuItem.Visible = true;
                    用户管理ToolStripMenuItem.Visible = false;
                    题库管理ToolStripMenuItem.Visible = true;
                    试卷管理ToolStripMenuItem.Visible = true;
                    考试ToolStripMenuItem.Visible = true;
                    批卷ToolStripMenuItem.Visible = true;
                    toolStripDropDownButton3.Visible = true;
                    toolStripButton3.Visible = true;
                    toolStripButton4.Visible = true;
                }
                if (LoginUser.UserType == UserType.管理人员 || LoginUser.UserType == UserType.系统管理员)
                {
                    tsddbCourseManage.Visible = true;
                    tsddbResourceManage.Visible = true;
                    tsddbUserManage.Visible = true;
                    toolStripDropDownButton2.Visible = true;
                    //toolStripDropDownButton1.Visible = true;
                    自定义素材管理ToolStripMenuItem.Visible = false;
                    自定义课件管理ToolStripMenuItem.Visible = false;
                    发布课件管理ToolStripMenuItem.Visible = false;
                    用户管理ToolStripMenuItem.Visible = true;
                    //试卷管理ToolStripMenuItem.Visible = false;
                    //试题管理ToolStripMenuItem.Visible = false;
                    //开始考试ToolStripMenuItem.Visible = false;
                    //查看成绩ToolStripMenuItem.Visible = false;
                    //学员成绩管理ToolStripMenuItem.Visible = true;                
                }
                if (LoginUser.UserType == UserType.超级管理员)
                {
                    tsddbCourseManage.Visible = true;
                    tsddbResourceManage.Visible = true;
                    tsddbUserManage.Visible = true;
                    //toolStripDropDownButton1.Visible = true;
                    toolStripDropDownButton2.Visible = false;
                    自定义素材管理ToolStripMenuItem.Visible = false;
                    自定义课件管理ToolStripMenuItem.Visible = false;
                    发布课件管理ToolStripMenuItem.Visible = false;
                    用户管理ToolStripMenuItem.Visible = true;
                    //试卷管理ToolStripMenuItem.Visible = false;
                    //试题管理ToolStripMenuItem.Visible = false;
                    //开始考试ToolStripMenuItem.Visible = true;
                    //查看成绩ToolStripMenuItem.Visible = true;
                    //学员成绩管理ToolStripMenuItem.Visible = false;
                }
               
            }
            else
            {
                toolStrip1.Hide();
                //button1.Show();
            }
        }

        private void 个人资料管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePwdForm cpf = new ChangePwdForm();
            cpf.ShowDialog();
        }

        private void 个人资料管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeUserInfoForm cuif = new ChangeUserInfoForm();
            cuif.ShowDialog();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserListForm ulf = new UserListForm();
            ulf.ShowDialog();
        }
        private void 学员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentFrom sf = new StudentFrom();
            sf.ShowDialog();
        }

        private void 班级管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StClassFrom sf = new StClassFrom();
            sf.ShowDialog();
        }
        private void 自定义素材管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CusResourceListForm crlf = new CusResourceListForm();
            crlf.ShowDialog();
        }

        private void 素材浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResourceListForm rlf = new ResourceListForm();
            rlf.ShowDialog();
        }

        private void 自定义课件管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CusCourseListForm cl = new CusCourseListForm();
            cl.ShowDialog();
        }

        private void 课件浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CourseListForm lf = new CourseListForm();
            lf.ShowDialog();
        }

        private void 发布课件管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PubCourseListForm lf = new PubCourseListForm();
            lf.ShowDialog();
        }

        private void 用户使用操作手册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OperationManual au = new OperationManual();
            au.ShowDialog();
        }

        private void 技术支持ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TechnicalSupport au = new TechnicalSupport();
            au.ShowDialog();
        }

        private void 关于我们ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs au = new AboutUs();
            au.ShowDialog();
        }

        private void 题库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TestQuestionsListForm tq = new TestQuestionsListForm();
            NewTestQuestionListForm tq = new NewTestQuestionListForm();
            tq.ShowDialog();
        }

        private void 试卷管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestPapersListForm tplf = new TestPapersListForm();
            tplf.ShowDialog();
        }

        private void 考试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoginUser.UserType == UserType.教师)
            {
                ChosePaperClassForm cpcf = new ChosePaperClassForm();
                cpcf.ShowDialog();
            }
            else if (LoginUser.UserType == UserType.学员)
            {
                StudentPaperForm stf = new StudentPaperForm();
                //stf.MdiParent = this.MdiParent;
                stf.ShowDialog();
            }
        }

        private void 批卷ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadPapersForm rpf = new ReadPapersForm();
            rpf.MdiParent = this.MdiParent;
            rpf.Show();
            //this.Close();
        }

        private void 考卷查阅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExaminationPaperForm epf = new ExaminationPaperForm();
            epf.MdiParent = this.MdiParent;
            epf.Show();
        }

        private void 更新二维码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpMaterial um = new UpMaterial();
            //um.Create_str();
            //um.ShowDialog();
        }

        private void 更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateMaterialPackage up = new UpdateMaterialPackage();
            //up.ShowDialog();
        }
        //类型管理
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddStType ast = new AddStType();
            ast.ShowDialog();

        }

        private void 成品课件发布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PublishCourseware plc = new PublishCourseware();
            plc.ShowDialog();
        }

        private void 教学统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagramShow Ds = new DiagramShow();
            Ds.ShowDialog();
        }

        private void 教学分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiagramAnalysis da = new DiagramAnalysis();
            da.ShowDialog();
        }

        private void 教学报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultsReport rr = new ResultsReport();
            rr.ShowDialog();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            txtLoginName.Focus();//登录名输入框获得焦点
            this.AcceptButton = btnLogin;//窗体中按下回车启动btnLogin按钮
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //DemoVideo dv = new DemoVideo();
            //dv.Show();
        }
    }
}
