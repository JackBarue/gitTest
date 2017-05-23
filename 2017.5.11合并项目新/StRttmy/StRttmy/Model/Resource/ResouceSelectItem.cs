using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class ResouceSelectItem
    {
        public ResouceSelectItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }

        /// <summary>
        /// 资源类型的SelectList列表
        /// </summary>
        public static IList<ResouceSelectItem> TypeSelectList
        {
            get
            {
                IList<ResouceSelectItem> _items = new List<ResouceSelectItem>();
                _items.Add(new ResouceSelectItem(ResourceType.车辆.ToString(), (int)ResourceType.车辆));
                _items.Add(new ResouceSelectItem(ResourceType.车务.ToString(), (int)ResourceType.车务));
                _items.Add(new ResouceSelectItem(ResourceType.电务.ToString(), (int)ResourceType.电务));
                _items.Add(new ResouceSelectItem(ResourceType.工务.ToString(), (int)ResourceType.工务));
                _items.Add(new ResouceSelectItem(ResourceType.供电.ToString(), (int)ResourceType.供电));
                _items.Add(new ResouceSelectItem(ResourceType.机务.ToString(), (int)ResourceType.机务));
                _items.Add(new ResouceSelectItem(ResourceType.通用.ToString(), (int)ResourceType.通用));
                return _items;
            }
        }
    }
}
