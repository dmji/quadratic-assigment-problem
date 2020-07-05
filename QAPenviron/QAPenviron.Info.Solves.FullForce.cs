using System;
using System.Collections.Generic;

namespace QAPenviron
{
    public partial class Info
    {
        /// <summary>
        /// fullforce(single-thread)
        /// </summary>
        /// <returns>list of individ</returns>
        public List<Individ> FullForce()
        {
            List<Individ> best_cost = new List<Individ>();
            double temp_cost = 0;

            int _isExist(ref Individ src, ref int point, int position)
            {
                for (int i = 0; i < position; i++)
                    if (src[i] == point)
                        return 1;
                return 0;
            }
            void recursion(Individ src, int position)
            {
                if (position != problem_size)
                {
                    for (int i = 0; i < problem_size; i++)
                    {
                        if (_isExist(ref src, ref i, position) == 0)
                        {
                            src[position] = i;
                            recursion(new Individ(src), position + 1);
                        }
                    }
                }
                else
                {
                    double cur_cost = cost(src);
                    if (best_cost.Count == 0 || cur_cost < temp_cost)
                    {
                        temp_cost = cur_cost;
                        best_cost.Clear();
                        best_cost.Add(src);
                    }
                    else if (cur_cost == temp_cost)
                    {
                        best_cost.Add(src);
                    }

                }
            }

            _algorithm.calculation_counter = 0;
            _algorithm._steppredict = 1;
            for (int i = 0; i < problem_size; i++) _algorithm._steppredict *= (i + 1);
            if (DEBUG_CONSOLE_OUT == 1) Console.WriteLine("FullForce start. Step to do: " + _algorithm._steppredict.ToString());

            _algorithm._timer.Restart();
            recursion(new Individ(problem_size), 0);
            _algorithm._timer.Stop();

            if (DEBUG_CONSOLE_OUT == 1)
            {
                Console.WriteLine("In-work time: " + _algorithm._timer.ElapsedMilliseconds + "\nCalculated: " + _algorithm.calculation_counter + "\nCost: "+cost(best_cost[0]));
                foreach (Individ a in best_cost) a.console_print();
                Console.WriteLine("\nDone!\n");
            }

            return best_cost;
        }
    }
}