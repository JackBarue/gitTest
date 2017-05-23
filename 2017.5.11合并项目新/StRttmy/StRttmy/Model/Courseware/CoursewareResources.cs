using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 课件素材
    /// </summary>
    public class CoursewareResource
    {
        /// <summary>
        /// 课件素材ID
        /// </summary>
        [Key]
        public int CoursewareResourcesId { get; set; }

        /// <summary>
        /// 课件Id
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid CoursewareId { get; set; }
        public virtual Courseware Courseware { get; set; }

        /// <summary>
        /// ztree ID
        /// </summary>
        [Required]
        public string id { get; set; }

        /// <summary>
        /// ztree父ID
        /// </summary>
        public string pId { get; set; }

        /// <summary>
        /// 课件名字
        /// </summary>
        [Required]
        public string name { get; set; }

        /// <summary>
        /// 媒体文件
        /// </summary>
        public string MainUrl { get; set; }

        /// <summary>
        /// 文字文件
        /// </summary>
        public string TextUrl { get; set; }

        /// <summary>
        /// 声音文件
        /// </summary>
        public string Mp3Url { get; set; }
    }
}