using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 培训班类
    /// </summary>
    public class StClass
    {
        [Key]
        public Guid StClassId { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        [Required(ErrorMessage = "×")]
        public string ClassName { get; set; }

        /// <summary>
        /// 班级状态
        /// </summary>
        [Required(ErrorMessage = "×")]
        [StringLength(10, ErrorMessage = "×")]
        public string ClassState { get; set; }

        /// <summary>
        /// 教师Id
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        //  延迟加载
        public virtual ICollection<ClassStudent> ClassStudents { get; set; }
    }
}
