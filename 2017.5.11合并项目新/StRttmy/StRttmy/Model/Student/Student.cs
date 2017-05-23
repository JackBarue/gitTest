using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 学员
    /// </summary>
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }


        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录名", Description = "1-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "×")]
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "×")]
        [StringLength(512)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// 学员名字
        /// </summary>
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "×")]
        public string Name { get; set; }

        /// <summary>
        /// 学员工号
        /// </summary>
        [Required(ErrorMessage = "×")]
        public int WorkNumber { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "×")]
        public string Telephone { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        [Required(ErrorMessage = "×")]
        public string WorkingUnit { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}
