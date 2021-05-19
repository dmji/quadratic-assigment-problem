using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Solution
{
    public partial class EvolutionAlgorithm
    {
        /// <summary>CX - Cycle Crossiver : all variant in List return</summary>
        protected List<Individ> CX_all_crossover(Individ a, Individ b, int limiter=-1)
        {
            List<List<int>> aCycles = new List<List<int>>();
            List<int> aCyclesSingle = new List<int>();
            List<int> aTemp = new List<int>();
            List<ushort> perm = new List<ushort>();
            List<Individ> aResult = new List<Individ>();
            //CYCLES CONSTRUCTION
            for(int i = 0; i < Size(); i++)
            {
                aTemp.Add(i);
                perm.Add(0);
            }
            while (aTemp.Count != 0)
            {
                List<int> curCycle = new List<int>();
                int iTemp = 0;
                while(iTemp != -1)
                {
                    curCycle.Add(aTemp[iTemp]);
                    ushort curVal2 = b[aTemp[iTemp]];
                    aTemp.RemoveAt(iTemp);
                    iTemp = a.FindIndex(curVal2);
                    iTemp = aTemp.IndexOf(iTemp);
                }
                if(curCycle.Count == 1)
                    aCyclesSingle.Add(curCycle[0]);
                else
                    aCycles.Add(curCycle);
            }
            //CYCLES CONSUMING
            //_recursion(aPerm,0);
            int n = (int)Math.Pow(2, aCycles.Count);
            if(Math.Pow(2, aCycles.Count) > int.MaxValue)
            {
                Msg("!aCycles overflow!");
                throw(new Exception("rand > int"));
            }

            Random rand = new Random();
            //if(size() > 50)
            //{
            for(int i = 0; i < limiter; i++)
            {
                int curVal = rand.Next(n);
                for(int j = 0; j < aCycles.Count; j++)
                {
                    int it = curVal << j & 1;
                    foreach(int val in aCycles[j])
                        perm[val] = it == 0 ? a[val] : b[val];
                }
                foreach(int val in aCyclesSingle)
                    perm[val] = a[val];
                aResult.Add(new Individ(m_problem, perm));
            }
            //
            //}
            //else
            //{
            //    System.Threading.Tasks.ParallelOptions opt = new System.Threading.Tasks.ParallelOptions();
            //    System.Threading.Tasks.Parallel.For(0, limiter
            //        , () => { return new List<ushort>(perm); }
            //        , (int i, System.Threading.Tasks.ParallelLoopState s, List<ushort> p) =>
            //        {
            //            int curVal = rand.Next(n);
            //            for(int j = 0; j < aCycles.Count; j++)
            //            {
            //                int it = curVal << j & 1;
            //                foreach(int val in aCycles[j])
            //                    p[val] = it == 0 ? a[val] : b[val];
            //            }
            //            foreach(int val in aCyclesSingle)
            //                p[val] = a[val];
            //            return p;
            //        }
            //        , (List<ushort> fin) => { Individ finInd = new Individ(m_q.calc, fin); lock(aResult) aResult.Add(finInd); });
            //}
            return aResult;
        }

        /// <summary>Panmixia</summary>
        protected List<Individ> REPRODUCTION(List<Individ> aPopulation, int C_SIZEi, int C_CHANCEi)
        {
            Random rand = new Random();
            List<Individ> aResult = new List<Individ>();
            List<int> aPool = new List<int>();
            for(int i = 0; i < aPopulation.Count; i++)
                aPool.Add(i);
            while(aPool.Count > 1)
            {
                int rnd = rand.Next(aPool.Count), v1 = aPool[rnd], v2=0;
                aPool.RemoveAt(rnd);
                rnd = rand.Next(aPool.Count);
                v2 = aPool[rnd];
                aPool.RemoveAt(rnd);
                if(C_CHANCEi >= rand.Next(101))
                    aResult.AddRange(CX_all_crossover(aPopulation[v1], aPopulation[v2], C_SIZEi));
            }
            return aResult;
        }
    }

}
