using System;
using System.Collections.Generic;

namespace AlgorithmsBase
{
    public partial class Evalution : Algorithms
    {
        public int CONSOLE_DEBUG = 0;
        const int _breakpointDef = 5;
        int _mutationCounter = 0, _breakvalue=int.MaxValue, _breakpoint= _breakpointDef;
        //ищем 1 индивид вместо нескольких 
        public List<int> curbest { get { return curbests[0]; } set { curbests[0] = value; } }
        public Evalution(Func<List<int>, int> calculate, int problem_size):base(calculate, problem_size) {}

        protected class Individ
        {
            public List<int> info;
            public int cost;
            public Individ(List<int> src, int val=0)
            {
                info = src;
                cost = val;
            }
            public Individ(Individ src)
            {
                info = new List<int>(src.info);
                cost = src.cost;
            }
            public string toStr()
            {
                string result = "";
                for (int i = 0; i < info.Count; i++)
                    result += info[i].ToString() + ' ';
                return result + " : "+cost.ToString();
            }
        }
    }
}
