using System;
using System.Collections.Generic;
using QAPenviron;

namespace QAPenviron
{
    public partial class QAP_FULLFORCE
    {
        protected Info _problem = null;
        protected int permutation_size;

        public int DEBUG_CONSOLE_OUT;
        public List<Individ> best_cost = new List<Individ>();
        public double temp_cost = 0;

        public QAP_FULLFORCE(Info src, int DEBUG_CONSOLE = 0)
        {
            _problem = src;
            permutation_size = src.problem_size;
            _problem._algorithm._steppredict = 1;
            for (int i = 0; i < permutation_size; i++) _problem._algorithm._steppredict *= (i + 1);
        }

        public void ShowInConsole()
        {
            if (best_cost.Count != 0)
            {
                Console.WriteLine("FullForce algorithm. Step to do: " + _problem._algorithm._steppredict.ToString());
                Console.WriteLine("In-work time: " + _problem._algorithm._timer.ElapsedMilliseconds + "\nCalculated: " + _problem._algorithm.calculation_counter + "\nCost: " + _problem.cost(best_cost[0]));
                foreach (Individ a in best_cost) a.console_print();
            }
            else if (best_cost.Count == 0 && _problem != null)
            {
                Console.WriteLine("FullForce algorithm not Start. Step to do: " + _problem._algorithm._steppredict.ToString());
            }
            else
            {
                Console.WriteLine("FullForce not started!");
            }
        }
    }
}