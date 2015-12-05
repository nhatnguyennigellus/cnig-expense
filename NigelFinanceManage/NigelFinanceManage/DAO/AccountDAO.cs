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
            string xpath = "/my-expense/account/profile[@id='" + accId + "']";

            XmlNode ndAcc = doc.SelectSingleNode(xpath);
            if (ndAcc == null)
            {
                return null;
            }

            Account account = new Account
            {
                Id = accId,
                Name = ndAcc.Attributes["name"].Value,
                Bank = ndAcc.Attributes["bank"].Value,
                CashWithdraw = int.Parse(ndAcc.Attributes["cash"].Value),
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
    }
}
