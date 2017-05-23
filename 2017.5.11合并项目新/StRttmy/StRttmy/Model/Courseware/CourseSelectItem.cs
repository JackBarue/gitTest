using StRttmy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class CourseSelectItem
    {
        public CourseSelectItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }

        /// <summary>
        /// 课件类型的SelectList列表
        /// </summary>
        public static IList<CourseSelectItem> TypeSelectList
        {
            get
            {
                IList<CourseSelectItem> _items = new List<CourseSelectItem>();
                _items.Add(new CourseSelectItem(CoursewareType.车辆.ToString(), (int)CoursewareType.车辆));
                _items.Add(new CourseSelectItem(CoursewareType.车务.ToString(), (int)CoursewareType.车务));
                _items.Add(new CourseSelectItem(CoursewareType.电务.ToString(), (int)CoursewareType.电务));
                _items.Add(new CourseSelectItem(CoursewareType.工务.ToString(), (int)CoursewareType.工务));
                _items.Add(new CourseSelectItem(CoursewareType.供电.ToString(), (int)CoursewareType.供电));
                _items.Add(new CourseSelectItem(CoursewareType.机务.ToString(), (int)CoursewareType.机务));
                _items.Add(new CourseSelectItem(CoursewareType.通用.ToString(), (int)CoursewareType.通用));
                return _items;
            }
        }
    }
}
