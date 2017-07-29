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
    public class BankDAO
    {
        public bool existedBank(XmlDataSource xml, string bank)
        {
            XmlDocument doc = xml.getXmlDocument();
            string xpath = "/my-expense/banks/bank[@name = '" + bank + "']";

            XmlNode ndBank = doc.SelectSingleNode(xpath);
            if (ndBank == null)
            {
                return false;
            }

            return true;
        }

        public bool addBank(XmlDataSource xml, Bank bank)
        {
            XmlDocument doc = xml.getXmlDocument();

            string xpath = "/my-expense/banks";
            XmlElement eleList = (XmlElement)doc.SelectSingleNode(xpath);
            XmlElement ele = doc.CreateElement("bank");

            ele.SetAttribute("name", bank.Name);
            ele.SetAttribute("inner-wdh-fee", bank.InnerWdhFee.ToString());
            ele.SetAttribute("outer-wdh-fee", bank.OuterWdhFee.ToString());
            ele.SetAttribute("currency", bank.Currency);

            eleList.AppendChild(ele);
            doc.Save(xml.XmlPath);

            return true;
        }
    }
}
