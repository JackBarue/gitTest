using StRttmy.BLL;
using StRttmy.Model;
using StRttmy.Models;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;

namespace StRttmy.UI
{
    public partial class PublishCourseware : Form
    {

        private CourseBLL cb = null;
        private CoursewareRepository coursewareRsy;
        private string foldPath = "";
        private int IndexNum = -1;
        private string ExportPath = Application.StartupPath + @"\ExportPath.xml";
        private CoursewareRepository dc;
        private StTypeRepository st;
        private CoursewareLevelRepository cl;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        public PublishCourseware()
        {
            InitializeComponent();
        }

        //窗体加载
        private List<CoursewareLevel> er = null;
        private List<StType> sd = null;
        private List<StTypeSupply> cr = null;
        private List<StLevel> fr = null;
        private void PublishCourseware_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(7, 72, 110);
            dgvCourseList.BackgroundColor = Color.FromArgb(72, 161, 201);
            dc = new CoursewareRepository();
            st = new StTypeRepository();
            cl = new CoursewareLevelRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();
            er = new List<CoursewareLevel>();
            IList<CoursewareLevel> cler = cl.CoursewareLevelsList();
            foreach (CoursewareLevel a in cler)
            {
                if (a.CoursewareLevelId != Guid.Parse("73bea99c-ab04-46ad-8df3-8ecbb0dffe7c"))
                {
                    er.Add(a);
                }
            }
            CoursewareLevel iner = new CoursewareLevel();
            iner.CoursewareLevelId = Guid.Empty;
            iner.Name = "全部";
            er.Insert(0, iner);
            cmbType.DisplayMember = "Name";
            cmbType.ValueMember = "CoursewareLevelId";
            cmbType.DataSource = er;

            sd = st.StTypeList(Guid.Parse("510d3f7e-496c-45a0-9370-57a27b4a3cee"));
            StType ins = new StType();
            ins.StTypeId = Guid.Empty;
            ins.Name = "全部";
            sd.Insert(0, ins);
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "StTypeId";
            comboBox1.DataSource = sd;

            cr = sts.StTypeSupplyList().ToList();
            StTypeSupply insts = new StTypeSupply();
            insts.StTypeSupplyId = Guid.Empty;
            insts.StTypeName = "全部";
            cr.Insert(0, insts);
            comboBox3.DisplayMember = "StTypeName";
            comboBox3.ValueMember = "StTypeSupplyId";
            comboBox3.DataSource = cr;

            fr = sl.StLevelList().ToList();
            StLevel insl = new StLevel();
            insl.StLevelId = Guid.Empty;
            insl.StLevelName = "全部";
            fr.Insert(0, insl);
            comboBox4.DisplayMember = "StLevelName";
            comboBox4.ValueMember = "StLevelId";
            comboBox4.DataSource = fr;
            ShowCourseList(txtKeyword.Text, cmbTypestr, categorystr, levelstr, subjects, TypeOfWork, SystemType);
            txtKeyword.Focus();
            foldPath = StRttmy.Common.XmlMenuUtility.ReadExportPath(ExportPath);
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowCourseList(txtKeyword.Text, cmbTypestr, categorystr, levelstr, subjects, TypeOfWork, SystemType);
        }

        //查询方法
        private void ShowCourseList(string keyword, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            if (Program.mf.LoginUser != null)
            {
                dgvCourseList.DataSource = null;
                dgvCourseList.Columns.Clear();
                IList<Model.Courseware> Coursewares = null;
                cb = new CourseBLL();
                Coursewares = cb.CoursewareList(keyword, Guid.Empty, type, category, level, subjects, typeofwork, systemtype);
                this.dgvCourseList.DataSource = Coursewares;
                //添加重写过的按钮(可控制按钮的各种状态)
                DataGridViewDisableButtonColumn column1 = new DataGridViewDisableButtonColumn();
                column1.Name = "ManageExport";
                column1.Text = "课件导出";
                column1.Width = 82;
                column1.HeaderText = "";
                column1.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
                column1.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
                column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvCourseList.Columns.Add(column1);
                for (int i = 0; i < dgvCourseList.RowCount; i++)
                {
                    dgvCourseList.Rows[i].Cells["ManageExport"].Value = "课件导出";
                }
                //隔行变色
                //this.dgvCourseList.RowsDefaultCellStyle.BackColor = Color.FromArgb(232, 219, 210);//单元格颜色(淡灰色)
                //this.dgvCourseList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 240, 236);//奇数单元格颜色(米黄色)
                //this.dgvResourceList.GridColor = Color.FromArgb(208, 255, 255);//网格的颜色(天蓝色)
                SetDgv(Coursewares.Count());
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //列表表头和操作按钮赋值及属性值
        private void SetDgv(int ListLenght)
        {
            dgvCourseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//列表内容填满datagridview
            dgvCourseList.Columns[0].HeaderText = "课件ID";
            dgvCourseList.Columns[1].HeaderText = "课件类型";
            dgvCourseList.Columns[1].Width = 100;
            dgvCourseList.Columns[2].HeaderText = "课件级别";
            dgvCourseList.Columns[3].HeaderText = "课件关键字";
            dgvCourseList.Columns[3].Width = 320;
            dgvCourseList.Columns[4].HeaderText = "课件名称";
            dgvCourseList.Columns[4].Width = ListLenght > 23 ? 300 : 320;
            dgvCourseList.Columns[5].HeaderText = "课件类型";
            dgvCourseList.Columns[5].Width = 80;
            dgvCourseList.Columns[6].HeaderText = "创建人Id";
            dgvCourseList.Columns[7].HeaderText = "创建人";
            dgvCourseList.Columns[8].HeaderText = "创建时间";
            dgvCourseList.Columns[9].HeaderText = "课件名称";
            dgvCourseList.Columns[10].HeaderText = "课件关键字";
            dgvCourseList.Columns[11].HeaderText = "课件简介";
            dgvCourseList.Columns["CoursewareId"].Visible = false;
            dgvCourseList.Columns["CoursewareLevel"].Visible = false;
            dgvCourseList.Columns["CoursewareLevelId"].Visible = false;
            dgvCourseList.Columns["StLevel"].Visible = false;
            dgvCourseList.Columns["StLevelId"].Visible = false;
            dgvCourseList.Columns["StType"].Visible = false;
            dgvCourseList.Columns["StTypeId"].Visible = false;
            dgvCourseList.Columns["StTypeSupply"].Visible = false;
            dgvCourseList.Columns["StTypeSupplyId"].Visible = false;
            dgvCourseList.Columns["UserId"].Visible = false;
            dgvCourseList.Columns["User"].Visible = false;
            dgvCourseList.Columns["CreateTime"].Visible = false;
            dgvCourseList.Columns["coursewareResources"].Visible = false;

            dgvCourseList.EnableHeadersVisualStyles = false;
            dgvCourseList.Columns[9].HeaderCell.Style.BackColor = Color.FromArgb(23, 111, 161);
            dgvCourseList.Columns[9].HeaderCell.Style.ForeColor = Color.White;
            dgvCourseList.Columns[10].HeaderCell.Style.BackColor = Color.FromArgb(92, 156, 194);
            dgvCourseList.Columns[10].HeaderCell.Style.ForeColor = Color.White;
            dgvCourseList.Columns[11].HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvCourseList.Columns[9].DefaultCellStyle.BackColor = Color.FromArgb(97, 147, 170);
            dgvCourseList.Columns[9].DefaultCellStyle.ForeColor = Color.White;
            dgvCourseList.Columns[10].DefaultCellStyle.BackColor = Color.FromArgb(139, 187, 207);
            dgvCourseList.Columns[11].DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            DataGridViewButtonColumn dgvButtonColEdit = new DataGridViewButtonColumn();// 设置列名
            dgvButtonColEdit.Name = "ManageDetail";
            // 设置列标题
            dgvButtonColEdit.HeaderText = "";
            // 设置按钮标题
            dgvButtonColEdit.UseColumnTextForButtonValue = true;
            dgvButtonColEdit.Text = "查看信息";
            dgvButtonColEdit.Width = 82;
            dgvButtonColEdit.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEdit.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataGridViewButtonColumn dgvButtonColContent = new DataGridViewButtonColumn();
            // 设置列名
            dgvButtonColContent.Name = "ManageContent";
            // 设置列标题
            dgvButtonColContent.HeaderText = "";
            // 设置按钮标题
            dgvButtonColContent.UseColumnTextForButtonValue = true;
            dgvButtonColContent.Text = "查看内容";
            dgvButtonColContent.Width = 82;
            dgvButtonColContent.HeaderCell.Style.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColContent.DefaultCellStyle.BackColor = Color.FromArgb(174, 212, 225);
            dgvButtonColContent.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            // 向DataGridView追加
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColEdit);
            dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColContent);
            //dgvCourseList.Columns.Insert(dgvCourseList.Columns.Count, dgvButtonColExport);
        }

        //添加行号
        private void dgvCourseList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgvCourseList.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgvCourseList.RowHeadersDefaultCellStyle.Font, rectangle, dgvCourseList.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //单元格点击
        private void dgvCourseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.RowIndex为-1时为标题行
            if (e.RowIndex != -1)
            {
                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageDetail")
                {
                    UI.DetailCourseForm dcf = new DetailCourseForm();//查看信息
                    dcf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    dcf.ShowDialog();
                }
                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageContent")
                {
                    UI.CourseResourceForm crf = new CourseResourceForm();//查看内容
                    crf.coursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;
                    crf.ShowDialog();
                }
                if (dgvCourseList.Columns[e.ColumnIndex].Name == "ManageExport")
                {
                    IndexNum = e.RowIndex;
                    DisableClick(false, IndexNum);
                    Guid CoursewareId = (Guid)dgvCourseList.Rows[e.RowIndex].Cells["CoursewareId"].Value;//导出课件需要的id
                    ExportCourse(CoursewareId);
                }
            }
        }

        //导出模式/正常模式
        private void DisableClick(bool isEnabled, int e)
        {
            dgvCourseList.Enabled = isEnabled;
            panel1.Enabled = isEnabled;
            lockframe = isEnabled;
            dgvCourseList.Rows[e].Cells["ManageExport"].Value = isEnabled ? "课件导出" : "导出中...";
            DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)dgvCourseList.Rows[e].Cells["ManageExport"];
            buttonCell.Enabled = isEnabled;
        }

        bool lockframe = true;
        //窗体禁用
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (!lockframe)
            {
                if (m.Msg != 0x0112 && m.WParam != (IntPtr)0xF012)
                {
                    base.WndProc(ref m);
                }
            }
            else
                base.WndProc(ref m);
        }

        //设置导出课件存放路径
        private void button1_Click(object sender, EventArgs e)
        {
            SetPath sp = new SetPath(foldPath, SetPath);
            sp.ShowDialog();
        }

        private void SetPath(string PathStr)
        {
            foldPath = PathStr;
        }

        //导出课件
        private void ExportCourse(Guid coursewareId)
        {
            coursewareRsy = new CoursewareRepository();
            Model.Courseware _courseware = new Model.Courseware();
            _courseware = coursewareRsy.Find(coursewareId);
            DialogResult dr = MessageBox.Show("导出课件可能需要的时间比较长，请耐心等待。请在本课件导出完成后再导出另一课件。确定导出课件《" + _courseware.Name + "》吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                bool ret = true;
                string mess = "";
                string xmlStr = null;
                bool isEmpty = true;
                try
                {
                    string sPath = foldPath + "\\" + _courseware.Name;//文件存放路径
                    string strSwfPath = sPath + "\\Swf\\";
                    string strTextPath = sPath + "\\Text\\";
                    string strSoundPath = sPath + "\\Sound\\";
                    for (int i = 0; i < _courseware.coursewareResources.Count; i++)
                    {
                        string str1 = null, str2 = null, str3 = null, str4 = null;
                        string MainStr = _courseware.coursewareResources.ToList()[i].MainUrl, TextStr = _courseware.coursewareResources.ToList()[i].TextUrl, SoundStr = _courseware.coursewareResources.ToList()[i].Mp3Url;
                        if (!string.IsNullOrEmpty(MainStr))
                        {
                            str1 = Application.StartupPath + "\\Resources\\ContentFiles\\" + System.IO.Path.GetFileName(MainStr);
                            if (System.IO.File.Exists(str1))
                            {
                                if (!Directory.Exists(strSwfPath))
                                    Directory.CreateDirectory(strSwfPath);
                                System.IO.File.Copy(str1, strSwfPath + System.IO.Path.GetFileName(str1), true);//复制保存主文件
                                isEmpty = false;
                            }
                            //媒体内容文件名后缀为html的，需要处理.unity3d文件
                            string[] filePathStr = str1.Split(new char[] { '.' });
                            if (filePathStr[1].Equals("html"))
                            {
                                str4 = filePathStr[0] + ".unity3d";
                                if (System.IO.File.Exists(str4))
                                {
                                    System.IO.File.Copy(str4, strSwfPath + System.IO.Path.GetFileName(str4), true);//复制保存u3d配套文件
                                    isEmpty = false;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(TextStr))
                        {
                            str2 = Application.StartupPath + "\\Resources\\TextFiles\\" + System.IO.Path.GetFileName(TextStr);
                            if (System.IO.File.Exists(str2))
                            {
                                if (!Directory.Exists(strTextPath))
                                    Directory.CreateDirectory(strTextPath);
                                System.IO.File.Copy(str2, strTextPath + System.IO.Path.GetFileName(str2), true);//复制保存文字文件
                            }
                        }
                        if (!string.IsNullOrEmpty(SoundStr))
                        {
                            str3 = Application.StartupPath + "\\Resources\\SoundFiles\\" + System.IO.Path.GetFileName(SoundStr);
                            if (System.IO.File.Exists(str3))
                            {
                                if (!Directory.Exists(strSoundPath))
                                    Directory.CreateDirectory(strSoundPath);
                                System.IO.File.Copy(str3, strSoundPath + System.IO.Path.GetFileName(str3), true);//复制保存声音文件
                            }
                        }
                    }
                    if (isEmpty)
                    {
                        mess = "所要导出的课件内没有对应素材资源!请核实后重试";
                        DisableClick(true, IndexNum);
                        MessageBox.Show(mess);
                        return;
                    }
                    //获取课件导航菜单并加入到_zTreeNodes中
                    List<ZTreeNode> _zTreeNodes = new List<ZTreeNode>();
                    foreach (var cr in _courseware.coursewareResources)
                    {
                        ZTreeNode ztnode = new ZTreeNode();
                        string strSwf = "";
                        string Text = "";
                        string strSound = "";
                        if (cr.MainUrl != null)
                        {
                            strSwf = cr.MainUrl.Substring(cr.MainUrl.LastIndexOf("/") + 1);
                            strSwf = "Swf\\" + strSwf;
                        }
                        if (cr.TextUrl != null)
                        {
                            Text = cr.TextUrl.Substring(cr.TextUrl.LastIndexOf("/") + 1);
                            Text = "Text\\" + Text;
                        }
                        if (cr.Mp3Url != null)
                        {
                            strSound = cr.Mp3Url.Substring(cr.Mp3Url.LastIndexOf("/") + 1);
                            strSound = "Sound\\" + strSound;
                        }
                        ztnode.id = cr.id;
                        ztnode.pId = cr.pId;
                        ztnode.name = cr.name;
                        ztnode.MainUrl = cr.MainUrl;
                        ztnode.TextUrl = cr.TextUrl;
                        ztnode.Mp3Url = cr.Mp3Url;
                        _zTreeNodes.Add(ztnode);
                    }
                    //处理xml菜单
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc = StRttmy.Common.XmlMenuUtility.EntitiesToXmlMenuFile(_zTreeNodes);
                    string destMenuName = null;
                    //保存创建好的XML文档                   
                    xmlStr = sPath + _courseware.Name + ".xml";
                    xmlDoc.Save(xmlStr);//未加密的xml导航菜单
                    destMenuName = sPath + "\\Menu.xml";//需要加密的xml导航菜单
                    StRttmy.Common.XmlMenuUtility.EncryptMenuXml(xmlStr, destMenuName);
                    //生成单机版xml配置文件
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                    doc.AppendChild(dec);
                    XmlElement root = doc.CreateElement("root");
                    doc.AppendChild(root);
                    XmlNode node = doc.CreateElement("node");
                    root.AppendChild(node);
                    node.InnerText = _courseware.Name;
                    doc.Save(sPath + "\\config.xml");
                }
                catch (Exception ex)
                {
                    ret = false;
                    mess = "课件导出中断！";
                    DisableClick(true, IndexNum);
                    MessageBox.Show(mess + ex.ToString());
                    return;//throw new Exception(mess + e.ToString());
                }
                mess = ret ? "课件导出完成。" : "课件导出失败。";
                if (xmlStr != null)
                    System.IO.File.Delete(xmlStr);
                MessageBox.Show(mess);
            }
            DisableClick(true, IndexNum);
        }

        private Guid cmbTypestr = Guid.Empty;
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)cmbType.SelectedValue != Guid.Empty)
            {
                cmbTypestr = (Guid)cmbType.SelectedValue;
            }
            else
            {
                cmbTypestr = Guid.Empty;
            }
        }
        private Guid SystemType = Guid.Empty;
        private List<StType> sd1 = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox1.SelectedValue != Guid.Empty)
            {
                SystemType = (Guid)comboBox1.SelectedValue;
                sd1 = st.StTypeList(SystemType);
                StType ins = new StType();
                ins.StTypeId = Guid.Empty;
                ins.Name = "全部";
                sd1.Insert(0, ins);
                comboBox2.DisplayMember = "Name";
                comboBox2.ValueMember = "StTypeId";
                comboBox2.DataSource = sd1;
            }
            else
            {

                SystemType = Guid.Empty;

                comboBox2.ResetText();
                comboBox5.ResetText();
                comboBox2.SelectedText = "全部";
                TypeOfWork = Guid.Empty;
                comboBox5.SelectedText = "全部";
                subjects = Guid.Empty;
                comboBox2.DataSource = null;
                comboBox5.DataSource = null;
            }
        }
        private Guid TypeOfWork = Guid.Empty;
        private List<StType> sd2 = null;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedText != "全部")
            {
                if ((Guid)comboBox2.SelectedValue != Guid.Empty)
                {
                    TypeOfWork = (Guid)comboBox2.SelectedValue;
                    sd2 = st.StTypeList(TypeOfWork);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd2.Insert(0, ins);
                    comboBox5.DisplayMember = "Name";
                    comboBox5.ValueMember = "StTypeId";
                    comboBox5.DataSource = sd2;
                }
                else
                {
                    TypeOfWork = Guid.Empty;
                    subjects = Guid.Empty;
                    comboBox5.ResetText();
                    comboBox5.SelectedText = "全部";
                    comboBox5.DataSource = null;
                }
            }
            else
            {

                TypeOfWork = Guid.Empty;
                comboBox5.ResetText();
                comboBox5.SelectedText = "全部";
                comboBox5.DataSource = null;
            }

        }

        private Guid subjects = Guid.Empty;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedText != "全部")
            {
                if ((Guid)comboBox5.SelectedValue != Guid.Empty)
                {
                    subjects = (Guid)comboBox5.SelectedValue;
                }
                else
                {
                    subjects = Guid.Empty;
                }
            }
            else
            {
                subjects = Guid.Empty;
            }

        }


        private Guid categorystr = Guid.Empty;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox3.SelectedValue != Guid.Empty)
            {
                categorystr = (Guid)comboBox3.SelectedValue;
            }
            else
            {
                categorystr = Guid.Empty;
            }
        }

        private Guid levelstr = Guid.Empty;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Guid)comboBox4.SelectedValue != Guid.Empty)
            {
                levelstr = (Guid)comboBox4.SelectedValue;
            }
            else
            {
                levelstr = Guid.Empty;
            }
        }

        private void cmbType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(er[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(er[e.Index].Name, cmbType, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void cmbType_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cmbType);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd[e.Index].Name, comboBox1, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox1);
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd1[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd1[e.Index].Name, comboBox2, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox2);
        }

        private void comboBox3_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(cr[e.Index].StTypeName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(cr[e.Index].StTypeName, comboBox3, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox3_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox3);
        }

        private void comboBox4_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(fr[e.Index].StLevelName, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(fr[e.Index].StLevelName, comboBox4, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox4_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox4);
        }

        private void comboBox5_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 绘制背景
            e.DrawBackground();
            //绘制列表项目
            e.Graphics.DrawString(sd2[e.Index].Name, e.Font, System.Drawing.Brushes.Black, e.Bounds);
            //将高亮的列表项目的文字传递到toolTip1(之前建立ToolTip的一个实例)
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                toolTip1.Show(sd2[e.Index].Name, comboBox5, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height);
            e.DrawFocusRectangle();
        }

        private void comboBox5_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(comboBox5);
        }


    }




    //继承重写后可以对DataGridViewDisableButtonColumn这种单元格式的按钮进行更灵活的控制
    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        public DataGridViewDisableButtonColumn()
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
        }
    }

    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        //重写父类方法,以便使属性复制
        public override object Clone()
        {
            DataGridViewDisableButtonCell cell = (DataGridViewDisableButtonCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        //默认情况下,启用按钮
        public DataGridViewDisableButtonCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            // 按钮是禁用的,所以绘制单元格的背景,并禁用按钮 
            if (!this.enabledValue)
            {
                //如果指定了画出单元格的背景
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                //如果指定了画出按钮的边界
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

                //计算区域的绘制按钮
                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment = this.BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                //画出禁用按钮
                ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

                //画出禁用按钮文本
                if (this.FormattedValue is String)
                    TextRenderer.DrawText(graphics, (string)this.FormattedValue, this.DataGridView.Font, buttonArea, SystemColors.GrayText);
            }
            else
            {
                //按钮启用了按钮,让基类处理这幅画
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            }
        }


       
    }
}
