using System;
using System.Collections.Generic;

namespace QAPenviron
{
    /// <summary>Class <c>Individ</c> models a single permutation in QAP (like in evalution algorithm).</summary>
    public partial class Individ
    {
        ///<summary>permutation</summary>
        private List<int> p;

        public int this[int i]{ get => p[i]; set => p[i] = value; }
        /// <summary>
        /// return current permutation size
        /// </summary>
        public int size { get => p.Count; }
        //
        // constructors
        //
        ///<summary>Empty permutation list</summary>
        public Individ()
        {
            p = new List<int>();
        }
        ///<summary>Construct permutation from exist one</summary>
        public Individ( Individ src)
        {
            p = new List<int>(10);
            foreach (int a in src.p)
                p.Add(a);
        }
        ///<summary>Construct permutation from array-listing</summary>
        public Individ(int[] src)
        {
            p = new List<int>();
            foreach (int a in src)
                p.Add(a);
        }
        ///<summary>Construct random permutation, <c>count</c> is problem size</summary>
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

    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class Info
    {
        /// <summary>
        /// to get some conole output set 1 
        /// </summary>
        public int DEBUG_CONSOLE_OUT = 0;
        /// <summary>D-matrix</summary>
		public int[,] stream;
        ///<summary> F-matix</summary>
        public int[,] price;
        ///<summary> C-matix</summary>
        public int[,] position_cost;
        ///<summary>n</summary>
		public int problem_size;
        //
        // constructors
        //
        protected void base_init(int problem_size)
		{
			price = new int[problem_size, problem_size];
			stream = new int[problem_size, problem_size];
			position_cost = new int[problem_size, problem_size];
		}

        /// <summary>Empty (zero-size) problem.</summary>
		public Info()
		{
            problem_size = 0;
			stream = null;
			price = null;
			position_cost = null;
        }
    }
}
