using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    /// <summary>
    /// 学员考卷类
    /// </summary>
    public class StudentExamPaper
    {
        /// <summary>
        /// 学员考卷Id
        /// </summary>
        [Key]
        public Guid StudentExamPaperId { get; set; }

        /// <summary>
        /// 学员Id
        /// </summary>
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        /// <summary>
        /// 考卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }
        //public virtual TestPaper TestPaper { get; set; }

        public TestStateType TestState { get; set; }
    }
}
