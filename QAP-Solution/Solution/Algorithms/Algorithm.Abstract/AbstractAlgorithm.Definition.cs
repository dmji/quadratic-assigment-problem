using System.Collections.Generic;

namespace Solution
{
    public interface IAlgorithm : IName
    {
        public IDiagnostic Start(IOptions option);
        void reset(IProblem problem);
        IPermutation result { get; set; }
    }

    public abstract partial class AAlgorithm : IAlgorithm, IDiagnostic
    {
        public virtual IDiagnostic Start(IOptions prm) => this;
        public abstract string getName();
        public void reset(IProblem problem) { diagReset(); m_q = problem; }
        public IPermutation result { get => m_p[0]; set { m_p.Clear(); m_p.Add(value); } }

        // size problem
        protected ushort size() => m_q.size();
        protected AAlgorithm(IProblem problem) { reset(problem); }

        protected IProblem m_q;
        protected List<IPermutation> m_p;
        protected bool m_bFinish;
    }
}
