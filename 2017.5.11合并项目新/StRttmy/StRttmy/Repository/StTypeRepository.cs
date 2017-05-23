using StRttmy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Repository
{
    public class StTypeRepository : RepositoryBase<StType>
    {
        /// <summary>
        /// 依据父ID 查询关联类型
        /// </summary>
        /// <param name="Sid">父ID</param>
        /// <returns>关联类型集合</returns>
        public List<StType> StTypeList(Guid Sid)
        {
            return dbContext.StTypes.Where(r => r.Fid == Sid).OrderBy(n => n.CreateTime).ToList();
        }


        /// <summary>
        /// 依据名字
        /// </summary>
        /// <param name="Sid">名字</param>
        /// <returns>关联类型集合</returns>
        public StType StType(String name)
        {
            if (name != "全部")
            {
                return dbContext.StTypes.SingleOrDefault(r => r.Name == name);
            }
            return null;
        }

        /// <summary>
        /// 依据Id
        /// </summary>
        /// <param name="Sid">Fid</param>
        /// <returns>关联类型集合</returns>
        public StType StType(Guid Id)
        {
            return dbContext.StTypes.SingleOrDefault(r => r.StTypeId == Id);
        }

        /// <summary>
        /// 查询全部类别
        /// </summary>
        /// <returns> 类别名字</returns>
        public List<StType> StTypesNameList(Guid userId)
        {
            return dbContext.StTypes.Where(r => r.UserId == userId).OrderBy(n => n.CreateTime).ToList();   
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(StType sttype)
        {
            if (sttype == null)
            {
                return false;
            }
            dbContext.StTypes.Add(sttype);
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
        public override bool Update(StType sttype)
        {
            dbContext.StTypes.Attach(sttype);
            dbContext.Entry<StType>(sttype).State = System.Data.EntityState.Modified;
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
        /// 检查类型否存在
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public bool Exists(string str)
        {
            if (dbContext.StTypes.Any(u => u.Name.ToUpper() == str.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="str">类型</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid id)
        {
            dbContext.StTypes.Remove(dbContext.StTypes.SingleOrDefault(r => r.StTypeId == id));
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
