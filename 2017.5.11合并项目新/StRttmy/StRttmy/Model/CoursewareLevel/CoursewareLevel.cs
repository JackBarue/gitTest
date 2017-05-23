using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 课件类别
    /// </summary>
    public class CoursewareLevel
    {
        /// <summary>
        /// 课件类别ID
        /// </summary>
        [Key]
        public Guid CoursewareLevelId { get; set; }

        /// <summary>
        /// 类别名字
        /// </summary>
        [Required(ErrorMessage = "×")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
