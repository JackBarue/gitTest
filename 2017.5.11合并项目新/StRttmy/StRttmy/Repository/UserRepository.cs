using System;
using System.Linq;
using StRttmy.Model;
using System.Data;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace StRttmy.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(User user)
        {
            if (user == null)
            {
                return false;
            }
            dbContext.Users.Add(user);
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否更新成功</returns>
        public override bool Update(User user)
        {
            dbContext.Users.Attach(user);
            dbContext.Entry<User>(user).State = System.Data.EntityState.Modified;           
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }          
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="resourceId">用户ID</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid usersId)
        {
            dbContext.Users.Remove(dbContext.Users.SingleOrDefault(r => r.UserId == usersId));
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找用户，根据用户ID
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public override User Find(Guid Id)
        {
            return dbContext.Users.SingleOrDefault(u => u.UserId == Id);
        }

        /// <summary>
        /// 查找用户，根据用户登录名
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        public User Find(string loginName)
        {
            return dbContext.Users.SingleOrDefault(u => u.LoginName == loginName);
        }

        /// <summary>
        /// 检查用户登录名是否存在
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool Exists(string LoginName)
        {
            if (dbContext.Users.Any(u => u.LoginName.ToUpper() == LoginName.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户验证【0-成功；1-用户名不存在；2-密码错误】
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWrod"></param>
        /// <returns></returns>
        public int Authentication(string LoginName, string PassWrod, out User user)
        {
            var _user = dbContext.Users.SingleOrDefault(u => u.LoginName == LoginName);
            if (_user == null)
            {
                user = null;
                return 1;
            }
            else
            {
                if (_user.Password != PassWrod)
                {
                    user = null;
                    return 2;
                }
                else
                {
                    user = _user;
                    return 0;
                }
            }
        }

        /// <summary>
        /// 教师用户列表，不含管理员和超级管理员
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的用户</returns>
        public IList<User> TeacherList(string keyword)
        {
            var queryable = dbContext.Users.AsQueryable();            
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => (r.LoginName.Contains(str) || r.UserName.Contains(str) || r.WorkingUnit.Contains(str)) && (r.UserType != UserType.超级管理员 && r.UserType != UserType.管理人员 && r.UserType != UserType.系统管理员));
                }
            }
            else
            {
                queryable = queryable.Where(r => r.UserType != UserType.超级管理员 && r.UserType != UserType.管理人员 && r.UserType != UserType.系统管理员);
            }
            IList<User> users = queryable.ToList<User>();
            return users;
        }
    }
}
