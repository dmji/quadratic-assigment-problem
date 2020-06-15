using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QAPenviron
{
	public partial class Info
	{

		///<summary>Construct problem from file with formatting:<para>problem_size</para><para>D-matrix</para><para>F-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public Info(string fname)
		{
			StreamReader file = new StreamReader(fname);
			string buf = file.ReadToEnd();
			file.Close();
			problem_size = Convert.ToInt32(buf.Substring(0, buf.IndexOf('\n')));
			buf = buf.Substring(buf.IndexOf('\n') + 1);

			base_init(problem_size);

			for (int i = 0; i < problem_size; i++)
				for (int j = 0; j < problem_size; j++)
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
			if (buf.Length > problem_size * problem_size)
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
						buf = buf + price[i, j] + "\n";
					else
						buf = buf + price[i, j] + " ";
				}
			buf = buf + '\n';
			for (int i = 0; i < problem_size; i++)
				for (int j = 0; j < problem_size; j++)
				{
					if (j == problem_size - 1)
						buf = buf + stream[i, j] + "\n";
					else
						buf = buf + stream[i, j] + " ";
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
				file = new StreamWriter("ex_" + problem_size + ".txt");
			else
				file = new StreamWriter("ex_" + problem_size + " " + omeg + " " + z + ".txt");
			file.WriteLine(buf);
			file.Close();
		}
		/// <summary>
		/// calculate criterion
		/// </summary>
		/// <param name="IndividSrc">premutation to calculate</param>
		/// <returns>double value</returns>
		public double cost(Individ IndividSrc)
		{
			double res = 0;
			for (int i = 0; i < IndividSrc.size; i++)
				for (int j = 0; j < IndividSrc.size; j++)
					res = res + Convert.ToDouble(stream[IndividSrc[i], IndividSrc[j]] * price[i, j] + position_cost[i, IndividSrc[j]]);
			return res;
		}
	}
}
