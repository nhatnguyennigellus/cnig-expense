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
    public class XmlAdminConfig
    {
        private static XmlAdminConfig xmlInstance = null;
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

        protected XmlAdminConfig()
        {
            xmlPath = ConfigurationManager.AppSettings.Get("Config");
            doc = new XmlDocument();
            doc.Load(xmlPath);
        }

        public static XmlAdminConfig getInstance()
        {
            if (xmlInstance == null)
            {
                xmlInstance = new XmlAdminConfig();
            }
            return xmlInstance;
        }

        public XmlDocument getXmlDocument()
        {
            return this.doc;
        }
    }
}
