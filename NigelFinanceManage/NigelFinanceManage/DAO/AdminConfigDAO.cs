using NigelFinanceManage.Data;
using NigelFinanceManage.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NigelFinanceManage.DAO
{
    public class AdminConfigDAO
    {
        /*
         * XML Xpath
         */
        public DataTable createDataTable(XmlNodeList list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Method");
            dt.Columns.Add("Path");
            dt.Columns.Add("Nof Params");
            foreach (XmlElement ele in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["Method"] = ele.GetAttribute("method");
                row["Path"] = ele.GetAttribute("path");
                row["Nof Params"] = ele.GetAttribute("params");
            }
            dt.DefaultView.Sort = "Method ASC";
            return dt;
        }

        public DataTable getXpathList(XmlAdminConfig xml, string dao)
        {
            try
            {
                DataTable dt = new DataTable();

                XmlDocument doc = xml.getXmlDocument();
                XmlNode root = doc.DocumentElement;

                string xpath = "/config/xpaths/dao[@name='"
                    + dao + "']/xpath";
                XmlNodeList nodeList = root.SelectNodes(xpath);
                if (nodeList.Count > 0)
                {
                    dt = this.createDataTable(nodeList);
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public bool add(XmlAdminConfig xml, XMLXpathConfig xmlXpath)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();

                string xpath = "/config/xpaths/dao[@name='" + xmlXpath.Dao + "']";
                XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
                if (eleList == null)
                {
                    string xpathPath = "/config/xpaths";
                    XmlElement eleXpath = (XmlElement)doc.SelectSingleNode(xpathPath);
                    XmlElement eleDao = doc.CreateElement("dao");
                    eleDao.SetAttribute("name", xmlXpath.Dao);

                    eleXpath.AppendChild(eleDao);

                    eleList = (XmlElement)doc.SelectSingleNode(xpath);
                }
                XmlElement ele = doc.CreateElement("xpath");

                ele.SetAttribute("method", xmlXpath.Method);
                ele.SetAttribute("path", xmlXpath.Path);
                ele.SetAttribute("params", xmlXpath.NofParams.ToString());

                eleList.AppendChild(ele);
                doc.Save(xml.XmlPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool modify(XmlAdminConfig xml, XMLXpathConfig xmlXpath)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/xpaths/dao[@name='" + xmlXpath.Dao
                    + "']/xpath[@method='" + xmlXpath.Method + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);

                ele.Attributes["params"].Value = xmlXpath.NofParams.ToString();
                ele.Attributes["path"].Value = xmlXpath.Path;

                doc.Save(xml.XmlPath);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool remove(XmlAdminConfig xml, XMLXpathConfig xmlXpath)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/xpaths/dao[@name='" + xmlXpath.Dao
                    + "']/xpath[@method='" + xmlXpath.Method + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);
                ele.ParentNode.RemoveChild(ele);
                doc.Save(xml.XmlPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool methodExist(XmlAdminConfig xml, XMLXpathConfig xmlXpath)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/xpaths/dao[@name='" + xmlXpath.Dao
                    + "']/xpath[@method='" + xmlXpath.Method + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);
                
                return (ele != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static XMLXpathConfig getXpath(XmlAdminConfig xml, string dao, string methodName)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/xpaths/dao[@name='" + dao
                    + "']/xpath[@method='" + methodName + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);

                XMLXpathConfig xmlXp = new XMLXpathConfig()
                {
                    Method = ele.Attributes["method"].Value.ToString(),
                    Path = ele.Attributes["path"].Value.ToString(),
                    NofParams = int.Parse(ele.Attributes["params"].Value.ToString())
                };
                return xmlXp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /*
         * DB XML
         */
        public bool modifyXmlDbPath(XmlAdminConfig xml, string pathSb, string pathHs)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();

                string xpathSb = "/config/xml/sandbox-db";
                string xpathHs = "/config/xml/house-db";

                XmlNode eleSb = doc.SelectSingleNode(xpathSb);
                XmlNode eleHs = doc.SelectSingleNode(xpathHs);

                eleSb.InnerText = pathSb;
                eleHs.InnerText = pathHs;

                doc.Save(xml.XmlPath);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string getXmlDbPath(XmlAdminConfig xml, int dbType)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "";

                if (dbType == XMLXpathConfig.SANDBOX)
                {
                    xpath = "/config/xml/sandbox-db";
                }
                else
                {
                    xpath = "/config/xml/house-db";
                }
                XmlNode ele = doc.SelectSingleNode(xpath);

                return ele.InnerText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        /*
         * Output
         */
        public bool modifyExcelOutputConfig(XmlAdminConfig xml, ExcelOutput excel)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                
                string xpathOutputPath = "/config/excel-output/output-path";
                XmlNode eleOutputPath = doc.SelectSingleNode(xpathOutputPath);
                eleOutputPath.InnerText = excel.Path;

                string xpathExtension = "/config/excel-output/extension";
                XmlNode eleExtension = doc.SelectSingleNode(xpathExtension);
                eleExtension.InnerText = excel.Extension;

                string xpathDayRange = "/config/excel-output/day-range";
                XmlNode eleDayRange = doc.SelectSingleNode(xpathDayRange);
                eleDayRange.Attributes["report"].Value = excel.DayRangeFilename;
                eleDayRange.Attributes["param"].Value = excel.DayRangeParams.ToString();
                eleDayRange.Attributes["title"].Value = excel.DayRangeTitle;

                string xpathMonthly = "/config/excel-output/monthly";
                XmlNode eleMonthly = doc.SelectSingleNode(xpathMonthly);
                eleMonthly.Attributes["report"].Value = excel.MonthlyFilename;
                eleMonthly.Attributes["param"].Value = excel.MonthlyParams.ToString();
                eleMonthly.Attributes["title"].Value = excel.MonthlyTitle;

                string xpathDate = "/config/excel-output/date";
                XmlNode eleDate = doc.SelectSingleNode(xpathDate);
                eleDate.Attributes["report"].Value = excel.DateFilename;
                eleDate.Attributes["param"].Value = excel.DateParams.ToString();
                eleDate.Attributes["title"].Value = excel.DateTitle;

                string xpathDateFormat = "/config/excel-output/date-format";
                XmlNode eleDateFormat = doc.SelectSingleNode(xpathDateFormat);
                eleDateFormat.InnerText = excel.DateFormat;
                
                doc.Save(xml.XmlPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public ExcelOutput getExcelOutputConfig(XmlAdminConfig xml)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpathRoot = "/config/excel-output";

                XmlNode eleOutputPath = doc.SelectSingleNode(xpathRoot + "/output-path");
                XmlNode eleExtension = doc.SelectSingleNode(xpathRoot + "/extension");
                XmlNode eleDayRange = doc.SelectSingleNode(xpathRoot + "/day-range");
                XmlNode eleMonthly = doc.SelectSingleNode(xpathRoot + "/monthly");
                XmlNode eleDate = doc.SelectSingleNode(xpathRoot + "/date");
                XmlNode eleDateFormat = doc.SelectSingleNode(xpathRoot + "/date-format");

                ExcelOutput excel = new ExcelOutput()
                {
                    Path = eleOutputPath.InnerText,
                    Extension = eleExtension.InnerText,
                    DayRangeFilename = eleDayRange.Attributes["report"].Value,
                    DayRangeParams = eleDayRange.Attributes["param"].Value == "" ? 0 :
                        int.Parse(eleDayRange.Attributes["param"].Value),
                    DayRangeTitle = eleDayRange.Attributes["title"].Value,
                    DateFilename = eleDate.Attributes["report"].Value,
                    DateParams = eleDate.Attributes["param"].Value == "" ? 0 :
                        int.Parse(eleDate.Attributes["param"].Value),
                    DateTitle = eleDate.Attributes["title"].Value,
                    MonthlyFilename = eleMonthly.Attributes["report"].Value,
                    MonthlyParams = eleMonthly.Attributes["param"].Value == "" ? 0 :
                        int.Parse(eleMonthly.Attributes["param"].Value),
                    MonthlyTitle = eleMonthly.Attributes["title"].Value,
                    DateFormat = eleDateFormat.InnerText
                };

                return excel;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
