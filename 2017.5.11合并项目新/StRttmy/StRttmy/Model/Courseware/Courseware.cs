using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 课件
    /// </summary>
    public class Courseware
    {
        [Key]
        public Guid CoursewareId { get; set; }

        /// <summary>
        /// 课件类型
        /// </summary>      
        [Required(ErrorMessage = "×")]
        public Guid CoursewareLevelId { get; set; }
        public virtual CoursewareLevel CoursewareLevel { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid StTypeId { get; set; }
        public virtual StType StType { get; set; }

       

        /// <summary>
        /// 类别
        /// </summary>
        public Guid StTypeSupplyId { get; set; }
        public virtual StTypeSupply StTypeSupply { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public Guid StLevelId { get; set; }
        public virtual StLevel StLevel { get; set; }

        /// <summary>
        /// 课件名称
        /// </summary>
        [Display(Name = "课件名称")]
        [Required(ErrorMessage = "×")]
        public string Name { get; set; }

        /// <summary>
        /// 课件关键字
        /// </summary>
        [Display(Name = "课件关键字")]
        [Required(ErrorMessage = "×")]
        public string Keyword { get; set; }

       


        /// <summary>
        /// 课件简介
        /// </summary>
        [Display(Name = "课件简介")]
        public string Description { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        //  延迟加载
        public virtual ICollection<CoursewareResource> coursewareResources { get; set; }

    }
}