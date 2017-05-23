using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 题目类
    /// </summary>
    public class TestQuestion
    {
        /// <summary>
        /// 题目Id
        /// </summary>
        [Key]
        public Guid TestQuestionId { get; set; }

        /// <summary>
        /// 题目
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public TestQuestionType QuestionType { get; set; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public Guid StTypeId { get; set; }
        public virtual StType StType { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public Guid? StTypeSupplyId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public Guid? StLevelId { get; set; }

        /// <summary>
        /// 素材
        /// </summary>
        public Guid? ResourceId { get; set; }
        //public virtual Resource Resource { get; set; }

        /// <summary>
        /// 创建教师
        /// </summary>
        public Guid UserId { get; set; }
        //public virtual User User { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public double? Score { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }

        /// <summary>
        /// 正确答案
        /// </summary>        
        public string Correct { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        //public virtual ICollection<ExamPaper> ExamPapers { get; set; }
    }
}
