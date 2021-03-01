using System;
using System.IO;
using System.Xml;

namespace Util
{
    public interface ITabler
    {
        public bool addRow();
        public bool addCell(string style, string str, int mergeRight = 0, int mergeDown = 0);
        public bool addCells(string style, params string[] str);
        public bool addCellsNumber(string style, params double[] str);
        public bool Close();
    }

    public class TablerEmpty : ITabler
    {
        public TablerEmpty() { }
        public bool addRow() => false;
        public bool addCell(string style, string str, int mergeRight = 0, int mergeDown = 0) => false;
        public bool addCells(string style, params string[] str) => false;
        public bool addCellsNumber(string style, params double[] str) => false;
        public bool Close() => false;
    }

    public class Tabler : ITabler
    {
        XmlDocument m_doc;
        XmlNode m_table;
        XmlElement m_row;
        string m_pathResult;
        string[] s = { "reservedTAGTypeH0", "reservedTAGFontH0", "reservedTAGCharsetH0", "reservedTAGFamilyH0", "reservedTAGSizeH0", "reservedTAGBold", // 0-5
        "reservedTAGIDH0", "reservedTAGColorH0", "reservedTAGPatternH0", "reservedTAGStyleIDH0", "reservedTAGAutoFitHeightH0", "reservedTAGMergeAcrossH0", "reservedTAGMergeDownH0", // 6-12 
        "reservedTAGPositionH0","reservedTAGLineStyleH0","reservedTAGWeightH0","reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0","reservedTAGH0" // 13-20
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
            return buf.Replace(" xmlns=\"\"", "")
            .Replace(s[0], "ss:Type")
            .Replace(s[1], "ss:FontName")
            .Replace(s[2], "x:CharSet")
            .Replace(s[3], "x:Family")
            .Replace(s[4], "ss:Size")
            .Replace(s[5], "ss:Bold")
            .Replace(s[6], "ss:ID")
            .Replace(s[7], "ss:Color")
            .Replace(s[8], "ss:Pattern")
            .Replace(s[9], "ss:StyleID")
            .Replace(s[10], "ss:AutoFitHeight")
            .Replace(s[11], "ss:MergeAcross")
            .Replace(s[12], "ss:MergeDown")
            .Replace(s[13], "ss:Position")
            .Replace(s[14], "ss:LineStyle")
            .Replace(s[15], "ss:Weight");
            //.Replace(s[], );
        }


        public Tabler() { }
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
                styles.AppendChild(createStyle("simple", "#FBF7F7"));
                styles.AppendChild(createStyle("yellowColored", "#FFCFF0"));
                styles.AppendChild(createStyle("greenColored", "#8AFF15"));
                styles.AppendChild(createStyle("redColored", "#FF0000"));
                styles.AppendChild(createStyle("greyColored", "#EAEAEA"));

                XmlElement boldGreyStyle = createStyle("boldGrey", "#DFDFDF");
                boldGreyStyle.AppendChild(createFont("Calibri", "11", true));
                styles.AppendChild(boldGreyStyle);

                XmlElement boldStyle = createStyle("bold", "#FFFFF0");
                boldStyle.AppendChild(createFont("Calibri", "11", true));
                styles.AppendChild(boldStyle);
                
                XmlElement boldStyleLight = createStyle("boldLight", "#FFFF96");
                boldStyleLight.AppendChild(createFont("Calibri", "11", true));
                styles.AppendChild(boldStyleLight);

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
        private XmlElement createStyle(string id, string color, bool bBorder = true)
        {
            XmlElement elem = m_doc.CreateElement("Style");
            elem.SetAttribute(s[6],  id);
            if(color.Length > 0)
            {
                XmlElement attr = m_doc.CreateElement("Interior");
                attr.SetAttribute(s[7], color);
                attr.SetAttribute(s[8], "Solid");
                elem.AppendChild(attr);
            }
            if(bBorder)
            {
                XmlElement borders = m_doc.CreateElement("Borders");
                string[] sides = { "Bottom", "Left", "Right", "Top" };
                for(int i = 0; i < sides.Length; i++)
                {
                    XmlElement border = m_doc.CreateElement("Border");
                    border.SetAttribute(s[13], sides[i]);
                    border.SetAttribute(s[14], "Continuous");
                    border.SetAttribute(s[15], "1");
                    borders.AppendChild(border);
                }
                elem.AppendChild(borders);
            }
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
                double dP = 0;
                if(double.TryParse(val,out dP))
                    addCellsNumber(style, dP);
                else
                    addCell(style, val);
            }
            return true;
        }
        public bool addCellsNumber(string style, params double[] str)
        {
            foreach(double val in str)
            {
                XmlElement data = m_doc.CreateElement("Data");
                data.SetAttribute(s[0],  "Number");
                data.InnerText = val.ToString().Replace(',','.');
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
            StreamWriter wr = new StreamWriter(m_pathResult);
            wr.Write(filter(buf));
            wr.Close();
            return false;
        }
    }
}
