using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.Xml;

namespace StPublicUtility
{
    public static class StSec
    {
        public static string EncryptDecryptKey = "!$study%";
        private static byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };

        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="SourceFile">待解密的文件名</param>
        /// <returns>返回解密文件</returns>
        public static string JmWj(string SourceFile)
        {
            string decstr = DecryptStr(ReadXml(), EncryptDecryptKey);
            string myPassword = decstr;
            //string myPassword = ReadXml();
            string myEncryptedFile = SourceFile;
            string myDecryptedFile = Application.StartupPath + @"\" + @"SecSwfD\" + SplitFilename(SourceFile);
            SecHelp.DecryptFile(myEncryptedFile, myDecryptedFile, myPassword);
            //MessageBox.Show("解密成功。");
            return myDecryptedFile;
        }

        /// <summary>
        /// 加密文件，并删除
        /// </summary>
        /// <param name="PlainFile">待加密的文件</param>
        public static void JmWjJ(string PlainFile)
        {
            string decstr = DecryptStr(ReadXml(), EncryptDecryptKey);
            string myPassword = decstr;
            //string myPassword = ReadXml();
            string myPlainFile = PlainFile;
            string myEncryptedFile = Application.StartupPath + @"\" + @"SecSwfD\" + SplitFilename(PlainFile);
            SecHelp.EncryptFile(myPlainFile, myEncryptedFile, myPassword);
            //MessageBox.Show("加密成功。");
            File.Delete(Application.StartupPath + @"\" + @"SecSwfD\" + SplitFilename(PlainFile));
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string DecryptStr(string decryptString, string decryptKey)
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

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string EncryptStr(string encryptString, string encryptKey)
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
        /// 读入（文件加密）加密字符串
        /// </summary>
        /// <returns></returns>
        private static string ReadXml()
        {
            string PathXml = Application.StartupPath + @"\Resources\ServiceSecXMLFile.xml";
            XmlDocument Doc = new XmlDocument();
            Doc.Load(PathXml);
            XmlNode node = Doc.SelectSingleNode("/root/node");
            return node.InnerText;
        }

        /// <summary>
        /// 把字符串写入XML文件
        /// </summary>
        /// <param name="str"></param>
        public static void WriteXml(string str)
        {
            string PathXml = Application.StartupPath + @"\Resources\ServiceXMLFile.xml";
            XmlDocument Doc = new XmlDocument();
            Doc.Load(PathXml);
            XmlNode node = Doc.SelectSingleNode("/root/node");
            node.InnerText = str;
            Doc.Save(PathXml);
        }

        /// <summary>
        /// 根据字符串分离出文件
        /// </summary>
        /// <param name="str">待分离的字符</param>
        /// <returns>文件名</returns>
        private static string SplitFilename(string str)
        {
            string strName = str.Substring(str.LastIndexOf(@"\") + 1, str.Length - (str.LastIndexOf(@"\") + 1));
            return strName;
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

        /// <summary>
        /// 解密XML文件
        /// </summary>
        /// <param name="Doc">待解密的XmlDocument文档</param>
        /// <param name="Alg">对称算法参数</param>
        public static void Decrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            XmlElement encryptedElement = Doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;
            EncryptedData edElement = new EncryptedData();
            edElement.LoadXml(encryptedElement);
            EncryptedXml exml = new EncryptedXml();
            byte[] rgbOutput = exml.DecryptData(edElement, Alg);
            exml.ReplaceData(encryptedElement, rgbOutput);
        }

        private static UMHCONTROLLib.IRCUMHDog dog = new UMHCONTROLLib.RCUMHDogClass();

        /// <summary>
        /// 检查加密狗是否存在及软件编号是否正确。
        /// </summary>
        /// <param name="ProSn">软件编号</param>
        public static void ChkDog(string ProSn)
        {
            string str, tmpstr, str1, tmpstr1, str2, tmpstr2;
            tmpstr = DogCheck();
            str = DecryptStr(tmpstr, EncryptDecryptKey);
            if (str == "0")
            {
                //MessageBox.Show("找不到加密狗！");
                //DogOut = true;
                Application.Exit();
            }
            else
            {
                tmpstr1 = ReaddogFsec();
                str1 = DecryptStr(tmpstr1, EncryptDecryptKey);
                if (str1 == "0")
                {
                    Application.Exit();
                }
                else
                {
                    if (str1 != "&technology=start 2003 first only&")
                    {
                        //MessageBox.Show("加密狗不正确！");
                        //DogOut = true;
                        Application.Exit();
                    }
                    else
                    {
                        tmpstr2 = ReaddogSn();
                        str2 = DecryptStr(tmpstr2, EncryptDecryptKey);
                        if (str2 == "0")
                        {
                            Application.Exit();
                        }
                        else
                        {
                            if (str2 != ProSn)
                            {
                                //MessageBox.Show("非本程序加密狗，请核查！");
                                //DogOut = true;
                                Application.Exit();
                            }
                        }
                    }
                }

            }

        }

        private static string DogCheck()
        {
            int retCode;
            string enstr, tmpstr;

            dog.m_command = 1;
            retCode = dog.OperateDog();

            if (retCode != 0)
            {
                tmpstr = "0";
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
            else
            {
                tmpstr = "true";
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
        }

        private static string ReaddogFsec()
        {
            int retCode;
            string enstr, tmpstr;

            dog.m_addr = 20;
            dog.m_bytes = 34;
            dog.m_command = 2;
            retCode = dog.OperateDog();

            if (retCode != 0)
            {
                tmpstr = "0";
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
            else
            {
                tmpstr = dog.Memdata;
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
        }

        private static string ReaddogSn()
        {
            int retCode;
            string enstr, tmpstr;

            dog.m_addr = 60;
            dog.m_bytes = 3;
            dog.m_command = 2;
            retCode = dog.OperateDog();

            if (retCode != 0)
            {
                tmpstr = "0";
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
            else
            {
                tmpstr = dog.Memdata;
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
        }

    }
}
 