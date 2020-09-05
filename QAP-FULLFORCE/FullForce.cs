using System;
using System.Collections.Generic;

namespace AlgorithmsBase
{
    public partial class QAP_FULLFORCE : Algorithms
    {
        public int DEBUG_CONSOLE_OUT;
        public double temp_cost = 0;

        public QAP_FULLFORCE(Func<List<int>, int> calculate, int problem_size):base(calculate,problem_size)
        {
            _steppredict = 1;
            for (int i = 0; i < problem_size; i++)
                _steppredict *= (i + 1);
        }

    }
}