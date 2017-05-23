using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Model
{
    /// <summary>
    /// TreeNode Tag数据对象
    /// </summary>
    [NotMapped]
    public class CourseResourceTag
    {
        public string id { get; set; }

        public string pId { get; set; }

        public string MainUrl { get; set; }

        public string TextUrl { get; set; }

        public string Mp3Url { get; set; }
    }
}
