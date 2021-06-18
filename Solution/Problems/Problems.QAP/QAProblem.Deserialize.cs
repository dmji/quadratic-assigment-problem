using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP aData.</summary>
	public partial class CQAProblem
	{
		///<summary>Construct problem from file with formatting:<para>size()</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public override bool Deserialize(string fname)
		{
			if(m_log != null)
				Msg($"Start importing problem from file: {fname}");
			string buf = new TestSystem.CFile(fname).ReadToEnd();
			string[] aData;
			if(buf != "")
			{
				{
					while(buf.Contains("  "))
						buf = buf.Replace("  ", " ");
					while(buf.Contains("\n "))
						buf = buf.Replace("\n ", "\n");
					buf = buf.Replace("\r\n", "\n");
				}
				Init(ushort.Parse(buf.Substring(0, buf.IndexOf('\n'))));
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
						for(int i = 0; i < Size(); i++)
						{
							for(int j = 0; j < Size(); j++)
							{
								switch(iData)
								{
									case 0:
										SetFlow(int.Parse(pData[ind++]), i, j);
										break;
									case 1:
										SetDist(int.Parse(pData[ind++]), i, j);
										break;
									case 2:
										SetPCost(int.Parse(pData[ind++]), i, j);
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
					int n = Convert.ToInt32(Math.Pow(Size(), 2)), ind=0;
					if(data.Length >= n)
					{
						for(int i = 0; i < Size(); i++)
						{
							for(int j=0;j<Size();j++)
								SetFlow(int.Parse(data[ind++]),i,j);
						}
						if(data.Length >= 2*n)
                        {
							for(int i = 0; i < Size(); i++)
							{
								for(int j = 0; j < Size(); j++)
									SetDist(int.Parse(data[ind++]),i,j);
							}
							if(data.Length >= 3*n)
							{
								for(int i = 0; i < Size(); i++)
								{
									for(int j = 0; j < Size(); j++)
										SetPCost(int.Parse(data[ind++]), i, j);
								}
							}
						}
					}
				}
			}
			if(m_log != null)
				Msg($"Finish importing problem from file: {fname}");
			return true;
		}
	}
}
