using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    public class TeachingRepository : RepositoryBase<Teaching>
    {
        /// <summary>
        /// 添加教学
        /// </summary>
        /// <param name="user">教学信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(Teaching teach)
        {
            if (teach == null)
            {
                return false;
            }
            dbContext.Teaching.Add(teach);
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
        /// 时间段内的教学情况
        /// </summary>
        /// <param name="star">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public IList<Teaching> TeachingAll(DateTime star, DateTime end)
        {
            return dbContext.Teaching.Where(r => r.Endtime >= star && r.Endtime <= end).ToList<Teaching>();
        }
    }
}
