using System;
using System.Collections.Generic;
using System.Linq;
using StRttmy.Model;
using System.Linq.Expressions;
using System.Reflection;

namespace StRttmy.Repository
{
    public class ResourceRepository : RepositoryBase<Resource>
    {
        /// <summary>
        /// 自定义素材列表（按关键查找）
        /// </summary>
        /// <returns>返回符合条件的素材</returns>
        public IList<Resource> CusResourceList(string keyword, Guid userId, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype, ResourceLevel? level)
        {
            List<Resource> resources = new List<Resource>();
            var queryable = dbContext.Resources.AsQueryable();
            if (userId != Guid.Empty)
            {
                queryable = queryable.Where(r => r.UserId == userId);
            }
            if (level != null)
            {
                queryable = queryable.Where(r => r.Level == level);
            }
            if (category != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StTypeSupplyId == category);
            }
            if (stlevel != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StLevelId == stlevel);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Title.Contains(str)));
                }
            }
          
            if (subjects != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StTypeId == subjects);
            }
            else
            {
                if (typeofwork != Guid.Empty)
                {
                    queryable = queryable.Where(r => r.StType.Fid == typeofwork);
                }
                else
                {
                    if (systemtype != Guid.Empty)
                    {

                        List<StType> er = dbContext.StTypes.Where(r => r.Fid == systemtype).ToList();
                        foreach (StType a in er)
                        {
                            IList<Resource> coursewares1 = queryable.Where(r => r.StType.Fid == a.StTypeId).ToList<Resource>();
                            foreach (Resource b in coursewares1)
                            {
                                resources.Add(b);
                            }
                        }
                        return resources;
                    }
                }
            }
            resources = queryable.ToList<Resource>(); 
            return resources;
        }

        /// <summary>
        /// 自定义素材列表（按素材类型和关键字查找）
        /// </summary>
        /// <param name="resourceType">素材类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的素材</returns>
        public IList<Resource> CusResourceList(int resourceType, string keyword, Guid userId, ResourceLevel level)
        {
            var queryable = dbContext.Resources.AsQueryable();             
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                  //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Title.Contains(str)) && (r.StType == (ResourceType)resourceType && r.UserId == userId && r.Level == level));
                }
            }
            else
            {
              //  queryable = queryable.Where(r => r.StType == (ResourceType)resourceType && r.UserId == userId && r.Level == level);
            }
            IList<Resource> resources = queryable.ToList<Resource>();
            return resources;
        }

        /// <summary>
        /// 添加素材
        /// </summary>
        /// <param name="resource">素材素材</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(Resource resource)
        {
            if (resource == null)
            {
                return false;
            }
            dbContext.Resources.Add(resource);
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
        /// 查找媒体文件（按多媒体文件名及扩展名查找）
        /// </summary>
        /// <param name="contentFileName">多媒体文件名</param>
        /// <returns>素材媒体文件是否存</returns>
        public bool ContentFileNameIs(string contentFileName)
        {
            //  精确查找
            int res = dbContext.Resources.Where(r => r.ContentFile == contentFileName).Count();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找声音文件（按声音文件名及扩展名查找）
        /// </summary>
        /// <param name="soundFileName">声音文件名</param>
        /// <returns>素材声音文件是否存</returns>
        public bool SoundFileNameIs(string soundFileName)
        {
            //  精确查找
            int res = dbContext.Resources.Where(r => r.SoundFile == soundFileName).Count();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找文字文件（按文字文件名及扩展名查找）
        /// </summary>
        /// <param name="contentFileName">文字文件名</param>
        /// <returns>素材文字文件是否存</returns>
        public bool TextFileNameIs(string textFileName)
        {
            //  精确查找
            int res = dbContext.Resources.Where(r => r.TextFile == textFileName).Count();
            if (res > 0)
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
        public override bool Update(Resource resource)
        {
            dbContext.Resources.Attach(resource);
            dbContext.Entry<Resource>(resource).State = System.Data.EntityState.Modified;
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
        /// 查找素材（按素材ID查找）
        /// </summary>
        /// <param name="resourceId">素材Id</param>
        /// <returns>返回素材</returns>
        public override Resource Find(Guid resourceId)
        {
            return dbContext.Resources.SingleOrDefault(r => r.ResourceId == resourceId);
        }

        /// <summary>
        /// 查找素材（按素材媒体名字查找）
        /// </summary>
        /// <param name="resourceId">素材名字</param>
        /// <returns>返回素材</returns>
        public int FindFile(string name)
        {
            return dbContext.Resources.Where(r => r.ContentFile == name).Count();
        }

        /// <summary>
        /// 删除素材
        /// </summary>
        /// <param name="resourceId">素材ID</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid resourceId)
        {
            dbContext.Resources.Remove(dbContext.Resources.SingleOrDefault(r => r.ResourceId == resourceId));
            if (dbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// </summary>
        /// <param name="media">素材媒体文件</param>
        /// <param name="mp3">素材声音文件</param>
        /// <param name="txt">素材文字文件</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(string media, string mp3, string txt)
        {
            dbContext.Resources.Remove(dbContext.Resources.Where(r => r.ContentFile == media && r.SoundFile == mp3 && r.TextFile == txt && r.Level == 0).FirstOrDefault());//固化课件等级为0的条件删除
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
        /// 删除素材
        /// </summary>
        /// <param name="resourceId">素材ID</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(Guid resourceId, out Resource resource)
        {
            Resource rs = Find(resourceId);
            //  判断媒体文件是否被其他资源文件引用，是否被课件引用
            //  课件引用媒体文件数量，小于1才能被删除
            IQueryable<CoursewareResource> crs = dbContext.CoursewareResources.Where(r => r.MainUrl.Contains(rs.ContentFile) && r.Mp3Url.Contains(rs.SoundFile) && r.TextUrl.Contains(rs.TextFile));
            int crsCount = crs.Count();
            //  资源引用媒体文件数量，等于1才能被删除
            IQueryable<Resource> res = dbContext.Resources.Where(r => r.ContentFile == rs.ContentFile && r.SoundFile == rs.SoundFile && r.TextFile == rs.TextFile);
            int resCount = res.Count();

            if (crsCount > 0 || resCount > 1)
            {
                resource = null;
                return false;
            }
            else {
                dbContext.Resources.Remove(dbContext.Resources.SingleOrDefault(r => r.ResourceId == resourceId));
                if (dbContext.SaveChanges() > 0)
                {
                    resource = rs;
                    return true;
                }
                else
                {
                    resource = null;
                    return false;
                }
            }
            
        }

        /// <summary>
        /// 素材列表（按关键查找）
        /// </summary>
        /// <returns>返回符合条件的素材</returns>
        public IList<Resource> ResourceList(string keyword)
        {
            var queryable = dbContext.Resources.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Title.Contains(str)));
                }
            }
            IList<Resource> resources = queryable.ToList<Resource>();
            return resources;
        }

        /// <summary>
        /// 素材列表（按素材类型和关键字查找）
        /// </summary>
        /// <param name="resourceType">素材类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的素材</returns>
        public IList<Resource> ResourceList(string keyword, Guid systemtype,Guid typeofwork,Guid subjects,Guid category, Guid stlevel, ResourceLevel level)
        {
            Guid gy = Guid.Parse("6223f38c-c9c0-4ca7-a35f-8687124b3d88");
            List<Resource> resources = new List<Resource>();
            var queryable = dbContext.Resources.AsQueryable();
            queryable = queryable.Where(r => r.UserId == gy);
            queryable = queryable.Where(r => r.Level == level);
   
            if (category != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StTypeSupplyId == category);
            }
            if (stlevel != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StLevelId == stlevel);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Title.Contains(str)));
                }
            }

            if (subjects != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StTypeId == subjects);
            }
            else
            {
                if (typeofwork != Guid.Empty)
                {
                    queryable = queryable.Where(r => r.StType.Fid == typeofwork);
                }
                else
                {
                    if (systemtype != Guid.Empty)
                    {

                        List<StType> er = dbContext.StTypes.Where(r => r.Fid == systemtype).ToList();
                        foreach (StType a in er)
                        {
                            IList<Resource> coursewares1 = queryable.Where(r => r.StType.Fid == a.StTypeId).ToList<Resource>();
                            foreach (Resource b in coursewares1)
                            {
                                resources.Add(b);
                            }
                        }
                        return resources;
                    }
                }
            }
            resources = queryable.ToList<Resource>();
            return resources;
        }

        public IList<ResourceListClass> ResourceListShow(string keyword, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype, ResourceLevel level)
        {
            string sql = "";
            string sqlwhere = "";
            Guid gy = Guid.Parse("6223f38c-c9c0-4ca7-a35f-8687124b3d88");
            List<ResourceListClass> resources = new List<ResourceListClass>();
            if (systemtype != Guid.Empty)
            {
                sqlwhere = string.Format(@"and a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE Fid IN(
                    SELECT StTypeId FROM StTypes WHERE StTypeId=N'{0}' OR Fid=N'{0}')))", systemtype);
            }

            if (typeofwork != Guid.Empty)
            {
                sqlwhere = string.Format(@"and a.StTypeId IN(SELECT StTypeId FROM StTypes WHERE StTypeId IN(
                    SELECT StTypeId FROM StTypes WHERE Fid=N'{0}'))", typeofwork);
            }

            if (subjects != Guid.Empty)
            {
                sqlwhere = string.Format(@"and a.StTypeId =N'{0}'", subjects);
            }

            if (category != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlwhere))
                {
                    sqlwhere = sqlwhere + string.Format(@"and a.StTypeSupplyId=N'{0}'", category);
                }
                else
                {
                    sqlwhere = string.Format(@" a.StTypeSupplyId=N'{0}'", category);
                }
            }
            if (stlevel != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(sqlwhere))
                {
                    sqlwhere = sqlwhere + string.Format(@" and a.StLevelId=N'{0}'", stlevel);
                }
                else
                {
                    sqlwhere = string.Format(@" a.StLevelId=N'{0}'", stlevel);
                }
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                if (!string.IsNullOrEmpty(sqlwhere))
                {
                    sqlwhere = sqlwhere + string.Format(@" and (a.Keyword like N'%{0}%' or a.Title like N'%{0}%')", keyword);
                }
                else
                {
                    sqlwhere = string.Format(@" and (a.Keyword like N'%{0}%' or a.Title like N'%{0}%')", keyword);
                }
            }
            sql = string.Format(@"select d.Name+'-'+c.Name+'-'+b.Name+'-'+e.StTypeName+'-'+f.StLevelName as ResourceStyle,a.* from Resources a
            left join StTypes b
            on a.StTypeId=b.StTypeId
            left join StTypes c
            on c.StTypeId=b.Fid
            left join StTypes d
            on d.StTypeId=c.Fid
            left join StTypeSupplies e
            on a.StTypeSupplyId=e.StTypeSupplyId
            left join StLevels f
            on a.StLevelId=f.StLevelId where a.Level={0} and a.UserId=N'{1}' {2}", (int)level, gy, sqlwhere);
            resources = dbContext.Database.SqlQuery<ResourceListClass>(sql).ToList();
            return resources;
        }

        //判断是否引用素材
        public bool CheckFileDelete(string FilePath, string FileType)
        {
            bool IsDel = false;
            IQueryable<Resource> res = null;
            if (FileType == "Content")
                res = dbContext.Resources.Where(r => r.ContentFile == FilePath);
            if (FileType == "Sound")
                res = dbContext.Resources.Where(r => r.SoundFile == FilePath);
            if (FileType == "Text")
                res = dbContext.Resources.Where(r => r.TextFile == FilePath);
            int resCount = res.Count();
            if (resCount > 0)
                IsDel = false;
            else
                IsDel = true;
            return IsDel;
        }

        /// <summary>
        /// 依据创建时间查询
        /// </summary>
        /// <param name="star">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public IList<Resource> ResourceAll(DateTime star, DateTime end)
        {
            return dbContext.Resources.Where(r => r.CreateTime >= star && r.CreateTime <= end).ToList<Resource>();
        }
    }
}
