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
    public class ErrorDAO
    {
        public DataTable createDataTable(XmlNodeList list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Code");
            dt.Columns.Add("Description");
            foreach (XmlElement ele in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["Code"] = ele.GetAttribute("code");
                row["Description"] = ele.GetAttribute("description");
            }
            dt.DefaultView.Sort = "Code ASC";
            return dt;
        }

        public DataTable getErrorList(XmlAdminConfig xml)
        {
            try
            {
                DataTable dt = new DataTable();

                XmlDocument doc = xml.getXmlDocument();
                XmlNode root = doc.DocumentElement;
                
                string xpath = "/config/errors/error";
                
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

        public List<Error> getErrorListByType(XmlAdminConfig xml, string prefix)
        {
            List<Error> list = new List<Error>();
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                XmlNode root = doc.DocumentElement;

                string xpath = "/config/errors/error[starts-with(@code, '"
                    + prefix + "')]";
                XmlNodeList nodeList = root.SelectNodes(xpath);
                if (nodeList.Count > 0)
                {
                    foreach (XmlElement ele in nodeList)
                    {
                        Error error = new Error()
                        {
                            Code = ele.Attributes["code"].Value,
                            Description = ele.Attributes["description"].Value
                        };
                        list.Add(error);
                    }
                }
                return list.OrderBy(o => o.Code).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return list;
            }
        }

        public bool add(XmlAdminConfig xml, Error error)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();

                string xpath = "/config/errors";
                XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
                XmlElement ele = doc.CreateElement("error");

                ele.SetAttribute("code", error.Code);
                ele.SetAttribute("description", error.Description);

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

        public bool modify(XmlAdminConfig xml, Error error)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/errors/error[@code='"
                    + error.Code + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);

                ele.Attributes["description"].Value = error.Description;

                doc.Save(xml.XmlPath);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool remove(XmlAdminConfig xml, string errorCode)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/errors/error[@code='"
                    + errorCode + "']";
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

        public bool errorExist(XmlAdminConfig xml, string error)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/errors/error[@code='"
                    + error + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);

                return (ele != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string getError(XmlAdminConfig xml, string code)
        {
            try
            {
                XmlDocument doc = xml.getXmlDocument();
                string xpath = "/config/errors/error[@code = '"
                    + code + "']";
                XmlNode ele = doc.SelectSingleNode(xpath);

                Error error = new Error()
                {
                    Code = ele.Attributes["code"].Value,
                    Description = ele.Attributes["description"].Value
                };

                return error.Description;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
