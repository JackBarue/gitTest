using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    public class StudentRepository : RepositoryBase<Student>
    {
        /// <summary>
        /// 查询所有学生
        /// </summary>
        /// <returns></returns>
        public IList<Student> studentsList() 
        {
            return dbContext.Students.AsQueryable().ToList<Student>();
        }
        // <summary>
        /// 查询单个学生
        /// </summary>
        /// <returns></returns>
        public Student students( Guid studentId)
        {
            return dbContext.Students.SingleOrDefault(r => r.StudentId == studentId);
        }

        /// <summary>
        /// 检查用户登录名是否存在
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool Exists(int worknumber)
        {
            if (dbContext.Students.Any(u => u.WorkNumber == worknumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(Student user)
        {
            if (user == null)
            {
                return false;
            }
            dbContext.Students.Add(user);
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
            dbContext.Students.Remove(dbContext.Students.SingleOrDefault(r => r.StudentId == usersId));
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
        public override bool Update(Student user)
        {
            var _student = students(user.StudentId);
            if (_student == null)
            {
                return false;
            }
            else
            {
                _student.LoginName = user.LoginName;
                _student.Password = user.Password;
                _student.Name = user.Name;
                _student.WorkNumber = user.WorkNumber;
                _student.Telephone = user.Telephone;
                _student.WorkingUnit = user.WorkingUnit;

                dbContext.Students.Attach(_student);
                dbContext.Entry<Student>(_student).State = System.Data.EntityState.Modified;
                if (dbContext.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Guid FindStudentId(int worknumber)
        {
            return dbContext.Students.SingleOrDefault(r => r.WorkNumber == worknumber).StudentId;
        }

        /// <summary>
        /// 查询所有学生
        /// </summary>
        /// <returns></returns>
        public IList<Student> FindStudentsList( string keyword)
        {
            return dbContext.Students.Where(r => r.LoginName.Contains(keyword) || r.WorkingUnit.Contains(keyword) || r.Telephone.Contains(keyword) || r.Name.Contains(keyword)).ToList<Student>();
        }

        /// <summary>
        /// 用户验证【0-成功；1-用户名不存在；2-密码错误】
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWrod"></param>
        /// <returns></returns>
        public int Authentication(string LoginName, string PassWrod, out Student user)
        {
            var _user = dbContext.Students.SingleOrDefault(u => u.LoginName == LoginName);
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
    }
}
