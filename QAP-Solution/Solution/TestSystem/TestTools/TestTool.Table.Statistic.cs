using System;
using System.Collections.Generic;

using System.IO;
using System.Xml;


namespace TestSystem
{
    public partial class CTestStatistic
    {
        public struct SRangeSize
        {
            public SRangeSize(int size) 
            { 
                m_size = size;
                m_range = "";
            }
            public int m_size;
            public string m_range;

            public void addResult(long row, long cell)
            {
                m_range += (m_range.Length > 0 ? ";" : "") + $"R{row}C{cell}";
            }
        }
        public struct SStatCollector
        {
            public string m_name { get; set; }
            public List<SRangeSize> m_aRange { get; set; }
            
            public SStatCollector(string name)
            {
                m_name = name;
                m_aRange = new List<SRangeSize>();
            }

            public string getRange()
            {
                string result = "";
                foreach(SRangeSize r in m_aRange)
                    result += (result.Length > 0 ? ";" : "") + r.m_range;
                return result;
            }
        }

        List<SStatCollector> m_aStats;
        public CTestStatistic()
        {
            m_aStats = new List<SStatCollector>();
        }

        public void releaseOptStat(ITabler tbl)
        {
            if(m_aStats.Count > 0)
            {
                tbl.addRow();
                tbl.addRow();
                tbl.addCells("boldGrey", "", "Avg Error, %");
                tbl.addCell("boldGrey", "Tabbling info, avaraged by size", m_aStats[0].m_aRange.Count - 1);
                tbl.addRow();
                tbl.addCells("greyColored", "sizes", "total");

                m_aStats[0].m_aRange.Sort((SRangeSize f, SRangeSize s) => { if(f.m_size > s.m_size) return 1; else if(f.m_size == s.m_size) return 0; else return -1; });
                foreach(SRangeSize b in m_aStats[0].m_aRange)
                    tbl.addCellsNumber("greyColored", b.m_size);
                tbl.addRow();

                foreach(var a in m_aStats)
                {
                    a.m_aRange.Sort((SRangeSize f, SRangeSize s) => { if(f.m_size > s.m_size) return 1; else if(f.m_size == s.m_size) return 0; else return -1; });

                    string allRange = a.getRange();
                    tbl.addCells("greyColored", a.m_name, $"=AVERAGE({allRange})");
                    foreach(SRangeSize b in a.m_aRange)
                        tbl.addCell("greyColored", $"=AVERAGE({b.m_range})");
                    tbl.addRow();
                }
                m_aStats.Clear();
            }
        }

        public void addStat(string name, int size, long row, long cell)
        {
            bool bOk = false;
            foreach(SStatCollector a in m_aStats)
            {
                if(a.m_name == name)
                {
                    bool bInc = false;
                    foreach(SRangeSize b in a.m_aRange)
                    {
                        if(b.m_size == size)
                        {
                            b.addResult(row, cell);
                            bInc = true;
                            break;
                        }
                    }
                    if(!bInc)
                    {
                        SRangeSize range = new SRangeSize(size);
                        range.addResult(row, cell);
                        a.m_aRange.Add(range);
                    }
                    bOk = true;
                    break;
                }
            }
            if(!bOk)
            {
                SStatCollector stat = new SStatCollector(name);
                SRangeSize range = new SRangeSize(size);
                range.addResult(row, cell);
                stat.m_aRange.Add(range);
                m_aStats.Add(stat);
            }
        }
    }
}
