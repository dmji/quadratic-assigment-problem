using System.Collections.Generic;
using System.Linq;

namespace TestSystem
{
    public interface IDelayedRow
    {
        long AddRow(double criterio, params string[] aStr);
        void Release();
    }

    public class CDelayedRow : IDelayedRow
    {
        public struct SRow
        {
            public SRow(double d, string[] aStr) { value = d; aCells = aStr; }
            public double value;
            public string[] aCells;
        }
        List<SRow> m_aRow;
        ITabler m_table;
        long m_nRowCounter;

        public CDelayedRow(ITabler table)
        {
            m_aRow = new List<SRow>();
            m_table = table;
            m_nRowCounter = table.nRows();
        }
        public long AddRow(double criterio, params string[] aStr) 
        { 
            m_aRow.Add(new SRow(criterio, aStr));
            return m_nRowCounter + m_aRow.Count;
        }
        public void Release()
        {
            if(m_aRow.Count == 0)
                return;

            // find row with minimal value
            double minVal = m_aRow.Min(x => x.value);
            int min = m_aRow.FindIndex(x => x.value == minVal);
            
            // fill table rows
            for(int i = 0; i < m_aRow.Count; i++)
            {
                m_table.addRow();
                m_table.addCells(i == min ? "greenColored" : "simple", m_aRow[i].aCells);
            }
        }
    }
}

