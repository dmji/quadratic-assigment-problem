using System;
using System.Collections.Generic;
using Problem;

namespace Algorithms
{
    public interface IOptions
    {
        void serielize(string path);
        string getName();
        int getSeed();
        string getValues();
        string getValuesNames();
    }

    public abstract partial class Algorithm
    {
        protected IProblem m_q;
        protected List<IPermutation> m_p;
        protected bool m_bFinish;

        //size problem
        protected ushort size() => m_q.size();

        public abstract string getName();

        protected Algorithm(IProblem problem)
        {
            reset(problem);
        }

        public void reset(IProblem problem)
        {
            diagReset();
            m_stepPredict = 0;
            m_q = problem;
        }

        //ищем 1 индивид вместо нескольких 
        public Problem.IPermutation result { get => m_p[0]; set { m_p.Clear(); m_p.Add(value); } }

        public static IOptions GetOptionsSet(string path) => null;
        public virtual void Start(IOptions prm) { }
    }
}
