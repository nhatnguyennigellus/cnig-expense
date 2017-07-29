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
            dt.DefaultView.Sort = "Date ASC";
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

            XmlNode ndWdh = doc.SelectSingleNode(xpath);
            if (ndWdh == null)
            {
                return null;
            }

            Withdrawal withdr = new Withdrawal
            {
                Id = ndWdh.Attributes["id"].Value,
                Amount = int.Parse(ndWdh.Attributes["amount"].Value),
                Currency = ndWdh.Attributes["currency"].Value,
                DateExpense = DateTime.Parse(ndWdh.Attributes["date"].Value),
                Description = ndWdh.Attributes["atm"].Value,
                Extra = int.Parse(ndWdh.Attributes["extra"].Value)
            };

            return withdr;
        }

        public bool add(XmlDataSource xml, Withdrawal newInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId + "']/withdraw-history";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("withdraw");

            ele.SetAttribute("id", newInfo.Id);
            ele.SetAttribute("amount", newInfo.Amount.ToString());
            ele.SetAttribute("currency", newInfo.Currency);
            ele.SetAttribute("date", newInfo.DateExpense.ToString("yyyy.MM.dd"));
            ele.SetAttribute("atm", newInfo.Description);
            ele.SetAttribute("extra", newInfo.Extra.ToString());

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool modify(XmlDataSource xml, Withdrawal mdfInfo, string accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id='" + accId
                + "']/withdraw-history/withdraw[@id='" + mdfInfo.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);

            ele.Attributes["amount"].Value = mdfInfo.Amount.ToString();
            ele.Attributes["currency"].Value = mdfInfo.Currency;
            ele.Attributes["date"].Value = mdfInfo.DateExpense.ToString("yyyy.MM.dd");
            ele.Attributes["atm"].Value = mdfInfo.Description;
            ele.Attributes["extra"].Value = mdfInfo.Extra.ToString();

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
                Withdrawal info = new Withdrawal
                {
                    Id = ele.GetAttribute("id"),
                    Amount = int.Parse(ele.GetAttribute("amount")),
                    Currency = ele.GetAttribute("currency"),
                    DateExpense = DateTime.Parse(ele.GetAttribute("date").ToString()),
                    Description = ele.GetAttribute("atm"),
                    Extra = int.Parse(ele.GetAttribute("extra"))
                };

                list.Add(info);
            }

            return list;
        }

        public List<String> getATMListByBank(XmlDataSource xml, String bank)
        {
            List<String> list = new List<String>();
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/banks/bank[@name='" + bank + "']/atm";
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes(xpath);
            foreach (XmlElement ele in nodeList)
            {
                list.Add(ele.GetAttribute("name"));
            }

            return list;
        }

        public List<String> getATMOtherList(XmlDataSource xml, Account account)
        {
            List<String> list = new List<String>();
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id = '"
                + account.Id + "']/atm-other/atm";
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes(xpath);
            foreach (XmlElement ele in nodeList)
            {
                list.Add(ele.GetAttribute("name"));
            }

            return list;
        }

        public int getWithdrawFee(XmlDataSource xml, String bank, int io)
        {
            List<String> list = new List<String>();
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/banks/bank[@name='" + bank + "']";
            XmlNode root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes(xpath);
            foreach (XmlElement ele in nodeList)
            {
                return io == Withdrawal.INNER_FEE ?
                    int.Parse(ele.GetAttribute("inner-wdh-fee")) : 
                    int.Parse(ele.GetAttribute("outer-wdh-fee"));
            }

            return 0;
        }

        public bool addATM(XmlDataSource xml, string atmName, string bank)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/banks/bank[@name ='" + bank + "']";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("atm");

            ele.SetAttribute("name", atmName);

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool addATM(XmlDataSource xml, string atmName, Account account)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/expense-data/data[@profile-id = '" 
                +  account.Id + "']/atm-other";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("atm");

            ele.SetAttribute("name", atmName);

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool removeATM(XmlDataSource xml, string atmName, Account account)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/expense-data/data[@profile-id = '"
                + account.Id + "']/atm-other/atm[@name = '" + atmName + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);
            ele.ParentNode.RemoveChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }

        public bool removeATM(XmlDataSource xml, string atmName, string bank)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/banks/bank[@name ='" + bank + "']/atm[@name = '" + atmName + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);
            ele.ParentNode.RemoveChild(ele);
            doc.Save(xml.XmlPath);
            return true;
        }


        public bool add(XmlDataSource xml, FinanceInfo newInfo, string accId)
        {
            throw new NotImplementedException();
        }

        public bool modify(XmlDataSource xml, FinanceInfo mdfInfo, string accId)
        {
            throw new NotImplementedException();
        }
    }
}
