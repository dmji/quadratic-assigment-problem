using System;
using System.Collections.Generic;
using System.Threading;
using QAPenviron;

namespace AlgorithmsBase
{
    public partial class QAP_FULLFORCE
    {
        protected int _isExist(Individ src, int point, int position)
        {
            for (int i = 0; i < position; i++)
                if (src[i] == point)
                    return 1;
            return 0;
        }
        protected void recursion(Individ src, int position)
        {
            if (position != size)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_isExist(src, i, position) == 0)
                    {
                        src[position] = i;
                        recursion(new Individ(src), position + 1);
                    }
                }
            }
            else
            {
                double cur_cost = _problem.cost(src);
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

        public List<Individ> Start()
        {
            _stats.calculation_counter = 0;

            _stats._timer.Restart();
            recursion(new Individ(size), 0);
            _stats._timer.Stop();

            return best_cost;
        }
    }
}