using StRttmy.Repository;
using StRttmy.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StRttmy
{
    static class Program
    {
        public static UI.MainForm mf;
        private static string EncryptDecryptKey = "!$study%";
        private static byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };
        private static UMHCONTROLLib.IRCUMHDog dog = new UMHCONTROLLib.RCUMHDogClass();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //IniRunClient();
            mf = new UI.MainForm();
            Application.Run(mf);


            //StRttmyContext df = new StRttmyContext();
            //if (df.Database.CreateIfNotExists())
            //{
            //    MessageBox.Show("1");
            //}
            //else
            //{
            //    MessageBox.Show("2");
            //}
        }


        



       
        //加密狗检测
        private static void IniRunClient()
        {
            string str, tmpstr, str1, tmpstr1;//, str2, tmpstr2
            tmpstr = DogCheck();
            str = DecryptStr(tmpstr, EncryptDecryptKey);
            if (str == "0")
            {
                MessageBox.Show("找不到加密狗！");
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
                        MessageBox.Show("加密狗不正确！");
                        Application.Exit();
                    }
                    else
                    {
                        CompareStr(tmpstr1);
                        mf = new UI.MainForm();
                        Application.Run(mf);
                        //tmpstr2 = ReaddogSn();
                        //str2 = DecryptStr(tmpstr2, EncryptDecryptKey);
                        //if (str2 == "0")
                        //{
                        //    Application.Exit();
                        //}
                        //else
                        //{
                        //    if (str2 != "427")//427 //490
                        //    {
                        //        MessageBox.Show("非本程序加密狗，请核查！");
                        //        Application.Exit();
                        //    }
                        //    else
                        //    {
                        //        CompareStr(tmpstr1);
                        //        mf = new UI.MainForm();
                        //        Application.Run(mf);
                        //    }
                        //}
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

        private static string DecryptStr(string decryptString, string decryptKey)
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

        private static void CompareStr(string tmpstr1)
        {
            string xmlstr;
            xmlstr = ReadXml();
            if (tmpstr1 != xmlstr)
            {
                WriteXml(tmpstr1);
            }
        }

        private static string ReadXml()
        {
            string PathXml = Application.StartupPath + @"\Resources\ServiceXMLFile.xml";
            XmlDocument Doc = new XmlDocument();
            Doc.Load(PathXml);
            XmlNode node = Doc.SelectSingleNode("/root/node");
            return node.InnerText;
        }

        private static void WriteXml(string str)
        {
            string PathXml = Application.StartupPath + @"\Resources\ServiceXMLFile.xml";
            XmlDocument Doc = new XmlDocument();
            Doc.Load(PathXml);
            XmlNode node = Doc.SelectSingleNode("/root/node");
            node.InnerText = str;
            Doc.Save(PathXml);
        }

        private static string EncryptStr(string encryptString, string encryptKey)
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

    }
}
