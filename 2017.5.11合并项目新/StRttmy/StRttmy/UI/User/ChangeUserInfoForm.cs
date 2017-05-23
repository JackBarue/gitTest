using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StRttmy.BLL;
using StRttmy.Model;

namespace StRttmy.UI
{
    public partial class ChangeUserInfoForm : Form
    {
        private UserBLL ub = null;

        public ChangeUserInfoForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void ChangeUserInfoForm_Load(object sender, EventArgs e)
        {
            ShowUser();
        }

        //窗体激活
        private void ChangeUserInfoForm_Activated(object sender, EventArgs e)
        {
            txtPwd.Focus();
            this.AcceptButton = btnEdit;//窗体中按下回车启动btnEdit按钮
        }

        //初始化
        private void ShowUser()
        {
            if (Program.mf.LoginUser != null)
            {
                User curUser = Program.mf.LoginUser;
                if (curUser.UserType == (UserType)2)
                {
                    lblLoginName.Text = curUser.LoginName+"(加密显示)";
                }
                else {
                    lblLoginName.Text = curUser.LoginName;
                }               
                //txtPwd.Text = curUser.Password;
                txtUserName.Text = curUser.UserName;
                label2.Text = curUser.UserType.ToString();
                txtWorkingUnit.Text = curUser.WorkingUnit;
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            }
        }

        //修改
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ChangeUserInfo();                       
        }

        //修改方法
        private void ChangeUserInfo()
        {
            if (string.IsNullOrEmpty(txtPwd.Text))
            {
                MessageBox.Show("密码不能为空值。");
                txtPwd.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("用户名不能为空值。");
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtWorkingUnit.Text))
            {
                MessageBox.Show("工作单位不能为空值。");
                txtWorkingUnit.Focus();
                return;
            }

            User curUser = new User();
            curUser.LoginName = lblLoginName.Text;
            curUser.Password = txtPwd.Text;
            curUser.UserName = txtUserName.Text;
            curUser.WorkingUnit = txtWorkingUnit.Text;

            if (Program.mf.LoginUser != null)
            {
                ub = new UserBLL();
                if (ub.ChangeUserInfo(curUser, Program.mf.LoginUser.LoginName))
                {
                    MessageBox.Show("修改资料成功，请重新登录系统。");
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();                    
                }
                else
                {
                    MessageBox.Show("修改资料失败,请查询后重试。");
                    txtPwd.Focus();
                }
            }
            else
            {
                MessageBox.Show("当前登录用户失效，请重新登录系统。");
                Program.mf.UnRegister();//调用注销方法
                this.Close();
            } 
        }

    }
}
