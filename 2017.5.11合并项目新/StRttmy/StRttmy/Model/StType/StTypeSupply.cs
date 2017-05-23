using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 类别类
    /// </summary>
    public class StTypeSupply
    {
        /// <summary>
        /// 类别Id
        /// </summary>
        [Key]
        public Guid StTypeSupplyId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required(ErrorMessage = "×")]
        public string StTypeName { get; set; }

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
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
