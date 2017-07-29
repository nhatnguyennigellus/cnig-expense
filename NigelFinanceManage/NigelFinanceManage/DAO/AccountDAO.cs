using NigelFinanceManage.Data;
using NigelFinanceManage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NigelFinanceManage.DAO
{
    public class AccountDAO
    {
        public Account getAccountById(XmlDataSource xml, String accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/account/profile[contains(@id, '" + accId + "')]";

            XmlNode ndAcc = doc.SelectSingleNode(xpath);
            if (ndAcc == null)
            {
                return null;
            }
            string Name = ndAcc.Attributes["name"].Value;
            Account account = new Account
            {
                Id = accId,
                Name = ndAcc.Attributes["name"].Value,
                Bank = ndAcc.Attributes["bank"].Value,
                CashWithdraw = int.Parse(ndAcc.Attributes["cash-withdraw"].Value),
                Balance = int.Parse(ndAcc.Attributes["balance"].Value),
                Currency = ndAcc.Attributes["currency"].Value
            };
            return account;
        }

        public bool isAuthenticated(XmlDataSource xml, String id, String pin)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/account/profile[@id='" + id + "' and @pin='" + pin + "']";

            XmlNode ndAcc = doc.SelectSingleNode(xpath);
            if (ndAcc == null)
            {
                return false;
            }
            
            return true;
        }

        public bool updateBudget(XmlDataSource xml, string id, int newAmt, string budget)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/account/profile[@id='" + id + "']";

            XmlNode eleBudget = doc.SelectSingleNode(xpath);

            if (budget.Equals("Cash"))
            {
                eleBudget.Attributes["cash-withdraw"].Value = newAmt.ToString();
            }
            else if (budget.Equals("Bank"))
            {
                eleBudget.Attributes["balance"].Value = newAmt.ToString();
            }
            else return false;

            doc.Save(xml.XmlPath);
            return true;
        }

        public bool existedAccount(XmlDataSource xml, String accId)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/account/profile[contains(@id, '" + accId + "')]";

            XmlNode ndAcc = doc.SelectSingleNode(xpath);
            if (ndAcc == null)
            {
                return false;
            }
            return true;
        }

        public bool addAccount(XmlDataSource xml, Account account)
        {
            XmlDocument doc = xml.getXmlDocument();

            // Add profile
            string xpath = "/my-expense/account";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement elePrf = doc.CreateElement("profile");

            elePrf.SetAttribute("id", account.Id);
            elePrf.SetAttribute("name", account.Name);
            elePrf.SetAttribute("pin", account.Pin);
            elePrf.SetAttribute("balance", account.Balance.ToString());
            elePrf.SetAttribute("bank", account.Bank);
            elePrf.SetAttribute("cash-withdraw", account.CashWithdraw.ToString());
            elePrf.SetAttribute("currency", account.Currency);

            eleList.AppendChild(elePrf);

            doc.Save(xml.XmlPath);
            return true;
        }

        public bool addProfile(XmlDataSource xml, Account account)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/expense-data";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);

            // Add data
           
            XmlElement eleData = doc.CreateElement("data");
            eleData.SetAttribute("profile-id", account.Id);
            eleList.AppendChild(eleData);

            XmlElement eleAtmOth = doc.CreateElement("atm-other");
            eleData.AppendChild(eleAtmOth);

            XmlElement eleIncome = doc.CreateElement("income-log");
            eleData.AppendChild(eleIncome);

            XmlElement elePayment = doc.CreateElement("payments-log");
            eleData.AppendChild(elePayment);

            XmlElement elePlan = doc.CreateElement("expense-plan");
            eleData.AppendChild(elePlan);

            XmlElement eleWdh = doc.CreateElement("withdraw-history");
            eleData.AppendChild(eleWdh);

            XmlElement eleQE = doc.CreateElement("quick-entry");
            eleData.AppendChild(eleQE);

            doc.Save(xml.XmlPath);
            return true;
        }

        public bool modifyAccount(XmlDataSource xml, Account account)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/account/profile[@id='" + account.Id + "']";
            XmlNode ele = doc.SelectSingleNode(xpath);

            ele.Attributes["name"].Value = account.Name;
            ele.Attributes["pin"].Value = account.Pin;
            ele.Attributes["currency"].Value = account.Currency;
            ele.Attributes["balance"].Value = account.Balance.ToString();
            ele.Attributes["bank"].Value = account.Bank;
            ele.Attributes["cash-withdraw"].Value = account.CashWithdraw.ToString();

            doc.Save(xml.XmlPath);

            return true;
        }
    }
}
