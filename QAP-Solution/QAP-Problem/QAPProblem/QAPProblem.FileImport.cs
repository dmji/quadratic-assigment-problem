using System;
using System.IO;
using System.Collections.Generic;

namespace Problem
{
	/// <summary>Class <c>Info</c> is all-in one QAP aData.</summary>
	public partial class CQAPProblem
	{
		///<summary>Construct problem from file with formatting:<para>m_ProblemSize</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public CQAPProblem(string fname) : this()
		{
			msg($"Start importing problem from file: {fname}");
			StreamReader file;
			string buf = "";
			string[] aData;
			try
			{
				file = new StreamReader(fname);
				buf = file.ReadToEnd();
				file.Close();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				msg($"Corruption importing. Exception: {ex.Message}");
				throw ex;
			}
			if(buf != "")
			{
				{
					while(buf.Contains("  "))
						buf = buf.Replace("  ", " ");
					while(buf.Contains("\n "))
						buf = buf.Replace("\n ", "\n");
					buf = buf.Replace("\r\n", "\n");
					//while(buf.Contains("\n\n"))
					//	buf = buf.Replace("\n\n", "\n");
				}
				m_ProblemSize = ushort.Parse(buf.Substring(0, buf.IndexOf('\n')));
				aData = buf.Substring(buf.IndexOf('\n') + 1).Split("\n\n");
				for(int i = 0; i < aData.Length; i++)
				{
					aData[i] = aData[i].Replace('\n', ' ');
					aData[i] = aData[i].Trim(' ');
				}

				init(m_ProblemSize);

				for(int iData = 0; iData < aData.Length; iData++)
				{
					if(aData[iData].Length == 0)
						continue;
					int ind = 0;
					string[] pData = aData[iData].Split(' ', StringSplitOptions.RemoveEmptyEntries);
					for(int i = 0; i < m_ProblemSize; i++)
					{
						for(int j = 0; j < m_ProblemSize; j++)
						{
							switch(iData)
							{
								case 0:
									m_tFlow[i, j] = int.Parse(pData[ind++]);
									break;
								case 1:
									m_tDistance[i, j] = int.Parse(pData[ind++]);
									break;
								case 2:
									m_tPositionCost[i, j] = int.Parse(pData[ind++]);
									break;
								default:
									break;
							}
						}
					}
				}
			}
			msg($"Finish importing problem from file: {fname}");
		}
	}
}
