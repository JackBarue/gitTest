using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class QuestionSelectItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public QuestionSelectItem(string text, int value)
        {
            this.Text = text;
            this.Value = value;
        }
        /// <summary>
        /// 试题类型列表
        /// </summary>
        public static IList<QuestionSelectItem> TypeSelectList
        {
            get
            {
                IList<QuestionSelectItem> _items = new List<QuestionSelectItem>();
                _items.Add(new QuestionSelectItem(TestQuestionType.全部.ToString(), (int)TestQuestionType.全部));
                _items.Add(new QuestionSelectItem(TestQuestionType.单选题.ToString(), (int)TestQuestionType.单选题));
                _items.Add(new QuestionSelectItem(TestQuestionType.判断题.ToString(), (int)TestQuestionType.判断题));
                _items.Add(new QuestionSelectItem(TestQuestionType.简答题.ToString(), (int)TestQuestionType.简答题));
                _items.Add(new QuestionSelectItem(TestQuestionType.论述题.ToString(), (int)TestQuestionType.论述题));
                return _items;
            }
        }
    }
}
