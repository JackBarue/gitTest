using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class TestStateSelectItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public TestStateSelectItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// 试题类型列表
        /// </summary>
        public static IList<TestStateSelectItem> TypeSelectList
        {
            get
            {
                IList<TestStateSelectItem> _items = new List<TestStateSelectItem>();
                _items.Add(new TestStateSelectItem(TestStateType.全部.ToString(), (int)TestStateType.全部));
                _items.Add(new TestStateSelectItem(TestStateType.未考.ToString(), (int)TestStateType.未考));
                _items.Add(new TestStateSelectItem(TestStateType.未批卷.ToString(), (int)TestStateType.未批卷));
                _items.Add(new TestStateSelectItem(TestStateType.已批卷.ToString(), (int)TestStateType.已批卷));
                _items.Add(new TestStateSelectItem(TestStateType.缺考.ToString(), (int)TestStateType.缺考));
                _items.Add(new TestStateSelectItem(TestStateType.补考.ToString(), (int)TestStateType.补考));
                return _items;
            }
        }
    }
}
