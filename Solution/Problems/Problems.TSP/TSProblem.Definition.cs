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
            bool bDebug = Consts.bDebug;
            long res = 0;
            if(bDebug)
            {
                string s = "";
                for(int i = 1; i < src.Size(); i++)
                    s += $"+R[{src[i-1]}][{src[i]}]";
                s += $"+R[{src[Size()-1]}][{src[0]}]";
            }
            for(int i = 1; i < src.Size(); i++)
                res += GetDist(src[i-1], src[i]);
            res += GetDist(src[Size() - 1], src[0]);
            return res;
        }
        
        public override long CalcedSwap(IPermutation src, int x, int y)
        {
            if(x > y)
            {
                var t = x;
                x = y;
                y = t;
            }

            bool bDebug = Consts.bDebug;
            long res = src.Cost();
            if(x == y)
                return res;
            if(bDebug)
            {
                if(Calc(src) != res)
                {
                    Msg("ERROR VALUE CALCULATED CALCEDSWAP");
                    throw new System.Exception("WRONG CALC");
                }
            }

            var xPrev = x == 0 ? Size() - 1 : x - 1;
            var yNext = y == Size() - 1 ? 0 : y + 1;

            if(xPrev == y && yNext == x)
            {
                // prew y pos
                res -= GetDist(src[y - 1], src[y]);
                res += GetDist(src[y - 1], src[x]);

                // x pos
                res -= GetDist(src[y], src[x]);
                res += GetDist(src[x], src[y]);
                
                // y pos
                res -= GetDist(src[x], src[x + 1]);
                res += GetDist(src[y], src[x + 1]);
            }
            else if(x + 1 == y)
            {
                // prew pos
                res -= GetDist(src[xPrev], src[x]);
                res += GetDist(src[xPrev], src[y]);

                // x pos 
                res -= GetDist(src[x], src[y]);
                res += GetDist(src[y], src[x]);

                // y pos
                res -= GetDist(src[y], src[yNext]);
                res += GetDist(src[x], src[yNext]);    
            }
            else
            {
                res -= GetDist(src[xPrev], src[x]);
                res -= GetDist(src[y], src[yNext]);

                if(x + 1 != y && (y + 1) % Size() != x)
                {
                    res -= GetDist(src[x], src[x + 1]);
                    res -= GetDist(src[y - 1], src[y]);

                    res += GetDist(src[y], src[x + 1]);
                    res += GetDist(src[y - 1], src[x]);
                }
                else
                {
                    res -= GetDist(src[x], src[y]);
                    res += GetDist(src[y], src[x]);
                }
                res += GetDist(src[xPrev], src[y]);
                res += GetDist(src[x], src[yNext]);
            }            

            if(bDebug)
            {
                string s1 = "";
                string s2 = "";

                //s1 += $"+R[{src[xPrev]}][{src[x]}]";
                //s1 += $"+R[{src[y]}][{src[yNext]}]";

                //if(x + 1 != y)
                //{
                //    s1 += $"+R[{src[x]}][{src[x + 1]}]";
                //    s1 += $"+R[{src[y - 1]}][{src[y]}]";
                //    s2 += $"+R[{src[y]}][{src[x + 1]}]";
                //    s2 += $"+R[{src[y - 1]}][{src[x]}]";
                //}
                //else
                //{
                //    s1 += $"+R[{src[y]}][{src[x]}]";
                //    s2 += $"+R[{src[x]}][{src[y]}]";
                //}

                //s2 += $"+R[{src[xPrev]}][{src[y]}]";
                //s2 += $"+R[{src[x]}][{src[yNext]}]";

                var t = src.Clone();
                var tx = t[x];
                t[x] = t[y];
                t[y] = tx;
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
