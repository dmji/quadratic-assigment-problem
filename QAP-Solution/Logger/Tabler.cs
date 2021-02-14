using System;
using System.IO;
using System.Xml;

namespace Util
{
    public class Tabler
    {
        XmlDocument m_doc;
        XmlNode m_table;
        XmlElement m_row;
        string m_pathResult;
        string[] s = { "reservedTAGTypeH0", "reservedTAGFontH0", "reservedTAGCharsetH0", "reservedTAGFamilyH0", "reservedTAGSizeH0", "reservedTAGBold", // 0-5
        "reservedTAGIDH0", "reservedTAGColorH0", "reservedTAGPatternH0", "reservedTAGStyleIDH0", "reservedTAGAutoFitHeightH0", "reservedTAGMergeAcrossH0", "reservedTAGMergeDownH0", // 6-12 
        "reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0" // 13-20
        };

        public enum TAGS
        {
            eNone           = 0,
            eType           = 1,
            eFontName       = 2,
            eCharSet        = 3,
            eFamily         = 4,
            eSize           = 5,
            eBold           = 6,
            eID             = 7,
            eColor          = 8,
            ePattern        = 9,
            eStyleID        = 10,
            eAutoFitHeight  = 11,
            eMergeAcross    = 12,
            eMergeDown      = 13
        };

        private string filter(string buf)
        {
            buf = buf.Replace(" xmlns=\"\"", "");
            buf = buf.Replace(s[0], "ss:Type");
            buf = buf.Replace(s[1], "ss:FontName");
            buf = buf.Replace(s[2], "x:CharSet");
            buf = buf.Replace(s[3], "x:Family");
            buf = buf.Replace(s[4], "ss:Size");
            buf = buf.Replace(s[5], "ss:Bold");
            buf = buf.Replace(s[6], "ss:ID");
            buf = buf.Replace(s[7], "ss:Color");
            buf = buf.Replace(s[8], "ss:Pattern");
            buf = buf.Replace(s[9], "ss:StyleID");
            buf = buf.Replace(s[10], "ss:AutoFitHeight");e
            buf = buf.Replace(s[11], "ss:MergeAcross");
            buf = buf.Replace(s[12], "ss:MergeDown");
            //buf = buf.Replace(s[], );
            return buf;
        }


        public Tabler(string path, string sAlg, string pathTemplate)
        {
            if(path.Length > 0 && sAlg.Length > 0 && pathTemplate.Length > 0)
            {
                string time = DateTime.Now.ToString().Replace(":", "_").Replace(" ", "_").Replace(".", "_");
                m_pathResult = $"{path}{sAlg}_{time}_results.~.xml";

                m_doc = new XmlDocument();
                StreamReader template = new StreamReader(pathTemplate);
                m_doc.LoadXml(template.ReadToEnd());
                template.Close();
                m_table = m_doc.GetElementsByTagName("Table")[0];

                XmlNode styles = m_doc.GetElementsByTagName("Styles")[0];
                styles.AppendChild(createStyle("yellowColored", "#FFCFF0"));
                styles.AppendChild(createStyle("greenColored", "#009933"));
                styles.AppendChild(createStyle("redColored", "#FF0000"));

                XmlElement boldStyle = createStyle("bold", "#FFFFF0");
                boldStyle.AppendChild(createFont("Calibri", "11", true));
                styles.AppendChild(boldStyle);

                m_row = m_doc.CreateElement("Row");
            }
        }
        private XmlElement createFont(string font, string size, bool bBold)
        {
            XmlElement fontElem = m_doc.CreateElement("Font");
            fontElem.SetAttribute(s[1], font);
            fontElem.SetAttribute(s[2], "204");
            fontElem.SetAttribute(s[3], "Swiss");
            fontElem.SetAttribute(s[4], size);
            if(bBold)
                fontElem.SetAttribute(s[5], "1");
            return fontElem;
        }
        private XmlElement createStyle(string id, string color)
        {
            XmlElement elem = m_doc.CreateElement("Style");
            elem.SetAttribute(s[6],  id);
            XmlElement attr = m_doc.CreateElement("Interior");
            attr.SetAttribute(s[7], color);
            attr.SetAttribute(s[8], "Solid");
            elem.AppendChild(attr);
            return elem;
        }
        public bool addRow()
        {
            m_row.SetAttribute(s[10],  "0");
            m_table.AppendChild(m_row);
            m_row = m_doc.CreateElement("Row");
            return true;
        }

        private XmlElement createCell(XmlElement data, string style)
        {
            XmlElement cell = m_doc.CreateElement("Cell");
            if(style.Length>0)
                cell.SetAttribute(s[9],  style);
            cell.AppendChild(data);
            return cell;
        }

        public bool addCell(string style, string str, int mergeRight = 0, int mergeDown = 0)
        {
            XmlElement data = m_doc.CreateElement("Data");
            data.SetAttribute(s[0],  "String");
            data.InnerText = str;
            XmlElement cell = createCell(data, style);
            if(mergeRight>0)
                cell.SetAttribute(s[11],  mergeRight.ToString());
            if(mergeDown > 0)
                cell.SetAttribute(s[12],  mergeDown.ToString());
            m_row.AppendChild(cell);
            return true;
        }
        public bool addCells(string style, params string[] str)
        {
            foreach(string val in str)
            {
                XmlElement data = m_doc.CreateElement("Data");
                data.SetAttribute(s[0],  "String");
                data.InnerText = val;
                m_row.AppendChild(createCell(data, style));
            }
            return true;
        }
        public bool addCellsNumber(string style, params double[] str)
        {
            foreach(double val in str)
            {
                XmlElement data = m_doc.CreateElement("Data");
                data.SetAttribute(s[0],  "Number");
                data.InnerText = val.ToString();
                m_row.AppendChild(createCell(data, style));
            }
            return true;
        }

        public bool Close()
        {
            m_table.AppendChild(m_row);
            m_doc.Save(m_pathResult);
            StreamReader rd = new StreamReader(m_pathResult);
            string buf = rd.ReadToEnd();
            rd.Close();
            StreamWriter wr = new StreamWriter(m_pathResult.Replace(".xml", ".xlsx"));
            wr.Write(filter(buf));
            wr.Close();
            return false;
        }
    }
}
