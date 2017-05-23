using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Common
{
    class QuestionHelp
    {
        public static int TestTime;//考试总时间
        public static int RemainSeconds;//剩余时间
        public static Guid stuid;
        public static Guid paperId;
        public static Guid[] AllQuestionId;//所有试题Id数组
        public static int QuestionCount;//试题总数
        public static Guid[] SelectQuestionId;
        public static string[] StudentAnswer;
        public static string[] AnswerFile;
        public static double[] Score;
        public static string[] ScoreInfo;
        public static double CountScore;
        public static double StudentScore;
        public static Guid StudentExamPaperId;
        public static Dictionary<Guid, string> maps = new Dictionary<Guid, string>();//学院答案
    }
}
