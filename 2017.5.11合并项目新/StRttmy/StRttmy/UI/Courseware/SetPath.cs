using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StRttmy.UI
{
    public delegate void SavePath(string Str);
    public partial class SetPath : Form
    {
        private string foldPath = "";
        private string OldPath = "";
        private string ExportPath = Application.StartupPath + @"\ExportPath.xml";
        private bool isSelect = false;
        private SavePath sp;
        public SetPath()
        {
            InitializeComponent();
        }

        public SetPath(string foldPath, SavePath sp)
        {
            InitializeComponent();
            this.foldPath = foldPath;
            this.sp = sp;
        }

        //窗体加载
        private void SetPath_Load(object sender, EventArgs e)
        {
            foldPath = StRttmy.Common.XmlMenuUtility.ReadExportPath(ExportPath);
            textBox1.Text = foldPath;
            toolTip1.SetToolTip(this.textBox1, textBox1.Text);
            OldPath = textBox1.Text;
            if (textBox1.Text == Application.StartupPath + @"\ExportCourse")
                checkBox1.Checked = true;
        }

        //更换路径
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "当前存放路径:" + foldPath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                checkBox1.Checked = false;
                foldPath = dialog.SelectedPath;
                OldPath = foldPath;
            }
            textBox1.Text = foldPath;
            toolTip1.SetToolTip(this.textBox1, textBox1.Text);
        }

        //使用默认路径
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isSelect = isSelect ? false : true;
            foldPath = isSelect ? Application.StartupPath + @"\ExportCourse" : OldPath;
            textBox1.Text = foldPath;
            toolTip1.SetToolTip(this.textBox1, textBox1.Text);
        }

        //确认保存
        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            XmlNode node = doc.CreateElement("node");
            root.AppendChild(node);
            node.InnerText = foldPath;
            doc.Save(ExportPath);
            sp(foldPath);
            this.Close();
        }

    }
}
