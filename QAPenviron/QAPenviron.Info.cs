using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace QAPenviron
{
	public partial class Info
	{

		///<summary>Construct problem from file with formatting:<para>problem_size</para><para>D-matrix</para><para>F-matrix</para><para>C-matrix</para></summary>
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
            }
			if (buf != "")
			{
				int parseInd = 0;
				while(buf.Contains("  ")==true)
					buf.Replace("  ", " ");
				fparse=buf.Split('\n');
				problem_size = int.Parse(fparse[parseInd++]);
				//problem_size = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
				//buf = buf.Substring(buf.IndexOf('\n') + 1);
				base_init(problem_size);

				for (int i = 0; i < problem_size; i++)
					if (fparse[parseInd].Length > problem_size)
					{
						buf = fparse[parseInd++];
						string[] bufParse = buf.Split(' ');
						for (int j = 0; j < problem_size; j++)
							distance[i, j] = int.Parse(bufParse[j]);
					}
					else
						parseInd++;

				for (int i = 0; i < problem_size; i++)
					if (fparse[parseInd].Length > problem_size)
					{
						buf = fparse[parseInd++];
						string[] bufParse = buf.Split(' ');
						for (int j = 0; j < problem_size; j++)
							flow[i, j] = int.Parse(bufParse[j]);
					}
					else
						parseInd++;
				
				if(parseInd+problem_size/2 < fparse.Length)
					for (int i = 0; i < problem_size; i++)
						if (fparse[parseInd].Length > problem_size)
						{
							buf = fparse[parseInd++];
							string[] bufParse = buf.Split(' ');
							for (int j = 0; j < problem_size; j++)
								position_cost[i, j] = int.Parse(bufParse[j]);
						}
						else
							parseInd++;

				/*
				for (int i = 0; i < problem_size; i++)
					for (int j = 0; j < problem_size; j++)
					{
						if (j < problem_size - 1)
						{
							distance[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf(' ')));
							buf = buf.Substring(buf.IndexOf(' ') + 1);
						}
						else
						{
							distance[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
							buf = buf.Substring(buf.IndexOf('\n') + 1);
						}
					}
				if (buf.Length > problem_size * problem_size)
				{
					for (int i = 0; i < problem_size; i++)
						for (int j = 0; j < problem_size; j++)
						{
							if (j < problem_size - 1)
							{
								flow[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf(' ')));
								buf = buf.Substring(buf.IndexOf(' ') + 1);
							}
							else
							{
								flow[i, j] = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
								buf = buf.Substring(buf.IndexOf('\n') + 1);
							}
						}
				}
				if (buf.Length > problem_size * problem_size)
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
		public double calculate(Individ IndividSrc)
		{
			double res = 0;
			for (int i = 0; i < IndividSrc.size; i++)
				for (int j = 0; j < IndividSrc.size; j++)
					res = res + Convert.ToDouble(flow[IndividSrc[i], IndividSrc[j]] * distance[i, j] + position_cost[i, IndividSrc[j]]);
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
