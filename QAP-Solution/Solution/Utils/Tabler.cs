using System;
using System.IO;
using System.Xml;

namespace Solution.Util
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

        public class Tags
        {
            public static string
            eNone           = "reservedTAGH0",
            eType           = "reservedTAGTypeH0",
            eFontName       = "reservedTAGFontH0",
            eCharSet        = "reservedTAGCharsetH0",
            eFamily         = "reservedTAGFamilyH0",
            eSize           = "reservedTAGSizeH0",
            eBold           = "reservedTAGBold",
            eID             = "reservedTAGIDH0",
            eColor          = "reservedTAGColorH0",
            ePattern        = "reservedTAGPatternH0",
            eStyleID        = "reservedTAGStyleIDH0",
            eAutoFitHeight  = "reservedTAGAutoFitHeightH0",
            eMergeAcross    = "reservedTAGMergeAcrossH0",
            eMergeDown      = "reservedTAGMergeDownH0",
            ePosition       = "reservedTAGPositionH0",
            eLineStyle      = "reservedTAGLineStyleH0",
            eTAGWeight      = "reservedTAGWeightH0";
        };

        private string filter(string buf)
        {
            return buf.Replace(" xmlns=\"\"", "")
            .Replace(Tags.eType, "ss:Type")
            .Replace(Tags.eFontName, "ss:FontName")
            .Replace(Tags.eCharSet, "x:CharSet")
            .Replace(Tags.eFamily, "x:Family")
            .Replace(Tags.eSize, "ss:Size")
            .Replace(Tags.eBold, "ss:Bold")
            .Replace(Tags.eID, "ss:ID")
            .Replace(Tags.eColor, "ss:Color")
            .Replace(Tags.ePattern, "ss:Pattern")
            .Replace(Tags.eStyleID, "ss:StyleID")
            .Replace(Tags.eAutoFitHeight, "ss:AutoFitHeight")
            .Replace(Tags.eMergeAcross, "ss:MergeAcross")
            .Replace(Tags.eMergeDown, "ss:MergeDown")
            .Replace(Tags.ePosition, "ss:Position")
            .Replace(Tags.eLineStyle, "ss:LineStyle")
            .Replace(Tags.eTAGWeight, "ss:Weight");
            //.Replace(Tags, );
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
            fontElem.SetAttribute(Tags.eFontName, font);
            fontElem.SetAttribute(Tags.eCharSet, "204");
            fontElem.SetAttribute(Tags.eFamily, "Swiss");
            fontElem.SetAttribute(Tags.eSize, size);
            if(bBold)
                fontElem.SetAttribute(Tags.eBold, "1");
            return fontElem;
        }
        private XmlElement createStyle(string id, string color, bool bBorder = true)
        {
            XmlElement elem = m_doc.CreateElement("Style");
            elem.SetAttribute(Tags.eID,  id);
            if(color.Length > 0)
            {
                XmlElement attr = m_doc.CreateElement("Interior");
                attr.SetAttribute(Tags.eColor, color);
                attr.SetAttribute(Tags.ePattern, "Solid");
                elem.AppendChild(attr);
            }
            if(bBorder)
            {
                XmlElement borders = m_doc.CreateElement("Borders");
                string[] sides = { "Bottom", "Left", "Right", "Top" };
                for(int i = 0; i < sides.Length; i++)
                {
                    XmlElement border = m_doc.CreateElement("Border");
                    border.SetAttribute(Tags.ePosition, sides[i]);
                    border.SetAttribute(Tags.eLineStyle, "Continuous");
                    border.SetAttribute(Tags.eTAGWeight, "1");
                    borders.AppendChild(border);
                }
                elem.AppendChild(borders);
            }
            return elem;
        }
        public bool addRow()
        {
            m_row.SetAttribute(Tags.eAutoFitHeight,  "0");
            m_table.AppendChild(m_row);
            m_row = m_doc.CreateElement("Row");
            return true;
        }

        private XmlElement createCell(XmlElement data, string style)
        {
            XmlElement cell = m_doc.CreateElement("Cell");
            if(style.Length>0)
                cell.SetAttribute(Tags.eStyleID,  style);
            cell.AppendChild(data);
            return cell;
        }

        public bool addCell(string style, string str, int mergeRight = 0, int mergeDown = 0)
        {
            XmlElement data = m_doc.CreateElement("Data");
            data.SetAttribute(Tags.eType,  "String");
            data.InnerText = str;
            XmlElement cell = createCell(data, style);
            if(mergeRight>0)
                cell.SetAttribute(Tags.eMergeAcross,  mergeRight.ToString());
            if(mergeDown > 0)
                cell.SetAttribute(Tags.eMergeDown,  mergeDown.ToString());
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
                data.SetAttribute(Tags.eType,  "Number");
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
