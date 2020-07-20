using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Evalution
    {
        /// <summary>
        /// CX - Cycle Crossiver
        /// </summary>
        protected List<Individ> _crossover(Individ a, Individ b)
        {
            List<List<int>> cycles = new List<List<int>>();
            List<int> temp = new List<int>();
            List<Individ> result = new List<Individ>();
            //
            //FUNCTION FOR LOOP
            //
            int _find(int that)
            {
                for(int i = 0; i<problem.problem_size;i++)
                    if(a[i]==that)
                        return i;
                return -1;
            }
            int _findPool(int that)
            {
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i] == that)
                        return i;
                return -1;
            }
            void _recursion(Individ src, int curcycle)
            {
                if (curcycle < cycles.Count)
                {
                    for(int i = 0;i<cycles[curcycle].Count;i++)
                        src[cycles[curcycle][i]] = a[cycles[curcycle][i]];
                    _recursion(src, curcycle + 1);
                    for (int i = 0; i < cycles[curcycle].Count; i++)
                        src[cycles[curcycle][i]] = b[cycles[curcycle][i]];
                    _recursion(src, curcycle + 1);
                }
                else
                    result.Add(new Individ(src));
            }
            //
            //CYCLES CONSTRUCTION
            //
            int ind;
            for (int i = 0; i < problem.problem_size; i++)
                temp.Add(i);
            while (temp.Count != 0)
            {
                cycles.Add(new List<int>());
                ind = 0;
                do
                {
                    cycles[cycles.Count - 1].Add(temp[ind]);
                    temp.RemoveAt(ind);
                    ind = _findPool(_find(b[cycles[cycles.Count - 1][cycles[cycles.Count - 1].Count - 1]]));
                } while (ind != -1);
            }
            //
            //CYCLES CONSUMING
            //
            _recursion(new Individ(problem.problem_size), 0);
            return result;
        }
        /// <summary>
        /// Panmixia
        /// </summary>
        protected List<Individ> _reproduction(List<Individ> population, int loopcount)
        {
            List<Individ> result = new List<Individ>(), current = new List<Individ>();
            for (int i = 0; i < loopcount; i++)
            {
                current = _crossover(population[new Random().Next(population.Count)], population[new Random().Next(population.Count)]);
                result.AddRange(current);
            }
            return result;
        }
    }

}
