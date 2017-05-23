using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    class StClassRepository : RepositoryBase<StClass>
    {
        /// <summary>
        /// 查询所有学生
        /// </summary>
        /// <returns></returns>
        public IList<StClass> ClassList()
        {
            return dbContext.StClasses.AsQueryable().ToList<StClass>();
        }

        public StClass ClassNumber(string name)
        {
            return dbContext.StClasses.SingleOrDefault(r => r.ClassName == name);
        }

        public StClass ClassContent(Guid classId)
        {
            return dbContext.StClasses.SingleOrDefault(r => r.StClassId == classId);
        }

        /// <summary>
        /// 检查班级名是否存在
        /// </summary>
        /// <param name="stclass"></param>
        /// <returns></returns>
        public bool Exists(string stclass)
        {
            if (dbContext.StClasses.Any(u => u.ClassName == stclass))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="stclass">班级信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(StClass stclass)
        {
            if (stclass == null)
            {
                return false;
            }
            dbContext.StClasses.Add(stclass);
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
        /// 更新班级状态信息
        /// </summary>
        /// <param name="user">班级Id</param>
        /// <returns>是否更新成功</returns>
        public  bool UpdateStype(Guid classId)
        {
            var _class = ClassContent(classId);
            if (_class == null)
            {
                return false;
            }
            else
            {
             
                dbContext.StClasses.Attach(_class);
                dbContext.Entry<StClass>(_class).State = System.Data.EntityState.Modified;
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
}
