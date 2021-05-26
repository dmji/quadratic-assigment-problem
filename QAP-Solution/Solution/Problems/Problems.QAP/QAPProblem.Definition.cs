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
            string s = "";
            long res = 0;
            for(int i = 0; i < src.Size(); i++)
            {
                for(int j = 0; j < src.Size(); j++)
                {
                    s += $"+D[{src[i]}][{src[j]}]*F[{i}][{j}]";
                    res += GetDist(src[i], src[j]) * GetFlow(i, j); // + GetPCost(i, src[j]);
                }
            }

            return res;
        }
        
        public override long CalcedSwap(IPermutation src, int ix, int iy)
        {
            int x = src[ix], y = src[iy];
            long res = src.Cost();
            for(int ij = 0; ij < src.Size(); ij++)
            {
                var j = src[ij];
                res += (GetDist(y, j) - GetDist(x, j)) * GetFlow(ix, ij);
                res += (GetDist(x, j) - GetDist(y, j)) * GetFlow(iy, ij);
                res += (GetDist(j, y) - GetDist(j, x)) * GetFlow(ij, ix);
                res += (GetDist(j, x) - GetDist(j, y)) * GetFlow(ij, iy);
            }
            res += (GetDist(y, x) - GetDist(x, y)) * GetFlow(ix, iy);
            res += (GetDist(x, y) - GetDist(y, x)) * GetFlow(iy, ix);
            res += (GetDist(y, y) - GetDist(x, x)) * GetFlow(ix, ix);
            res += (GetDist(x, x) - GetDist(y, y)) * GetFlow(iy, iy);
            src.Swap(ix, iy);
            return res;
        }
    }
}
