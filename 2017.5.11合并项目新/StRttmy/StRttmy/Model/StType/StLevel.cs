using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 等级类
    /// </summary>
    public class StLevel
    {
        /// <summary>
        /// 等级Id
        /// </summary>
        [Key]
        public Guid StLevelId { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        [Required(ErrorMessage = "×")]
        public string StLevelName { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "×")]
        public byte ReferenceType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
