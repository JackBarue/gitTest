using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    class StLevelRepository : RepositoryBase<StLevel>
    {
        /// <summary>
        /// 查询全部等级
        /// </summary>
        /// <returns> 等级名字</returns>
        public IList<string> StLevelsList()
        {
            return dbContext.StLevels.Select(r => r.StLevelName).ToList();
        }

        /// <summary>
        /// 查询全部等级
        /// </summary>
        /// <returns> 等级名字</returns>
        public IList<StLevel> StLevelList()
        {
            return dbContext.StLevels.OrderBy(n => n.CreateTime).ToList();
        }

        /// <summary>
        /// 查询全部等级
        /// </summary>
        /// <returns> 等级名字</returns>
        public StLevel StLevelsList(string name)
        {
            return dbContext.StLevels.SingleOrDefault(r => r.StLevelName == name);
        }

        /// <summary>
        /// 查询全部等级
        /// </summary>
        /// <returns> 等级名字</returns>
        public StLevel StLevelsList(Guid Id)
        {
            return dbContext.StLevels.SingleOrDefault(r => r.StLevelId == Id);
        }

        /// <summary>
        /// 查询全部等级
        /// </summary>
        /// <returns> 等级名字</returns>
        public List<StLevel> StLevelsNameList(Guid userId)
        {
            return dbContext.StLevels.Where(r => r.UserId == userId).OrderBy(n => n.CreateTime).ToList<StLevel>();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(StLevel stLevel)
        {
            if (stLevel == null)
            {
                return false;
            }
            dbContext.StLevels.Add(stLevel);
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
        /// 检查等级否存在
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Exists(string str)
        {
            if (dbContext.StLevels.Any(u => u.StLevelName.ToUpper() == str.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除等级
        /// </summary>
        /// <param name="str">等级</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid id)
        {
            dbContext.StLevels.Remove(dbContext.StLevels.SingleOrDefault(r => r.StLevelId == id));
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
        /// 更新素材
        /// </summary>
        /// <param name="resource">素材信息</param>
        /// <returns>是否更新成功</returns>
        public override bool Update(StLevel stlevel)
        {
            dbContext.StLevels.Attach(stlevel);
            dbContext.Entry<StLevel>(stlevel).State = System.Data.EntityState.Modified;
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
