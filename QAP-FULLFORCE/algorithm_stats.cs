using System;
using System.Collections.Generic;
using QAPenviron;

namespace QAPenviron
{
public class AlgorithmStatistic ALGORITHM_STATISTIC
        {
            public System.Diagnostics.Stopwatch _timer;
            public int _steppredict {get; set;};
            public int calculation_counter {get; set;};

            public algorithm_properties()
            {
                _timer = new System.Diagnostics.Stopwatch();
                _steppredict = 0;
                calculation_counter = 0;
            }
        }
}