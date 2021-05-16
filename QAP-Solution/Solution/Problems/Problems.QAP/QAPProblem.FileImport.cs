using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP aData.</summary>
	public partial class CQAPProblem
	{
		///<summary>Construct problem from file with formatting:<para>size()</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
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
				}
				init(ushort.Parse(buf.Substring(0, buf.IndexOf('\n'))));
				buf = buf.Substring(buf.IndexOf('\n') + 1);
				aData = buf.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
				if(aData.Length > 1 && aData.Length < 3)
				{
					for(int i = 0; i < aData.Length; i++)
					{
						aData[i] = aData[i].Replace('\n', ' ');
						aData[i] = aData[i].Trim(' ');
					}
					for(int iData = 0; iData < aData.Length; iData++)
					{
						if(aData[iData].Length == 0)
							continue;
						int ind = 0;
						string[] pData = aData[iData].Split(' ', StringSplitOptions.RemoveEmptyEntries);
						for(int i = 0; i < size(); i++)
						{
							for(int j = 0; j < size(); j++)
							{
								switch(iData)
								{
									case 0:
										setFlow(int.Parse(pData[ind++]), i, j);
										break;
									case 1:
										setDist(int.Parse(pData[ind++]), i, j);
										break;
									case 2:
										setPCost(int.Parse(pData[ind++]), i, j);
										break;
									default:
										break;
								}
							}
						}
					}
				}
				else
                {
					buf = buf.Replace("\n\n", "\n");
					buf = buf.Replace("\n", " ");
					string[] data = buf.Split(' ', StringSplitOptions.RemoveEmptyEntries);
					int n = Convert.ToInt32(Math.Pow(size(), 2)), ind=0;
					if(data.Length >= n)
					{
						for(int i = 0; i < size(); i++)
						{
							for(int j=0;j<size();j++)
								setFlow(int.Parse(data[ind++]),i,j);
						}
						if(data.Length >= 2*n)
                        {
							for(int i = 0; i < size(); i++)
							{
								for(int j = 0; j < size(); j++)
									setDist(int.Parse(data[ind++]),i,j);
							}
							if(data.Length >= 3*n)
							{
								for(int i = 0; i < size(); i++)
								{
									for(int j = 0; j < size(); j++)
										setPCost(int.Parse(data[ind++]), i, j);
								}
							}
						}
					}
				}
			}
			msg($"Finish importing problem from file: {fname}");
		}
	}
}
