using System;
using System.Collections.Generic;

namespace QAPenviron
{
    public partial class Individ
    {
        private List<int> p;

        public int this[int i]
        {
            get => p[i];
            set => p[i] = value;
        }

        public int size { get => p.Count; }

        // constructors
        public Individ()
        {
            p = new List<int>();
        }
        public Individ( Individ src)
        {
            p = new List<int>(10);
            foreach (int a in src.p)
                p.Add(a);
        }
        public Individ(int[] src)
        {
            p = new List<int>();
            foreach (int a in src)
                p.Add(a - 1);
        }
        public Individ(int count)
        {
            int k;
            p = new List<int>(count);
            List<int> filler = new List<int>();
            for (int i = 0; i < count; i++)
                filler.Add(i);
            for (int i = 0; i < count; i++)
            {
                k = new Random().Next(0, filler.Count);
                p.Add(filler[k]);
                filler.RemoveAt(k);
            }
        }
    }

    public partial class Info
    {
        public class Solves
        {
        }
        public class TestGeneration
        {
        }
    }
}
