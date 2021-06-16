using System;
using System.Collections.Generic;

namespace Solution
{
    public partial class CEvolutionAlgorithm
    {
        protected class CIndivid : CPermutation
        {
            public CIndivid(CPermutation src) : base(src) { }
            public CIndivid(IPermutation src) : base(src) { }
            public CIndivid(IProblem problem,ICollection<ushort> src) : base(problem, src) {}

            /// <summary>Макромутация: Сальтация </summary>
            /// <param name="src"></param>
            public void _mutationSaltation(int M_SALT_SIZEi = 4)
            {
                Random rand = new Random();
                List<ushort> pool = ToArray();
                ushort iFirst = (ushort)rand.Next(Size());
                pool.Remove(iFirst);
                for(int i = 0; i < M_SALT_SIZEi; i++)
                {
                    ushort iSecond = (ushort)rand.Next(pool.Count);
                    pool.Remove(iSecond);
                    Swap(iFirst, iSecond);
                }
            }

            /// <summary>Точечная мутация</summary>
            /// <param name="src"></param>
            public void _mutationDot()
            {
                int iRnd = new Random().Next(Size() - 1);
                Swap(iRnd, iRnd + 1);
            }

            public int FindIndex(ushort a)
            {
                for(int i=0;i<Size();i++)
                {
                    if(this[i] == a)
                        return i;
                }
                return -1;
            }
        }
    }
}
