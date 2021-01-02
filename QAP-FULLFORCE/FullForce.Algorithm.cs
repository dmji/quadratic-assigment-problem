using System;
using System.Collections.Generic;

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
            if (src.Count < problem_size)
            {
                src.Add(-1);
                for (int i = 0; i < problem_size; i++)
                    if (_isExist(src, i) == 0)
                    {
                        src[src.Count-1]=i;
                        recursion(new List<int>(src));
                    }
            }
            else
            {
                double cur_cost = calculate(src);

                lock (curbests)
                {
                    if (curbests.Count == 0 || cur_cost < temp_cost)
                    {
                        temp_cost = cur_cost;
                        curbests.Clear();
                        curbests.Add(src);
                    }
                    else if (cur_cost == temp_cost)
                        curbests.Add(src);
                }
            }
        }

        public List<List<int>> Start()
        {
            statReset();
            recursion(new List<int>());
            _timer.Stop();
            return curbests;
        }
    }
}