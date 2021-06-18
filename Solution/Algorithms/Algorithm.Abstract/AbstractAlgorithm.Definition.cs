using System.Collections.Generic;

namespace Solution
{
    public interface IAlgorithm : IName, IToString, ISetLogger
    {
        public void Start(IOptions option);
        void Reset(IProblem problem);
        long GetCalcCount();
        long GetResultValue();
        IPermutation Result { get; set; }
    }

    public abstract partial class AAlgorithm : ALoggerContainer, IAlgorithm
    {
        public virtual void Start(IOptions prm) { }
        public abstract string Name();
        public void Reset(IProblem problem) { ResetDiagnostic(); m_problem = problem; }
        public IPermutation Result { get => m_result; set { m_result = value; } }
        public override string ToString()
        {
            string log = Name() + " algorithm.\n";
            if(m_bFinish)
            {
                log += $"Finished with {m_calculationCounter} calculations. Final cost: {m_result.Cost().ToString()}\n";
                log += m_result.ToString() + '\n';
            }
            else
                log = ("Not yet started!");
            return log;
        }

        // size problem
        protected ushort Size() => m_problem.Size();
        protected AAlgorithm(IProblem problem) { m_log = null; Reset(problem); }

        protected IProblem m_problem;
        protected IPermutation m_result;
        protected bool m_bFinish;
    }
}
