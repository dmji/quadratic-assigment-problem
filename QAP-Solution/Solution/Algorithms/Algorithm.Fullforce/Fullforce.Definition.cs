using System.Collections.Generic;

namespace Solution
{
    public partial class FullforceAlgorithm : AAlgorithm
    {
        public override string Name() => "Fullforce algorithm";

        public FullforceAlgorithm(IProblem problem) : base(problem) { }

        protected int _isExist(List<ushort> src, ushort point)
        {
            for(int i = 0; i < src.Count-1; i++)
                if(src[i] == point)
                    return 1;
            return 0;
        }

        protected void recursionParallel(List<ushort> src)
        {
            src.Add(0);
            System.Threading.Tasks.ParallelOptions opt = new System.Threading.Tasks.ParallelOptions();
            System.Threading.Tasks.Parallel.For(0, size(),
                opt,
                () => 0,
               (int i, System.Threading.Tasks.ParallelLoopState state, long b) =>
               {
                   long a = b;
                   if(_isExist(src, (ushort)i) == 0)
                   {
                       src[src.Count - 1] = (ushort)i;
                       recursion(new List<ushort>(src));
                   }
                   return a;
               },
               (long a) => { });
        }

        protected void recursion(List<ushort> src)
        {
            if(src.Count < size())
            {
                src.Add(0);
                for(ushort i = 0; i < size(); i++)
                {
                    if(_isExist(src, i) == 0)
                    {
                        src[src.Count - 1] = i;
                        recursion(new List<ushort>(src));
                    }
                }
            }
            else
            {
                CPermutation curPerm = new CPermutation(m_q.Calc, src);
                double cur_cost = curPerm.Cost();
                lock(m_p)
                {
                    if(m_p.Count == 0 || cur_cost < Result.Cost())
                    {
                        m_p.Clear();
                        m_p.Add(new CPermutation(m_q.Calc, src));
                    }
                    else if(cur_cost == Result.Cost())
                        m_p.Add(new CPermutation(m_q.Calc, src));
                }
            }
        }

        public override IDiagnostic Start(IOptions opt)
        {
            ResetDiagnostic();
            recursionParallel(new List<ushort>());
            return this;
        }
    }
}