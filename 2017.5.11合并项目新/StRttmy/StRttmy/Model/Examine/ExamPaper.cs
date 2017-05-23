using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 考卷类
    /// </summary>
    public class ExamPaper
    {
        /// <summary>
        /// 考卷Id
        /// </summary>
        [Key]
        public Guid ExamPaperId { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }
        //public virtual TestPaper TestPaper { get; set; }

        /// <summary>
        /// 题目Id
        /// </summary>
        public Guid TestQuestionId { get; set; }
        //public virtual TestQuestion TestQuestion { get; set; }

        public int? TestQuestionNo { get; set; }
    }
}
