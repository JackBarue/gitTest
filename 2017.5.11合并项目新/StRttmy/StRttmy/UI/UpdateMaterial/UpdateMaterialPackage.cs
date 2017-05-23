using ICSharpCode.SharpZipLib.Zip;
using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StRttmy.UI.UpdateMaterial
{
    public partial class UpdateMaterialPackage : Form
    {
        public UpdateMaterialPackage()
        {
            InitializeComponent();
        }
        private CoursewareRepository coursewareRsy;
        private ResourceRepository resourceRsy;
        private XmlDocument xmlDocument;
        public string EncryptDecryptKey = "!$study%";//密匙
        private byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };
        private UMHCONTROLLib.IRCUMHDog dog = new UMHCONTROLLib.RCUMHDogClass();//加密狗注册
        private string IdentificationCode = "";//文件根目录唯一吗文件夹名
        private int UpdateProgress = 0;//进度条
        string fileName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;
            fd.Filter = "zip文件|*.zip";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                fileName = fd.FileName;
                label1.Text = fileName;
                if (fileName != "" || fileName != null)
                {
                    button1.Enabled = true;
                    string UrlFile = System.Windows.Forms.Application.StartupPath;//系统相对路径
                    UpdateProgress = UpdateProgress + 10;
                    progressBar1.Value = UpdateProgress;
                    bool up_zip = true;
                    try
                    {
                        up_zip = UnZip(fileName, UrlFile);//无压缩密码解压
                        DeleteUnZipFile();
                        MessageBox.Show("非法压缩包");
                    }
                    catch
                    {
                        string Key = ReaddogSn();//读取加密狗号
                        if (Key != "0")
                        {
                            up_zip = UnZip(fileName, UrlFile, Key);//有压缩密码解压
                        }
                        else
                        {
                            up_zip = false;
                            MessageBox.Show("加密狗有误！");
                        }

                    }
                    if (up_zip)
                    {
                        UpdateProgress = UpdateProgress + 20;
                        progressBar1.Value = UpdateProgress;
                        if (IdentificationCode.Substring(0, 1) == "D")
                        {
                            Up_Date();//升级
                        }
                        if (IdentificationCode.Substring(0, 1) == "T")
                        {
                            unsubscribe();//退订
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请选择正确的压缩包！");
                }
            }
        }
        /// <summary>
        /// 订货
        /// </summary>
        private void Up_Date()
        {
            XmlNode topM1 = ReadDate("Log.xml");
            if (topM1 != null)
            {
                bool StopUp = true;
                foreach (XmlNode a in topM1)
                {
                    //效验日志文件是否有升级
                    if (DecryptStr(a.InnerText, EncryptDecryptKey) == IdentificationCode)
                    {
                        StopUp = false;
                        DeleteUnZipFile();
                        MessageBox.Show("此素材包已添加过");
                        break;
                    }
                    else
                    {
                        StopUp = true;
                        continue;
                    }
                }
                if (StopUp)
                {
                    if (SqlFiles())//数据库添加是否成功
                    {
                        UpdateProgress = UpdateProgress + 20;
                        progressBar1.Value = UpdateProgress;
                        if (UpdateResourcesFile())//物理文件添加是否成功及授权文件
                        {
                            UpdateProgress = UpdateProgress + 30;
                            progressBar1.Value = UpdateProgress;
                            double DogStr1 = 0.00001;
                            WriteDate("Log.xml", EncryptStr(IdentificationCode, EncryptDecryptKey), "content");//写入日志文件
                            DeleteUnZipFile();//删除更新文件夹
                            UpdateProgress = UpdateProgress + 10;
                            progressBar1.Value = UpdateProgress;
                            //写入加密狗标识
                            if (ReaddogStr(65) != "0")
                            {
                                DogStr1 = Convert.ToDouble(ReaddogStr(65).Substring(1, 6));
                                DogStr1 = DogStr1 + 0.0001;
                                WritedogStr("D" + DogStr1.ToString(), 65);
                                UpdateProgress = UpdateProgress + 10;
                                progressBar1.Value = UpdateProgress;
                                MessageBox.Show("更新完成");
                                progressBar1.Value = UpdateProgress = 0;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 退订
        /// </summary>
        private void unsubscribe()
        {
            XmlNode topM1 = ReadDate("Log.xml");
            if (topM1 != null)
            {
                bool StopUp = true;
                foreach (XmlNode a in topM1)
                {
                    if (DecryptStr(a.InnerText, EncryptDecryptKey) == IdentificationCode)
                    {
                        StopUp = false;
                        DeleteUnZipFile();
                        MessageBox.Show("此素材包已退订过");
                        break;
                    }
                    else
                    {
                        StopUp = true;
                        continue;
                    }
                }
                if (StopUp)
                {
                    if (Uninstall())//先用数据库效验物理文件是否有重复所以先删除物理文件
                    {
                        UpdateProgress = UpdateProgress + 30;
                        progressBar1.Value = UpdateProgress;
                        if (SqlFiles())
                        {
                            UpdateProgress = UpdateProgress + 20;
                            progressBar1.Value = UpdateProgress;
                            double DogStr1 = 0;
                            WriteDate("Log.xml", EncryptStr(IdentificationCode, EncryptDecryptKey), "content");
                            DeleteUnZipFile();
                            UpdateProgress = UpdateProgress + 10;
                            progressBar1.Value = UpdateProgress;
                            if (ReaddogStr(71) != "0")
                            {
                                DogStr1 = Convert.ToDouble(ReaddogStr(72).Substring(1, 6));
                                DogStr1 = DogStr1 + 0.0001;
                                WritedogStr("T" + DogStr1.ToString(), 72);
                                UpMaterial df = new UpMaterial();
                                df.Create_str(EncryptStr(IdentificationCode, EncryptDecryptKey));
                                df.ShowDialog();
                                UpdateProgress = UpdateProgress + 10;
                                progressBar1.Value = UpdateProgress;
                                MessageBox.Show("退订完成");
                                progressBar1.Value = UpdateProgress = 0;
                            }
                        }
                    }
                }
            }
        }
        #region 删除物理文件
        /// <summary>
        /// 去除原有授权文件中的重复文件
        /// </summary>
        private bool Uninstall()
        {
            try
            {
                ResourceRepository df = new ResourceRepository();
                //遍历Resources去除重复的文件               
                string[] filenames01 = Directory.GetFileSystemEntries(System.Windows.Forms.Application.StartupPath + "\\" + IdentificationCode + "\\Authorization");//需要删除的授权文件目录
                foreach (string file01 in filenames01)// 遍历所有的文件和目录
                {
                    string file02 = file01.Substring(file01.LastIndexOf("\\") + 1);
                    char kzmfgf = '.';
                    string[] kzm1 = file02.Split(kzmfgf);
                    int n = df.FindFile(kzm1[0] + ".mp4");
                    if (n == 1)
                    {
                        DeleteFile("\\Resources\\ContentFiles", kzm1[0] + ".mp4");
                        DeleteFile("\\Resources\\SoundFiles", kzm1[0] + ".mp3");
                        DeleteFile("\\Resources\\TextFiles", kzm1[0] + ".html");
                        DeleteFile("\\Resources\\Authorization", kzm1[0] + ".xml");
                    }

                }

                return true;
            }
            catch
            {
                MessageBox.Show("物理文件缺失！");
                return false;
            }
        }
        /// <summary>
        /// 删除物理文件
        /// </summary>
        /// <param name="folder">删除文件路径</param>
        /// <param name="filename">删除文件名</param>
        /// <returns>是否删除成功</returns>
        private bool DeleteFile(string folder, string filename)//
        {

            try
            {
                string[] filenames = Directory.GetFileSystemEntries(System.Windows.Forms.Application.StartupPath + folder);
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    string file01 = file.Substring(file.LastIndexOf("\\") + 1);
                    if (filename == file01)
                    {
                        if (File.Exists(file))
                        {
                            //如果存在则删除
                            File.Delete(file);
                        }
                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("物理文件删除失败");
                return false;
            }
        }
        #endregion

        #region 解压
        /// <summary>
        /// 带密码正常解压
        /// </summary>
        /// <param name="zipFilePath">待解压文件地址</param>
        /// <param name="unZipDir">解压后文件地址</param>
        /// <param name="password">解压码</param>
        /// <returns></returns>
        public bool UnZip(string zipFilePath, string unZipDir, string password)
        {
            try
            {
                if (zipFilePath == string.Empty)
                {
                    throw new Exception("压缩文件不能为空！");
                }
                if (!File.Exists(zipFilePath))
                {
                    throw new FileNotFoundException("压缩文件不存在！");
                }
                //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹 
                if (unZipDir == string.Empty)
                    unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
                if (!unZipDir.EndsWith("/"))
                    unZipDir += "/";
                if (!Directory.Exists(unZipDir))
                    Directory.CreateDirectory(unZipDir);
                using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        s.Password = password;
                    }
                    ZipEntry theEntry;
                    IdentificationCode = DecryptStr(Path.GetDirectoryName(s.GetNextEntry().Name), EncryptDecryptKey);
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (!string.IsNullOrEmpty(directoryName))
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {
                                int size;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid password")
                {
                    DeleteUnZipFile();
                    MessageBox.Show("压缩包密码错误");
                }
                else
                {
                    MessageBox.Show("压缩包已损坏，请联系相关工作人员！");
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 无密码非法解压
        /// </summary>
        /// <param name="zipFilePath">解压文件地址</param>
        /// <param name="unZipDir">解压后文件地址</param>
        /// <returns></returns>
        private bool UnZip(string zipFilePath, string unZipDir)
        {
            try
            {
                if (zipFilePath == string.Empty)
                {
                    throw new Exception("压缩文件不能为空！");
                }
                if (!File.Exists(zipFilePath))
                {
                    throw new FileNotFoundException("压缩文件不存在！");
                }

                //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹 

                if (unZipDir == string.Empty)
                    unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
                if (!unZipDir.EndsWith("/"))
                    unZipDir += "/";
                if (!Directory.Exists(unZipDir))
                    Directory.CreateDirectory(unZipDir);
                using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    ZipEntry theEntry;
                    IdentificationCode = DecryptStr(Path.GetDirectoryName(s.GetNextEntry().Name), EncryptDecryptKey);

                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (!string.IsNullOrEmpty(directoryName))
                        {
                            Directory.CreateDirectory(unZipDir + directoryName);
                        }
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                            {
                                int size;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 删除解压后的文件
        /// </summary>
        private void DeleteUnZipFile()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\" + IdentificationCode);
                di.Delete(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 更新数据库

        /// <summary>
        /// 更新数据/删除数据
        /// </summary>
        /// <returns>是否更新或删除成功</returns>
        private bool SqlFiles()
        {
            bool Datebase = false;
            try
            {
                string[] filenames = Directory.GetFileSystemEntries(System.Windows.Forms.Application.StartupPath + "\\" + IdentificationCode + "\\Sql");
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (IdentificationCode.Substring(0, 1) == "D")
                    {
                        if (UpdateSqlFile(file))
                        {
                            Datebase = true;
                            continue;
                        }
                        else
                        {
                            Datebase = false;
                            break;
                        }
                    }
                    if (IdentificationCode.Substring(0, 1) == "T")
                    {
                        if (Deletedate(file))
                        {
                            Datebase = true;
                            continue;
                        }
                        else
                        {
                            Datebase = false;
                            break;
                        }
                    }

                }
                return Datebase;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Datebase;
            }

        }
        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="FileName">导入路径</param>
        private bool UpdateSqlFile(string FileName)
        {
            List<PlatformZTreeNode> zTreeNodes = new List<PlatformZTreeNode>();
            try
            {
                zTreeNodes = XmlDocToEntityList(FileName);
            }
            catch (Exception err)
            {
                MessageBox.Show("读取菜单失败！" + err.Message.ToString());
            }

            coursewareRsy = new CoursewareRepository();
            Courseware tmpCourseware = new Courseware();
            tmpCourseware = PlatformZTreeNodeToCourseware(zTreeNodes[0]);
            tmpCourseware.UserId = 1;//固定值
            tmpCourseware.Level = CoursewareLevel.基本;
            //  给tmpCourseware的课件资源添加内容
            tmpCourseware.coursewareResources = new List<CoursewareResource>();
            //  把课件所用的资源信息添加到资源数据表中
            bool isFalse = false;
            foreach (PlatformZTreeNode tmpPlatformZTreeNode in zTreeNodes)
            {
                CoursewareResource tmpCoursewareResource = new CoursewareResource();
                tmpCoursewareResource = PlatformZTreeNodeToCoursewareResource(tmpPlatformZTreeNode);
                tmpCourseware.coursewareResources.Add(tmpCoursewareResource);
                //  zTreeNodes节点的媒体、文字、声音属性为空时，不转换为资源文件
                if (!(string.IsNullOrEmpty(tmpPlatformZTreeNode.MainUrl) || string.IsNullOrEmpty(tmpPlatformZTreeNode.Mp3Url) || string.IsNullOrEmpty(tmpPlatformZTreeNode.TextUrl)))
                {
                    Resource tmpResource = new Resource();
                    tmpResource = PlatformZTreeNodeToResource(tmpPlatformZTreeNode);
                    tmpResource.Level = ResourceLevel.基本;//
                    tmpResource.UserId = 1;//固化成超级管理员
                    resourceRsy = new ResourceRepository();
                    if (!resourceRsy.Add(tmpResource))//保存Resource表数据
                    {
                        isFalse = true;
                    }
                }
            }
            if (isFalse)
            {
                MessageBox.Show("素材添加不完整，请与管理员联系！");
                return false;
            }
            if (coursewareRsy.Add(tmpCourseware))//由于model中Courseware表和CoursewareResource表是一对多关联关系,所以保存CoursewareResource时两张表都保存了
            {
                // MessageBox.Show("课件导入成功！");
                return true;
            }
            else
            {
                MessageBox.Show("课件导入失败,请与管理员联系！");
                return false;
            }
        }
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="FileName">删除路径</param>
        /// <returns>是否删除成功</returns>
        private bool Deletedate(string FileName)
        {
            List<PlatformZTreeNode> zTreeNodes = new List<PlatformZTreeNode>();
            try
            {
                zTreeNodes = XmlDocToEntityList(FileName);
            }
            catch (Exception err)
            {
                MessageBox.Show("读取菜单失败！" + err.Message.ToString());
            }
            coursewareRsy = new CoursewareRepository();
            bool isFalse = false;
            foreach (PlatformZTreeNode tmpPlatformZTreeNode in zTreeNodes)
            {
                string media = tmpPlatformZTreeNode.MainUrl.Substring(tmpPlatformZTreeNode.MainUrl.LastIndexOf("/") + 1);
                string mp3 = tmpPlatformZTreeNode.Mp3Url.Substring(tmpPlatformZTreeNode.Mp3Url.LastIndexOf("/") + 1);
                string txt = tmpPlatformZTreeNode.TextUrl.Substring(tmpPlatformZTreeNode.TextUrl.LastIndexOf("/") + 1);
                //  zTreeNodes节点的媒体、文字、声音属性为空时，不转换为资源文件
                if (!(string.IsNullOrEmpty(tmpPlatformZTreeNode.MainUrl) || string.IsNullOrEmpty(tmpPlatformZTreeNode.Mp3Url) || string.IsNullOrEmpty(tmpPlatformZTreeNode.TextUrl)))
                {

                    resourceRsy = new ResourceRepository();
                    if (!resourceRsy.Delete(media, mp3, txt))//保存Resource表数据
                    {
                        isFalse = true;
                    }
                }
            }
            if (isFalse)
            {
                MessageBox.Show("卸载出错，请与管理员联系！");
                return false;
            }

            if (coursewareRsy.Delete(zTreeNodes[0].name))//由于model中Courseware表和CoursewareResource表是一对多关联关系,所以保存CoursewareResource时两张表都保存了
            {
                // MessageBox.Show("课件导入成功！");
                return true;
            }
            else
            {
                MessageBox.Show("课件卸载失败,请与管理员联系！");
                return false;
            }

        }
        /// <summary>
        /// 读取xml文件，并把xml文件转换为实体对象列表
        /// </summary>
        /// <param name="xmlFileName">实体对象列表</param>
        /// <returns>返回实体对象列表</returns>
        public List<PlatformZTreeNode> XmlDocToEntityList(string fileName)
        {
            List<PlatformZTreeNode> zTreeNodes = new List<PlatformZTreeNode>();
            xmlDocument = new XmlDocument();
            //  根节点
            XmlElement rootNode;
            xmlDocument.Load(fileName);
            rootNode = xmlDocument.DocumentElement;
            WriteZTreeNode(rootNode, zTreeNodes);
            //return zTreeNodes;
            List<PlatformZTreeNode> tmpzTreeNodes = new List<PlatformZTreeNode>();
            tmpzTreeNodes = ChangeIdPid(zTreeNodes);
            return tmpzTreeNodes;
        }

        /// <summary>
        /// 把从rootNode开始的所有节点（包括子孙节点）的属性值写入到zTreeNode实体对象集合列表中。
        /// </summary>
        /// <param name="rootNode">开始查找的节点</param>
        /// <param name="zTreeNodes">zTreeNode实体对象集合列表</param>
        private void WriteZTreeNode(XmlElement rootNode, List<PlatformZTreeNode> zTreeNodes)
        {
            if (!(rootNode.Attributes == null))
            {
                PlatformZTreeNode zTreeNode = new PlatformZTreeNode();
                zTreeNode = XmlElementToEntity(rootNode);
                zTreeNodes.Add(zTreeNode);
                if (rootNode.HasChildNodes)
                {
                    foreach (XmlElement tmpEle in rootNode.ChildNodes)
                    {
                        WriteZTreeNode(tmpEle, zTreeNodes);
                    }
                }
            }
        }

        /// <summary>
        /// 遍历所有节点的id，id编号改变为Guid，pid号也作相应改变。
        /// </summary>
        /// <param name="allNodes"></param>
        /// <returns></returns>
        private List<PlatformZTreeNode> ChangeIdPid(List<PlatformZTreeNode> allNodes)
        {
            for (int i = 0; i < allNodes.Count; i++)
            {
                string oldId = allNodes[i].id;
                string newId = Guid.NewGuid().ToString();
                allNodes[i].id = newId;
                for (int j = i; j < allNodes.Count; j++)
                {
                    if (allNodes[j].pId == oldId)
                    {
                        allNodes[j].pId = newId;
                    }
                }
            }
            return allNodes;
        }

        /// <summary>
        /// Xml节点转换为实体对象
        /// </summary>
        /// <param name="xmlEle">Xml节点</param>
        /// <returns>返回实体对象</returns>
        private PlatformZTreeNode XmlElementToEntity(XmlElement xmlEle)
        {
            PlatformZTreeNode zTreeNode = new PlatformZTreeNode();
            zTreeNode.id = xmlEle.Attributes["id"].Value.ToString().Trim();
            zTreeNode.pId = xmlEle.Attributes["pId"].Value.ToString().Trim();
            zTreeNode.name = xmlEle.Attributes["Label"].Value.ToString().Trim();
            zTreeNode.Descrption = xmlEle.Attributes["Descrption"].Value.ToString().Trim();
            //string mainUrlStr = StrReplace(zTreeNode.MainUrl);
            zTreeNode.MainUrl = xmlEle.Attributes["MainUrl"].Value.ToString().Trim();
            //string mp3UrlStr = StrReplace(zTreeNode.Mp3Url);
            zTreeNode.Mp3Url = xmlEle.Attributes["Mp3Url"].Value.ToString().Trim();
            //string textUrlStr = StrReplace(zTreeNode.TextUrl);
            zTreeNode.TextUrl = xmlEle.Attributes["TextUrl"].Value.ToString().Trim();
            zTreeNode.Keyword = xmlEle.Attributes["Keyword"].Value.ToString().Trim();
            zTreeNode.Type = xmlEle.Attributes["Type"].Value.ToString().Trim();
            return zTreeNode;
        }

        /// <summary>
        /// 把PlatformZTreeNode对象转换为Courseware对象
        /// </summary>
        /// <param name="ztreeNode">PlatformZTreeNode对象</param>
        /// <returns>返回Courseware对象</returns>
        private Courseware PlatformZTreeNodeToCourseware(PlatformZTreeNode ztreeNode)
        {
            Courseware tmpCourseware = new Courseware();
            if (Enum.IsDefined(typeof(CoursewareType), ztreeNode.Type))
            {
                object tmpObj = Enum.Parse(typeof(CoursewareType), ztreeNode.Type);
                tmpCourseware.Type = (CoursewareType)tmpObj;
            }
            else
            {
                throw new Exception("名称为“" + ztreeNode.name + "”课件类型错误，请核对。");
            }

            tmpCourseware.Level = CoursewareLevel.基本;
            if (!string.IsNullOrEmpty(ztreeNode.Keyword))
            {
                tmpCourseware.Keyword = ztreeNode.Keyword;
            }
            else
            {
                throw new Exception("名称为“" + ztreeNode.name + "”关键字有错误（关键字不能为空），请核对。");
            }
            tmpCourseware.Name = ztreeNode.name;
            if (!string.IsNullOrEmpty(ztreeNode.Descrption))
            {
                tmpCourseware.Description = ztreeNode.Descrption;
            }
            else
            {
                throw new Exception("名称为“" + ztreeNode.name + "”课件简介有错误（课件简介不能为空），请核对。");
            }
            //tmpCourseware.UserId = "";
            tmpCourseware.CreateTime = System.DateTime.Now;
            return tmpCourseware;
        }

        private CoursewareResource PlatformZTreeNodeToCoursewareResource(PlatformZTreeNode ztreeNode)
        {
            CoursewareResource tmpCoursewareResource = new CoursewareResource();
            tmpCoursewareResource.id = ztreeNode.id;
            tmpCoursewareResource.pId = ztreeNode.pId;
            tmpCoursewareResource.name = ztreeNode.name;
            tmpCoursewareResource.MainUrl = ztreeNode.MainUrl;
            tmpCoursewareResource.TextUrl = ztreeNode.TextUrl;
            tmpCoursewareResource.Mp3Url = ztreeNode.Mp3Url;
            return tmpCoursewareResource;
        }

        private Resource PlatformZTreeNodeToResource(PlatformZTreeNode ztreeNode)
        {
            //  ztreeNode节点的媒体、文字、声音属性的相对路径需要去除，只留文件名
            Resource tmpResource = new Resource();
            if (Enum.IsDefined(typeof(ResourceType), ztreeNode.Type))
            {
                object tmpObj = Enum.Parse(typeof(ResourceType), ztreeNode.Type);
                tmpResource.Type = (ResourceType)tmpObj;
            }
            else
            {
                throw new Exception("名称为“" + ztreeNode.name + "”素材类型错误，请核对。");
            }

            tmpResource.Level = ResourceLevel.基本;
            tmpResource.Title = ztreeNode.name;
            if (!string.IsNullOrEmpty(ztreeNode.Keyword))
            {
                tmpResource.Keyword = ztreeNode.Keyword;
            }
            else
            {
                throw new Exception("名称为“" + ztreeNode.name + "”关键字有错误（关键字不能为空），请核对。");
            }
            tmpResource.ContentFile = ztreeNode.MainUrl.Substring(ztreeNode.MainUrl.LastIndexOf(@"/") + 1);
            tmpResource.TextFile = ztreeNode.TextUrl.Substring(ztreeNode.TextUrl.LastIndexOf(@"/") + 1);
            tmpResource.SoundFile = ztreeNode.Mp3Url.Substring(ztreeNode.Mp3Url.LastIndexOf(@"/") + 1);
            //tmpCourseware.UserId = "";
            tmpResource.CreateTime = System.DateTime.Now;
            return tmpResource;
        }

        #endregion

        #region 更新物理数据
        /// <summary>
        /// 升级物理资源、升级授权文件
        /// </summary>
        private bool UpdateResourcesFile()
        {
            try
            {
                string[] filenames = Directory.GetFileSystemEntries(System.Windows.Forms.Application.StartupPath + "\\" + IdentificationCode);
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    string cc = file.Substring(file.LastIndexOf(".") + 1);
                    string ff = file.Substring(file.LastIndexOf("\\") + 1);
                    if (cc != "xml")
                    {
                        if (ff == "Resources")
                        {
                            CopyDirectory(file, System.Windows.Forms.Application.StartupPath + "\\Resources");//物理文件
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="srcdir">复制路径</param>
        /// <param name="desdir">粘贴路径</param>
        private void CopyDirectory(string srcdir, string desdir)
        {
            try
            {
                string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);
                string desfolderdir = desdir + "\\";
                if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
                {
                    desfolderdir = desdir + folderName;
                }
                string[] filenames = Directory.GetFileSystemEntries(srcdir);
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    {
                        string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                        if (!Directory.Exists(currentdir))
                        {
                            Directory.CreateDirectory(currentdir);
                        }
                        CopyDirectory(file, desfolderdir);
                    }
                    else // 否则直接copy文件
                    {
                        string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);
                        srcfileName = desfolderdir + "\\" + srcfileName;
                        if (!Directory.Exists(desfolderdir))
                        {
                            Directory.CreateDirectory(desfolderdir);
                        }
                        File.Copy(file, srcfileName, true);
                    }
                }
            }
            catch
            {
                MessageBox.Show("升级压缩包损坏，请联系管理员！");

            }
        }


        #endregion

        #region 加解密方法
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

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public string DecryptStr(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        #region 加密狗读写
        /// <summary>
        /// 获取狗号
        /// </summary>
        /// <returns>加密后的狗号</returns>
        private string ReaddogSn()
        {
            int retCode;
            string tmpstr = "0";

            dog.m_addr = 60;
            dog.m_bytes = 3;
            dog.m_command = 2;
            retCode = dog.OperateDog();

            if (retCode == 0)
            {
                tmpstr = dog.Memdata;
            }
            return tmpstr;
        }

        /// <summary>
        /// 写入狗的字符串
        /// </summary>
        private bool WritedogStr(string str, short location)
        {
            int retCode;
            dog.m_addr = location;
            dog.m_bytes = 7;
            dog.m_command = 3;
            dog.Memdata = str;
            retCode = dog.OperateDog();
            if (retCode != 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 获取狗的字符串
        /// </summary>
        /// <returns>狗的字符串</returns>
        private string ReaddogStr(short location)
        {
            int retCode;
            string tmpstr = "0";
            dog.m_addr = location;
            dog.m_bytes = 7;
            dog.m_command = 2;
            retCode = dog.OperateDog();
            if (retCode == 0)
            {
                tmpstr = dog.Memdata;
                return tmpstr;
            }
            return tmpstr;
        }
        #endregion

        #region XML读写
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="path">写入日志地址</param>
        /// <param name="txtnode">写入日志内容</param>
        private void WriteDate(string path, string txtnode, string childelement)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.StartupPath + "\\" + path);
            //创建一个新节点<book>  
            XmlElement newBook = xmlDocument.CreateElement(childelement);
            XmlText bookAuthor = xmlDocument.CreateTextNode(txtnode);
            newBook.AppendChild(bookAuthor);
            XmlNode xmlRoot = xmlDocument.DocumentElement;
            xmlRoot.InsertBefore(newBook, xmlRoot.FirstChild);
            xmlDocument.Save(System.Windows.Forms.Application.StartupPath + "\\" + path);
        }
        /// <summary>
        /// 读取日志
        /// </summary>
        /// <param name="XmlPath">读取日志地址</param>
        /// <returns>返回日志集合</returns>
        private XmlNode ReadDate(string XmlPath)
        {
            try
            {
                string time = System.Windows.Forms.Application.StartupPath + "\\" + XmlPath;
                xmlDocument = new XmlDocument();
                xmlDocument.Load(time);
                XmlNode topM1 = xmlDocument.FirstChild.NextSibling;
                return topM1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DeleteUnZipFile();
                MessageBox.Show("缺少必要的运行文件，请与管理员！");
                return null;
            }
        }

        #endregion

        private void UpdateMaterialPackage_Load(object sender, EventArgs e)
        {
           // textBox1.Text = Guid.NewGuid().ToString("N");
        }
        public delegate void Close_detection();
        public Close_detection  C_detection;
        private void button2_Click(object sender, EventArgs e)
        {
            C_detection += new MainForm().CloseDetection;           
            C_detection();
            this.Close();
        }
    }
}
