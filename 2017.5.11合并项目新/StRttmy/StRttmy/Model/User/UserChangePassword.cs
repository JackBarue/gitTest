using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 用户修改视图模型
    /// </summary>
    [NotMapped]
    public class UserChangePassword
    {
        ///<summary>
        /// 原密码
        /// </summary>
        [Display(Name = "原密码")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码", Description = "再次输入密码。")]
        [Compare("NewPassword", ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
