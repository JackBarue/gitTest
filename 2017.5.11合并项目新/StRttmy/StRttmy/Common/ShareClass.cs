using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using UMHCONTROLLib;
using System.Xml;
using System.Collections;

namespace StRttmy.Common
{
    public class ShareClass
    {
        private UMHCONTROLLib.IRCUMHDog dog = new RCUMHDog();

        private static string EncryptDecryptKey = "!$study%";
        private static byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };


        public string DogCheck()
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

        public string ReaddogFsec()
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

        public string ReaddogSn()
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

        private string EncryptStr(string encryptString, string encryptKey)
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