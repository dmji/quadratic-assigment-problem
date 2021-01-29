using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        /// <summary>CX - Cycle Crossiver : only one variant return</summary>
        protected List<int> single_crossover(List<int> a, List<int> b)
        {
            List<int> temp = new List<int>(), result = new List<int>();
            //
            //FUNCTION FOR LOOP
            //
            int _findPool(int that)
            {
                int findInd = -1;
                for (int i = 0; i < problem_size; i++)
                    if (a[i] == that)
                    {
                        findInd= i;
                        break;
                    }
                if(findInd!=-1)
                    for (int i = 0; i < temp.Count; i++)
                        if (temp[i] == findInd)
                            return i;
                return -1;
            }
            //
            //CYCLES CONSTRUCTION
            //
            int ind;
            for (int i = 0; i < problem_size; i++)
            {
                temp.Add(i);
                result.Add(-1);
            }    
            while (temp.Count != 0)
            {
                ind = 0;
                double tempRand = new Random().NextDouble();
                do
                {
                    int tempind = temp[ind];
                    if (tempRand > 0.5)
                        result[tempind] = a[tempind];
                    else
                        result[tempind] = b[tempind];
                    temp.RemoveAt(ind);
                    ind = _findPool(b[tempind]);
                } while (ind != -1);
            }
            //Console.WriteLine("###\n" + getPermutation(a) + '\n' + getPermutation(b) + '\n' + getPermutation(result) + "\n###");
            return result;
        }


        protected List<List<int>> duo_crossover(List<int> a, List<int> b)
        {
            List<int> temp = new List<int>();
            List<List<int>> result = new List<List<int>>();
            //
            //FUNCTION FOR LOOP
            //
            int _findPool(int that)
            {
                int findInd = -1;
                for (int i = 0; i < problem_size; i++)
                    if (a[i] == that)
                    {
                        findInd = i;
                        break;
                    }
                if (findInd != -1)
                    for (int i = 0; i < temp.Count; i++)
                        if (temp[i] == findInd)
                            return i;
                return -1;
            }
            //
            //CYCLES CONSTRUCTION
            //
            int ind;
            result.Add(new List<int>());
            for (int i = 0; i < problem_size; i++)
            {
                temp.Add(i);
                result[0].Add(-1);
                result[1].Add(-1);
            }
            while (temp.Count != 0)
            {
                ind = 0;
                do
                {
                    int tempind = temp[ind];
                    if (new Random().NextDouble() > 0.5)
                    {
                        if(result[0].Contains(a[tempind])==false)
                            result[0][tempind] = a[tempind];
                        else
                            result[0][tempind] = b[tempind];
                        if (result[1].Contains(b[tempind]) == false)
                            result[1][tempind] = b[tempind];
                        else
                            result[1][tempind] = a[tempind];
                    }
                    else
                    {
                        if (result[0].Contains(a[tempind]) == false)
                            result[0][tempind] = b[tempind];
                        else
                            result[0][tempind] = a[tempind];
                        if (result[1].Contains(b[tempind]) == false)
                            result[1][tempind] = a[tempind];
                        else
                            result[1][tempind] = b[tempind];
                    }
                    temp.RemoveAt(ind);
                    ind = _findPool(b[tempind]);
                } while (ind != -1);
            }
            //Console.WriteLine("###\n" + getPermutation(a) + '\n' + getPermutation(b) + '\n' + getPermutation(result) + "\n###");
            return result;
        }

        /// <summary>CX - Cycle Crossiver : all variant in List return</summary>
        protected List<List<int>> multi_crossover(List<int> a, List<int> b)
        {
            List<List<int>> cycles = new List<List<int>>();
            List<int> temp = new List<int>();
            List<List<int>> result = new List<List<int>>();
            //
            //FUNCTION FOR LOOP
            //
            int _findPool(int that)
            {
                int findInd = -1;
                for (int i = 0; i < problem_size; i++)
                    if (a[i] == that)
                    {
                        findInd = i;
                        break;
                    }
                if (findInd != -1)
                    for (int i = 0; i < temp.Count; i++)
                        if (temp[i] == findInd)
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
                    ind = _findPool(b[cycles[cycles.Count - 1][cycles[cycles.Count - 1].Count - 1]]);
                } while (ind != -1);
            }
            //
            //CYCLES CONSUMING
            //
            _recursion(randomPermutation(problem_size), 0);
            return result;
        }



        /// <summary>Panmixia</summary>
        protected List<List<int>> multi_reproduction(List<List<int>> population, int loopcount)
        {
            List<List<int>> result = new List<List<int>>(), current;
            for (int i = 0; i < loopcount; i++)
            {
                int temp1 = new Random().Next(population.Count), temp2 = new Random().Next(population.Count);
                while (temp2 == temp1) temp2 = new Random().Next(population.Count);

                current = multi_crossover(population[temp1], population[temp2]);
                result.AddRange(current);
            }
            return result;
        }
        protected List<List<int>> duo_reproduction(List<List<int>> population, int loopcount)
        {
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < population.Count - 1; i++)
                for (int j = i + 1; j < population.Count; j++)
                    result.AddRange(duo_crossover(population[i], population[j]));
            return result;
        }
        protected List<List<int>> single_reproduction(List<List<int>> population)
        {
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < population.Count - 1; i++)
                for (int j = i + 1; j < population.Count; j++)
                    result.Add(single_crossover(population[i], population[j]));
            return result;
        }


        protected List<Individ> _reproduction(List<Individ> src, int count)
        {
            Random rnd = new Random();
            List<int> pool=new List<int>();
            List<Individ> result = new List<Individ>();
            while(result.Count <= count)
            {
                int a = rnd.Next(src.Count), b = rnd.Next(src.Count);
                result.Add(new Individ(single_crossover(src[a].info, src[b].info)));
                pool.Add(a);
                pool.Add(b);
            }
            return result;
        }
    }

}
