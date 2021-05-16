using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
	public partial class CQAPProblem
	{
		/// <summary>
		/// Export current problem to txt
		/// <para>if current problem is test generated to save param in file path-name</para>
		/// </summary>
		public void export_txt(CPermutation p = null, int omeg = -1, int z = -1)
		{
			msg("ExportTxt begin");
			string buf = size().ToString() + "\n";
			for(int i = 0; i < size(); i++)
			{
				for(int j = 0; j < size(); j++)
					buf = buf + getDist(i, j) + (j == size() - 1 ? "\n" : " ");
			}
			buf = buf + '\n';
			for(int i = 0; i < size(); i++)
			{	
				for(int j = 0; j < size(); j++)
					buf = buf + getFlow(i, j) + (j == size() - 1 ? "\n" : " ");
			}
			buf = buf + '\n';
			for(int i = 0; i < size(); i++)
			{
				for(int j = 0; j < size(); j++)
					buf = buf + getPCost(i, j) + (j == size() - 1 ? "\n" : " ");
			}
			if(p != null)
			{
				buf = buf + "\np={";
				for(int i = 0; i < size(); i++)
					buf = buf + p[i] + ", ";
				buf = buf + "}";
			}
			StreamWriter file;
			string pathFile = omeg == -1 ? $"ex_{size()}_{DateTime.Now}.bin" : pathFile = $"ex_{size()} {omeg} {z}_{DateTime.Now}.bin";
			if(!System.IO.File.Exists(pathFile))
				System.IO.File.Create(pathFile).Close();
			file = new StreamWriter(pathFile);
			file.WriteLine(buf);
			file.Close();
			msg("ExportTxt end");
		}
	}
}