using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace StRttmy.UI.UpdateMaterial
{
    public partial class UpMaterial : Form
    {
        public UpMaterial()
        {
            InitializeComponent();
        }
        public static string EncryptDecryptKey = "!$study%";
        private static byte[] Keys = { 0xED, 0x38, 0xCA, 0xCD, 0xAE, 0xEB, 0xFA, 0xEF };
        private static UMHCONTROLLib.IRCUMHDog dog = new UMHCONTROLLib.RCUMHDogClass();

        private void UpMaterial_Load(object sender, EventArgs e)
        {
            //Create_str();
        }
        /// <summary>
        /// 获取字符串*****此功能未使用
        /// </summary>
        /// <returns>加密后的字符串</returns>
        private string ReaddogFsec()
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
        /// <summary>
        /// 获取狗号
        /// </summary>
        /// <returns>加密后的狗号</returns>
        private string ReaddogSn()
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
        /// <summary>
        /// 写入狗的字符串
        /// </summary>
        private void WritedogSn()
        {
            int retCode;
            dog.m_addr = 65;
            dog.m_bytes = 130;
            dog.m_command = 3;
            dog.Memdata = "";
            retCode = dog.OperateDog();
            if (retCode != 0)
            {
                MessageBox.Show("写入失败");
            }
            else
            {
                MessageBox.Show("写入完成");
            }
        }
        /// <summary>
        /// 获取狗的字符串
        /// </summary>
        /// <returns></returns>
        private string ReaddogString()
        {
            int retCode;
            string enstr, tmpstr;

            dog.m_addr = 65;
            dog.m_bytes = 14;
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
                //richTextBox1.Text = tmpstr;
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
        }
        /// <summary>
        /// 获取狗序列号*****此功能未使用
        /// </summary>
        /// <returns></returns>
        private string Create_result()
        {
            string enstr, tmpstr;
            dog.m_command = 5;

            if (dog.OperateDog() == 0)
            {
                tmpstr = dog.m_result.ToString();
                enstr = EncryptStr(tmpstr, EncryptDecryptKey);
                return enstr;
            }
            else
            {
                return "";
            }

        }
        /// <summary>
        /// 获取程序的说明字符串
        /// </summary>
        /// <returns></returns>
        private string AssemblyDescription()
        {

            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        /// <summary>
        /// 返回的字符串拼接
        /// </summary>
        public void Create_str()
        {
            string str1 = ReaddogSn();//获取加密后的狗号
            string str2 = ReaddogString();//获取加密后的字符串
            //string str3 = Create_result();//获取狗的系列号(没有使用)
            string str4 = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();//获取程序集的版本号
            string str5 = AssemblyDescription();
            string str = str1 + "\\" + str2 +"\\" + str4 + "\\" + str5;//AssemblyVersion
            pictureBox1.Image = Create_ImgCode(str,7);

        }
        /// <summary>
        /// 返回的字符串拼接
        /// </summary>
        public void Create_str(string name)
        {
            string str1 = ReaddogSn();//获取加密后的狗号
            string str2 = ReaddogString();//获取加密后的字符串
            //string str3 = Create_result();//获取狗的系列号(没有使用)
            string str4 = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();//获取程序集的版本号
            string str5 = AssemblyDescription();
            string str = str1 + "\\" + str2 + "\\" + name + "\\" + str4 + "\\" + str5;//AssemblyVersion
            pictureBox1.Image = Create_ImgCode(str, 5);

        }

      

        #region 加解密方法
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
        #endregion

        /// <summary>  
        /// 生成二维码图片  
        /// </summary>  
        /// <param name="codeNumber">要生成二维码的字符串</param>       
        /// <param name="size">大小尺寸</param>  
        /// <returns>二维码图片</returns>  
        private Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }


    }
}
