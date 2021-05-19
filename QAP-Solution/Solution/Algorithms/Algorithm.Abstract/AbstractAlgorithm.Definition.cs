using System.Collections.Generic;

namespace Solution
{
    public interface IAlgorithm : IName, IToString, ISetLogger
    {
        public IResultAlg Start(IOptions option);
        void Reset(IProblem problem);
        IPermutation Result { get; set; }
    }

    public abstract partial class AAlgorithm : IAlgorithm
    {
        public virtual IResultAlg Start(IOptions prm) => this;
        public abstract string Name();
        public void Reset(IProblem problem) { ResetDiagnostic(); m_problem = problem; }
        public IPermutation Result { get => m_results[0]; set { m_results.Clear(); m_results.Add(value); } }
        public override string ToString()
        {
            string log = Name() + " algorithm.\n";
            if(m_bFinish)
            {
                log += $"Finished with {m_calculationCounter} calculations. Final cost: {m_problem.Calc(m_results[0]).ToString()}\n";
                foreach(IPermutation a in m_results)
                    log += a.ToString() + '\n';
            }
            else
                log = ("Not yet started!");
            return log;
        }

        // size problem
        protected ushort Size() => m_problem.Size();
        protected AAlgorithm(IProblem problem) { Reset(problem); }

        protected IProblem m_problem;
        protected List<IPermutation> m_results;
        protected bool m_bFinish;
    }
}
