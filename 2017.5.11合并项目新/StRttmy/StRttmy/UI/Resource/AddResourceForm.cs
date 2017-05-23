using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Xml;
using System.Security.Cryptography;
using StRttmy.Repository;

namespace StRttmy.UI
{
    public partial class AddResourceForm : Form
    {
        private ResourceBLL rb = null;
        public CusResourceListForm crlf = null;
        public delegate void FreshData(string keyword, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype);
        public string ContentStr = "";
        public string SoundStr = "";
        public string TextStr = "";
        public string EncryptDecryptKey = "!$study%";//密匙
        private byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };
        private ResourceRepository dc;
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        public AddResourceForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void AddResourceForm_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(52, 152, 186);
            dc = new ResourceRepository();
            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();

            sd = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
            StType ins = new StType();
            ins.StTypeId = Guid.Empty;
            ins.Name = "全部";
            sd.Insert(0, ins);
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "StTypeId";
            cmbType.DataSource = sd;

            cr = sts.StTypeSupplyList().ToList();
            StTypeSupply insts = new StTypeSupply();
            insts.StTypeSupplyId = Guid.Empty;
            insts.StTypeName = "全部";
            cr.Insert(0, insts);
            comboBox2.DisplayMember = "StTypeName";
            comboBox2.ValueMember = "StTypeSupplyId";
            comboBox2.DataSource = cr;

            fr = sl.StLevelList().ToList();
            StLevel insl = new StLevel();
            insl.StLevelId = Guid.Empty;
            insl.StLevelName = "全部";
            fr.Insert(0, insl);
            comboBox3.DisplayMember = "StLevelName";
            comboBox3.ValueMember = "StLevelId";
            comboBox3.DataSource = fr;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
        }

        //窗体激活
        private void AddResourceForm_Activated(object sender, EventArgs e)
        {
            txtTitle.Focus();
            this.AcceptButton = btnAdd;//窗体中按下回车启动btnAdd按钮
        }

        //新建
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddResource();
        }

        //提交方法
        private void AddResource()
        {
            if (Program.mf.LoginUser != null)
            {
                string nameStr = Path.GetFileName(txtContentFile.Text);//取源文件的文件名和扩展名
                var suffixStr = nameStr.Split('.');//把源文件的文件名和扩展名分开 
                if (subjects == Guid.Empty)
                {
                    MessageBox.Show("课件科目不能为空");
                    cmbType.BackColor = Color.Red;
                    comboBox1.BackColor = Color.Red;
                    comboBox4.BackColor = Color.Red;
                    return;
                }
                if (categorystr == Guid.Empty)
                {
                    MessageBox.Show("课件类别不能为空");
                    comboBox2.BackColor = Color.Red;
                    return;
                }
                if (levelstr == Guid.Empty)
                {
                    MessageBox.Show("课件等级不能为空");
                    comboBox3.BackColor = Color.Red;
                    return;
                }
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    MessageBox.Show("素材标题不能为空值。");
                    txtTitle.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtKeyword.Text))
                {
                    MessageBox.Show("素材关键字不能为空值。");
                    txtKeyword.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtContentFile.Text))
                {
                    MessageBox.Show("媒体文件不能为空值。");
                    txtContentFile.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSoundFile.Text) && (!checkBox1.Checked || suffixStr[1].ToLower() != "mp4"))
                {
                    MessageBox.Show("声音文件不能为空值。");
                    txtSoundFile.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTextFile.Text) && !checkBox2.Checked)
                {
                    MessageBox.Show("文字文件不能为空值。");
                    txtTextFile.Focus();
                    return;
                }
                rb = new ResourceBLL();
                Resource newResource = new Resource();
                //newResource.Type = (ResourceType)cmbResourceType.SelectedValue;
                newResource.ResourceId = Guid.NewGuid();
                newResource.StLevelId = levelstr;
                newResource.StTypeSupplyId = categorystr;
                newResource.StTypeId = subjects;
                newResource.Title = txtTitle.Text;
                newResource.Keyword = txtKeyword.Text;
                newResource.ContentFile = Path.GetFileName(txtContentFile.Text);
                newResource.SoundFile = suffixStr[1].ToLower() == "mp4" || checkBox1.Checked ? "specificempty001.mp3" : Path.GetFileName(txtSoundFile.Text);
                newResource.TextFile = checkBox2.Checked ? "specificempty001.html" : Path.GetFileName(txtTextFile.Text);
                newResource.UserId = Program.mf.LoginUser.UserId;  
                //处理ppt转成的htm配套文件的解析拷贝
                //if (suffixStr[1].ToLower() == "htm")
                //{
                //    string srcdir = URL1[0] + ".files";//后缀为files的文件夹的源文件完整路径
                //    if (Directory.Exists(srcdir))//判断同文件夹下同名的files后缀文件夹是否存在
                //    {
                //        string[] filenames = Directory.GetFileSystemEntries(srcdir);
                //        if (filenames.Length >= 1 && !Directory.Exists(Application.StartupPath + @"\Resources\ContentFiles\" + suffixStr[0] + ".files"))
                //        {
                //            Directory.CreateDirectory(Application.StartupPath + @"\Resources\ContentFiles\" + suffixStr[0] + ".files");
                //        }
                //        // 遍历所有的文件和目录
                //        foreach (string file in filenames)
                //        {
                //            string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                //            if (fileName == "outline.htm")
                //            {
                //                string con = "";
                //                StreamReader sr = new StreamReader(file, Encoding.Default);
                //                con = sr.ReadToEnd();
                //                //拷贝文件时就隐藏掉"全屏幻灯片放映"功能
                //                con = con.Replace("<td nowrap>\r\n"
                //                    + " <div id=\"nb_sldshwText\" title=\"全屏幻灯片放映\" align=center style='position:relative;margin-left:20px;padding:3px;"
                //                    , "<td nowrap style='display:none;'>\r\n"
                //                    + " <div id=\"nb_sldshwText\" title=\"全屏幻灯片放映\" align=center style='position:relative;margin-left:20px;padding:3px;");
                //                sr.Close();
                //                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\Resources\ContentFiles\" + suffixStr[0] + ".files\\" + fileName, true, Encoding.Default);
                //                sw.WriteLine(con);
                //                sw.Close();
                //            }
                //            else
                //            {
                //                File.Copy(file, Application.StartupPath + @"\Resources\ContentFiles\" + suffixStr[0] + ".files\\" + fileName, true);
                //            }
                //        }
                //    }
                //}
                var URL1 = txtContentFile.Text.Substring(0, txtContentFile.Text.LastIndexOf("."));//完整的源文件路径去掉扩展名
                string pathUrl = Application.StartupPath + @"\Resources\";
                if (suffixStr[1].ToLower() == "html")
                {
                    //判断同文件下同名的unity3d后缀文件是否存在
                    if (File.Exists(URL1 + ".unity3d"))
                    {
                        File.Copy(URL1 + ".unity3d", pathUrl + "ContentFiles" + suffixStr[0] + ".unity3d", true);
                    }
                }
                if (suffixStr[1].ToLower() == "swf")
                {
                    //判断同文件下同名的htm后缀文件是否存在
                    if (File.Exists(URL1 + ".htm"))
                    {
                        File.Copy(URL1 + ".htm", pathUrl + "ContentFiles" + suffixStr[0] + ".htm", true);
                    }
                    //判断同文件下同名的xml后缀文件是否存在
                    if (File.Exists(URL1 + ".xml"))
                    {
                        File.Copy(URL1 + ".xml", pathUrl + "ContentFiles" + suffixStr[0] + ".xml", true);
                    }
                }
                string contentFilePath = pathUrl + "ContentFiles\\" + Path.GetFileName(txtContentFile.Text);
                if (!File.Exists(contentFilePath))
                    File.Copy(ContentStr, contentFilePath, true);
                SoundStr = suffixStr[1].ToLower() == "mp4" || checkBox1.Checked ? pathUrl + "SoundFiles\\specificempty001.mp3" : SoundStr;
                string soundFilePath = pathUrl + "SoundFiles\\" + Path.GetFileName(SoundStr);
                if (!File.Exists(soundFilePath))
                    File.Copy(SoundStr, soundFilePath, true);
                TextStr = checkBox2.Checked ? pathUrl + "TextFiles\\specificempty001.html" : TextStr;
                string textFilePath = pathUrl + "TextFiles\\" + Path.GetFileName(TextStr);
                if (!File.Exists(textFilePath))
                    File.Copy(TextStr, textFilePath, true);
                if (rb.AddCusResource(newResource))
                {
                    AuthorizationFile(txtContentFile.Text);
                    ClearControl();
                    if (crlf != null)
                    {
                       FreshData fd = new FreshData(crlf.ShowResourceList);
                       fd("", Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty);
                    }
                }
                else
                {
                    MessageBox.Show("新建素材失败。");
                    txtTitle.Focus();
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //清空表单信息
        private void ClearControl()
        {
            txtTitle.Clear();
            txtKeyword.Clear();
            txtContentFile.Clear();
            txtSoundFile.Clear();
            txtTextFile.Clear();
            txtTitle.Focus();
        }

        //选择媒体文件
        private void btnContentFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "支持文件(*.unity3d,*.swf,*.mp4,*.html,*.htm,*.gif,*.png,*.jpg)|*.unity3d;*.swf;*.mp4;*.html;*.htm;*.gif;*.png;*.jpg";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtContentFile.Text = Path.GetFileName(openFileDialog1.FileName);
                ContentStr = openFileDialog1.FileName;
                if (txtContentFile.Text.Substring(txtContentFile.Text.LastIndexOf(".") + 1) == "mp4")
                {
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                }
                else
                {
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                }
            }
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                long contentMaxFilesize = 209715200;//200mb
                if (fi.Length > contentMaxFilesize)
                {
                    MessageBox.Show("请选择小于" + contentMaxFilesize / 1024 / 1024 + "MB的文件。");
                    txtContentFile.Clear();
                }
                rb = new ResourceBLL();
                if (rb.ContentFileResourceIs(Path.GetFileName(txtContentFile.Text)))
                {
                    MessageBox.Show("文件已经存在，请修改此文件名后再试。");
                    txtContentFile.Clear();
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                }
            }
        }

        //选择声音文件
        private void btnSoundFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "支持文件(*.mp3)|*.mp3";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSoundFile.Text = Path.GetFileName(openFileDialog1.FileName);
                SoundStr = openFileDialog1.FileName;
            }
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                long soundMaxFilesize = 15728640;//15mb
                if (fi.Length > soundMaxFilesize)
                {
                    MessageBox.Show("请选择小于" + soundMaxFilesize / 1024 / 1024 + "MB的文件。");
                    txtSoundFile.Clear();
                }
                rb = new ResourceBLL();
                if (rb.SoundFileResourceIs(Path.GetFileName(txtSoundFile.Text)))
                {
                    MessageBox.Show("文件已经存在，请修改此文件名后再试。");
                    txtSoundFile.Clear();
                }
            }
        }

        //选择文字文件
        private void btnTextFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "支持文件(*.html,*.htm)|*.html;*.htm";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtTextFile.Text = Path.GetFileName(openFileDialog1.FileName);
                TextStr = openFileDialog1.FileName;
            }
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
            {
                FileInfo fi = new FileInfo(openFileDialog1.FileName);
                long textMaxFilesize = 15728640;//15mb
                if (fi.Length > textMaxFilesize)
                {
                    MessageBox.Show("请选择小于" + textMaxFilesize / 1024 / 1024 + "MB的文件。");
                    txtTextFile.Clear();
                }
                rb = new ResourceBLL();
                if (rb.TextFileResourceIs(Path.GetFileName(txtTextFile.Text)))
                {
                    MessageBox.Show("文件已经存在，请修改此文件名后再试。");
                    txtTextFile.Clear();
                }
            }
        }

        //声音不上传单选框
        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            btnSoundFile.Enabled = checkBox1.Checked ? false : true;
            SoundStr = checkBox1.Checked ? "specificempty001.mp3" : "";
        }

        //文字不上传单选框
        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            btnTextFile.Enabled = checkBox2.Checked ? false : true;
            TextStr = checkBox2.Checked ? "specificempty001.html" : "";
        }

        private void AuthorizationFile(string file ) 
        {
            char hzmfgf = '.';
            string[] file1 = file.Split(hzmfgf);
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建根节点  
            XmlNode root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);
            CreateNode(xmlDoc, root, "credentials", "");
            CreateNode(xmlDoc, root, "name", EncryptStr(file1[0], EncryptDecryptKey));
            CreateNode(xmlDoc, root, "validation", EncryptStr("0", EncryptDecryptKey));
            try
            {
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + "\\Resources\\Authorization\\" + file1[0] + ".xml");
            }
            catch (Exception ex)
            {
                //显示错误信息  
                Console.WriteLine(ex.Message);
            }  
        }
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public string EncryptStr(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        private Guid SystemType = Guid.Empty;
        private List<StType> sd1 = null;
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)cmbType.SelectedValue != Guid.Empty)
            {
                SystemType = (Guid)cmbType.SelectedValue;
                sd1 = st.StTypeList(SystemType);
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd1.Insert(0, ins);
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "StTypeId";
                comboBox1.DataSource = sd1;
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
        private List<StType> sd2 = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedText != "全部")
            {
                if ((Guid)comboBox1.SelectedValue != Guid.Empty)
                {
                    TypeOfWork = (Guid)comboBox1.SelectedValue;
                    sd2 = st.StTypeList(TypeOfWork);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd2.Insert(0, ins);
                    comboBox4.DisplayMember = "Name";
                    comboBox4.ValueMember = "StTypeId";
                    comboBox4.DataSource = sd2;
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

        private void cmbType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd[e.Index].Name, cmbType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbType);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd1[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd1[e.Index].Name, comboBox1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox1);
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(cr[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(cr[e.Index].StTypeName, comboBox2, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox2);
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(fr[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(fr[e.Index].StLevelName, comboBox3, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox3);
        }

        private void comboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd2[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd2[e.Index].Name, comboBox4, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox4);
        }
    }
}
