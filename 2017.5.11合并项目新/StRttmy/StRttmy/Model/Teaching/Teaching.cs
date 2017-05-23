using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 教学表
    /// </summary>
    public class Teaching
    {
        [Key]
        public Guid TeachingId { get; set; }       

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "×")]
        public DateTime? Startime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "×")]
        public DateTime? Endtime { get; set; }

        [Required(ErrorMessage = "×")]
        public Guid StClassId { get; set; }
        public virtual StClass StClass { get; set; }

        /// <summary>
        /// 课件Id
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid CoursewareId { get; set; }

        /// <summary>
        /// 教师Id
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid UserId { get; set; }
        
    }
}
