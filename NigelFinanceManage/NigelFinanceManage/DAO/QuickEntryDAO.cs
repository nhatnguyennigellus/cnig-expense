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
    public class QuickEntryDAO
    {
        public DataTable getDataList(XmlDataSource xml, string accId)
        {
            DataTable dt = new DataTable();

            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/quick-entry/item";
            XmlNodeList nodeList = root.SelectNodes(xpath);
            if (nodeList.Count > 0)
            {
                dt = this.createDataTable(nodeList);
            }

            return dt;
        }

        public List<QuickEntry> getDataListByType(XmlDataSource xml, string accId, string type)
        {
            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/quick-entry/item[@type='" + type + "']";
            XmlNodeList nodeList = root.SelectNodes(xpath);
            List<QuickEntry> list = new List<QuickEntry>();
            foreach (XmlElement ele in nodeList)
            {
                QuickEntry info = new QuickEntry
                {
                    Id = int.Parse(ele.GetAttribute("id")),
                    Type = ele.GetAttribute("type"),
                    Description = ele.GetAttribute("description"),
                };

                list.Add(info);
            }

            return list;
        }

        public List<QuickEntry> getList(XmlDataSource xml, string accId)
        {
            DataTable dt = new DataTable();

            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/quick-entry/item";
            XmlNodeList nodeList = root.SelectNodes(xpath);

            List<QuickEntry> list = new List<QuickEntry>();
            foreach (XmlElement ele in nodeList)
            {
                QuickEntry info = new QuickEntry
                {
                    Id = int.Parse(ele.GetAttribute("id")),
                    Type = ele.GetAttribute("type"),
                    Description = ele.GetAttribute("description"),
                };

                list.Add(info);
            }

            return list;
        }

        public DataTable createDataTable(XmlNodeList list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Type");
            dt.Columns.Add("Description");
            foreach (XmlElement ele in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = ele.GetAttribute("id");
                row["Type"] = ele.GetAttribute("type");
                row["Description"] = ele.GetAttribute("description");
            }
            return dt;
        }

        public QuickEntry getById(XmlDataSource xml, string id, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='"
                + accId + "']/quick-entry/item[@id='" + id + "']";

            XmlNode ndQE = doc.SelectSingleNode(xpath);
            if (ndQE == null)
            {
                return null;
            }

            QuickEntry qe = new QuickEntry
            {
                Id = int.Parse(ndQE.Attributes["id"].Value),
                Type = ndQE.Attributes["type"].Value,
                Description = ndQE.Attributes["description"].Value
            };

            return qe;
        }

        public bool add(XmlDataSource xml, QuickEntry newInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId + "']/quick-entry";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("item");

            ele.SetAttribute("id", newInfo.Id.ToString());
            ele.SetAttribute("type", newInfo.Type);
            ele.SetAttribute("description", newInfo.Description);

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool modify(XmlDataSource xml, QuickEntry mdfInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/quick-entry/item[@id='" + mdfInfo.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);

            ele.Attributes["id"].Value = mdfInfo.Id.ToString();
            ele.Attributes["type"].Value = mdfInfo.Type;
            ele.Attributes["description"].Value = mdfInfo.Description;

            doc.Save(xml.XmlPath);

            return true;
        }

        public bool remove(XmlDataSource xml, QuickEntry rmvInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/quick-entry/item[@id='" + rmvInfo.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);
            ele.ParentNode.RemoveChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }
    }
}
