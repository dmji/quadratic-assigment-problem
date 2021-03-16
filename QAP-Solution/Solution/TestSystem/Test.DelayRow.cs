using System.Collections.Generic;

namespace Solution
{
    public partial class TestSystem
    {
        partial class TestInfo
        {
            void resetStats()
            {
                m_aCriterio = new List<double>();
                m_aCells = new List<string[]>();
            }

            List<double> m_aCriterio;
            List<string[]> m_aCells;
            public void AddRow(double criterio, params string[] aStr)
            {
                m_aCriterio.Add(criterio);
                m_aCells.Add(aStr);
            }

            public void RelaseRow(Util.ITabler tbl)
            {
                if(m_aCells.Count > 0)
                {
                    int min = 0;
                    double minVal = m_aCriterio[0];
                    for(int i = 1; i < m_aCriterio.Count; i++)
                    {
                        if(m_aCriterio[i] < minVal)
                        {
                            min = i;
                            minVal = m_aCriterio[i];
                        }
                    }
                    if(minVal == -1)
                        min = -1;
                    for(int i = 0; i < m_aCells.Count; i++)
                    {
                        tbl.addRow();
                        if(i == min)
                            tbl.addCells("greenColored", m_aCells[i]);
                        else
                            tbl.addCells("simple", m_aCells[i]);
                    }

                    m_aCriterio.Clear();
                    m_aCells.Clear();
                }
            }
        }
    }
}
