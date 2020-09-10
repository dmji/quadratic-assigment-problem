using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace QAPenviron
{
	public partial class Info
	{

		///<summary>Construct problem from file with formatting:<para>problem_size</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public Info(string fname)
		{
			StreamReader file;
			string buf="";
			string[] fparse;
			try
			{
				file = new StreamReader(fname);
				buf = file.ReadToEnd();
				file.Close();
			}
			catch(Exception ex)
            {
				Console.WriteLine(ex.Message);
				throw ex;
            }
			if (buf != "")
			{
				int parseInd = 0;
				while(buf.Contains("  ")==true)
					buf = buf.Replace("  ", " ");
				while (buf.Contains("\n ") == true)
					buf = buf.Replace("\n ", "\n");
				buf = buf.Replace("\r\n", "\n");
				problem_size = int.Parse(buf.Substring(0,buf.IndexOf('\n')));
				fparse = buf.Substring(buf.IndexOf('\n')+1).Split("\n\n");
				for (int i = 0; i < fparse.Length; i++)
					fparse[i] = fparse[i].Replace('\n', ' ');

				base_init(problem_size);
				for (int arr = 0; arr < fparse.Length; arr++)
				{
					int ind = 0;
					string[] bufParse = fparse[arr].Split(' ');
					for (int i = 0; i < problem_size; i++)
						for (int j = 0; j < problem_size; j++)
						{
							switch (arr)
							{
								case 0:
									flow[i, j] = int.Parse(bufParse[ind++]);
									break;
								case 1:
									distance[i, j] = int.Parse(bufParse[ind++]);
									break;
								case 2:
									position_cost[i, j] = int.Parse(bufParse[ind++]);
									break;
							}
						}
				}


				/*
				for (int i = 0; i < problem_size; i++)
					if (fparse[parseInd].Length > problem_size)
					{
						buf = fparse[parseInd++];
						string[] bufParse = buf.Split(' ');
						for (int j = 0; j < problem_size; j++)
							distance[i, j] = int.Parse(bufParse[j]);
					}
					else
					{
						i--;
						parseInd++;
					}

				if(parseInd+problem_size-1< fparse.Length)
					for (int i = 0; i < problem_size; i++)
						if (fparse[parseInd].Length > problem_size)
						{
							buf = fparse[parseInd++];
							string[] bufParse = buf.Split(' ');
							for (int j = 0; j < problem_size; j++)
								position_cost[i, j] = int.Parse(bufParse[j]);
						}
						else
						{
							i--;
							parseInd++;
						}
				*/
			}
		}
		/// <summary>
		/// Export current problem to txt
		/// <para>if current problem is test generated to save param in file path-name</para>
		/// </summary>
		public void export_txt(Individ p = null, int omeg = -1, int z = -1)
		{
			string buf = problem_size.ToString() + "\n";
			for (int i = 0; i < problem_size; i++)
				for (int j = 0; j < problem_size; j++)
				{
					if (j == problem_size - 1)
						buf = buf + distance[i, j] + "\n";
					else
						buf = buf + distance[i, j] + " ";
				}
			buf = buf + '\n';
			for (int i = 0; i < problem_size; i++)
				for (int j = 0; j < problem_size; j++)
				{
					if (j == problem_size - 1)
						buf = buf + flow[i, j] + "\n";
					else
						buf = buf + flow[i, j] + " ";
				}
			buf = buf + '\n';
			for (int i = 0; i < problem_size; i++)
				for (int j = 0; j < problem_size; j++)
				{
					if (j == problem_size - 1)
						buf = buf + position_cost[i, j] + "\n";
					else
						buf = buf + position_cost[i, j] + " ";
				}
			if (p != null)
			{
				buf = buf + "\np={";
				for (int i = 0; i < problem_size; i++)
					buf = buf + p[i] + ", ";
				buf = buf + "}";
			}
			StreamWriter file;
			if (omeg == -1)
				file = new StreamWriter("ex_" + problem_size + "_" + DateTime.Now + ".txt");
			else
				file = new StreamWriter("ex_" + problem_size + " " + omeg + " " + z + "_" + DateTime.Now + ".txt");
			file.WriteLine(buf);
			file.Close();
		}

		/// <summary>calculate criterion</summary>
		/// <param name="IndividSrc">premutation to calculate</param>
		/// <returns>double value</returns>
		public int calculate(Individ IndividSrc)
		{
			return calculate(IndividSrc.GetList);
		}

		public int calculate(List<int> IndividSrc)
		{
			int res = 0;
			for (int i = 0; i < IndividSrc.Count; i++)
				for (int j = 0; j < IndividSrc.Count; j++)
					res = res + Convert.ToInt32(flow[IndividSrc[i], IndividSrc[j]] * distance[i, j] + position_cost[i, IndividSrc[j]]);
			return res;
		}

		protected int individComparision(Individ x, Individ y) // decrease -> y-x // increase -> x-y
		{
			if (x == null)
			{
				if (y == null)
					return 0;
				else
					return -1;
			}
			else
			{
				if (y == null)
					return 1;
				else
				{
					if (calculate(x) == calculate(y))
						return 0;
					else if (calculate(x) > calculate(y))
						return 1;
					else
						return -1;
				}
			}
		}
	}
}