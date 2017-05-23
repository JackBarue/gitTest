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
    public partial class ChangePwdForm : Form
    {
        private UserBLL ub = null;

        public ChangePwdForm()
        {
            InitializeComponent();
        }

        //窗体加载
        private void ChangePwdForm_Load(object sender, EventArgs e)
        {
            ClearControl();
        }  

        //窗体激活
        private void ChangePwdForm_Activated(object sender, EventArgs e)
        {
            txtPwd.Focus();
            this.AcceptButton = btnEdit;//窗体中按下回车启动btnEdit按钮
        }

        //修改
        private void btnEdit_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }

        //修改方法
        private void ChangePassword()
        {
            if (string.IsNullOrEmpty(txtPwd.Text) || string.IsNullOrEmpty(txtNewPwd.Text) || string.IsNullOrEmpty(txtConfirmPwd.Text))
            {
                MessageBox.Show("密码不能为空值。");
                txtPwd.Focus();
                return;
            }
            if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                MessageBox.Show("新密码与确认密码不一致，请核对。");
                txtNewPwd.Focus();
                return;
            }
            UserChangePassword userChangePassword = new UserChangePassword();
            userChangePassword.Password = txtPwd.Text;
            userChangePassword.NewPassword = txtNewPwd.Text;
            userChangePassword.ConfirmPassword = txtConfirmPwd.Text;
            if (Program.mf.LoginUser != null)
            {
                ub = new UserBLL();
                if (ub.ChangePwd(userChangePassword, Program.mf.LoginUser))
                {
                    MessageBox.Show("修改密码成功，请重新登录系统。");                   
                    Program.mf.UnRegister();//调用注销方法
                    this.Close();
                }
                else
                {
                    MessageBox.Show("修改密码失败。");
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

        //清空表单信息
        private void ClearControl()
        {
            txtPwd.Clear();
            txtNewPwd.Clear();
            txtConfirmPwd.Clear();
            txtPwd.Focus();
        }
    }
}
