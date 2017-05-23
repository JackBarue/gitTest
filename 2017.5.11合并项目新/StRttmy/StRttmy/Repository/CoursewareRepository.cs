using System;
using System.Linq;
using StRttmy.Model;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace StRttmy.Repository
{
    public class CoursewareRepository : RepositoryBase<Courseware>
    {

        public IList<Courseware> CoursewareAll(DateTime star ,DateTime end)
        {
            return dbContext.Coursewares.Where(r => r.CreateTime >= star && r.CreateTime <= end).ToList<Courseware>();
        }


        public Courseware FindFront(Guid coursewareId)
        {
            return dbContext.Coursewares.Include("User").Include("coursewareCommentes").SingleOrDefault(r => r.CoursewareId == coursewareId);
        }

        public Courseware FindCourseware(Guid coursewareId)
        {
            return dbContext.Coursewares.SingleOrDefault(r => r.CoursewareId == coursewareId);
        }

        /// <summary>
        /// 查找课件（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的资源</returns>
        public Courseware Find(string keyword)
        {
            //  精确查找
            return dbContext.Coursewares.SingleOrDefault(r => r.Keyword == keyword);
        }

        /// <summary>
        /// 查找课件（按课件名称查找）
        /// </summary>
        /// <param name="coursewareName">课件名称</param>
        /// <returns>返回符合条件的资源</returns>
        public Courseware FindName(string coursewareName)
        {
            //  精确查找
            return dbContext.Coursewares.SingleOrDefault(r => r.Name == coursewareName);
        }

        #region 搜索课件

        #region 搜索全部基本课件

        /// <summary>
        /// 搜索全部基本课件
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有基本课件</returns>
        public IQueryable<Courseware> SearchBasic(string keyword)
        {
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    return dbContext.Coursewares.Where(r => (r.Keyword.Contains(keyword) || r.Name.Contains(keyword) || r.Description.Contains(keyword)) && (r.Level == CoursewareLevel.基本));
            //}
            //else
            //{
            //    return dbContext.Coursewares.Where(r => r.Level == CoursewareLevel.基本);
            //}
            return null;
        }

        /// <summary>
        /// 搜索全部基本课件（按课件类型、课件名称或关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的所有基本课件</returns>
        public IQueryable<Courseware> SearchBasic(int coursewareType, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
               // return dbContext.Coursewares.Where(r => (r.Keyword.Contains(keyword) || r.Name.Contains(keyword) || r.Description.Contains(keyword)) && (r.Type == (CoursewareType)coursewareType && r.Level == CoursewareLevel.基本));
            }
            else
            {
                //return dbContext.Coursewares.Where(r => r.Type == (CoursewareType)coursewareType && r.Level == CoursewareLevel.基本);
            }
            return null;
        }

        #endregion

        #region 搜索全部课件

        /// <summary>
        /// 搜索全部课件
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有课件</returns>
        public IQueryable<Courseware> Search(string keyword)
        {
            var queryable = dbContext.Coursewares.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str));
                }
            }
            return queryable;
        }

        /// <summary>
        /// 搜索全部课件（按课件类型、课件名称或关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的资源集</returns>
        public IQueryable<Courseware> Search(int coursewareType, string keyword)
        {
            var queryable = dbContext.Coursewares.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    //queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType));
                }
            }
            else
            {
                //queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType);
            }
            return queryable;
        }

        #endregion

        #endregion

        #region 基本课件

        /// <summary>
        /// 课件列表（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有课件</returns>
        public IQueryable<Courseware> List(string keyword, CoursewareLevel level)
        {
            var queryable = dbContext.Coursewares.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                   // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level == level));
                }
            }
            else
            {
              //  queryable = queryable.Where(r => r.Level == level);
            }
            return queryable;
        }

        /// <summary>
        /// 课件列表（按课件类型和关键字查找）
        /// </summary>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回资源类型的所有课件</returns>
        public IQueryable<Courseware> List(int coursewareType, string keyword, CoursewareLevel level)
        {
            var queryable = dbContext.Coursewares.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                  //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.Level == level));
                }
            }
            else
            {
               // queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.Level == level);
            }
            return queryable;
        }

        #endregion

        #region 自定义课件

        /// <summary>
        /// 课件列表（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有自定义课件</returns>
        public IQueryable<Courseware> ListCustomized(string keyword, int userId, UserType userType, CoursewareLevel level)
        {
            if (userType == UserType.教师)
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                       // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.UserId == userId && r.Level == level));
                    }
                }
                else
                {
                   // queryable = queryable.Where(r => r.UserId == userId && r.Level == level);
                }
                return queryable;
            }
            else
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                       // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level == level));
                    }
                }
                else
                {
                    //queryable = queryable.Where(r => r.Level == level);
                }
                return queryable;
            }
        }

        /// <summary>
        /// 课件列表（按课件类型和关键字查找）
        /// </summary>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回资源类型的所有课件</returns>
        public IQueryable<Courseware> ListCustomized(int coursewareType, string keyword, int userId, UserType userType, CoursewareLevel level)
        {
            if (userType == UserType.教师)
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                      //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level));
                    }
                }
                else
                {
                   // queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level);
                }
                return queryable;
            }
            else
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                        //queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.Level == level));
                    }
                }
                else
                {
                    //queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.Level == level);
                }
                return queryable;
            }
        }

        #endregion

        #region 共享课件

        /// <summary>
        /// 课件列表（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有共享课件</returns>
        public IQueryable<Courseware> ListSharing(string keyword, int userId, UserType userType, CoursewareLevel level)
        {
            if (userType == UserType.教师)
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                       // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.UserId == userId && r.Level == level));
                    }
                }
                else
                {
                   // queryable = queryable.Where(r => r.UserId == userId && r.Level == level);
                }
                return queryable;
            }
            else
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                      //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level == level));
                    }
                }
                else
                {
                   // queryable = queryable.Where(r => r.Level == level);
                }
                return queryable;
            }
        }

        /// <summary>
        /// 课件列表（按课件类型和关键字查找）
        /// </summary>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回资源类型的所有课件</returns>
        public IQueryable<Courseware> ListSharing(int coursewareType, string keyword, int userId, UserType userType, CoursewareLevel level)
        {
            if (userType == UserType.教师)
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                       // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level));
                    }
                }
                else
                {
                   // queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level);
                }
                return queryable;
            }
            else
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                      //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.Level == level));
                    }
                }
                else
                {
                  //  queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.Level == level);
                }
                return queryable;
            }
        }

        #endregion

        #region 经典课件

        /// <summary>
        /// 课件列表（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有经典课件</returns>
        public IQueryable<Courseware> ListClassic(string keyword, int userId, UserType userType, CoursewareLevel level)
        {
            if (userType == UserType.教师)
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                      //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.UserId == userId && r.Level == level));
                    }
                }
                else
                {
                   // queryable = queryable.Where(r => r.UserId == userId && r.Level == level);
                }
                return queryable;
            }
            else
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                       // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level == level));
                    }
                }
                else
                {
                   /// queryable = queryable.Where(r => r.Level == level);
                }
                return queryable;
            }
        }

        /// <summary>
        /// 课件列表（按课件类型和关键字查找）
        /// </summary>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回资源类型的所有课件</returns>
        public IQueryable<Courseware> ListClassic(int coursewareType, string keyword, int userId, UserType userType, CoursewareLevel level)
        {
            if (userType == UserType.教师)
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                        //queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level));
                    }
                }
                else
                {
                    //queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level);
                }
                return queryable;
            }
            else
            {
                var queryable = dbContext.Coursewares.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    char[] splitChar = { ' ' };
                    string[] strArr = keyword.Split(splitChar);
                    foreach (string str in strArr)
                    {
                      //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.Level == level));
                    }
                }
                else
                {
                    //queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.Level == level);
                }
                return queryable;
            }
        }

        #endregion

        #region 获取资源和查询

        public IQueryable<Resource> ListResource(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return dbContext.Resources.Where(r => r.Keyword.Contains(keyword));
            }
            else
            {
                return dbContext.Resources;
            }
        }

        public IQueryable<Resource> ListResource(int resourceType, string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
               // return dbContext.Resources.Where(r => r.Keyword.Contains(keyword) && r.Type == (ResourceType)resourceType);
            }
            else
            {
               // return dbContext.Resources.Where(r => r.Type == (ResourceType)resourceType);
            }
            return null;
        }

        #endregion

        #region 课件评论

        public IQueryable<Courseware> ListCoursewareComment(string keyword)
        {
            var queryable = dbContext.Coursewares.Include("coursewareCommentes").AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                  //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level != CoursewareLevel.基本));
                }
            }
            else
            {
              //  queryable = queryable.Where(r => r.Level != CoursewareLevel.基本);
            }
            return queryable;
        }

        public IQueryable<Courseware> ListCoursewareComment(int coursewareType, string keyword)
        {
            var queryable = dbContext.Coursewares.Include("coursewareCommentes").AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                  //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.Level != CoursewareLevel.基本));
                }
            }
            else
            {
                //queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.Level != CoursewareLevel.基本);
            }
            return queryable;
        }

        #endregion

        #region 前台

        #region 搜索课件

        /// <summary>
        /// 搜索课件
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有课件</returns>
        public IQueryable<Courseware> SearchFront(string keyword, CoursewareLevel level)
        {
            var queryable = dbContext.Coursewares.Include("coursewareCommentes").Include("User").AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                  //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level == level));
                }
            }
            else
            {
               // queryable = queryable.Where(r => r.Level == level);
            }
            return queryable;
        }

        /// <summary>
        /// 搜索课件（按课件类型、课件名称或关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回符合条件的资源集</returns>
        public IQueryable<Courseware> SearchFront(int coursewareType, string keyword, CoursewareLevel level)
        {
            var queryable = dbContext.Coursewares.Include("coursewareCommentes").Include("User").AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                   // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.Level == level));
                }
            }
            else
            {
                //queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.Level == level);
            }
            return queryable;
        }

        #endregion

        #endregion


        ///////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 自定义课件列表（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有自定义课件</returns>
        public IList<Courseware> CusCourseList(string keyword, Guid userId, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            List<Courseware> coursewares = new List<Courseware>();
            var queryable = dbContext.Coursewares.AsQueryable();

            if (userId != Guid.Empty)
            {
                queryable = queryable.Where(r => r.UserId == userId);
            }
            if (type != Guid.Empty)
            {
                queryable = queryable.Where(r => r.CoursewareLevelId == type);
            }
            if (category != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StTypeSupplyId == category);
            }
            if (level != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StLevelId == level);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)));
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
                            IList<Courseware> coursewares1 = queryable.Where(r => r.StType.Fid == a.StTypeId).ToList<Courseware>();
                            foreach (Courseware b in coursewares1)
                            {
                                coursewares.Add(b);
                            }
                        }
                        return coursewares;
                    }
                }
            }
           
        
            coursewares = queryable.ToList<Courseware>();
            return coursewares;            
        }

      
        /// <summary>
        /// 自定义课件列表（按课件类型和关键字查找）
        /// </summary>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回资源类型的所有课件</returns>
        public IList<Courseware> CusCourseList(int coursewareType, string keyword, int userId, CoursewareLevel level)
        {
            var queryable = dbContext.Coursewares.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                  //  queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level));
                }
            }
            else
            {
              //  queryable = queryable.Where(r => r.Type == (CoursewareType)coursewareType && r.UserId == userId && r.Level == level);
            }
            IList<Courseware> coursewares = queryable.ToList<Courseware>();
            return coursewares;            
        }

        /// <summary>
        /// 课件列表（按关键字查找）
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回所有课件</returns>
        public IList<Courseware> CourseList(string keyword)
        {
            var queryable = dbContext.Coursewares.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                   // queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)) && (r.Level != CoursewareLevel.自定义));
                }
            }
            else
            {
               // queryable = queryable.Where(r => r.Level != CoursewareLevel.自定义);
            }
            IList<Courseware> coursewares = queryable.ToList<Courseware>();
            return coursewares;
        }


     

      

        /// <summary>
        /// 课件列表（按课件类型和关键字查找）
        /// </summary>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回课件类型的所有课件</returns>
        public IList<Courseware> CourseList(string keyword, Guid userId, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            Guid basis = Guid.Parse("fa140227-b9f9-4ae0-a7f1-0327ecd69e0d");
            Guid Shared = Guid.Parse("b6c51980-f0c2-4ce1-90d5-d8fc726e0e3b");
            Guid job = Guid.Parse("531fcc0c-933f-402f-9eb7-1de6c2eb30e2");
            List<Courseware> coursewares = new List<Courseware>();
            var queryable = dbContext.Coursewares.AsQueryable();

            if (userId != Guid.Empty)
            {
                queryable = queryable.Where(r => r.UserId == userId);
            }
            if (type != Guid.Empty)
            {
                queryable = queryable.Where(r => r.CoursewareLevelId == type);
            }
            else
            {
                queryable = queryable.Where(r => r.CoursewareLevelId == basis || r.CoursewareLevelId == Shared || r.CoursewareLevelId == job);
            }
            if (category != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StTypeSupplyId == category);
            }
            if (level != Guid.Empty)
            {
                queryable = queryable.Where(r => r.StLevelId == level);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                char[] splitChar = { ' ' };
                string[] strArr = keyword.Split(splitChar);
                foreach (string str in strArr)
                {
                    queryable = queryable.Where(r => (r.Keyword.Contains(str) || r.Name.Contains(str) || r.Description.Contains(str)));
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
                            IList<Courseware> coursewares1 = queryable.Where(r => r.StType.Fid == a.StTypeId).ToList<Courseware>();
                            foreach (Courseware b in coursewares1)
                            {
                                coursewares.Add(b);
                            }
                        }
                        return coursewares;
                    }
                }
            }


            coursewares = queryable.ToList<Courseware>();
            return coursewares;
        }

        /// <summary>
        /// 添加课件
        /// </summary>
        /// <param name="courseware">课件信息</param>
        /// <returns>是否添加成功</returns>
        public override bool Add(Courseware courseware)
        {
            if (courseware == null)
            {
                return false;
            }
            dbContext.Coursewares.Add(courseware);

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
        /// 更新课件
        /// </summary>
        /// <param name="courseware">课件信息</param>
        /// <returns>是否更新成功</returns>
        public override bool Update(Courseware courseware)
        {
            dbContext.Coursewares.Attach(courseware);
            dbContext.Entry<Courseware>(courseware).State = System.Data.EntityState.Modified;
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
        /// 查找课件（按课件ID查找）
        /// </summary>
        /// <param name="coursewareId">课件Id</param>
        /// <returns>返回课件</returns>
        public override Courseware Find(Guid coursewareId)
        {
            return dbContext.Coursewares.SingleOrDefault(r => r.CoursewareId == coursewareId);
        }

        /// <summary>
        /// 删除课件
        /// </summary>
        /// <param name="coursewareId">课件ID</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(Guid coursewareId)
        {
            dbContext.Coursewares.Remove(dbContext.Coursewares.SingleOrDefault(r => r.CoursewareId == coursewareId));
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
        /// 删除课件
        /// </summary>
        /// <param name="coursewareName">课件名字</param>
        /// <param name="level">课件等级</param>
        /// <returns>是否删除成功</returns>
        public override bool Delete(string coursewareName)
        {
            Guid level = Guid.Parse("fa140227-b9f9-4ae0-a7f1-0327ecd69e0d");
            dbContext.Coursewares.Remove(dbContext.Coursewares.Where(r => r.Name == coursewareName && r.CoursewareLevelId == level).FirstOrDefault());//固化课件等级为基本课件
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
        /// 删除课件所属的课件资源
        /// </summary>
        /// <param name="coursewareId">课件ID</param>
        /// <returns>是否删除成功</returns>
        public int DelCourseResource(Guid coursewareId)
        {
            return dbContext.Database.ExecuteSqlCommand("DELETE FROM CoursewareResources where CoursewareId='"+coursewareId+"'");
        }
    }
}
