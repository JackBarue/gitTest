using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Repository;
using StRttmy.Model;

namespace StRttmy.BLL
{
    public class CourseBLL
    {
        private CoursewareRepository courseRsy;

        /// <summary>
        /// 自定义课件列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<Courseware> CusCoursewareList(string keyword, Guid userId, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            courseRsy = new CoursewareRepository();
            IList<Courseware> Coursewares = null;        
            Coursewares = courseRsy.CusCourseList(keyword, userId, type, category, level, subjects, typeofwork, systemtype);
            return Coursewares;
        }

        public Courseware FindCourseware(Guid coursewareId)
        {
            courseRsy = new CoursewareRepository();
            return courseRsy.FindCourseware(coursewareId);
        }

        /// <summary>
        /// 课件列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<Courseware> CoursewareList(string keyword, Guid userId, Guid type, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            courseRsy = new CoursewareRepository();
            IList<Courseware> Coursewares = null;      
            Coursewares = courseRsy.CourseList(keyword, userId, type, category, level, subjects, typeofwork, systemtype);
            return Coursewares;
        }

        /// <summary>
        /// 发布课件列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<Courseware> PubCoursewareList(string keyword, int userId, int coursewareType)
        {
            courseRsy = new CoursewareRepository();
            IList<Courseware> Coursewares = null;
            //if (coursewareType == -1)
            //{
            //    Coursewares = courseRsy.CusCourseList(keyword, userId, CoursewareLevel.发布);
            //}
            //else
            //{
            //    Coursewares = courseRsy.CusCourseList(coursewareType, keyword, userId, CoursewareLevel.发布);
            //}
            return Coursewares;
        }

        public bool AddCusCourse(Courseware courseware)
        {
           // courseware.Level = CoursewareLevel.自定义;
            courseware.CreateTime = System.DateTime.Now;
            courseRsy = new CoursewareRepository();
            if (courseRsy.Add(courseware))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditCusCourse(Courseware courseware)
        {
            courseRsy = new CoursewareRepository();
            var _courseware = courseRsy.Find(courseware.CoursewareId);
            if (_courseware == null)
            {
                return false;
            }
            else
            {
                _courseware.CoursewareId = courseware.CoursewareId;
                _courseware.StLevelId = courseware.StLevelId;
                _courseware.StTypeSupplyId = courseware.StTypeSupplyId;
                _courseware.StTypeId = courseware.StTypeId;
                _courseware.Name = courseware.Name;
                _courseware.Keyword = courseware.Keyword;
                _courseware.Description = courseware.Description;
                _courseware.CreateTime = System.DateTime.Now;

                if (courseRsy.Update(_courseware))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool EditPubCourse(Courseware courseware)
        {
            courseRsy = new CoursewareRepository();
            var _courseware = courseRsy.Find(courseware.CoursewareId);
            if (_courseware == null)
            {
                return false;
            }
            else
            {
                //_courseware.Type = courseware.Type;
                _courseware.StLevelId = courseware.StLevelId;
                _courseware.StTypeSupplyId = courseware.StTypeSupplyId;
                _courseware.StTypeId = courseware.StTypeId;
                _courseware.Name = courseware.Name;
                _courseware.Keyword = courseware.Keyword;
                _courseware.Description = courseware.Description;
               // _courseware.Level = CoursewareLevel.发布;
                _courseware.CreateTime = System.DateTime.Now;

                if (courseRsy.Update(_courseware))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Courseware GetCourse(Guid coursewareId)
        {
            Courseware courseware = null;
            courseRsy = new CoursewareRepository();
            courseware = courseRsy.Find(coursewareId);
            return courseware;
        }

        public Courseware GetCourseByName(string coursewareName)
        {
            Courseware courseware = null;
            courseRsy = new CoursewareRepository();
            courseware = courseRsy.FindName(coursewareName);
            return courseware;
        }

        public bool DelCourse(Guid coursewareId)
        {
            courseRsy = new CoursewareRepository();
            if (courseRsy.Delete(coursewareId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveCourseResources(List<CoursewareResource> crs, Guid courseId)
        {
            courseRsy = new CoursewareRepository();
            Courseware courseware = new Courseware();
            courseRsy.DelCourseResource(courseId);
            courseware = courseRsy.Find(courseId);           
            courseware.coursewareResources = crs;
            if (courseRsy.Update(courseware))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PublishCusCourse(Guid coursewareId)
        {
            courseRsy = new CoursewareRepository();
            var _courseware = courseRsy.Find(coursewareId);
            if (_courseware == null)
            {
                return false;
            }
            else
            {
                _courseware.CoursewareLevelId = Guid.Parse("b6c51980-f0c2-4ce1-90d5-d8fc726e0e3b");
                _courseware.CreateTime = System.DateTime.Now;

                if (courseRsy.Update(_courseware))
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
