using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Repository;
using StRttmy.Common;
using StRttmy.Model;
using StRttmy.UI;
using System.Data;

namespace StRttmy.BLL
{
    public class ResourceBLL
    {
        private ResourceRepository resRsy;

        /// <summary>
        /// 自定义素材列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<Resource> CusResourceList(string keyword, Guid userId, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            resRsy = new ResourceRepository();
            IList<Resource> resources = null;
            //if (resourceType == -1)
            //{
               // resources = resRsy.CusResourceList(keyword, userId, ResourceLevel.自定义);
            //}
            //else
            //{
            resources = resRsy.CusResourceList(keyword, userId, category, level, subjects, typeofwork, systemtype, ResourceLevel.自编);
            //}
            return resources;


        }

        public bool AddCusResource(Resource resource)
        {
            resource.Level = ResourceLevel.自编;
            resource.CreateTime = System.DateTime.Now;
            resRsy = new ResourceRepository();
            if (resRsy.Add(resource))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContentFileResourceIs(string contentFileName)
        {
            resRsy = new ResourceRepository();
            if (resRsy.ContentFileNameIs(contentFileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SoundFileResourceIs(string soundFileName)
        {
            resRsy = new ResourceRepository();
            if (resRsy.SoundFileNameIs(soundFileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TextFileResourceIs(string textFileName)
        {
            resRsy = new ResourceRepository();
            if (resRsy.TextFileNameIs(textFileName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditCusResource(Resource resource)
        {
            resRsy = new ResourceRepository();
            var _resource = resRsy.Find(resource.ResourceId);
            if (_resource == null)
            {
                return false;
            }
            else
            {
                //_resource.Type = resource.Type;
                _resource.Title = resource.Title;
                _resource.Keyword = resource.Keyword;
                _resource.Level = ResourceLevel.自编;
                _resource.StTypeId = resource.StTypeId;
                _resource.StTypeSupplyId = resource.StTypeSupplyId;
                _resource.StLevelId = resource.StLevelId;
                _resource.CreateTime = System.DateTime.Now;

                if (resRsy.Update(_resource))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Resource GetResource(Guid resourceId)
        {
            Resource resource = null;
            resRsy = new ResourceRepository();
            resource = resRsy.Find(resourceId);
            return resource;
        }

        public bool DelResource(Guid resourceId, out Resource resource)
        {
            resRsy = new ResourceRepository();
            Resource rs = null;
            if (resRsy.Delete(resourceId,out rs))
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

        /// <summary>
        /// 素材列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<Resource> ResourceList(string keyword, Guid systemtype,Guid typeofwork,Guid subjects,Guid category, Guid level)
        {
            resRsy = new ResourceRepository();
            IList<Resource> resources = null;
            resources = resRsy.ResourceList(keyword, systemtype, typeofwork, subjects, category, level, ResourceLevel.基础);
            return resources;
        }

        public IList<ResourceListClass> ResourceListShow(string keyword, Guid category, Guid stlevel, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            resRsy = new ResourceRepository();
            IList<ResourceListClass> resource = null;
            resource = resRsy.ResourceListShow(keyword, category, stlevel, subjects, typeofwork, systemtype, ResourceLevel.基础);
            return resource;
        }

        /// <summary>
        /// 派生时所有素材列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<Resource> AllResourceList(string keyword,Guid userId, Guid category, Guid level, Guid subjects, Guid typeofwork, Guid systemtype)
        {
            resRsy = new ResourceRepository();
            IList<Resource> resources = null;
            resources = resRsy.CusResourceList(keyword,userId, category, level, subjects, typeofwork, systemtype, null);
            return resources;
        }

        /// <summary>
        /// 检测物理文件是否被Resource引用
        /// </summary>
        /// <param name="FilePath">物理文件路径</param>
        /// <param name="FileType">物理文件类型</param>
        /// <returns></returns>
        public bool CheckFile(string FilePath, string FileType)
        {
            resRsy = new ResourceRepository();
            return resRsy.CheckFileDelete(FilePath, FileType);
        }

    }
}
