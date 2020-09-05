using System;
using System.Collections.Generic;
using System.Threading;
using QAPenviron;

namespace AlgorithmsBase
{
    public partial class QAP_FULLFORCE
    {
        protected int _isExist(List<int> src, int point)
        {
            for (int i = 0; i < src.Count; i++)
                if (src[i] == point)
                    return 1;
            return 0;
        }

        protected void recursion(List<int> src)
        {
            if (src.Count < size)
            {
                src.Add(-1);
                for (int i = 0; i < size; i++)
                    if (_isExist(src, i) == 0)
                    {
                        src[src.Count-1]=i;
                        recursion(new List<int>(src));
                    }
            }
            else
            {
                double cur_cost = calculate(src);
                lock (_stats._timer)
                    _stats.calculation_counter++;
                lock (best_cost)
                {
                    if (best_cost.Count == 0 || cur_cost < temp_cost)
                    {
                        temp_cost = cur_cost;
                        best_cost.Clear();
                        best_cost.Add(src);
                    }
                    else if (cur_cost == temp_cost)
                        best_cost.Add(src);
                }
            }
        }

        public List<List<int>> Start()
        {
            _stats.calculation_counter = 0;
            _stats._timer.Restart();
            recursion(new List<int>());
            _stats._timer.Stop();

            return best_cost;
        }
    }
}