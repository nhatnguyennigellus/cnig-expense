using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Collections.Specialized;

namespace NigelFinanceManage.Data
{
    public class XmlDataSource
    {
        private static XmlDataSource xmlInstance = null;
        private XmlDocument doc;
        
        private string xmlPath;
        public string XmlPath
        {
            get { return xmlPath; }
            set { 
                xmlPath = value;
                doc = new XmlDocument();
                doc.Load(xmlPath);
            }
        }

        protected XmlDataSource()
        {
            xmlPath = ConfigurationManager.AppSettings.Get("SandboxDb");
            doc = new XmlDocument();
            doc.Load(xmlPath);
        }

        public static XmlDataSource getInstance()
        {
            if (xmlInstance == null)
            {
                xmlInstance = new XmlDataSource();
            }
            return xmlInstance;
        }

        public XmlDocument getXmlDocument()
        {
            return this.doc;
        }
    }
}
