using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace StRttmy.Model
{
    /// <summary>
    /// 答案类
    /// </summary>
    public class StudentAnswer
    {
        /// <summary>
        /// 答案Id
        /// </summary>
        [Key]
        public Guid AnswerId { get; set; }

        /// <summary>
        /// 学员Id
        /// </summary>
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        /// <summary>
        /// 考卷题目Id
        /// </summary>        
        public Guid TestQuestionId { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }

        /// <summary>
        /// 学员答案
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 答案得分
        /// </summary>
        public double AnswerScore { get; set; }
    }
}
