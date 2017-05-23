using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    class CoursewareLevelRepository : RepositoryBase<CoursewareLevel>
    {

        /// <summary>
        /// 查询全部课件类型
        /// </summary>
        /// <returns> 全部课件类型名字</returns>
        public IList<string> CoursewareLevelList()
        {
            return dbContext.CoursewareLevels.Select(r => r.Name).ToList();
        }
        /// <summary>
        /// 查询全部课件类型
        /// </summary>
        /// <returns> 全部课件类型名字</returns>
        public IList<CoursewareLevel> CoursewareLevelsList()
        {
            return dbContext.CoursewareLevels.ToList();
        }

        /// <summary>
        /// 依据名字查询课件类型ID
        /// </summary>
        /// <param name="name">课件名字</param>
        /// <returns>课件类型ID</returns>
        public CoursewareLevel coursewarelevel(string name)
        {
            return dbContext.CoursewareLevels.SingleOrDefault(r => r.Name == name);
        }

        /// <summary>
        /// 依据名字查询课件类型ID
        /// </summary>
        /// <param name="name">课件名字</param>
        /// <returns>课件类型ID</returns>
        public CoursewareLevel coursewarelevel(Guid id)
        {
            return dbContext.CoursewareLevels.SingleOrDefault(r => r.CoursewareLevelId == id);
        }
    }
}
