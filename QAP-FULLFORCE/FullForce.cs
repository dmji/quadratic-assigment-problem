using System;
using System.Collections.Generic;
using QAPenviron;

namespace AlgorithmsBase
{
    public partial class QAP_FULLFORCE
    {
        protected int size;
        public AlgorithmStatistic _stats;
        public List<List<int>> best_cost = new List<List<int>>();
        private Func<List<int>, int> calculate;
        public int DEBUG_CONSOLE_OUT;
        public double temp_cost = 0;

        public QAP_FULLFORCE(Func<List<int>, int> calculate, int problem_size)
        {
            this.calculate = calculate;
            size = problem_size;
            _stats = new AlgorithmStatistic();

            _stats._steppredict = 1;
            for (int i = 0; i < size; i++)
                _stats._steppredict *= (i + 1);
        }

        protected string getPermutation(List<int> src)
        {
            string res="";
            foreach (int a in src)
                res = res + a.ToString() + " ";
            return res;
        }

        public string ToStr()
        {
            string log = "";
            if (best_cost.Count != 0)
            {
                log = log + "FullForce algorithm. Step to do: " + _stats._steppredict.ToString();
                log = log + "\nIn-work time: " + _stats._timer.ElapsedMilliseconds.ToString() + "\nCalculated: " + _stats.calculation_counter.ToString() + "\nCost: " + calculate(best_cost[0]).ToString() + '\n';
                foreach (List<int> a in best_cost) 
                    log += getPermutation(a) + '\n';
            }
            else
                log = ("FullForce not started!");
            return log;
        }
    }
}