using System;
using System.Collections.Generic;
using AlgorithmsBaseStats;
using QAPenviron;

namespace AlgorithmsBase
{
    public partial class QAP_FULLFORCE
    {
        protected Info _problem = null;

        public delegate int calculateFunc();
        protected calculateFunc _del;

        protected int size;

        public AlgorithmStatistic _stats;

        public int DEBUG_CONSOLE_OUT;
        public List<Individ> best_cost = new List<Individ>();
        public double temp_cost = 0;

        public QAP_FULLFORCE(calculateFunc src,int size, int DEBUG_CONSOLE = 0)
        {
            _del = src;
            this.size = size;
        }

        public QAP_FULLFORCE(Info src, int DEBUG_CONSOLE = 0)
        {
            _problem = src;
            size = src.problem_size;
        }

        public string ToStr()
        {
            string log = "";
            if (best_cost.Count != 0)
            {
                _stats._steppredict = 1;
                for (int i = 0; i < size; i++) _stats._steppredict *= (i + 1);
                Console.WriteLine("FullForce algorithm. Step to do: " + _stats._steppredict.ToString());
                Console.WriteLine("In-work time: " + _stats._timer.ElapsedMilliseconds + "\nCalculated: " + _stats.calculation_counter + "\nCost: " + _problem.cost(best_cost[0]));
                foreach (Individ a in best_cost) a.console_print();
            }
            else if (best_cost.Count == 0 && _problem != null)
            {
                _stats._steppredict = 1;
                for (int i = 0; i < size; i++) _stats._steppredict *= (i + 1);
                Console.WriteLine("FullForce algorithm not Start. Step to do: " + _stats._steppredict.ToString());
            }
            else
            {
                Console.WriteLine("FullForce not started!");
            }
            return log;
        }
    }
}