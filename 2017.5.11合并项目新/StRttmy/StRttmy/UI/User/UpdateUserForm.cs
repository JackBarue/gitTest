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
    public partial class UpdateUserForm : Form
    {
        private UserBLL ub = null;
        public Guid userId = Guid.Empty;//= 000-000-000-000000000000;
        public UserListForm ulf = null;
        UserType userType;
        public delegate void FreshData(string keyword);

        public UpdateUserForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void UpdateUserForm_Load(object sender, EventArgs e)
        {
            ShowUser();
        }

        //窗体激活
        private void UpdateUserForm_Activated(object sender, EventArgs e)
        {
            txtUserName.Focus();
            this.AcceptButton = btnEdit;//窗体中按下回车启动btnEdit按钮
        }

        //初始化
        private void ShowUser()
        {
            if (userId != Guid.Empty)
            {
                User oldUser = new User();
                ub = new UserBLL();
                oldUser = ub.GetUser(userId);
                userType = oldUser.UserType;
                lblLoginName.Text = oldUser.LoginName;
                label2.Text = oldUser.UserType.ToString();
                txtUserName.Text = oldUser.UserName;
                //txtPwd.Text = oldUser.Password;
                txtWorkingUnit.Text = oldUser.WorkingUnit;
            }
            else
            {
                MessageBox.Show("此用户不存在。");
                Close();
            }
        }

        //修改
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditUser();
        }

        //修改方法
        private void EditUser()
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("用户名不能为空值。");
                txtUserName.Focus();
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

            if (userId != Guid.Empty)
            {
                User newUser = new User();
                newUser.UserId = userId;
                newUser.LoginName = lblLoginName.Text;
                newUser.UserName = txtUserName.Text;
                newUser.Password = txtPwd.Text;
                newUser.WorkingUnit = txtWorkingUnit.Text;
                newUser.UserType = userType;
                newUser.CreateTime = System.DateTime.Now;
                ub = new UserBLL();
                if (ub.EditUser(newUser))
                {
                    MessageBox.Show("修改用户信息成功。");
                    if (ulf != null)
                    {
                        FreshData fd = new FreshData(ulf.ShowUserList);
                        fd("");
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("修改用户信息失败。");
                    txtUserName.Focus();
                }
            }
            else
            {
                MessageBox.Show("此用户不存在。");
                Close();
            }
        }

    }
}
