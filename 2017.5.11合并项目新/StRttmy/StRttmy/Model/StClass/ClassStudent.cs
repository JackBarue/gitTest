using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace StRttmy.Model
{
    public class ClassStudent
    {
        [Key]
        public Guid ClassStudentId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid StClassId { get; set; }
        public virtual StClass StClass { get; set; }

        /// <summary>
        /// 学员Id
        /// </summary>
        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
