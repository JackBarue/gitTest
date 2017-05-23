using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class GeneratePaperSelectItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public GeneratePaperSelectItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// 试题类型列表
        /// </summary>
        public static IList<GeneratePaperSelectItem> TypeSelectList
        {
            get
            {
                IList<GeneratePaperSelectItem> _items = new List<GeneratePaperSelectItem>();
                _items.Add(new GeneratePaperSelectItem(GenerateType.手动生成.ToString(), (int)GenerateType.手动生成));
                _items.Add(new GeneratePaperSelectItem(GenerateType.自动生成.ToString(), (int)GenerateType.自动生成));
                return _items;
            }
        }
    }
}
