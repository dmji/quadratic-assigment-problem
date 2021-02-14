using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Problem;

namespace Algorithms
{
    public partial class EvalutionAlgorithm
    {
        protected class Individ : Problem.CPermutation
        {
            ///<summary>Construct permutation from exist one</summary>
            public Individ(CPermutation src) : base(src) { }

            ///<summary>Construct permutation from list</summary>
            public Individ(ICollection<ushort> src) : base(src) {}

            ///<summary>Construct corrupted permutation, <c>count</c> is problem size, <c>fill</c> is int in all slots </summary>
            public Individ(ushort count, ushort filler) : base(count,filler) {}

            ///<summary>Construct random permutation, <c>count</c> is problem size</summary>
            public Individ(ushort count = 0) : base(count, 0)
            {
                List<ushort> filler = new List<ushort>();
                for(ushort i = 0; i < count; i++)
                    filler.Add(i);
                for(int i = 0; i < count; i++)
                {
                    int k = rand.next(filler.Count);
                    this[i] = filler[k];
                    filler.RemoveAt(k);
                }
            }

            public static bool operator==(Individ a, Individ b) => a.cost() == b.cost();
            public static bool operator!=(Individ a, Individ b) => a.cost() != b.cost();

            /// <summary>Макромутация: Сальтация </summary>
            /// <param name="src"></param>
            public void _mutationSaltation(int distance = 4)
            {
                List<ushort> pool = new List<ushort>(this.ToArray());
                ushort iFirst = (ushort)rand.next(size());
                pool.Remove(iFirst);
                for(int i = 0; i < distance; i++)
                {
                    ushort iSecond = (ushort)rand.next(pool.Count);
                    pool.Remove(iSecond);
                    swap(iFirst, iSecond);
                }
            }

            /// <summary>Точечная мутация</summary>
            /// <param name="src"></param>
            public void _mutationDot()
            {
                int iRnd = rand.next(size() - 1);
                swap(iRnd, iRnd + 1);
            }

            public int findIndex(ushort a)
            {
                for(int i=0;i<size();i++)
                {
                    if(this[i] == a)
                        return i;
                }
                return -1;
            }
        }
    }
}
