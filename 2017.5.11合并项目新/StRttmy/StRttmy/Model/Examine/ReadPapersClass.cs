﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StRttmy.Model
{
    public class ReadPapersClass
    {
        public Guid StudentExamPaperId { get; set; }

        public string TestName { get; set; }

        public string ClassName { get; set; }

        public string Name { get; set; }

        public TestStateType TestState { get; set; }

    }
}
