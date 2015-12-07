using NigelFinanceManage.Entity;
using NigelFinanceManage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace NigelFinanceManage.DAO
{
    public interface IFinanceDAO <T>
        where T : class
    {
        List<T> getList(XmlNodeList list);
        DataTable createDataTable(XmlNodeList list);
        DataTable getDataList(XmlDataSource xml, string accId);
        T getById(XmlDataSource xml, string id, string accId);
        bool add(XmlDataSource xml, FinanceInfo newInfo, string accId);
        bool modify(XmlDataSource xml, FinanceInfo mdfInfo, string accId);
        bool remove(XmlDataSource xml, FinanceInfo rmvInfo, string accId);
    }
}
