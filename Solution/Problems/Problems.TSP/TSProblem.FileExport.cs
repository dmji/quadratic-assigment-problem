using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
	public partial class CTSProblem
	{
		/// <summary>
		/// Export current problem to txt
		/// <para>if current problem is test generated to save param in file path-name</para>
		/// </summary>
		public void Serielize(CPermutation p = null, int omeg = -1, int z = -1)
		{
			Msg("ExportTxt begin");
			string buf = Size().ToString() + "\n";
			for(int i = 0; i < Size(); i++)
			{
				for(int j = 0; j < Size(); j++)
					buf = buf + GetDist(i, j) + (j == Size() - 1 ? "\n" : " ");
			}
			buf = buf + '\n';
			for(int i = 0; i < Size(); i++)
			{	
				for(int j = 0; j < Size(); j++)
					buf = buf + GetFlow(i, j) + (j == Size() - 1 ? "\n" : " ");
			}
			buf = buf + '\n';
			for(int i = 0; i < Size(); i++)
			{
				for(int j = 0; j < Size(); j++)
					buf = buf + GetPCost(i, j) + (j == Size() - 1 ? "\n" : " ");
			}
			if(p != null)
			{
				buf = buf + "\np={";
				for(int i = 0; i < Size(); i++)
					buf = buf + p[i] + ", ";
				buf = buf + "}";
			}
			StreamWriter file;
			string pathFile = omeg == -1 ? $"ex_{Size()}_{DateTime.Now}.bin" : pathFile = $"ex_{Size()} {omeg} {z}_{DateTime.Now}.bin";
			if(!System.IO.File.Exists(pathFile))
				System.IO.File.Create(pathFile).Close();
			file = new StreamWriter(pathFile);
			file.WriteLine(buf);
			file.Close();
			Msg("ExportTxt end");
		}
	}
}