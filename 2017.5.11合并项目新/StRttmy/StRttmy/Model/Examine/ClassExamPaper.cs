using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class ClassExamPaper
    {
        /// <summary>
        /// 班级考卷Id
        /// </summary>
        [Key]
        public Guid ClassExamPaperId { get; set; }

        /// <summary>
        /// 班级Id
        /// </summary>
        public Guid StClassId { get; set; }
        public virtual StClass StClass { get; set; }

        /// <summary>
        /// 考卷Id
        /// </summary>
        public Guid TestPaperId { get; set; }
        //public virtual TestPaper TestPaper { get; set; }        
    }
}
