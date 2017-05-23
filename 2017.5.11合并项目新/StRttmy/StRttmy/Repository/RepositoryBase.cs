using StRttmy.Model;
using System;

namespace StRttmy.Repository
{
    public class RepositoryBase<TModel>
    {
        protected StRttmyContext dbContext;

        public RepositoryBase()
        {
            dbContext = new StRttmyContext();
        }

        /// <summary>
        /// 添加【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Add(TModel Tmodel) { return false; }

        /// <summary>
        /// 更新【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Update(TModel Tmodel) { return false; }

        /// <summary>
        /// 删除【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Delete(Guid Id) { return false; }
        public virtual bool Delete(string Name) { return false; }
        public virtual bool Delete(String media, string mp3, string txt) { return false; }


        /// <summary>
        /// 查找指定值【继承类重写后才能正常使用】
        /// </summary>
        public virtual TModel Find(Guid Id) { return default(TModel); }

        ~RepositoryBase()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
