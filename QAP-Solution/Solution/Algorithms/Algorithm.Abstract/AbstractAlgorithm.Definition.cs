using System.Collections.Generic;

namespace Solution
{
    public interface IOptions
    {
        void serielize(string path);
        string getName();
        int getSeed();
        string getValues();
        string getValuesNames();
    }

    public interface IAlgorithm
    {
        string getName();
        void reset(IProblem problem);
        IPermutation result { get; set; }
        IOptions GetOptionsSet(string path);
    }

    public abstract partial class AAlgorithm : IAlgorithm
    {
        public void reset(IProblem problem) { diagReset(); m_q = problem; }
        //ищем 1 индивид вместо нескольких 
        public IPermutation result { get => m_p[0]; set { m_p.Clear(); m_p.Add(value); } }
        public static IOptions GetOptionsSet(string path) => null;
        public IOptions GetOptionsSet(string path) => this.GetOptionsSet(path);
        public virtual void Start(IOptions prm) { }
        public abstract string getName();

        //size problem
        protected ushort size() => m_q.size();
        protected AAlgorithm(IProblem problem) { reset(problem); }

        protected IProblem m_q;
        protected List<IPermutation> m_p;
        protected bool m_bFinish;
    }
}
