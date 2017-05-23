using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 类型
    /// </summary>
    public class StType
    {
        [Key]
        public Guid StTypeId { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Required(ErrorMessage = "×")]
        public Guid Fid { get; set; }

        /// <summary>
        /// 类型名字
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "×")]
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "×")]
        public byte ReferenceType { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 培训学时，以小时为单位
        /// </summary>
        public double? StTime { get; set; }

        /// <summary>
        /// 教学目的
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// 教学内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
