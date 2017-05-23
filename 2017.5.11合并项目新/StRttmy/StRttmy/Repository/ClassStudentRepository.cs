using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    class ClassStudentRepository : RepositoryBase<ClassStudent>
    {
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool Exists(Guid StudentId)
        {
            if (dbContext.ClassStudents.Any(u => u.StudentId == StudentId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // <summary>
        /// 查询班级学生
        /// </summary>
        /// <returns></returns>
        public IList<ClassStudent> students(Guid classId)
        {
            return dbContext.ClassStudents.Where(r => r.StClassId == classId).ToList<ClassStudent>();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(ClassStudent classstudents)
        {
            if (classstudents == null)
            {
                return false;
            }
            dbContext.ClassStudents.Add(classstudents);
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
}
