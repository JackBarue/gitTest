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

namespace StRttmy.UI
{
    public delegate void DelDataGridView(User u);
    public partial class LoginForm : Form
    {
        private UserBLL ub = null;
        private DelDataGridView _deldgw;
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(DelDataGridView deldgw)
        {
            InitializeComponent();
            this._deldgw = deldgw;
        }
     

        //登录确认
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }      
        
        private void Login()
        {
            
            if (string.IsNullOrEmpty(txtLoginName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("登录名或密码不能为空值。");
                txtLoginName.Focus();
                return;
            }
            UserLogin login = new UserLogin();
            login.LoginName = txtLoginName.Text;
            login.Password = txtPassword.Text;

            ub = new UserBLL();
            User curUser = new User();
            Student curstudent = new Student();
            if (ub.Login(login, out curUser))
            {
                _deldgw(curUser);//委托返回给主窗体需要的curUser
                Program.mf.Text = curUser.UserName + "，欢迎使用---轨道交通多媒体课件资源编制管理系统";
                this.Close();
            }
            else
            {

                if (ub.sLogin(login, out curstudent))
                {
                    User curUser1 = new User();
                    curUser1.UserId = curstudent.StudentId;
                    curUser1.UserName = curstudent.Name;
                    curUser1.LoginName = curstudent.LoginName;
                    curUser1.Password = curstudent.Password;
                    curUser1.UserType = UserType.学员;
                    _deldgw(curUser1);//委托返回给主窗体需要的curUser                    
                    Program.mf.Text = curUser1.UserName + "，欢迎使用---轨道交通多媒体课件资源编制管理系统";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("登录失败！");
                    txtLoginName.Focus();//登录名输入框获得焦点                   
                }
               
            }
        }

        //窗体加载
        //private void LoginForm_Load(object sender, EventArgs e)
        //{
        //    txtLoginName.Focus();//登录名输入框获得焦点
        //    this.AcceptButton = btnLogin;////登录窗体中按下回车启动btnLogin按钮
        //}

        //窗体激活
        private void LoginForm_Activated(object sender, EventArgs e)
        {
            txtLoginName.Focus();//登录名输入框获得焦点
            this.AcceptButton = btnLogin;//窗体中按下回车启动btnLogin按钮
        }
    }
}
