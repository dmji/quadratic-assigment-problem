using System;
using System.IO;
using System.Xml;

namespace TestSystem
{
    public interface ITabler : Solution.IClose
    {
        long AddRow();
        bool AddCell(string style, string str, int mergeRight = 0, int mergeDown = 0);
        bool AddCells(string style, params string[] str);
        bool AddCellsNumber(string style, params double[] str);
        long RowCount();
    }

    public class CTablerEmpty : ITabler
    {
        public CTablerEmpty() { }
        public long AddRow() => 0;
        public bool AddCell(string style, string str, int mergeRight = 0, int mergeDown = 0) => false;
        public bool AddCells(string style, params string[] str) => false;
        public bool AddCellsNumber(string style, params double[] str) => false;

        public bool Close() => false;
        public long RowCount() => 0;
    }

    public class CTablerExcel : ITabler
    {
        XmlDocument m_doc;
        XmlNode m_table;
        XmlElement m_row;
        string m_pathResult;
        long m_nRowCounter;

        public long RowCount() => m_nRowCounter;

        public struct Styles
        {
            public static string
                eStyleSimple            = "reservedStyleSimple",
                eStyleYellow            = "reservedStyleYellow",
                eStyleGreen             = "reservedStyleGreen",
                eStyleRed               = "reservedStyleRed",
                eStyleGrey              = "reservedStyleGrey",
                eStyleGreyBold          = "reservedStyleGreyBold",
                eStyleSimpleBold        = "reservedStyleSimpleBold",
                eStyleSimpleBoldLight   = "reservedStyleSimpleBoldLight";
        }

        public struct Tags
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
            eTAGWeight      = "reservedTAGWeightH0",
            eFormula        = "reservedTAGFormula";

            public static string Filter(string buf)
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
                .Replace(Tags.eTAGWeight, "ss:Weight")
                .Replace(Tags.eFormula, "ss:Formula");
                //.Replace(Tags, );
            }
        };

        public CTablerExcel() { m_nRowCounter = 1; }
        public CTablerExcel(string path, string sAlg, string pathTemplate)
        {
            if(path.Length > 0 && sAlg.Length > 0 && pathTemplate.Length > 0)
            {
                string time = DateTime.Now.ToString().Replace(":", "_").Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_");
                m_pathResult = $"{path}{sAlg}_results.~.xml";

                m_doc = new XmlDocument();
                StreamReader template = new StreamReader(pathTemplate);
                m_doc.LoadXml(template.ReadToEnd());
                template.Close();
                m_table = m_doc.GetElementsByTagName("Table")[0];

                XmlNode styles = m_doc.GetElementsByTagName("Styles")[0];
                styles.AppendChild(CreateStyle(Styles.eStyleSimple, "#FBF7F7"));
                styles.AppendChild(CreateStyle(Styles.eStyleYellow, "#FFCFF0"));
                styles.AppendChild(CreateStyle(Styles.eStyleGreen, "#8AFF15"));
                styles.AppendChild(CreateStyle(Styles.eStyleRed, "#FF0000"));
                styles.AppendChild(CreateStyle(Styles.eStyleGrey, "#EAEAEA"));

                XmlElement boldGreyStyle = CreateStyle(Styles.eStyleGreyBold, "#DFDFDF");
                boldGreyStyle.AppendChild(CreateFont("Calibri", "11", true));
                styles.AppendChild(boldGreyStyle);

                XmlElement boldStyle = CreateStyle(Styles.eStyleSimpleBold, "#FFFFF0");
                boldStyle.AppendChild(CreateFont("Calibri", "11", true));
                styles.AppendChild(boldStyle);
                
                XmlElement boldStyleLight = CreateStyle(Styles.eStyleSimpleBoldLight, "#FFFF96");
                boldStyleLight.AppendChild(CreateFont("Calibri", "11", true));
                styles.AppendChild(boldStyleLight);

                m_row = m_doc.CreateElement("Row");
                m_nRowCounter = 1;
            }
        }
        public long AddRow()
        {
            m_row.SetAttribute(Tags.eAutoFitHeight, "0");
            m_table.AppendChild(m_row);
            m_row = m_doc.CreateElement("Row");
            m_nRowCounter++;
            return m_nRowCounter;
        }
        public bool AddCell(string style, string str, int mergeRight = 0, int mergeDown = 0)
        {
            XmlElement data = m_doc.CreateElement("Data");
            XmlElement cell;
            //if(str[0]=='=')
            //{
            //    cell = CreateCell(style, data);
            //    cell.SetAttribute(Tags.eFormula, str);
            //    data.SetAttribute(Tags.eType, "Number");
            //}
            //else
            {
                cell = CreateCell(style, data);
                data.SetAttribute(Tags.eType, "String");
                data.InnerText = str;   
            }
            if(mergeRight > 0)
                cell.SetAttribute(Tags.eMergeAcross, mergeRight.ToString());
            if(mergeDown > 0)
                cell.SetAttribute(Tags.eMergeDown, mergeDown.ToString());
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
                data.SetAttribute(Tags.eType, "Number");
                data.InnerText = val.ToString().Replace(',', '.');
                m_row.AppendChild(CreateCell(style, data));
            }
            return true;
        }
        public bool Close()
        {
            m_table.AppendChild(m_row);
            SFile.CheckDir(m_pathResult);
            m_doc.Save(m_pathResult);
            StreamReader rd = new StreamReader(m_pathResult);
            string buf = rd.ReadToEnd();
            rd.Close();
            StreamWriter wr = new StreamWriter(m_pathResult);
            wr.Write(Tags.Filter(buf));
            wr.Close();
            return false;
        }
        ~CTablerExcel()
        {
            Close();
        }

        private XmlElement CreateFont(string font, string size, bool bBold)
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
        private XmlElement CreateStyle(string id, string color, bool bBorder = true)
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
        private XmlElement CreateCell(string style, XmlElement data = null)
        {
            XmlElement cell = m_doc.CreateElement("Cell");
            if(style.Length>0)
                cell.SetAttribute(Tags.eStyleID,  style);
            if(data != null)
            cell.AppendChild(data);
            return cell;
        }
    }
}
