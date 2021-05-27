using System;
using System.Collections.Generic;

namespace Solution
{
    public partial class CEvolutionAlgorithm
    {
        protected class CIndivid : CPermutation
        {
            ///<summary>Construct permutation from exist one</summary>
            public CIndivid(CPermutation src) : base(src) { }
            public CIndivid(IPermutation src) : base(src) { }

            ///<summary>Construct permutation from list</summary>
            public CIndivid(IProblem problem,ICollection<ushort> src) : base(problem, src) {}

            ///<summary>Construct corrupted permutation, <c>count</c> is problem size, <c>fill</c> is int in all slots </summary>
            public CIndivid(IProblem problem, ushort count, ushort filler) : base(problem, count,filler) {}

            ///<summary>Construct random permutation, <c>count</c> is problem size</summary>
            public CIndivid(IProblem problem, ushort count = 0) : base(problem, count, 0)
            {
                OnEdit();
                Random rand = new Random();
                List<ushort> filler = new List<ushort>();
                for(ushort i = 0; i < count; i++)
                    filler.Add(i);
                for(int i = 0; i < count; i++)
                {
                    int k = rand.Next(filler.Count);
                    this[i] = filler[k];
                    filler.RemoveAt(k);
                }
            }

            /// <summary>Макромутация: Сальтация </summary>
            /// <param name="src"></param>
            public void _mutationSaltation(int M_SALT_SIZEi = 4)
            {
                OnEdit();
                Random rand = new Random();
                List<ushort> pool = new List<ushort>(this.ToArray());
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
                OnEdit();
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
