using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class TestQuestionResource
    {
        /// <summary>
        /// 试题素材Id
        /// </summary>
        [Key]
        public Guid QuestionResourceId { get; set; }

        /// <summary>
        /// 试题Id
        /// </summary>
        public Guid TestQuestionId { get; set; }
        //public virtual TestQuestion TestQuestion { get; set; }

        /// <summary>
        /// 素材Id
        /// </summary>
        public Guid ResourceId { get; set; }
        //public virtual Resource Resource { get; set; }
    }
}
