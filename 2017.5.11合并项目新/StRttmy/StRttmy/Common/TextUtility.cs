using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace StRttmy.Common
{
    public class TextUtility
    {
        /// <summary>
        /// 获取验证码【字符串】
        /// </summary>
        /// <param name="Length">验证码长度【必须大于0】</param>
        /// <returns></returns>
        public static string VerificationText(int Length)
        {
            char[] _verification = new Char[Length];
            Random _random = new Random();
            char[] _dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < Length; i++)
            {
                _verification[i] = _dictionary[_random.Next(_dictionary.Length - 1)];
            }
            return new string(_verification);
        }

        public static string Sha256(string plainText)
        {
            if (plainText != null)
            {
                SHA256Managed _sha256 = new SHA256Managed();
                byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
                return Convert.ToBase64String(_cipherText);
            }
            else
            {
                return plainText;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public short dmOrientation;
            public short dmPaperSize;
            public short dmPaperLength;
            public short dmPaperWidth;
            public short dmScale;
            public short dmCopies;
            public short dmDefaultSource;
            public short dmPrintQuality;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int ChangeDisplaySettings([In] ref DEVMODE lpDevMode, int dwFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool EnumDisplaySettings(string lpszDeviceName, Int32 iModeNum, ref DEVMODE lpDevMode);
        public static void ChangeRes(int i, int j)
        {
            DEVMODE DevM = new DEVMODE();
            DevM.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            bool mybool;
            mybool = EnumDisplaySettings(null, 0, ref DevM);
            DevM.dmPelsWidth = i;//宽 
            DevM.dmPelsHeight = j;//高 
            DevM.dmDisplayFrequency = 60;//刷新频率 
            DevM.dmBitsPerPel = 32;//颜色象素 
            long result = ChangeDisplaySettings(ref DevM, 0);
        }
        public static void FuYuan(int i, int j)
        {
            DEVMODE DevM = new DEVMODE();
            DevM.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
            bool mybool;
            mybool = EnumDisplaySettings(null, 0, ref DevM);
            DevM.dmPelsWidth = i;//恢复宽 
            DevM.dmPelsHeight = j;//恢复高 
            DevM.dmDisplayFrequency = 60;//刷新频率 
            DevM.dmBitsPerPel = 32;//颜色象素 
            long result = ChangeDisplaySettings(ref DevM, 0);
        }

        //替换模板html的关键部分变量值
        public static string Ecode(string ContentFileStr)
        {
            string ContentFileName = Path.GetFileName(ContentFileStr);
            string OldHtmlUrl = Application.StartupPath + @"\Resources\ContentFiles\$generalhtml001.html";
            string ECodeStr = TextUtility.Encrypt();
            string stroutput = "";
            string[] filelist = File.ReadAllLines(OldHtmlUrl, Encoding.Default);//Path，文件路径
            for (int linenum = 0; linenum < filelist.Length; linenum++)
            {
                if (filelist[linenum].IndexOf("var messgeStr1 = ") > -1)
                    filelist[linenum] = "var messgeStr1 = \"" + ContentFileName + "\";";//替换要播放的unity3d文件名
                if (filelist[linenum].IndexOf("var messgeStr2 = ") > -1)
                    filelist[linenum] = "var messgeStr2 = \"" + ECodeStr + "\";";//替换验证码
                stroutput += linenum == filelist.Length - 1 ? filelist[linenum] : filelist[linenum] + "\r\n";
            }
            Stream StreamOpen = new FileStream(OldHtmlUrl, FileMode.Open);//打开html
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");//编码集gb2312  最后要使用utf-8
            StreamWriter sw = new StreamWriter(StreamOpen, encode);//修改html
            sw.WriteLine(stroutput);
            sw.Flush();
            sw.Close();
            StreamOpen.Close();
            return OldHtmlUrl;
        }

        //u3d加密
        public static string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string Encrypt()
        {
            DateTime oldTime = new DateTime(1970, 1, 1);//计算机元年
            double allTime = DateTime.Now.Subtract(oldTime).TotalMilliseconds;//总毫秒数
            double effectiveTime = allTime + 30000;//加上有效时长10秒
            long effectiveTimeStr = (long)effectiveTime;//取整
            string allTimeStr = effectiveTimeStr + "";
            double allTimeDel = Convert.ToDouble(allTimeStr);//转成double
            DateTime nowTime = oldTime.AddMilliseconds(allTimeDel);//转成日期格式,用于显示校正对比
            Random rd = new Random();
            int numBase = rd.Next(0, 10);
            int numType = rd.Next(0, 10);
            string allBeforeStr = allTimeStr.Substring(0, 7);//取前半段7位数
            string allAfterStr = allTimeStr.Substring(7);//取后半段6位数,以后总毫秒数达到14位时将会是7位
            //string fixedStr = "Hh1wjhcBWvvLznCFofErZ3g15KbMNk7aFqQrGCUxXSYs/Fc3TekLLQ=="非平台;//移动端在不使用加密狗的情况下将用不到,PC端之后会使用,暂时先使用下面的固定数字字符串
            //6TjnFBJTrrntto9tBuxIhuXuRAxOr33rff5YnTt+lkAbNEz7A3Ey8Q==平台
            string fixedStr1 = "5824967";//固定数字字符串
            string fixedStr2 = "1352479";//固定数字字符串
            string fixedStr3 = "2397681";//固定数字字符串
            string comBeforeStr = numBase + "" + numType + fixedStr1 + allBeforeStr + fixedStr2 + allAfterStr + fixedStr3;//用于显示,不需要显示时删除即可
            string[] comBeforeStrArr = new string[] { fixedStr1, allBeforeStr, fixedStr2, allAfterStr, fixedStr3 };
            string comAfterStr = "";
            foreach (string cStr in comBeforeStrArr)
                comAfterStr += RulesEncrypt(cStr, numBase, numType);
            var FixedsArray = Fixeds.ToArray();
            //comAfterStr = FixedsArray[numType + numBase] + FixedsArray[numBase].ToString() + comAfterStr + "&Hh1wjhcBWvvLznCFofErZ3g15KbMNk7aFqQrGCUxXSYs/Fc3TekLLQ==";
            comAfterStr = FixedsArray[numType + numBase] + FixedsArray[numBase].ToString() + comAfterStr;
            return comAfterStr;
        }

        //u3d规则加密
        public static string RulesEncrypt(string Str, int NumB, int NumT)
        {           
            string needStr1 = "";
            string needStr2 = "";
            var StrArray = Str.ToArray();
            var FixedArray = NumT > 0 && NumT < 4 ? Fixeds.Substring(0, 10).ToArray() : NumT > 3 && NumT < 7 ? Fixeds.Substring(8, 10).ToArray() : Fixeds.Substring(16).ToArray();
            for (int i = 0; i < StrArray.Length; i++)
            {
                int aIndex = System.Int32.Parse(StrArray[i].ToString());
                int bIndex = aIndex + NumB;
                bIndex = bIndex > 9 ? System.Int32.Parse(bIndex.ToString().Substring(1)) : bIndex;
                int cIndex = System.Int32.Parse(StrArray[i].ToString());
                int dIndex = cIndex + NumB;
                dIndex = dIndex > 9 ? System.Int32.Parse(dIndex.ToString().Substring(1)) : dIndex;
                string tempStr = i == NumB || i == NumT ? dIndex.ToString() : i % 2 == 0 ? FixedArray[bIndex].ToString().ToLower() : FixedArray[bIndex].ToString();
                if (i % 2 == 0)
                    needStr2 += tempStr;
                else
                    needStr1 += tempStr;
            }
            return needStr1 + needStr2;
        }

        //MP4规则解密
        public static string MP4Decode(string Str)
        {
            string tempFileUrl = Str.Substring(0, Str.LastIndexOf("\\")) + "\\CheckFile";//加密后文件保存到的路径
            string nowFileName = Path.GetFileName(Str);
            if (Str.LastIndexOf("\\") > -1)
            {
                try
                {
                    if (!Directory.Exists(tempFileUrl))
                    {
                        Directory.CreateDirectory(tempFileUrl);
                    }
                    if (!System.IO.File.Exists(tempFileUrl + "\\" + nowFileName)) 
                    {
                        FileStream fileInput = new FileStream(Str, FileMode.Open);//打开目标文件
                        int ChunkSize = 1024 * 1024;//每次读取文件，只读取100k
                        long byteSlength = fileInput.Length;//获取下载的文件总大小
                        byte[] bytcontent = new byte[ChunkSize];//创建合适文件大小的数组

                        FileStream fileOutput = new FileStream(tempFileUrl + "\\" + nowFileName, FileMode.Create);//创建临时文件
                        int lengthRead = 0;//读取的大小
                        int encryptLength = 1024;
                        while ((lengthRead = fileInput.Read(bytcontent, 0, Convert.ToInt32(ChunkSize))) > 0)
                        {
                            if (lengthRead >= 16)
                            {
                                byte tmp;
                                for (int i = 0; i < encryptLength; ++i)
                                {
                                    byte rawByte = bytcontent[i];
                                    tmp = (byte)(rawByte ^ i);
                                    bytcontent[i] = tmp;
                                }
                                encryptLength = 16;
                            }
                            fileOutput.Write(bytcontent, 0, lengthRead);
                        }
                        fileInput.Close();
                        fileOutput.Close();
                    }
                }
                catch (Exception ex)
                {
                    return "解析出错!" + ex.Message;
                }               
            }
            return tempFileUrl + "\\" + nowFileName;
        }

        //检测并删除临时文件
        public static void CheckDelFile()
        {
            string checkFile = Application.StartupPath + @"\Resources\ContentFiles\CheckFile";
            string[] files = Directory.GetFiles(checkFile, "*.mp4", SearchOption.AllDirectories);//收集此文件夹下的所有MP4格式文件
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string fileNameUrl = checkFile + "\\" + Path.GetFileName(file);
                    if (System.IO.File.Exists(fileNameUrl))
                    {
                        System.IO.File.Delete(fileNameUrl);
                    }
                }
            }
        }

    }
}
