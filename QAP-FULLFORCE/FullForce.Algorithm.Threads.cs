using System;
using System.Collections.Generic;
using System.Threading;
using QAPenviron;

namespace AlgorithmsBase
{
    public partial class QAP_FULLFORCE
    {
        protected void recursion_starter(object srcobj)
        {
            recursion(new List<int>((List<int>)srcobj));
        }

        protected void recursion_MT()
        {
            List<int> src = new List<int>();
            src.Add(-1);
            List<Thread> trlist = new List<Thread>();
            for (int i = 0; i < size; i++)
            {
                src[src.Count-1]=i;
                trlist.Add(new Thread(recursion_starter));
                trlist[trlist.Count - 1].Start(new List<int>(src));
            }
            for (int i = 0; i < trlist.Count; i++) 
                trlist[i].Join();
        }

        /// <summary>FullForce w/ parralleling first level of tree</summary>
        public List<List<int>> StartMT()
        {
            best_cost.Clear();
            _stats.calculation_counter = 0;
            _stats._timer.Restart();

            recursion_MT();

            _stats._timer.Stop();
            return best_cost;
        }
    }
}