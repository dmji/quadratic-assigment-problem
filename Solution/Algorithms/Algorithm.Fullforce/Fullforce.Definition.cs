﻿using System.Collections.Generic;

namespace Solution
{
    public partial class CFullforceAlgorithm : AAlgorithm
    {
        string m_logSync = "";
        public override string Name() => "Fullforce algorithm";
        public CFullforceAlgorithm(IProblem problem) : base(problem) { }

        protected int _isExist(List<ushort> src, ushort point)
        {
            for(int i = 0; i < src.Count-1; i++)
                if(src[i] == point)
                    return 1;
            return 0;
        }

        protected void RecursionParallel(List<ushort> src)
        {
            src.Add(0);
            System.Threading.Tasks.ParallelOptions opt = new System.Threading.Tasks.ParallelOptions();
            System.Threading.Tasks.Parallel.For(0, Size(),
                opt,
                () => 0,
               (int i, System.Threading.Tasks.ParallelLoopState state, long b) =>
               {
                   long a = b;
                   if(_isExist(src, (ushort)i) == 0)
                   {
                       src[src.Count - 1] = (ushort)i;
                       Recursion(new List<ushort>(src));
                   }
                   return a;
               },
               (long a) => { });
        }

        protected void Recursion(List<ushort> src)
        {
            if(src.Count < Size())
            {
                src.Add(0);
                for(ushort i = 0; i < Size(); i++)
                {
                    if(_isExist(src, i) == 0)
                    {
                        src[src.Count - 1] = i;
                        Recursion(new List<ushort>(src));
                    }
                }
            }
            else
            {
                CPermutation curPerm = new CPermutation(m_problem, src);
                if(m_log != null)
                {
                    lock(m_logSync)
                    {
                        Msg($"{curPerm}");
                    }
                }
                lock(m_result)
                {
                    if(m_result == null)
                    {
                        m_result = curPerm.Clone();
                    }
                    else
                    {
                        int cmp = m_problem.PermutationComparision(curPerm, Result);
                        if(cmp > -1)
                        {
                            m_result = curPerm.Clone();
                        }
                    }
                }
            }
        }

        public override void Start(IOptions opt)
        {
            ResetDiagnostic();
            RecursionParallel(new List<ushort>());
        }
    }
}