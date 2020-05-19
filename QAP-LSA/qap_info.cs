using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QAP
{
    public partial class info
    {
		public int[,] stream;                                                                                               //D-matrix
		public int[,] price;                                                                                                //F-matix
		public int[,] position_cost;                                                                                        //C-matrix
		public int problem_size;                                                                                              //n

		public double cost(individ individSrc)
		{
			double res = 0;
			for (int i = 0; i < individSrc.size; i++)
				for (int j = 0; j < individSrc.size; j++)
					res = res + Convert.ToDouble(stream[individSrc[i], individSrc[j]] * price[i, j] + position_cost[i, individSrc[j]]);
			return res;
		}

		private void base_init(int problem_size)
		{
			price = new int[problem_size, problem_size];
			stream = new int[problem_size, problem_size];
			position_cost = new int[problem_size, problem_size];
		}
		public info(string fname)                                                                            //general constructor from file
		{
			StreamReader file = new StreamReader(fname);
			string buf = file.ReadToEnd();
			file.Close();
			problem_size = Convert.ToInt32( buf.Substring(0, buf.IndexOf('\n')));
			buf = buf.Substring(buf.IndexOf('\n') + 1);

			base_init(problem_size);

			for (int i=0;i<problem_size;i++)
				for(int j=0;j<problem_size;j++)
				{
					if (j < problem_size - 1)
					{
						price[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf(' ')));
						buf = buf.Substring(buf.IndexOf(' ') + 1);
					}
					else
					{
						price[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
						buf = buf.Substring(buf.IndexOf('\n') + 1);
					}
				}
			if(buf.Length>problem_size*problem_size)
			{
				for (int i = 0; i < problem_size; i++)
					for (int j = 0; j < problem_size; j++)
					{
						if (j < problem_size - 1)
						{
							stream[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf(' ')));
							buf = buf.Substring(buf.IndexOf(' ') + 1);
						}
						else
						{
							stream[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
							buf = buf.Substring(buf.IndexOf('\n') + 1);
						}
					}
			}
			if (buf.Length > problem_size*problem_size)
			{
				for (int i = 0; i < problem_size; i++)
					for (int j = 0; j < problem_size; j++)
					{
						if (j < problem_size - 1)
						{
							position_cost[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf(' ')));
							buf = buf.Substring(buf.IndexOf(' ') + 1);
						}
						else
						{
							position_cost[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
							buf = buf.Substring(buf.IndexOf('\n') + 1);
						}
					}
			}
		}

		public info()
		{
			problem_size = 0;
			stream = null;
			price = null;
			position_cost = null;
		}
	}
}
