using System;
using System.Collections.Generic;
using System.Threading;
using QAPenviron;

namespace QAPenviron
{
    public partial class QAP_FULLFORCE
    {
        protected void recursion_starter(object srcobj)
        {
            Individ src = new Individ((Individ)srcobj);
            int position = 0;
            for (; position < _problem.problem_size; position++) if (src[position] == -1) break;
            recursion(new Individ(src), position);
        }

        protected void recursion_MT(object capsule)
        {
            if (capsule.GetType().ToString() == "QAPenviron.Individ")
            {
                Individ src = new Individ((Individ)capsule);
                List<Thread> trlist = new List<Thread>();
                int position = 0;
                for (int i = 0; i < permutation_size; i++)
                {
                    src[position] = i;
                    //new Thread(recursion_starter).Start(new Individ(src));
                    trlist.Add(new Thread(recursion_starter));
                    trlist[trlist.Count - 1].Start(new Individ(src));
                }
                for (int i = 0; i < trlist.Count; i++) trlist[i].Join();
            }
            else
                Console.WriteLine("Error type in recursion");
        }
        /// <summary>
        /// FullForce w/ parralleling first level of tree
        /// </summary>
        public List<Individ> StartMT()
        {
            best_cost.Clear();
            _problem._algorithm.calculation_counter = 0;
            _problem._algorithm._timer.Restart();

            recursion_MT(new Individ(permutation_size, -1));

            _problem._algorithm._timer.Stop();
            return best_cost;
        }
    }
}