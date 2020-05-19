using System;
using System.Collections.Generic;
using System.Text;
using QAP_LSA;

namespace QAP
{
    public class individ
    {
        private List<int> p;

        public int this[int i]
        {
            get => p[i];
            set => p[i] = value;
        }
        public int size
        {
            get => p.Count;
        }
        public individ()
        {
            p = new List<int>();
        }
        
        public individ(individ src)
        {
            p = new List<int>(10);
            foreach (int a in src.p)
                p.Add(a);
        }

        public individ(int []src)
        {
            p = new List<int>();
            foreach (int a in src)
                p.Add(a-1);
        }
        public individ(int count)
        {
            int k;
            p = new List<int>(count);
            List<int> filler=new List<int>();
            for (int i = 0; i < count; i++)
                filler.Add(i);
            for (int i = 0; i < count; i++)
            {
                k = new Random().Next(0, filler.Count);
                p.Add(filler[k]);
                filler.RemoveAt(k);
            }
        }
        public void print()                                       //console out permutation
        {
            for (int i = 0; i < p.Count; i++)
                Console.Write(p[i] + " ");
            Console.WriteLine('\n');
        }
    }
}
