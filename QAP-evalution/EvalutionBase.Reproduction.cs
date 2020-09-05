using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        /// <summary>
        /// CX - Cycle Crossiver
        /// </summary>
        protected List<List<int>> _crossover(List<int> a, List<int> b)
        {
            List<List<int>> cycles = new List<List<int>>();
            List<int> temp = new List<int>();
            List<List<int>> result = new List<List<int>>();
            //
            //FUNCTION FOR LOOP
            //
            int _find(int that)
            {
                for(int i = 0; i<problem_size;i++)
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
            void _recursion(List<int> src, int curcycle)
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
                    result.Add(new List<int>(src));
            }
            //
            //CYCLES CONSTRUCTION
            //
            int ind;
            for (int i = 0; i < problem_size; i++)
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
            _recursion(randomPermutation(problem_size), 0);
            return result;
        }
        /// <summary>
        /// Panmixia
        /// </summary>
        protected List<List<int>> _reproduction(List<List<int>> population, int loopcount)
        {
            List<List<int>> result = new List<List<int>>(), current;
            for (int i = 0; i < loopcount; i++)
            {
                int temp1 = new Random().Next(population.Count), temp2 = new Random().Next(population.Count);
                while (temp2 == temp1) temp2 = new Random().Next(population.Count);

                current = _crossover(population[temp1], population[temp2]);
                result.AddRange(current);
            }
            return result;
        }
    }

}
