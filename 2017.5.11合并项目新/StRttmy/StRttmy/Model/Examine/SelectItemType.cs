using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StRttmy.Model
{
    public enum TestQuestionType
    {
        全部,单选题,判断题,简答题,论述题
    }

    public enum GenerateType
    {
        手动生成,自动生成
    }

    public enum TestStateType
    {
        全部, 未考, 未批卷, 已批卷, 缺考, 补考, 
    }
}
