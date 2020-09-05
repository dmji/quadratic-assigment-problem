using System;

namespace AlgorithmsBase
{
    public class AlgorithmStatistic
    {
        public System.Diagnostics.Stopwatch _timer;
        public int _steppredict { get; set; }
        public int calculation_counter { get; set; }

        public AlgorithmStatistic()
        {
            _timer = new System.Diagnostics.Stopwatch();
            _steppredict = 0;
            calculation_counter = 0;
        }
    }
}