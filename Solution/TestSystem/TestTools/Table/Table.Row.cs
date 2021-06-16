using System;
using System.IO;
using System.Xml;

namespace TestSystem
{
    public interface IRow
    {
        bool AddCell(string style, string str, int mergeRight = 0, int mergeDown = 0);
        bool AddCells(string style, params string[] str);
        bool AddCellsNumber(string style, params double[] str);
        long GetIndex();
    }

    public class CRowExcel : IRow
    {
        XmlDocument m_doc;
        XmlElement m_row;
        long m_index;
        public long GetIndex() => m_index;
        public CRowExcel(XmlDocument doc, XmlNode table, long index) 
        {
            m_index = index;
            m_doc = doc;
            m_row = m_doc.CreateElement("Row");
            m_row.SetAttribute(CTablerExcel.Tags.eAutoFitHeight, "0");
            table.AppendChild(m_row);
        }

        public bool AddCell(string style, string str, int mergeRight = 0, int mergeDown = 0)
        {
            XmlElement data = m_doc.CreateElement("Data");
            XmlElement cell;
            {
                cell = CreateCell(style, data);
                data.SetAttribute(CTablerExcel.Tags.eType, "String");
                data.InnerText = str;   
            }
            if(mergeRight > 0)
                cell.SetAttribute(CTablerExcel.Tags.eMergeAcross, mergeRight.ToString());
            if(mergeDown > 0)
                cell.SetAttribute(CTablerExcel.Tags.eMergeDown, mergeDown.ToString());
            m_row.AppendChild(cell);
            return true;
        }

        public bool AddCells(string style, params string[] str)
        {
            foreach(string val in str)
            {
                if(val.Length > 0)
                {
                    double dP = 0;
                    if(double.TryParse(val, out dP))
                        AddCellsNumber(style, dP);
                    else
                        AddCell(style, val);
                }
            }
            return true;
        }
        public bool AddCellsNumber(string style, params double[] str)
        {
            foreach(double val in str)
            {
                XmlElement data = m_doc.CreateElement("Data");
                data.SetAttribute(CTablerExcel.Tags.eType, "Number");
                data.InnerText = val.ToString().Replace(',', '.');
                m_row.AppendChild(CreateCell(style, data));
            }
            return true;
        }

        private XmlElement CreateCell(string style, XmlElement data = null)
        {
            XmlElement cell = m_doc.CreateElement("Cell");
            if(style.Length>0)
                cell.SetAttribute(CTablerExcel.Tags.eStyleID,  style);
            if(data != null)
            cell.AppendChild(data);
            return cell;
        }
    }
}
