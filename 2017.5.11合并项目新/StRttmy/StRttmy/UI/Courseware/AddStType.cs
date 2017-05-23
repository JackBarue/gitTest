using StRttmy.Model;
using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StRttmy.UI.Courseware
{
    public partial class AddStType : Form
    {
        public AddStType()
        {
            InitializeComponent();
        }
        private StTypeRepository st;
        private StLevelRepository sl;
        private StTypeSupplyRepository sts;
        private StType _subjects = null;
        private StType _TypeOfWork = null;
        private StType _SystemType = null;
        private StLevel _levelstr = null;
        private StTypeSupply _categorystr = null; 
        private void AddStType_Load(object sender, EventArgs e)
        {
            RefreshListBox();
        }
        private void RefreshListBox() 
        {
            st = new StTypeRepository();
            sl = new StLevelRepository();
            sts = new StTypeSupplyRepository();
            List<StType> sttype = st.StTypesNameList(Program.mf.LoginUser.UserId);//Guid.Parse("6223f38c-c9c0-4ca7-a35f-8687124b3d88")

            List<StType> systr = new List<StType>();//StTypesNameList
            List<StType> tystr = new List<StType>();
            List<StType> sjstr = new List<StType>();

            foreach (StType a in sttype)//
            {
                sjstr.Add(a);               
                tystr.Add(st.StType(a.Fid));
                systr.Add(st.StType(st.StType(a.Fid).Fid));
            }
            StType insy = new StType();
            insy.StTypeId = Guid.Empty;
            insy.Name = "全部";
            systr.Insert(0, insy);
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "StTypeId";
            listBox1.DataSource = systr;
            

            StType inst = new StType();
            inst.StTypeId = Guid.Empty;
            inst.Name = "全部";
            tystr.Insert(0, inst);
            listBox2.DataSource = tystr;
            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = "StTypeId";

            StType inty = new StType();
            inty.StTypeId = Guid.Empty;
            inty.Name = "全部";
            sjstr.Insert(0, inty);
            listBox3.DisplayMember = "Name";
            listBox3.ValueMember = "StTypeId";
            listBox3.DataSource = sjstr;



            List<StLevel> slstr = new List<StLevel>();
            foreach (StLevel a in sl.StLevelsNameList(Program.mf.LoginUser.UserId))//Program.mf.LoginUser.UserId
            {
                slstr.Add(a);
            }
            //StLevel inssl = new StLevel();
            //inssl.StLevelId = Guid.Empty;
            //inssl.StLevelName = "请选择";
            //slstr.Insert(0, inssl);
            listBox4.DataSource = slstr;
            listBox4.DisplayMember = "StLevelName";
            listBox4.ValueMember = "StLevelId";


            List<StTypeSupply> stsstr = new List<StTypeSupply>();
            foreach (StTypeSupply a in sts.StTypeSuppliesNameList(Program.mf.LoginUser.UserId))//Program.mf.LoginUser.UserId
            {
                stsstr.Add(a);
            }
            //StTypeSupply inssts = new StTypeSupply();
            //inssts.StTypeSupplyId = Guid.Empty;
            //inssts.StTypeName = "请选择";
            //stsstr.Insert(0, inssts);
            listBox5.DataSource = stsstr;
            listBox5.DisplayMember = "StTypeName";
            listBox5.ValueMember = "StTypeSupplyId";
            

            _subjects = null;
            _TypeOfWork = null;
            _SystemType = null;
            _levelstr = null;
            _categorystr = null; 
        }
        //新增
        private void button1_Click(object sender, EventArgs e)
        {
            AddAlltType aat = new AddAlltType();
            aat.ShowDialog();
            RefreshListBox();
        }
        //编辑
             
        private void button2_Click(object sender, EventArgs e)
        {
            AddAlltType aat = new AddAlltType();
            aat.subjects =_subjects;
            aat.TypeOfWork = _TypeOfWork;
            aat.SystemType = _SystemType;
            aat.levelstr = _levelstr;
            aat.categorystr = _categorystr;
            aat.ShowDialog();
            RefreshListBox();
        }
        //删除
        private void button3_Click(object sender, EventArgs e)
        {
            st = new StTypeRepository();
            if (_subjects != null)
            {
                if (_subjects.ReferenceType == 0)
                {
                    if (st.StTypeList(st.StType(_subjects.Fid).Fid).Count() == 1 && st.StTypeList(_subjects.Fid).Count() == 1)
                    {
                        if (st.Delete(st.StType(st.StType(_subjects.Fid).Fid).StTypeId))
                        {
                            if (st.Delete(st.StType(_subjects.Fid).StTypeId))
                            {
                                if (st.Delete(_subjects.StTypeId))
                                {
                                    MessageBox.Show("删除成功");
                                }
                            }
                            
                        }
                    }
                    else if (st.StTypeList(st.StType(_subjects.Fid).Fid).Count() > 1 && st.StTypeList(_subjects.Fid).Count() == 1)
                    {
                        if (st.Delete(st.StType(_subjects.Fid).StTypeId))
                        {
                            if (st.Delete(_subjects.StTypeId))
                            {
                                MessageBox.Show("删除成功");
                            }
                        }
                    }
                    else
                    {
                        if (st.Delete(_subjects.StTypeId))
                        {
                            MessageBox.Show("删除成功");
                        }
                    }
                                                                         
                }
                else
                {
                    MessageBox.Show("该类型已被引用无法删除");
                }
            }
           
           
            if (_levelstr != null)
            {
                if (_levelstr.ReferenceType == 0)
                {
                    sl.Delete(_levelstr.StLevelId);
                    MessageBox.Show("等级删除成功");
                }
                else
                {
                    MessageBox.Show("该类型已被引用无法删除");
                }
            }
            if (_categorystr != null)
            {
                if (_categorystr.ReferenceType == 0)
                {
                    sts.Delete(_categorystr.StTypeSupplyId);
                    MessageBox.Show("类别删除成功");
                }
                else
                {
                    MessageBox.Show("该类型已被引用无法删除");
                }
            }
            RefreshListBox();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //首次进入会出现异常 用try过滤
            try
            {
                if ((Guid)listBox1.SelectedValue != Guid.Empty)
                {
                    _SystemType = (StType)listBox1.SelectedItem;
                    listBox2.DataSource = null;
                    List<StType> sd = st.StTypeList(_SystemType.StTypeId);
                    StType ins = new StType();
                    ins.StTypeId = Guid.Empty;
                    ins.Name = "全部";
                    sd.Insert(0, ins);
                    listBox2.DisplayMember = "Name";
                    listBox2.ValueMember = "StTypeId";
                    listBox2.DataSource = sd;

                    st = new StTypeRepository();
                    List<StType> sttype = new List<StType>();
                    sttype = st.StTypesNameList(Program.mf.LoginUser.UserId);
                    StType inty = new StType();
                    inty.StTypeId = Guid.Empty;
                    inty.Name = "全部";
                    sttype.Insert(0, inty);
                    listBox3.DisplayMember = "Name";
                    listBox3.ValueMember = "StTypeId";
                    listBox3.DataSource = sttype;
                }
                else
                {
                    RefreshListBox();
                }
            }
            catch
            {

            }
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Guid)listBox2.SelectedValue != Guid.Empty)
                {
                    _TypeOfWork = (StType)listBox2.SelectedItem;
                    listBox3.DataSource = null;
                    List<StType> sd = st.StTypeList(_TypeOfWork.StTypeId);
                    StType inty = new StType();
                    inty.StTypeId = Guid.Empty;
                    inty.Name = "全部";
                    sd.Insert(0, inty);
                    listBox3.DisplayMember = "Name";
                    listBox3.ValueMember = "StTypeId";
                    listBox3.DataSource = sd;
                }
                else
                {
                    st = new StTypeRepository();
                    List<StType> sttype = new List<StType>();
                    sttype = st.StTypesNameList(Program.mf.LoginUser.UserId);//Guid.Parse("6223f38c-c9c0-4ca7-a35f-8687124b3d88")
                    StType inty = new StType();
                    inty.StTypeId = Guid.Empty;
                    inty.Name = "全部";
                    sttype.Insert(0, inty);
                    listBox3.DisplayMember = "Name";
                    listBox3.ValueMember = "StTypeId";
                    listBox3.DataSource = sttype;
                }
            }
            catch
            { 
                
            }

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Guid)listBox3.SelectedValue != Guid.Empty)
                {
                    _subjects = (StType)listBox3.SelectedItem;
                }
                else
                {
                    st = new StTypeRepository();
                    List<StType> sttype = new List<StType>();
                    sttype = st.StTypesNameList(Program.mf.LoginUser.UserId);//Guid.Parse("6223f38c-c9c0-4ca7-a35f-8687124b3d88")
                    StType inty = new StType();
                    inty.StTypeId = Guid.Empty;
                    inty.Name = "全部";
                    sttype.Insert(0, inty);
                    listBox3.DisplayMember = "Name";
                    listBox3.ValueMember = "StTypeId";
                    listBox3.DataSource = sttype;
                }
            }
            catch
            { 
            
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            _levelstr = (StLevel)listBox4.SelectedItem;
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            _categorystr = (StTypeSupply)listBox5.SelectedItem;
        }
    }
}
