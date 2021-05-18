namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CQAPProblem : AProblem
    {
        public CQAPProblem(ushort size = 0) : base(size) { init(size); }

        /// <summary>D-matrix</summary>
		int[] m_tDistance;
        ///<summary> F-matix</summary>
        int[] m_tFlow;
        ///<summary> C-matix</summary>
        int[] m_tPositionCost;

        public void setDist(int val, int i, int j) => m_tDistance[i * Size() + j] = val;
        public void setFlow(int val, int i, int j) => m_tFlow[i * Size() + j] = val;
        public void setPCost(int val, int i, int j) => m_tPositionCost[i * Size() + j] = val;

        public int getDist(int i, int j) => m_tDistance[i * Size() + j];
        public int getFlow(int i, int j) => m_tFlow[i * Size() + j];
        public int getPCost(int i, int j) => m_tPositionCost[i * Size() + j];

        protected override void init(ushort size)
        {
            base.init(size);
            m_tDistance = new int[size*size];
            m_tFlow = new int[size * size];
            m_tPositionCost = new int[size * size];
        }

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public override long Calc(IPermutation src)
        {
            long res = 0;
            for(int i = 0; i < src.Size(); i++)
            {
                for(int j = 0; j < src.Size(); j++)
                    res += getDist(src[i],src[j]) * getFlow(i, j) + getPCost(i, src[j]);
            }
            return res;
        }
    }
}
