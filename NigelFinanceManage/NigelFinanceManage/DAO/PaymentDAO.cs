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
    public class PaymentDAO : IFinanceDAO<Payment>
    {
        public DataTable createDataTable(XmlNodeList list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (XmlElement ele in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = ele.GetAttribute("id");
                row["Amount"] = ele.GetAttribute("amount");
                row["Date"] = ele.GetAttribute("dateExpense");
                row["Description"] = ele.GetAttribute("description");
            }
            return dt;
        }

        public DataTable getList(XmlDataSource xml, string accId)
        {
            DataTable dt = new DataTable();

            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = "/my-expense/expense-data/data[@profile-id='" 
                + accId + "']/payments-log/payment";
            XmlNodeList nodeList = root.SelectNodes(xpath);
            if (nodeList.Count > 0)
            {
                dt = this.createDataTable(nodeList);
            }

            return dt;
        }

        public Payment getById(XmlDataSource xml, string id, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" 
                + accId + "']/payments-log/payment[@id='" + id + "']";

            XmlNode ndPay = doc.SelectSingleNode(xpath);
            if (ndPay == null)
            {
                return null;
            }

            Payment payment = new Payment
            {
                Id = ndPay.Attributes["id"].Value,
                Amount = int.Parse(ndPay.Attributes["amount"].Value),
                Currency = ndPay.Attributes["currency"].Value,
                DateExpense = DateTime.Parse(ndPay.Attributes["dateExpense"].Value),
                Description = ndPay.Attributes["description"].Value
            };

            return payment;
        }

        public bool add(XmlDataSource xml, FinanceInfo newInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId + "']/payments-log";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("payment");

            ele.SetAttribute("id", newInfo.Id);
            ele.SetAttribute("amount", newInfo.Amount.ToString());
            ele.SetAttribute("currency", newInfo.Currency);
            ele.SetAttribute("dateExpense", newInfo.DateExpense.ToString("dd.MM.yyyy"));
            ele.SetAttribute("description", newInfo.Description);

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool modify(XmlDataSource xml, FinanceInfo mdfInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/payments-log/payment[@id='" + mdfInfo.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);

            ele.Attributes["amount"].Value = mdfInfo.Amount.ToString();
            ele.Attributes["currency"].Value = mdfInfo.Currency;
            ele.Attributes["dateExpense"].Value = mdfInfo.DateExpense.ToString("dd.MM.yyyy");
            ele.Attributes["description"].Value = mdfInfo.Description;

            doc.Save(xml.XmlPath);

            return true;
        }

        public bool remove(XmlDataSource xml, FinanceInfo rmvInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/payments-log/payment[@id='" + rmvInfo.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);
            ele.ParentNode.RemoveChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }
    }
}
