using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StRttmy.Models
{
    //[NotMapped]
    public class ZTreeNode
    {
        public string id { get; set; }
        public string pId { get; set; }
        public string name { get; set; }
        public string MainUrl { get; set; }
        public string TextUrl { get; set; }
        public string Mp3Url { get; set; }
    }
}