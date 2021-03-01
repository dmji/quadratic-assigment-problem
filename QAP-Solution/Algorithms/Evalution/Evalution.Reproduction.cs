using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Algorithms
{
    public partial class EvalutionAlgorithm
    {
        //int _findPool(Individ pool, int that)
        //{
        //    int findInd = -1;
        //    for(int i = 0; i < size(); i++)
        //        if(a[i] == that)
        //        {
        //            findInd = i;
        //            break;
        //        }
        //    if(findInd != -1)
        //    {
        //        for(int i = 0; i < temp.Count; i++)
        //        {
        //            if(temp[i] == findInd)
        //                return i;
        //        }
        //    }
        //    return -1;
        //}
        ///// <summary>CX - Cycle Crossiver : only one variant return</summary>
        //protected Individ single_crossover(Individ a, Individ b)
        //{
        //    Individ result = new Individ(size(), 0);
        //    double parentRnd = new Random().NextDouble();
        //    for(int i=0;i<size();i++)
        //        result[i] = parentRnd > 0.5 ? a[i] : b[i];
        //    return result;
        //}


        //protected List<Individ> duo_crossover(Individ a, Individ b)
        //{
        //    List<int> temp = new List<int>();
        //    List<Individ> result = new List<List<int>>();
        //    //
        //    //CYCLES CONSTRUCTION
        //    //
        //    int ind;
        //    result.Add(new List<int>());
        //    for (int i = 0; i < problem_size; i++)
        //    {
        //        temp.Add(i);
        //        result[0].Add(-1);
        //        result[1].Add(-1);
        //    }
        //    while (temp.Count != 0)
        //    {
        //        ind = 0;
        //        do
        //        {
        //            int tempind = temp[ind];
        //            if (new Random().NextDouble() > 0.5)
        //            {
        //                if(result[0].Contains(a[tempind])==false)
        //                    result[0][tempind] = a[tempind];
        //                else
        //                    result[0][tempind] = b[tempind];
        //                if (result[1].Contains(b[tempind]) == false)
        //                    result[1][tempind] = b[tempind];
        //                else
        //                    result[1][tempind] = a[tempind];
        //            }
        //            else
        //            {
        //                if (result[0].Contains(a[tempind]) == false)
        //                    result[0][tempind] = b[tempind];
        //                else
        //                    result[0][tempind] = a[tempind];
        //                if (result[1].Contains(b[tempind]) == false)
        //                    result[1][tempind] = a[tempind];
        //                else
        //                    result[1][tempind] = b[tempind];
        //            }
        //            temp.RemoveAt(ind);
        //            ind = _findPool(b[tempind]);
        //        } while (ind != -1);
        //    }
        //    //Console.WriteLine("###\n" + getPermutation(a) + '\n' + getPermutation(b) + '\n' + getPermutation(result) + "\n###");
        //    return result;
        //}

        /// <summary>CX - Cycle Crossiver : all variant in List return</summary>
        protected List<Individ> cx_all_crossover(Individ a, Individ b, int limiter=-1)
        {
            List<List<int>> aCycles = new List<List<int>>();
            List<int> aTemp = new List<int>();
            List<ushort> aPerm = new List<ushort>();
            List<Individ> aResult = new List<Individ>();
            //FUNCTION FOR LOOP
            void _recursion(List<ushort> src, int iCurCycle)
            {
                if(iCurCycle < aCycles.Count)
                {
                    foreach(int i in aCycles[iCurCycle])
                        src[i] = a[i];
                    _recursion(new List<ushort>(src), iCurCycle + 1);
                    if(aCycles[iCurCycle].Count > 1)
                    {
                        foreach(int i in aCycles[iCurCycle])
                            src[i] = b[i];
                        _recursion(new List<ushort>(src), iCurCycle + 1);
                    }
                }
                else
                {
                    aResult.Add(new Individ(m_q.calc, src));
                }
            }
            //CYCLES CONSTRUCTION
            for(int i = 0; i < size(); i++)
            {
                aTemp.Add(i);
                aPerm.Add(0);
            }
            while (aTemp.Count != 0)
            {
                aCycles.Add(new List<int>());
                List<int> curCycle = aCycles[aCycles.Count - 1];
                int iTemp = 0;
                while(iTemp != -1)
                {
                    curCycle.Add(aTemp[iTemp]);
                    ushort curVal2 = b[aTemp[iTemp]];
                    aTemp.RemoveAt(iTemp);
                    iTemp = a.findIndex(curVal2);
                    iTemp = aTemp.IndexOf(iTemp);
                }
            }
            //CYCLES CONSUMING
            _recursion(aPerm,0);
            if(limiter>0)
            {
                while(aResult.Count>limiter)
                    aResult.RemoveAt(rand.next(aResult.Count));
            }
            return aResult;
        }

        protected List<Individ> every_with_every_reproduction(List<Individ> population)
        {
            List<Individ> result = new List<Individ>();
            for(int i = 0; i < population.Count - 1; i++)
            {
                for(int j = i + 1; j < population.Count; j++)
                {
                    result.AddRange(cx_all_crossover(population[i], population[j]));
                }
            }
            return result;
        }

        /// <summary>Panmixia</summary>
        protected List<Individ> REPRODUCTION(List<Individ> aPopulation, int C_SIZEi, int C_CHANCEi)
        {
            List<Individ> aResult = new List<Individ>();
            List<int> aPool = new List<int>();
            for(int i = 0; i < aPopulation.Count; i++)
                aPool.Add(i);
            //while(iter++ < count)
            //{
            //    int rnd1 = rand.next(aPool.Count), rnd2 = rand.next(aPool.Count);
            //    aResult.AddRange(cx_all_crossover(aPopulation[aPool[rnd1]], aPopulation[aPool[rnd2]], count));
            //}
            while(aPool.Count > 1)
            {
                int rnd = rand.next(aPool.Count), v1 = aPool[rnd], v2=0;
                aPool.RemoveAt(rnd);
                rnd = rand.next(aPool.Count);
                v2 = aPool[rnd];
                aPool.RemoveAt(rnd);
                if(C_CHANCEi >= rand.next(101))
                    aResult.AddRange(cx_all_crossover(aPopulation[v1], aPopulation[v2], C_SIZEi));
            }
            return aResult;
        }
    }

}
