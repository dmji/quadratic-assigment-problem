namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CQAPProblem : AProblem
    {
        public CQAPProblem(ushort size = 0) : base() { Init(size); }

        /// <summary>D-matrix</summary>
		int[] m_tDistance;
        ///<summary> F-matix</summary>
        int[] m_tFlow;
        ///<summary> C-matix</summary>
        int[] m_tPositionCost;

        public void SetDist(int val, int i, int j) => m_tDistance[i * Size() + j] = val;
        public void SetFlow(int val, int i, int j) => m_tFlow[i * Size() + j] = val;
        public void SetPCost(int val, int i, int j) => m_tPositionCost[i * Size() + j] = val;

        public int GetDist(int i, int j) => m_tDistance[i * Size() + j];
        public int GetFlow(int i, int j) => m_tFlow[i * Size() + j];
        public int GetPCost(int i, int j) => m_tPositionCost[i * Size() + j];

        protected override void Init(ushort size)
        {
            base.Init(size);
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
                    res += GetDist(src[i],src[j]) * GetFlow(i, j) + GetPCost(i, src[j]);
            }
            return res;
        }
    }
}
