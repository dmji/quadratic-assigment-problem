﻿using System.Collections.Generic;
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
            public SRow(double d, string[] aStr) { m_value = d; m_aCells = aStr; }
            public double m_value;
            public string[] m_aCells;
        }
        List<SRow> m_aRow;
        ITabler m_table;
        long m_nRowCounter;
        bool m_bColor;

        public CDelayedRow(ITabler table, bool bColor)
        {
            m_aRow = new List<SRow>();
            m_table = table;
            m_nRowCounter = table.RowCount();
            m_bColor = bColor;
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
            double minVal = m_aRow.Min(x => x.m_value);
            int min = m_aRow.FindIndex(x => x.m_value == minVal);
            
            // fill table rows
            for(int i = 0; i < m_aRow.Count; i++)
            {
                m_table.AddRow();
                m_table.AddCells(i == min && m_bColor ? "greenColored" : "simple", m_aRow[i].m_aCells);
            }
        }
    }
}

