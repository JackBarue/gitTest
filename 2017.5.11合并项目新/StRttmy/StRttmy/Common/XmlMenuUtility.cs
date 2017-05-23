using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using StRttmy.Model;
using System.Windows.Forms;
using StRttmy.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace StRttmy.Common
{
    public class XmlMenuUtility
    {
        private static XmlDocument xmldoc;

        /// <summary>
        /// 实体对象转换为Xml节点
        /// </summary>
        /// <param name="xmlDoc">XmlDocument文档</param>
        /// <param name="zTreeNode">实体对象</param>
        /// <returns>返回Xml节点</returns>
        private static XmlElement EntityToXmlElement(XmlDocument xmlDoc, CoursewareResource cr)
        {
            XmlElement xmlEle;
            xmlEle = xmlDoc.CreateElement("menu");
            xmlEle.SetAttribute("id", cr.id);
            xmlEle.SetAttribute("pId", cr.pId);
            xmlEle.SetAttribute("Label", cr.name);
            string mainUrlStr = StrReplace(cr.MainUrl);
            xmlEle.SetAttribute("MainUrl", mainUrlStr);
            string mp3UrlStr = StrReplace(cr.Mp3Url);
            xmlEle.SetAttribute("Mp3Url", mp3UrlStr);
            string textUrlStr = StrReplace(cr.TextUrl);
            xmlEle.SetAttribute("TextUrl", textUrlStr);
            return xmlEle;
        }

        /// <summary>
        /// 实体对象转换为Xml节点
        /// </summary>
        /// <param name="xmlDoc">XmlDocument文档</param>
        /// <param name="zTreeNode">实体对象</param>
        /// <returns>返回Xml节点</returns>
        private static XmlElement EntityToXmlElement(XmlDocument xmlDoc, ZTreeNode cr)
        {
            XmlElement xmlEle;
            xmlEle = xmlDoc.CreateElement("menu");
            xmlEle.SetAttribute("id", cr.id);
            xmlEle.SetAttribute("pId", cr.pId);
            xmlEle.SetAttribute("Label", cr.name);
            string mainUrlStr = StrReplace(cr.MainUrl);
            xmlEle.SetAttribute("MainUrl", mainUrlStr);
            string mp3UrlStr = StrReplace(cr.Mp3Url);
            xmlEle.SetAttribute("Mp3Url", mp3UrlStr);
            string textUrlStr = StrReplace(cr.TextUrl);
            xmlEle.SetAttribute("TextUrl", textUrlStr);
            return xmlEle;
        }

        /// <summary>
        /// 相对路径转换为绝对路径
        /// </summary>
        /// <param name="str">相对路径字符串</param>
        /// <returns>返回绝对路径字符串</returns>
        private static string StrReplace(string str)
        {
            string newStr;
            if (string.IsNullOrEmpty(str))
            {
                newStr = "";
            }
            else
            {
                newStr = str.Replace("../", "").Replace('/', '\\').Trim();
            }
            return newStr;
        }

        /// <summary>
        /// 根据新节点pId值，在XmlNode开始的所有节点（包括子孙节点）中查找id值为新节点pId值的节点，如果有此节点存在，把新节点添加到此节点的子节点末尾
        /// </summary>
        /// <param name="xmlNode">开始查找的节点</param>
        /// <param name="newXmlEleChild">新节点</param>
        private static void WriteXmlNode(XmlNode xmlNode, XmlElement newXmlEleChild)
        {
            if (!(xmlNode.Attributes == null))
            {
                string tmpIdStr = xmlNode.Attributes["id"].Value.ToString().Trim();
                string tmpPidStr = newXmlEleChild.Attributes["pId"].Value.ToString().Trim();

                if (tmpIdStr == tmpPidStr)
                {
                    xmlNode.AppendChild(newXmlEleChild);
                }
                else
                {
                    if (xmlNode.HasChildNodes)
                    {
                        foreach (XmlNode tmpEle in xmlNode.ChildNodes)
                        {
                            WriteXmlNode(tmpEle, newXmlEleChild);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 实体对象列表转换为XmlDocument文档，并按id与pId关系层级排列
        /// </summary>
        /// <param name="crs">实体对象列表</param>
        /// <returns>返回XmlDocument文档</returns>
        public static XmlDocument EntitiesToXmlMenuFile(List<CoursewareResource> crs)
        {
            xmldoc = new XmlDocument();
            //  获取根节点
            XmlElement rootNode;
            ////加入XML的声明段落
            //xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            //xmldoc.AppendChild(xmlnode);
            ////加入encoding="UTF-8"
            //XmlDeclaration decl = (XmlDeclaration)xmldoc.FirstChild;
            //decl.Encoding = "UTF-8";

            //读取列表中的数据，根据数据生成菜单文件
            for (int i = 0; i < crs.Count; i++)
            {
                //  按顺序读取第一条资源作为根节点
                if (i == 0)
                {
                    XmlElement xmlEleRoot = EntityToXmlElement(xmldoc, crs[0]);
                    xmldoc.AppendChild(xmlEleRoot);
                }
                //  顺序读取资源，根据pId把资源添加到相应节点的子节点末尾
                else
                {
                    rootNode = xmldoc.DocumentElement;
                    //  创建新的xml节点
                    XmlElement newXmlEleChild = EntityToXmlElement(xmldoc, crs[i]);
                    WriteXmlNode(rootNode, newXmlEleChild);
                }
            }
            return xmldoc;
        }

        /// <summary>
        /// 实体对象列表转换为XmlDocument文档，并按id与pId关系层级排列
        /// </summary>
        /// <param name="crs">实体对象列表</param>
        /// <returns>返回XmlDocument文档</returns>
        public static XmlDocument EntitiesToXmlMenuFile(List<ZTreeNode> crs)
        {
            xmldoc = new XmlDocument();
            //  获取根节点
            XmlElement rootNode;
            ////加入XML的声明段落
            //xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            //xmldoc.AppendChild(xmlnode);
            ////加入encoding="UTF-8"
            //XmlDeclaration decl = (XmlDeclaration)xmldoc.FirstChild;
            //decl.Encoding = "UTF-8";

            //读取列表中的数据，根据数据生成菜单文件
            for (int i = 0; i < crs.Count; i++)
            {
                //  按顺序读取第一条资源作为根节点
                if (i == 0)
                {
                    XmlElement xmlEleRoot = EntityToXmlElement(xmldoc, crs[0]);
                    xmldoc.AppendChild(xmlEleRoot);
                }
                //  顺序读取资源，根据pId把资源添加到相应节点的子节点末尾
                else
                {
                    rootNode = xmldoc.DocumentElement;
                    //  创建新的xml节点
                    XmlElement newXmlEleChild = EntityToXmlElement(xmldoc, crs[i]);
                    WriteXmlNode(rootNode, newXmlEleChild);
                }
            }
            return xmldoc;
        }

        /// <summary>
        /// 加密菜单文件
        /// </summary>
        /// <param name="sourceMenuName">源明文菜单文件</param>
        /// <param name="destMenuName">加密后的菜单文件</param>
        public static void EncryptMenuXml(string sourceMenuName, string destMenuName)
        {
            RijndaelManaged key = new RijndaelManaged();
            //  设置密钥:key为32位=数字或字母16个=汉字8个
            byte[] byteKey = Encoding.Unicode.GetBytes("studyeasystudyea");
            key.Key = byteKey;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            //  加载要加密的XML文件
            xmlDoc.Load(sourceMenuName);
            Encrypt(xmlDoc, "menu", key);//需要加密的节点
            if (key != null)
            {
                key.Clear();
            }
            //  生成加密后的XML文件
            xmlDoc.Save(destMenuName);
        }

        /// <summary>
        /// 加密XML文件
        /// </summary>
        /// <param name="Doc">待加密的XmlDocument文档</param>
        /// <param name="ElementName">XML根节点名</param>
        /// <param name="Key">对称算法参数</param>
        public static void Encrypt(XmlDocument Doc, string ElementName, SymmetricAlgorithm Key)
        {
            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementName)[0] as XmlElement;
            EncryptedXml eXml = new EncryptedXml();
            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, Key, false);
            EncryptedData edElement = new EncryptedData();
            edElement.Type = EncryptedXml.XmlEncElementUrl;
            string encryptionMethod = null;

            if (Key is TripleDES)
            {
                encryptionMethod = EncryptedXml.XmlEncTripleDESUrl;
            }
            else if (Key is DES)
            {
                encryptionMethod = EncryptedXml.XmlEncDESUrl;
            }
            if (Key is Rijndael)
            {
                switch (Key.KeySize)
                {
                    case 128:
                        encryptionMethod = EncryptedXml.XmlEncAES128Url;
                        break;
                    case 192:
                        encryptionMethod = EncryptedXml.XmlEncAES192Url;
                        break;
                    case 256:
                        encryptionMethod = EncryptedXml.XmlEncAES256Url;
                        break;
                }
            }
            edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);
            edElement.CipherData.CipherValue = encryptedElement;
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }

        #region 平台菜单导入为平台课件、课件资源、资源数据添加到相应的数据库中

        /// <summary>
        /// Xml节点转换为实体对象
        /// </summary>
        /// <param name="xmlEle">Xml节点</param>
        /// <returns>返回实体对象</returns>
        private static CoursewareResource XmlElementToEntity(XmlElement xmlEle)
        {
            CoursewareResource cr = new CoursewareResource();
            cr.id = xmlEle.Attributes["id"].Value.ToString().Trim();
            cr.pId = xmlEle.Attributes["pId"].Value.ToString().Trim();
            cr.name = xmlEle.Attributes["Label"].Value.ToString().Trim();
            //cr.Descrption = xmlEle.Attributes["Descrption"].Value.ToString().Trim();
            cr.MainUrl = StrReplace1(xmlEle.Attributes["MainUrl"].Value.ToString().Trim());
            cr.Mp3Url = StrReplace1(xmlEle.Attributes["Mp3Url"].Value.ToString().Trim());
            cr.TextUrl = StrReplace1(xmlEle.Attributes["TextUrl"].Value.ToString().Trim());
            //cr.Keyword = xmlEle.Attributes["Keyword"].Value.ToString().Trim();
            //cr.Type = xmlEle.Attributes["Type"].Value.ToString().Trim();
            return cr;
        }

        /// <summary>
        /// 绝对路径转换为路径相对
        /// </summary>
        /// <param name="str">相对路径字符串</param>
        /// <returns>返回绝对路径字符串</returns>
        private static string StrReplace1(string str)
        {
            string newStr;
            if (string.IsNullOrEmpty(str))
            {
                newStr = "";
            }
            else
            {
                newStr = "../" + str.Replace('\\', '/').Trim();
            }
            return newStr;
        }

        /// <summary>
        /// 把从rootNode开始的所有节点（包括子孙节点）的属性值写入到zTreeNode实体对象集合列表中。
        /// </summary>
        /// <param name="rootNode">开始查找的节点</param>
        /// <param name="zTreeNodes">zTreeNode实体对象集合列表</param>
        private static void WriteZTreeNode(XmlElement rootNode, List<CoursewareResource> crs)
        {
            if (!(rootNode.Attributes == null))
            {
                CoursewareResource cr = new CoursewareResource();
                cr = XmlElementToEntity(rootNode);
                crs.Add(cr);
                if (rootNode.HasChildNodes)
                {
                    foreach (XmlElement tmpEle in rootNode.ChildNodes)
                    {
                        WriteZTreeNode(tmpEle, crs);
                    }
                }
            }
        }

        /// <summary>
        /// 读取xml文件，并把xml文件转换为实体对象列表
        /// </summary>
        /// <param name="xmlFileName">实体对象列表</param>
        /// <returns>返回实体对象列表</returns>
        public static List<CoursewareResource> XmlDocToEntityList(XmlDocument xmlDoc)
        {
            List<CoursewareResource> crs = new List<CoursewareResource>();
            //xmldoc = new XmlDocument();
            //  根节点
            XmlElement rootNode;
            //try
            //{
            //    xmldoc.Load(xmlFileName);
            //}
            //catch
            //{
            //    throw new Exception("读取Xml文件错误，请检查。");
            //}
            rootNode = xmlDoc.DocumentElement;
            WriteZTreeNode(rootNode, crs);

            return crs;
        }

        /// <summary>
        /// 读取指定路径上的xml中的导出路径
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string ReadExportPath(string Path)
        {
            string foldPath = "";
            if (System.IO.File.Exists(Path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path);
                XmlNode node = xmlDoc.SelectSingleNode("/root/node");
                foldPath = node.InnerText.ToString();
            }
            else
            {
                foldPath = Application.StartupPath + @"\ExportCourse";
            }
            return foldPath;
        }
        
        #endregion

    }
}