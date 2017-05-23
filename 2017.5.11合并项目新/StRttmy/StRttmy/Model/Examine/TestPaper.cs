using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 试卷类
    /// </summary>
    public class TestPaper
    {
        /// <summary>
        /// 试卷Id
        /// </summary>
        [Key]
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Guid StTypeId { get; set; }
        //public virtual StType StType { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public Guid StTypeSupplyId { get; set; }
        //public virtual StTypeSupply StTypeSupply { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public Guid StLevelId { get; set; }
        //public virtual StLevel StLevel { get; set; }

        /// <summary>
        /// 创建教师
        /// </summary>
        public Guid UserId { get; set; }
        public virtual User User { get; set; }        

        /// <summary>
        /// 考试时间
        /// </summary>
        public int TestTime { get; set; }

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        //public virtual ICollection<ExamPaper> ExamPapers { get; set; }        
    }
}
