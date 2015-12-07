﻿using NigelFinanceManage.Data;
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
    public class WithdrawalDAO : IFinanceDAO<Withdrawal>
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
                row["Date"] = ele.GetAttribute("date");
                row["Description"] = ele.GetAttribute("atm");
            }
            return dt;
        }

        public DataTable getDataList(XmlDataSource xml, string accId)
        {
            DataTable dt = new DataTable();

            XmlDocument doc = xml.getXmlDocument();
            XmlNode root = doc.DocumentElement;

            string xpath = "/my-expense/expense-data/data[@profile-id='"
                + accId + "']/withdraw-history/withdraw";
            XmlNodeList nodeList = root.SelectNodes(xpath);
            if (nodeList.Count > 0)
            {
                dt = this.createDataTable(nodeList);
            }

            return dt;
        }

        public List<FinanceInfo> getWithdrawalListByMonth(XmlDataSource xml, string id, int month, int year)
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

        public Withdrawal getById(XmlDataSource xml, string id, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='"
                + accId + "']/withdraw-history/withdraw[@id='" + id + "']";

            XmlNode ndPlan = doc.SelectSingleNode(xpath);
            if (ndPlan == null)
            {
                return null;
            }

            Withdrawal withdr = new Withdrawal
            {
                Id = ndPlan.Attributes["id"].Value,
                Amount = int.Parse(ndPlan.Attributes["amount"].Value),
                Currency = ndPlan.Attributes["currency"].Value,
                DateExpense = DateTime.Parse(ndPlan.Attributes["date"].Value),
                Description = ndPlan.Attributes["atm"].Value
            };

            return withdr;
        }

        public bool add(XmlDataSource xml, FinanceInfo newInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId + "']/withdraw-history";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("withdraw");

            ele.SetAttribute("id", newInfo.Id);
            ele.SetAttribute("amount", newInfo.Amount.ToString());
            ele.SetAttribute("currency", newInfo.Currency);
            ele.SetAttribute("date", newInfo.DateExpense.ToString("dd.MM.yyyy"));
            ele.SetAttribute("atm", newInfo.Description);

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool modify(XmlDataSource xml, FinanceInfo mdfInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/withdraw-history/withdraw[@id='" + mdfInfo.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);

            ele.Attributes["amount"].Value = mdfInfo.Amount.ToString();
            ele.Attributes["currency"].Value = mdfInfo.Currency;
            ele.Attributes["date"].Value = mdfInfo.DateExpense.ToString("dd.MM.yyyy");
            ele.Attributes["atm"].Value = mdfInfo.Description;

            doc.Save(xml.XmlPath);

            return true;
        }

        public bool remove(XmlDataSource xml, FinanceInfo rmvInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/withdraw-history/withdraw[@id='" + rmvInfo.Id + "']";
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

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/withdraw-history/withdraw";
            XmlNodeList nodeList = root.SelectNodes(xpath);

            List<FinanceInfo> list = new List<FinanceInfo>();
            foreach (XmlElement ele in nodeList)
            {
                Income info = new Income
                {
                    Id = ele.GetAttribute("id"),
                    Amount = int.Parse(ele.GetAttribute("amount")),
                    Currency = ele.GetAttribute("currency"),
                    DateExpense = DateTime.Parse(ele.GetAttribute("date").ToString()),
                    Description = ele.GetAttribute("atm"),
                };

                list.Add(info);
            }

            return list;
        }

        public List<String> getATMACBList(XmlDataSource xml)
        {
            List<String> list = new List<String>();
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/atm-acb/atm";
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes(xpath);
            foreach (XmlElement ele in nodeList)
            {
                list.Add(ele.GetAttribute("name"));
            }

            return list;
        }
    }
}
