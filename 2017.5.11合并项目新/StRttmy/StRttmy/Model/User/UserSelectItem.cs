using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class UserSelectItem
    {
        public UserSelectItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }

        /// <summary>
        /// 用户类型的SelectList列表
        /// </summary>
        public static IList<UserSelectItem> TypeSelectList
        {
            get
            {
                IList<UserSelectItem> _items = new List<UserSelectItem>();
                _items.Add(new UserSelectItem(UserType.教师.ToString(), (int)UserType.教师));
                _items.Add(new UserSelectItem(UserType.管理人员.ToString(), (int)UserType.管理人员));
                _items.Add(new UserSelectItem(UserType.系统管理员.ToString(), (int)UserType.系统管理员));
                return _items;
            }
        }
    }
}
