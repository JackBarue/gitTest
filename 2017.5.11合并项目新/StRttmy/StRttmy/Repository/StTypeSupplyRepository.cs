using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    class StTypeSupplyRepository : RepositoryBase<StTypeSupply>
    {
        /// <summary>
        /// 查询类别
        /// </summary>
        /// <returns>类别名字</returns>
        public IList<string> StTypeSuppliesList()
        {
            return dbContext.StTypeSupplies.Select(r => r.StTypeName).ToList();
        }

        /// <summary>
        /// 查询类别
        /// </summary>
        /// <returns>类别名字</returns>
        public IList<StTypeSupply> StTypeSupplyList()
        {
            return dbContext.StTypeSupplies.OrderBy(n => n.CreateTime).ToList();
        }

        /// <summary>
        /// 查询类别
        /// </summary>
        /// <returns>类别名字</returns>
        public StTypeSupply StTypeSuppliesList(string name)
        {
            return dbContext.StTypeSupplies.SingleOrDefault(r => r.StTypeName == name);
        }

        /// <summary>
        /// 查询类别
        /// </summary>
        /// <returns>类别名字</returns>
        public StTypeSupply StTypeSuppliesList(Guid Id)
        {
            return dbContext.StTypeSupplies.SingleOrDefault(r => r.StTypeSupplyId == Id);
        }


        /// <summary>
        /// 依据名字查询课件类型
        /// </summary>
        /// <param name="name">课件类型</param>
        /// <returns>课件类型ID</returns>
        public StTypeSupply stTypesupplies(string name)
        {
            return (StTypeSupply)dbContext.StTypeSupplies.Where(r => r.StTypeName == name);
        }

        /// <summary>
        /// 查询全部类别
        /// </summary>
        /// <returns> 类别名字</returns>
        public List<StTypeSupply> StTypeSuppliesNameList(Guid userId)
        {
            return dbContext.StTypeSupplies.Where(r => r.UserId == userId).OrderBy(n => n.CreateTime).ToList<StTypeSupply>();
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(StTypeSupply sttypesupply)
        {
            if (sttypesupply == null)
            {
                return false;
            }
            dbContext.StTypeSupplies.Add(sttypesupply);
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
            if (dbContext.StTypeSupplies.Any(u => u.StTypeName.ToUpper() == str.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

         /// <summary>
        /// 删除类别
        /// </summary>
        /// <param name="str">类别名称</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid id)
        {
            dbContext.StTypeSupplies.Remove(dbContext.StTypeSupplies.SingleOrDefault(r => r.StTypeSupplyId == id));
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
        public override bool Update(StTypeSupply sttypesupply)
        {
            dbContext.StTypeSupplies.Attach(sttypesupply);
            dbContext.Entry<StTypeSupply>(sttypesupply).State = System.Data.EntityState.Modified;
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
