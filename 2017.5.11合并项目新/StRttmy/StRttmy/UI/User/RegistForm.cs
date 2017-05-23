using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.Repository;
using StRttmy.BLL;
using StRttmy.Model;
using StRttmy.Common;
using System.Xml;

namespace StRttmy.UI
{
    public delegate void CheckCodeForm();
    public partial class RegistForm : Form
    {
        private CheckCodeForm _deldgw;

        public RegistForm()
        {
            InitializeComponent();
        }

        public RegistForm(CheckCodeForm deldgw)
        {
            InitializeComponent();
            this._deldgw = deldgw;
        }

        //窗体加载
        private void RegistForm_Load(object sender, EventArgs e)
        {
            //label3.Text = "";
            ////MachineInfo f = new MachineInfo();
            //label1.Text = "本机本次生成的识别码为:";
            //label2.Text = "请将此识别码发送给我们,我们将返回对应的注册码!";
            ////string MacStr = f.GetMacByNetworkInterface();//不插网线也可获取
            ////string MacStr = f.example();//不插网线不能获取
            //if (MacStr == "0" || MacStr == "")
            //{
            //    MessageBox.Show("无法正常获取识别码,请确保您的所有硬件及硬件驱动正常!");
            //    this.Close();
            //}
            //label3.Text = Encrypt(MacStr, 1);
            //label4.Text = "注:更换硬件设备可能会导致识别码的改变,将会造成已注册完成的软件失效!\r\n如更换了硬件设备后导致软件不能使用的请联系我们";
            //label5.Text = "填入返回给您的注册码";
        }

        //识别/注册码生成组合加密,目前支持12位的Mac地址字符串(自定义整理成28位的字符串,其中最后4位为无用的补位数字)加密后为长度为44的加密字符串,12位的Mac地址字符串只能是数字和字母组合
        private string Encrypt(string OriginalStr, int CodeType)
        {
            string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            DateTime oldTime = new DateTime(1970, 1, 1);//计算机元年
            double allTime = DateTime.Now.Subtract(oldTime).TotalMilliseconds;//总毫秒数
            double effectiveTime = allTime + 10000;//加上有效时长10秒
            long effectiveTimeStr = (long)effectiveTime;//取整
            string allTimeStr = effectiveTimeStr + "";
            double allTimeDel = Convert.ToDouble(allTimeStr);//转成double
            DateTime nowTime = oldTime.AddMilliseconds(allTimeDel);//转成日期格式,用于显示校正对比
            Random rd = new Random();
            int numBase = rd.Next(0, 10);
            int numType = rd.Next(0, 10);
            string allBeforeStr = allTimeStr.Substring(0, 7);//取前半段7位数
            string allAfterStr = allTimeStr.Substring(7) + CodeType;//取后半段6位数,最后一位表示识别码1/注册码2
            var fixedArr = OriginalStr.ToArray();
            string fixedStr = "";
            foreach (char f in fixedArr)//对传进来的字符串进行编码
            {
                if (isNumberic(f.ToString()))
                    fixedStr += f.ToString() + "0";//是数字的后面补0,0=00,1=10,2=20,3=30,4=40,5=50,6=60,7=70,8=80,9=90
                else
                {
                    int s = Fixeds.IndexOf(f.ToString().ToUpper()) + 1;
                    //是字母的转换为两位数字,小于10的前面补0,10和20从11和21开始往下算
                    //A=01,B=02,C=03,D=04,E=05,F=06,G=07,H=08,I=09,J=11,K=12,L=13,M=14,N=15,O=16,P=17,Q=18,R=19,S=21,T=22,U=23,V=24,W=25,X=26,Y=27,Z=28
                    fixedStr += s <= 9 ? "0" + s : s > 9 && s < 19 ? (s + 1) + "" : s > 18 ? (s + 2) + "" : s + "";
                }
            }
            string fixedStr1 = fixedStr.Substring(0, 7);
            string fixedStr2 = fixedStr.Substring(7, 7);
            string fixedStr3 = fixedStr.Substring(14, 7);
            string fixedStr4 = fixedStr.Substring(21);
            string comBeforeStr = fixedStr1 + allBeforeStr + fixedStr2 + allAfterStr + fixedStr4 + "0369" + fixedStr3;//"0369"为无用的补位数字
            string comAfterStr = "";
            for (int i = 0; i < comBeforeStr.Length + 1; i++)
            {
                if (i % 7 == 0 && i != 0)
                    comAfterStr += (CodeType == 2 ? "-" : "") + RulesEncrypt(comBeforeStr.Substring(i < 7 ? 0 : i - 7, 7), numBase, numType);//识别码和注册码在格式上要有区别
            }
            var FixedsArray = Fixeds.ToArray();
            comAfterStr = FixedsArray[numType + numBase] + FixedsArray[numBase].ToString() + comAfterStr;
            return comAfterStr;
        }

        //识别/注册码加密规则
        private string RulesEncrypt(string Str, int NumB, int NumT)
        {
            string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string needStr1 = "";
            string needStr2 = "";
            var StrArray = Str.ToArray();
            var FixedArray = NumT > 0 && NumT < 4 ? Fixeds.Substring(0, 10).ToArray() : NumT > 3 && NumT < 7 ? Fixeds.Substring(8, 10).ToArray() : Fixeds.Substring(16).ToArray();
            for (int i = 0; i < StrArray.Length; i++)
            {
                int aIndex = System.Int32.Parse(StrArray[i].ToString());
                int bIndex = aIndex + NumB;
                bIndex = bIndex > 9 ? System.Int32.Parse(bIndex.ToString().Substring(1)) : bIndex;
                string tempStr = i == NumB || i == NumT || i % 3 == 0 ? bIndex.ToString() : i % 2 == 0 ? FixedArray[bIndex].ToString().ToLower() : FixedArray[bIndex].ToString();
                if (i % 2 == 0)
                    needStr2 += tempStr;
                else
                    needStr1 += tempStr;
            }
            return needStr1 + needStr2;
        }

        //验证是否是数字
        private bool isNumberic(string intStr)
        {
            int result = -1;
            try
            {
                result = Convert.ToInt32(intStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //复制到剪切板
        private void button1_Click(object sender, EventArgs e)
        {
            if (label3.Text != "")
            {
                Clipboard.SetDataObject(label3.Text);
                MessageBox.Show("已复制到剪切板!");
            }
        }

        //注册产品
        private void button2_Click(object sender, EventArgs e)
        {
            CheckCode();
        }

        //注册码验证(还原注册码对比Mac地址)
        private void CheckCode()
        {
            //if (textBox1.Text == "" || textBox1.Text == null)
            //{
            //    label5.Text = "请填入注册码!";
            //    return;
            //}
            //if (textBox1.Text.Length != 50)
            //{
            //    label5.Text = "请填入长度为44的注册码!";
            //    return;
            //}
            //var textArr = textBox1.Text.ToArray();
            //string IndexStr = "";
            //for (int i = 0; i < textArr.Length; i++)
            //{
            //    string regexstr = @"^[A-Za-z]+$";
            //    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexstr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //    System.Text.RegularExpressions.Match m = regex.Match(textArr[i].ToString());
            //    if (!m.Success && !isNumberic(textArr[i].ToString()) && textArr[i].ToString() != "-")
            //    {
            //        label5.Text = "注册码只能是-、数字和字母组合!";
            //        return;
            //    }
            //    if (textArr[i].ToString() == "-")
            //        IndexStr += i;
            //}
            //if (IndexStr != "21018263442")
            //{
            //    label5.Text = "注册码格式不正确!";
            //    return;
            //}
            //MachineInfo f = new MachineInfo();
            //string aStr = f.GetMacByNetworkInterface();
            //string bStr = Decode(textBox1.Text, 2);
            //if (aStr == bStr)
            //{
            //    SaveXml(textBox1.Text);//注册成功要把注册码存放到SerialNumber.xml中
            //    label5.Text = "注册成功!您可以继续使用本软!\r\n请妥善保管您的注册码,系统或软件重装后此注册码依然可在本机使用。\r\n如果本机更换硬件导致此注册不能使用请联系我们,我们将酌情处理。";
            //    button2.Enabled = false;
            //    button3.Enabled = true;
            //    _deldgw();
            //}
            //else
            //{
            //    label5.Text = "注册不匹配当前主机!请确保您发送了正确的识别码";
            //    return;
            //}
        }

        //下一步
        private void button3_Click(object sender, EventArgs e)
        {
            //_deldgw();
            this.Close();
        }

        //识别/注册码解密(CodeType表示识别码解密时1/注册码解密时2)
        private string Decode(string comAfterStr, int CodeType)
        {
            string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (comAfterStr.Length == 44 || comAfterStr.Length == 50)
            {
                comAfterStr = comAfterStr.Length == 50 ? comAfterStr.Replace("-", "") : comAfterStr;//有"-"的话要去掉
                int baInt = Fixeds.IndexOf(comAfterStr.Substring(0, 1));
                int aInt = Fixeds.IndexOf(comAfterStr.Substring(1, 1));
                int bInt = baInt - aInt;
                string cStr = comAfterStr.Substring(2);//取第3个到最后的字符串
                string DecodeStr = "";
                for (int i = 0; i < cStr.Length + 1; i++)
                {
                    if (i % 7 == 0 && i != 0)
                        DecodeStr += RulesDecode(cStr.Substring(i < 7 ? 0 : i - 7, 7), aInt, bInt) + ",";//解密并按每7个字符串加","来分割开
                }
                var DecodeArr = DecodeStr.Split(',');
                string resolveStr = DecodeArr[0] + DecodeArr[2] + DecodeArr[5] + DecodeArr[4].Substring(0, 3);//24位Mac
                double allTime = Convert.ToDouble(DecodeArr[1] + DecodeArr[3].Substring(0, 6));//时间,解密时暂时用不到
                string OldCodeType = DecodeArr[3].Substring(6);//表示识别码解密时1/注册码解密时2
                if (CodeType == 2 && OldCodeType == "1")//解析注册码的地方放入了识别码
                    return "这是识别码,您需要的是注册码!";
                if (CodeType == 1 && OldCodeType == "2")//解析识别码的地方放入了注册码
                    return "这是注册码,您需要的是识别码!";
                string MacStr = "";
                for (int i = 0; i < resolveStr.Length + 1; i++)//把24位Mac还原成12位
                {
                    if (i % 2 == 0 && i != 0)
                    {
                        string aStr = resolveStr.Substring(i < 2 ? 0 : i - 2, 2);//循环中按顺序每次取一个两位数的字符串
                        if (aStr == "00")
                            MacStr += aStr.Substring(0, 1);//当字符串是"00"时取"0"
                        if (aStr.Substring(0, 1) != "0" && aStr.Substring(1) == "0")
                            MacStr += aStr.Substring(0, 1);//当字符串的第一个字符串不是"0"但第二个字符串是"0"时取第一个字符串
                        if (aStr.Substring(1) != "0")//当字符串的第二个字符串不是"0"时,按规则取出对应的字母
                        {
                            var FixedsArr = Fixeds.ToArray();
                            if (isNumberic(aStr))
                            {
                                int aIndex = System.Int32.Parse(aStr) - 1;
                                MacStr += FixedsArr[aIndex > 9 && aIndex < 20 ? aIndex - 1 : aIndex >= 20 ? aIndex - 2 : aIndex].ToString().ToLower();//按规则还原成字母
                            }
                            else
                                return "不是正确的序列号!";
                        }
                    }
                }
                return MacStr;
            }
            else
                return "序列号格式不正确!";
        }

        //识别/注册码解密规则
        private string RulesDecode(string Str, int NumB, int NumT)
        {
            string Fixeds = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var StrArray1 = Str.Substring(0, 3).ToCharArray();
            var StrArray2 = Str.Substring(3).ToCharArray();
            string allStr = "";
            allStr += StrArray2[0].ToString() + StrArray1[0].ToString();
            allStr += StrArray2[1].ToString() + StrArray1[1].ToString();
            allStr += StrArray2[2].ToString() + StrArray1[2].ToString();
            allStr += StrArray2[3].ToString();
            var allStrArray = allStr.ToCharArray();
            string decodeStr = "";
            for (int i = 0; i < allStrArray.Length; i++)
            {
                int aInt = -1;
                if (isNumberic(allStrArray[i].ToString()))
                    aInt = System.Int32.Parse(allStrArray[i].ToString());
                else
                {
                    var FixedArray = NumT > 0 && NumT < 4 ? Fixeds.Substring(0, 10).ToCharArray() : NumT > 3 && NumT < 7 ? Fixeds.Substring(8, 10).ToCharArray() : Fixeds.Substring(16).ToCharArray();
                    string FixedArrayStr = "";
                    foreach (char a in FixedArray)
                        FixedArrayStr += a.ToString();
                    aInt = FixedArrayStr.IndexOf(allStrArray[i].ToString().ToUpper());
                }
                int X = (aInt - NumB) < 0 ? (10 + aInt) - NumB : aInt - NumB;
                decodeStr += X.ToString();
            }
            return decodeStr;
        }

        //注册码xml的检测
        private void SaveXml(string Values)
        {
            string PathXml = Application.StartupPath + @"\SerialNumber.xml";
            if (!System.IO.File.Exists(PathXml))
                CreateXml(PathXml, Values);//SerialNumber.xml不存在就新建保存注册码
            else
            {
                System.IO.File.Delete(PathXml);//SerialNumber.xml存在就删除旧的,再新建保存注册码
                CreateXml(PathXml, Values);
            }
        }

        //创建存放注册码的xml
        private void CreateXml(string PathXml, string Values)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            XmlNode root = xmlDoc.CreateElement("User");
            xmlDoc.AppendChild(root);
            XmlNode Creatnode = xmlDoc.CreateNode(XmlNodeType.Element, "Code", null);
            Creatnode.InnerText = Values;
            root.AppendChild(Creatnode);
            xmlDoc.Save(PathXml);
        }

    }
}
