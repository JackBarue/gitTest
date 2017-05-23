using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StRttmy.Model
{
    class Validation
    {
        public string EncryptDecryptKey = "!$study%";//密匙
        private byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };
        private UMHCONTROLLib.IRCUMHDog dog = new UMHCONTROLLib.RCUMHDogClass();//加密狗注册
        private static string dogstr = "";
        private static bool dog_number = true;
        public bool validation_file(string str)
        {
            if (dog_number)
            {
                dogstr = ReaddogSn();
                dog_number = false;
            }

            bool pass = false;
            char hzmfgf = '.';
            string[] hzm = str.Split(hzmfgf);
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\Resources\\Authorization\\" + hzm[0] + ".xml"))
            {
                string time = System.Windows.Forms.Application.StartupPath + "\\Resources\\Authorization\\" + hzm[0] + ".xml";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(time);
                string credentials = xmldoc.SelectSingleNode("/root/credentials").InnerText;
                string name = xmldoc.SelectSingleNode("/root/name").InnerText;
                string validation = xmldoc.SelectSingleNode("/root/validation").InnerText;
                if (credentials != "" && credentials != null)
                {
                    if (DecryptStr(name, EncryptDecryptKey) == hzm[0] && DecryptStr(credentials, EncryptDecryptKey) == dogstr)
                    {
                        pass = true;
                    }
                }
                else
                {
                    if (DecryptStr(name, EncryptDecryptKey) == hzm[0])
                    {
                        pass = true;
                    }
                }
                return pass;
            }
            else
            {
                return pass;
            }
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        private string DecryptStr(string decryptString, string decryptKey)
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
    }
}
