using System;
using System.Collections.Generic;
using Problem;
using System.IO;
using System.Xml;


namespace Algorithms
{
    public partial class TestSystem
    {
        partial class OptionsStatistic
        {
            public struct result
            {
                public int m_size { get; set; }
                public int m_count { get; set; }
                public double m_result { get; set; }

                public result(int size, double result)
                {
                    m_size = size;
                    m_result = result;
                    m_count = 1;
                }

                public void addResult(double a)
                {
                    m_count++;
                    m_result += a;
                }

                public double getResultAvg() => m_result / m_count;
            }

            public struct optStat
            {
                public string m_name { get; set; }
                public List<result> m_aResults { get; set; }

                public optStat(string name)
                {
                    m_name = name;
                    m_aResults = new List<result>();
                }
            }

            public void releaseOptStat(Util.ITabler tbl)
            {
                if(m_aOptStats.Count > 0)
                {
                    tbl.addRow();
                    tbl.addRow();
                    tbl.addCells("boldGrey", "", "Avg Error, %");
                    tbl.addCell("boldGrey", "Tabbling info, avaraged by size", m_aOptStats[0].m_aResults.Count-1);
                    tbl.addRow();
                    foreach(optStat a in m_aOptStats)
                    {
                        a.m_aResults.Sort((result f, result s) => { if(f.m_size > s.m_size) return 1; else if(f.m_size == s.m_size) return 0; else return -1; });
                        double avgRes = 0;
                        foreach(result b in a.m_aResults)
                            avgRes += b.getResultAvg();
                        avgRes /= a.m_aResults.Count;
                        tbl.addCells("greyColored", a.m_name+" sizes", avgRes.ToString());
                        foreach(result b in a.m_aResults)
                            tbl.addCellsNumber("greyColored", b.m_size);
                        tbl.addRow();
                        tbl.addCells("greyColored", a.m_name+" vales", "");
                        foreach(result b in a.m_aResults)
                            tbl.addCellsNumber("greyColored", b.getResultAvg());
                        tbl.addRow();
                    }
                    m_aOptStats.Clear();    
                }
            }

            public void addStat(string name, int size, double result)
            {
                bool bOk = false;
                foreach(optStat a in m_aOptStats)
                {
                    if(a.m_name == name)
                    {
                        bool bInc = false;
                        foreach(result b in a.m_aResults)
                        {
                            if(b.m_size == size)
                            {
                                b.addResult(result);
                                bInc = true;
                                break;
                            }
                        }
                        if(!bInc)
                            a.m_aResults.Add(new result(size, result));
                        bOk = true;
                        break;
                    }
                }
                if(!bOk)
                {
                    m_aOptStats.Add(new optStat(name));
                    m_aOptStats[m_aOptStats.Count - 1].m_aResults.Add(new result(size, result));
                }
            }


            List<optStat> m_aOptStats;
            public OptionsStatistic()
            {
                m_aOptStats = new List<optStat>();
            }

        }
    }
}
