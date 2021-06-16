using System;
using System.IO;
using System.Xml;

namespace TestSystem
{
    public interface ITabler : Solution.IClose
    {
        IRow AddRow();
        long RowCount();
    }

    public class CTablerEmpty : ITabler
    {
        public CTablerEmpty() { }
        public IRow AddRow() => null;
        public bool Close() => false;
        public long RowCount() => 0;
    }

    public class CTablerExcel : ITabler
    {
        XmlDocument m_doc;
        XmlNode m_table;
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

        public CTablerExcel() { m_nRowCounter = 0; }
        public CTablerExcel(string path, string sAlg, string pathTemplate)
        {
            if(path.Length > 0 && sAlg.Length > 0 && pathTemplate.Length > 0)
            {
                string time = DateTime.Now.ToString().Replace(":", "_").Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace("-", "_");
                m_pathResult = $"{path}{sAlg}_{time}_results.~.xml";

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

                m_nRowCounter = 0;
            }
        }
        public IRow AddRow() => new CRowExcel(m_doc, m_table, ++m_nRowCounter);

        public bool Close()
        {
            CFile file = new CFile(m_pathResult);
            m_doc.Save(file.GetPath());
            string buf = file.ReadToEnd();
            file.WriteTotal(Tags.Filter(buf));
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
    }
}
