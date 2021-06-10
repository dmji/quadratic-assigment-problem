namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CTSProblem : AProblem
    {
        public CTSProblem(ushort size = 0) : base() { Init(size); }

        /// <summary>R-matrix</summary>
		int[] m_tDistance;

        public void SetDist(int val, int i, int j) => m_tDistance[i * Size() + j] = val;
        public int GetDist(int i, int j) => m_tDistance[i * Size() + j];

        protected override void Init(ushort size)
        {
            base.Init(size);
            m_tDistance = new int[size*size];
        }

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public override long Calc(IPermutation src)
        {
            bool bDebug = false;
            long res = 0;
            if(bDebug)
            {
                string s = "";
                for(int i = 0; i < src.Size(); i++)
                    s += $"+R[{i}][{src[i]}]";
            }
            for(int i = 0; i < src.Size(); i++)
                res += GetDist(i, src[i]);
            return res;
        }
        
        public override long CalcedSwap(IPermutation src, int ix, int iy)
        {
            bool bDebug = false;
            int x = src[ix], y = src[iy];
            long res = src.Cost();
            if(ix == iy)
                return res;
            if(bDebug)
            {
                if(Calc(src) != res)
                {
                    Msg("ERROR VALUE CALCULATED CALCEDSWAP");
                    throw new System.Exception("WRONG CALC");
                }
            }

            res -= GetDist(ix, x);
            res -= GetDist(iy, y);
            res += GetDist(ix, y); 
            res += GetDist(iy, x);

            if(bDebug)
            {
                string s1 = "";
                string s2 = "";

                s1 += $"+R[{ix}][{x}]";
                s1 += $"+R[{iy}][{y}]";

                s2 += $"+R[{ix}][{y}]";
                s2 += $"+R[{iy}][{x}]";

                var t = src.Clone();
                var tx = t[ix];
                t[ix] = t[iy];
                t[iy] = tx;
                if(Calc(t) != res)
                {
                    Msg("ERROR VALUE CALCULATED CALCEDSWAP");
                    throw new System.Exception("WRONGCALCEDSWAP");
                }
            }
            return res;
        }
    }
}
