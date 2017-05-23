using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class PlatformZTreeNode
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public string Descrption { get; set; }
        public string MainUrl { get; set; }
        public string TextUrl { get; set; }
        public string Mp3Url { get; set; }
        public string Keyword { get; set; }
        public string Type { get; set; }
        public string State { get; set; }   //素材状态 0确定素材、1未确定素材
    }
}
