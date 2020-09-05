using System;
using System.Collections.Generic;
namespace AlgorithmsBase
{
    public class Algorithms
    {
        public System.Diagnostics.Stopwatch _timer;
        public int _steppredict { get; set; }
        public int calculation_counter { get; set; }


        public List<List<int>> curbests;
        protected Func<List<int>, int> calculateFunc;
        protected int problem_size;

        protected int calculate(List<int> src)
        {
            lock (_timer)
                calculation_counter++;
            return calculateFunc(src);
        }

        protected List<int> randomPermutation(int permutation_size)
        {
            int k;
            List<int> res = new List<int>();
            List<int> filler = new List<int>();
            for (int i = 0; i < permutation_size; i++)
                filler.Add(i);
            for (int i = 0; i < permutation_size; i++)
            {
                k = new Random().Next(0, filler.Count);
                res.Add(filler[k]);
                filler.RemoveAt(k);
            }
            return res;
        }
        protected string getPermutation(List<int> src)
        {
            string res = "";
            foreach (int a in src)
                res = res + a.ToString() + " ";
            return res;
        }

        public string ToStr()
        {
            string log = "";
            if (curbests.Count != 0)
            {
                log = log + "FullForce algorithm.";
                if (_steppredict != 0)
                    log = log + "Step to do: " + _steppredict.ToString();
                log = log + "\nIn-work time: " + _timer.ElapsedMilliseconds.ToString() + "\nCalculated: " + calculation_counter.ToString() + "\nCost: " + calculate(curbests[0]).ToString() + '\n';
                foreach (List<int> a in curbests)
                    log += getPermutation(a) + '\n';
            }
            else
                log = ("FullForce not started!");
            return log;
        }

        protected void statReset()
        {
            curbests.Clear();
            calculation_counter = 0;
            _timer.Restart();
        }
        public Algorithms(Func<List<int>, int> calculate, int problem_size)
        {
            _timer = new System.Diagnostics.Stopwatch();
            _steppredict = 0;
            calculation_counter = 0;

            this.problem_size = problem_size;
            this.calculateFunc = calculate;
            curbests = new List<List<int>>();
        }
    }
}