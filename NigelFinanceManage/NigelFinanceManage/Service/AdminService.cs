using NigelFinanceManage.DAO;
using NigelFinanceManage.Data;
using NigelFinanceManage.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Service
{
    public class AdminService
    {
        private static AdminService instance = null;
        private XmlAdminConfig xml;
        AdminConfigDAO adminDAO = new AdminConfigDAO();
        ErrorDAO errorDAO = new ErrorDAO();

        protected AdminService()
        {
            xml = XmlAdminConfig.getInstance();
        }

        public static AdminService getInstance()
        {
            if (instance == null)
            {
                instance = new AdminService();
            }
            return instance;
        }

        /*
         * XML XPath
         */
        public DataTable getXpathList(string dao)
        {
            return adminDAO.getXpathList(xml, dao);
        }

        public bool addXpath(XMLXpathConfig xmlXpath)
        {
            return adminDAO.add(xml, xmlXpath);
        }

        public bool modifyXpath(XMLXpathConfig xmlXPath)
        {
            return adminDAO.modify(xml, xmlXPath);
        }

        public bool removeXpath(XMLXpathConfig xmlXpath)
        {
            return adminDAO.remove(xml, xmlXpath);
        }

        public bool methodExist(XMLXpathConfig xmlXPath)
        {
            return adminDAO.methodExist(xml, xmlXPath);
        }

        /*
         * DB XML
         */
        public bool modifyXmlDbPath(string pathSb, string pathHs)
        {
            return adminDAO.modifyXmlDbPath(xml, pathSb, pathHs);
        }

        public string getXmlDbPath(int dbType)
        {
            return adminDAO.getXmlDbPath(xml, dbType);
        }

        /*
         * Error
         */
        public DataTable getErrorList()
        {
            return errorDAO.getErrorList(xml);
        }

        public bool addError(Error error)
        {
            return errorDAO.add(xml, error);
        }

        public bool modifyError(Error error)
        {
            return errorDAO.modify(xml, error);
        }

        public bool removeError(string error)
        {
            return errorDAO.remove(xml, error);
        }

        public bool errorExist(string error)
        {
            return errorDAO.errorExist(xml, error);
        }

        public string getError(string code)
        {
            return errorDAO.getError(xml, code);
        }

        public string generateErrorCode(string prefix)
        {
            StringBuilder result = new StringBuilder();

            List<Error> list = errorDAO.getErrorListByType(xml, prefix);
            int maxSuffix = -1;
            if (list.Count == 0)
            {
                maxSuffix = 1;
            }
            else
            {
                maxSuffix = int.Parse(list[list.Count - 1].Code.Substring(1)) + 1;
            }

            string suffix = maxSuffix.ToString("D" + (Error.CODE_LENGTH - 1));

            result.Append(prefix);
            result.Append(suffix);
            return result.ToString();
        }

        /*
         * Excel Output
         */
        public bool modifyExcelConfig(ExcelOutput excel)
        {
            return adminDAO.modifyExcelOutputConfig(xml, excel);
        }

        public ExcelOutput getExcelConfig()
        {
            return adminDAO.getExcelOutputConfig(xml);
        }
    }
}
