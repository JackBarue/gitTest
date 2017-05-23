using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Repository;
using StRttmy.Common;
using StRttmy.Model;
using System.Windows.Forms;
using StRttmy.UI;
using System.Data;

namespace StRttmy.BLL
{
    public class UserBLL
    {
        private UserRepository userRsy; 

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public bool Login(UserLogin login, out User loginUser)
        {
            //if (!ShareClassSec.ChkSec())
            //{
            //    //MessageBox.Show("加密狗出错！");
            //    loginUser = null;
            //    return false;
            //}
            //else
            //{
                //验证账号密码
            StudentRepository sr = new StudentRepository();
                userRsy = new UserRepository();
                //User curUser = new User();
                if (login.LoginName.Equals("super"))
                {
                    //对超级管理员用户名进行加密
                    login.LoginName = SuperNameUtility.encodeSuperName(login.LoginName);
                }
                if (userRsy.Authentication(login.LoginName, Common.TextUtility.Sha256(login.Password), out loginUser) == 0)
                {
                    return true;
                }
                else
                {                   
                    return false;
                }
            //}
        }

        public bool sLogin(UserLogin login, out Student loginUser)
        {
            StudentRepository sr = new StudentRepository();
            userRsy = new UserRepository();
;
            if (sr.Authentication(login.LoginName, Common.TextUtility.Sha256(login.Password), out loginUser) == 0)
            {
                return true;
            }
            else
            {              
                return false;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userChangePassword"></param>
        /// <param name="LoginUser"></param>
        /// <returns></returns>
        public bool ChangePwd(UserChangePassword userChangePassword, User loginUser)
        {
            userRsy = new UserRepository();
            User curUser = new User();
            if (userRsy.Authentication(loginUser.LoginName, Common.TextUtility.Sha256(userChangePassword.Password), out curUser) == 0)
            {
                if (curUser == null)
                {
                    return false;
                }
                curUser.Password = Common.TextUtility.Sha256(userChangePassword.NewPassword);
                if (userRsy.Update(curUser))
                {
                    //MessageBox.Show("成功修改密码！请重新登录系统。");
                    return true;
                }
                else
                {
                    //MessageBox.Show("修改密码失败！修改密码时，更新数据失败。");
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("原密码不正确，请重新输入。");
                return false;
            }

        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool ChangeUserInfo(User user, string loginName)
        {
            userRsy = new UserRepository();
            User curUser = new User();
            if (userRsy.Authentication(loginName, Common.TextUtility.Sha256(user.Password), out curUser) == 0)
            {
                curUser.UserName = user.UserName;
                //curUser.Gender = user.Gender;
                curUser.WorkingUnit = user.WorkingUnit;
                //curUser.Telephone = user.Telephone;
                //curUser.TelDisplay = user.TelDisplay;

                if (userRsy.Update(curUser))
                {
                    //MessageBox.Show("修改资料成功，请重新登录系统。");
                    return true;
                }
                else
                {
                    //MessageBox.Show("修改资料失败。");
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("密码输入错误，修改资料失败。");
                return false;

            }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<User> UserList(string keyword)
        {
            userRsy = new UserRepository();
            IList<User> users = null;//用户列表
            users = userRsy.TeacherList(keyword);
            return users;
        }

        public bool AddUser(User user)
        {
            userRsy = new UserRepository();
            if (userRsy.Add(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ExistsLoginName(string loginName)
        {
            userRsy = new UserRepository();
            if (userRsy.Exists(loginName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditUser(User user)
        {
            userRsy = new UserRepository();
            var _user = userRsy.Find(user.UserId);
            if (_user == null)
            {
                return false;
            }
            else
            {
                _user.UserName = user.UserName;
                _user.Password = Common.TextUtility.Sha256(user.Password);
                _user.WorkingUnit = user.WorkingUnit;
                _user.CreateTime = System.DateTime.Now;

                if (userRsy.Update(_user))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public User GetUser(Guid userId)
        {
            User user = null;
            userRsy = new UserRepository();
            user = userRsy.Find(userId);
            return user;
 
        }

        public bool DelUser(Guid userId)
        {
            userRsy = new UserRepository();
            if (userRsy.Delete(userId))
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
    }
}
