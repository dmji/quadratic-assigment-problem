using System.Collections.Generic;

namespace Solution
{
    public interface IAlgorithm : IName
    {
        public IDiagnostic Start(IOptions option);
        void Reset(IProblem problem);
        IPermutation Result { get; set; }
    }

    public abstract partial class AAlgorithm : IAlgorithm, IDiagnostic
    {
        public virtual IDiagnostic Start(IOptions prm) => this;
        public abstract string Name();
        public void Reset(IProblem problem) { ResetDiagnostic(); m_q = problem; }
        public IPermutation Result { get => m_p[0]; set { m_p.Clear(); m_p.Add(value); } }

        // size problem
        protected ushort size() => m_q.Size();
        protected AAlgorithm(IProblem problem) { Reset(problem); }

        protected IProblem m_q;
        protected List<IPermutation> m_p;
        protected bool m_bFinish;
    }
}
