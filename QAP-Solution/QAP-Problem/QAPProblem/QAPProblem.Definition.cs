﻿using System;

namespace Problem
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CQAPProblem : AProblem
    {
        public CQAPProblem(ushort size = 0) : base(size) { init(size); }

        /// <summary>D-matrix</summary>
		public int[,] m_tDistance;
        ///<summary> F-matix</summary>
        public int[,] m_tFlow;
        ///<summary> C-matix</summary>
        public int[,] m_tPositionCost;

        void init(ushort size)
        {
            m_ProblemSize = size;
            m_tFlow = size == 0 ? null : new int[m_ProblemSize, m_ProblemSize];
            m_tDistance = size == 0 ? null : new int[m_ProblemSize, m_ProblemSize];
            m_tPositionCost = size == 0 ? null : new int[m_ProblemSize, m_ProblemSize];
        }

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public override ulong calc(IPermutation src)
        {
            ulong res = 0;
            for(int i = 0; i < src.size(); i++)
            {
                for(int j = 0; j < src.size(); j++)
                    res = res + Convert.ToUInt64(m_tFlow[src[i], src[j]] * m_tDistance[i, j] + m_tPositionCost[i, src[j]]);
            }
            return res;
        }
    }
}