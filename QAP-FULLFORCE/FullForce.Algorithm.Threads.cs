using System;
using System.Collections.Generic;
using System.Threading;

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
            for (int i = 0; i < problem_size; i++)
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
            statReset();
            recursion_MT();
            _timer.Stop();
            return curbests;
        }
    }
}