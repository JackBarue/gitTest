using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class TestQuestionClass
    {
        public Guid TestQuestionId { get; set; }

        public string Question { get; set; }

        public TestQuestionType QuestionType { get; set; }

        public string AnswerA { get; set; }

        public string AnswerB { get; set; }

        public string AnswerC{ get; set; }

        public string AnswerD { get; set; }

        public string Correct { get; set; }

        public double Score { get; set; }
    }
}
