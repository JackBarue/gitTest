using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 用户模型
    /// </summary>    
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [Display(Name = "用户类型")]
        [Required(ErrorMessage = "×")]
        public UserType UserType { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录名", Description = "1-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "×")]
        public string LoginName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名", Description = "1-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "×")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "×")]
        [StringLength(512)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// 工作单位
        /// </summary>
        [Display(Name = "工作单位")]
        [Required(ErrorMessage = "×")]
        public string WorkingUnit { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
