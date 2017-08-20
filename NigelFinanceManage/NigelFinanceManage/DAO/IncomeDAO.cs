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
    public class IncomeDAO : IFinanceDAO<Income>
    {

        public DataTable getDataList(XmlDataSource xml, string accId)
        {
            DataTable dt = new DataTable();

            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = AdminConfigDAO.getXpath(
                XmlAdminConfig.getInstance(), this.GetType().Name,
                "getDataList").Path
                .Replace("{0}", accId);
            XmlNodeList nodeList = root.SelectNodes(xpath);
            if (nodeList.Count > 0)
            {
                dt = this.createDataTable(nodeList);
            }

            return dt;
        }

        public List<FinanceInfo> getIncomeListByMonth(XmlDataSource xml, string id, int month, int year)
        {
            List<FinanceInfo> list = new List<FinanceInfo>();

            foreach (FinanceInfo info in this.getList(xml, id))
            {
                DateTime dateInfo = info.DateExpense;
                if (dateInfo.Month == month && dateInfo.Year == year)
                {
                    list.Add(info);
                }
            }

            return list;
        }

        public DataTable createDataTable(XmlNodeList list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            dt.Columns.Add("Budget");
            foreach (XmlElement ele in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = ele.GetAttribute("id");
                row["Amount"] = ele.GetAttribute("amount");
                row["Date"] = ele.GetAttribute("dateExpense");
                row["Description"] = ele.GetAttribute("description");
                row["Budget"] = ele.GetAttribute("budget");
            }
            dt.DefaultView.Sort = "Date ASC";
            return dt;
        }

        public Income getById(XmlDataSource xml, string id, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = AdminConfigDAO.getXpath(
                XmlAdminConfig.getInstance(), this.GetType().Name,
                "getById").Path
                .Replace("{0}", accId)
                .Replace("{1}", id);

            XmlNode ndInc = doc.SelectSingleNode(xpath);
            if (ndInc == null)
            {
                return null;
            }

            Income income = new Income
            {
                Id = ndInc.Attributes["id"].Value,
                Amount = int.Parse(ndInc.Attributes["amount"].Value),
                Currency = ndInc.Attributes["currency"].Value,
                DateExpense = DateTime.Parse(ndInc.Attributes["dateExpense"].Value),
                Description = ndInc.Attributes["description"].Value,
                Budget = int.Parse(ndInc.Attributes["budget"].Value)
            };

            return income;
        }

        public bool add(XmlDataSource xml, FinanceInfo newInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = AdminConfigDAO.getXpath(
                        XmlAdminConfig.getInstance(), this.GetType().Name,
                        "add").Path
                        .Replace("{0}", accId);
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("income");

            ele.SetAttribute("id", newInfo.Id);
            ele.SetAttribute("amount", newInfo.Amount.ToString());
            ele.SetAttribute("currency", newInfo.Currency);
            ele.SetAttribute("dateExpense", newInfo.DateExpense.ToString("yyyy.MM.dd"));
            ele.SetAttribute("description", newInfo.Description);
            ele.SetAttribute("budget", newInfo.Budget.ToString());

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool modify(XmlDataSource xml, FinanceInfo mdfInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = AdminConfigDAO.getXpath(
                XmlAdminConfig.getInstance(), this.GetType().Name,
                "modify").Path
                .Replace("{0}", accId)
                .Replace("{1}", mdfInfo.Id);
            XmlNode ele = doc.SelectSingleNode(xpath);

            ele.Attributes["amount"].Value = mdfInfo.Amount.ToString();
            ele.Attributes["currency"].Value = mdfInfo.Currency;
            ele.Attributes["dateExpense"].Value = mdfInfo.DateExpense.ToString("yyyy.MM.dd");
            ele.Attributes["description"].Value = mdfInfo.Description;
            ele.Attributes["budget"].Value = mdfInfo.Budget.ToString();

            doc.Save(xml.XmlPath);

            return true;
        }

        public bool remove(XmlDataSource xml, FinanceInfo rmvInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = AdminConfigDAO.getXpath(
                XmlAdminConfig.getInstance(), this.GetType().Name,
                "remove").Path
                .Replace("{0}", accId)
                .Replace("{1}", rmvInfo.Id);
            XmlNode ele = doc.SelectSingleNode(xpath);
            ele.ParentNode.RemoveChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public List<FinanceInfo> getList(XmlDataSource xml, string accId)
        {
            DataTable dt = new DataTable();

            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = AdminConfigDAO.getXpath(
                XmlAdminConfig.getInstance(), this.GetType().Name,
                "getList").Path
                .Replace("{0}", accId);
            XmlNodeList nodeList = root.SelectNodes(xpath);

            List<FinanceInfo> list = new List<FinanceInfo>();
            foreach (XmlElement ele in nodeList)
            {
                Income info = new Income
                {
                    Id = ele.GetAttribute("id"),
                    Amount = int.Parse(ele.GetAttribute("amount")),
                    Currency = ele.GetAttribute("currency"),
                    DateExpense = DateTime.Parse(ele.GetAttribute("dateExpense").ToString()),
                    Description = ele.GetAttribute("description"),
                    Budget = int.Parse(ele.GetAttribute("budget"))
                };

                list.Add(info);
            }

            return list;
        }
    }
}
