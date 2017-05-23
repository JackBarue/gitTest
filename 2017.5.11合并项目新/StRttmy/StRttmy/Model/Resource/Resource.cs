using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// 素材类
    /// </summary>
    public class Resource
    {
        [Key]
        public Guid ResourceId { get; set; }
   
        /// <summary>
        /// 素材类别
        /// </summary>
        [Display(Name = "类别")]
        [Required(ErrorMessage = "×")]
        public ResourceLevel Level { get; set; }

        /// <summary>
        /// 声音文件
        /// </summary>
        [Display(Name = "声音文件")]
        [Required(ErrorMessage = "×")]
        public string SoundFile { get; set; }

        /// <summary>
        /// 素材标题（生成课件时，导航所需的默认标题）
        /// </summary>
        [Display(Name = "素材标题")]
        [Required(ErrorMessage = "×")]
        public string Title { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [Display(Name = "素材关键字")]
        [Required(ErrorMessage = "×")]
        public string Keyword { get; set; }

    

        /// <summary>
        /// 文字文件
        /// </summary>
        [Display(Name = "文字文件")]
        [Required(ErrorMessage = "×")]
        public string TextFile { get; set; }

       

        /// <summary>
        /// 媒体文件
        /// </summary>
        [Display(Name = "媒体文件")]
        [Required(ErrorMessage = "×")]
        public string ContentFile { get; set; }

        /// <summary>
        /// 类型ID
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
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
    }
}