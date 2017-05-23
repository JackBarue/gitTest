using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class ResourceListClass
    {
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 素材类别
        /// </summary>
        public ResourceLevel Level { get; set; }

        public string ResourceStyle { get; set; }
        /// <summary>
        /// 声音文件
        /// </summary>
        public string SoundFile { get; set; }

        /// <summary>
        /// 素材标题（生成课件时，导航所需的默认标题）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 文字文件
        /// </summary>
        public string TextFile { get; set; }

        /// <summary>
        /// 媒体文件
        /// </summary>
        public string ContentFile { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public Guid StTypeId { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public Guid StTypeSupplyId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public Guid StLevelId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
