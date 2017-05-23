using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace StRttmy.Common
{
    public static class ShareClassSec
    {
        private static string EncryptDecryptKey = "!$study%";
        private static byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };

        public static bool ChkSec()
        {
            ShareClass obj = new ShareClass();
            string str, tmpstr, str1, tmpstr1, str2, tmpstr2;
            tmpstr = obj.DogCheck();
            str = DecryptStr(tmpstr, EncryptDecryptKey);
            if (str == "0")
            {
                return false;
            }
            else
            {
                tmpstr1 = obj.ReaddogFsec();
                str1 = DecryptStr(tmpstr1, EncryptDecryptKey);
                if (str1 == "0")
                {
                    return false;
                }
                else
                {
                    if (str1 != "&technology=start 2003 first only&")
                    {
                        return false;
                    }
                    else
                    {
                        tmpstr2 = obj.ReaddogSn();
                        str2 = DecryptStr(tmpstr2, EncryptDecryptKey);
                        if (str2 == "0")
                        {
                            return false;
                        }
                        else
                        {
                            if (str2 != "427")
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
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
    }
}