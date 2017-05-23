using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.Model;
using StRttmy.BLL;

namespace StRttmy.UI
{
    public partial class AddUserForm : Form
    {
        private UserBLL ub = null;
        public UserListForm ulf = null;
        public delegate void FreshData(string keyword);

        public AddUserForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void AddUserForm_Load(object sender, EventArgs e)
        {
            ClearControl();
            IList<UserSelectItem> typeSelectList = UserSelectItem.TypeSelectList;
            cmbType.DataSource = typeSelectList;
            cmbType.DisplayMember = "Text";
            cmbType.ValueMember = "Value";
        }

        //窗体激活
        private void AddUserForm_Activated(object sender, EventArgs e)
        {
            txtUserName.Focus();
            this.AcceptButton = btnAdd;//窗体中按下回车启动btnAdd按钮
        }

        //新建
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUser();
        }

        //新建方法
        private void AddUser()
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("用户名不能为空值。");
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtLoginName.Text))
            {
                MessageBox.Show("登录名不能为空值。");
                txtLoginName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPwd.Text))
            {
                MessageBox.Show("密码不能为空值。");
                txtPwd.Focus();
                return;
            }            
            if (string.IsNullOrEmpty(txtWorkingUnit.Text))
            {
                MessageBox.Show("工作单位不能为空值。");
                txtWorkingUnit.Focus();
                return;
            }

            ub = new UserBLL();
            if (ub.ExistsLoginName(txtLoginName.Text))
            {
                MessageBox.Show("此登录名已经存在，请使用其他登录名。");
                txtLoginName.Focus();
                return;
            }
            User newUser = new User();
            newUser.UserId = Guid.NewGuid();
            newUser.LoginName = txtLoginName.Text;
            newUser.UserName = txtUserName.Text;
            newUser.Password = Common.TextUtility.Sha256(txtPwd.Text);
            newUser.WorkingUnit = txtWorkingUnit.Text;
            newUser.UserType = (UserType)cmbType.SelectedValue;
            newUser.CreateTime = System.DateTime.Now;
            if (ub.AddUser(newUser))
            {
                ClearControl();
                if (ulf != null)
                {
                    FreshData fd = new FreshData(ulf.ShowUserList);
                    fd("");
                }
            }
            else
            {
                MessageBox.Show("新建用户失败。");
                txtUserName.Focus();
            }  
        }

        //清空表单
        private void ClearControl()
        {
            txtLoginName.Clear();
            txtUserName.Clear();
            txtPwd.Clear();
            txtWorkingUnit.Clear();
            txtUserName.Focus();
        }

    }
}
