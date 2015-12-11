using NigelFinanceManage.DAO;
using NigelFinanceManage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using NigelFinanceManage.Entity;
using System.Data;

namespace NigelFinanceManage.Service
{
    public class DiaryService
    {
        private XmlDataSource xml;
        AccountDAO accDAO = new AccountDAO();
        IncomeDAO incDAO = new IncomeDAO();
        PaymentDAO payDAO = new PaymentDAO();
        PlanDAO planDAO = new PlanDAO();
        WithdrawalDAO wdhDAO = new WithdrawalDAO();

        public DiaryService()
        {
            xml = XmlDataSource.getInstance();
        }

        public void chooseDatabase(int dbIdx)
        {
            if (dbIdx == 0)
            {
                xml.XmlPath = ConfigurationManager.AppSettings.Get("SandboxDb");
            }
            else
            {
                xml.XmlPath = ConfigurationManager.AppSettings.Get("HouseDb");
            }
        }

        public string generateId(string prefix, List<FinanceInfo> list)
        {
            StringBuilder result = new StringBuilder();

            int maxSuffix = -1;
            if (list.Count == 0)
            {
                maxSuffix = 1;
            }
            else
            {
                maxSuffix = int.Parse(list[list.Count - 1].Id.Substring(1)) + 1;
            }


            string suffix = maxSuffix.ToString("D" + (FinanceInfo.ID_SUFFIX_LENGTH - 1));

            result.Append(prefix);
            result.Append(suffix);
            return result.ToString();
        }

        /**
         * Account Service
         */
        public bool isAuthenticated(string id, string pin)
        {
            return accDAO.isAuthenticated(xml, id, pin);
        }

        public Account getAccountById(string id)
        {
            return accDAO.getAccountById(xml, id);
        }

        public bool updateBudget(string id, int newAmount, string budget)
        {
            return accDAO.updateBudget(xml, id, newAmount, budget);
        }

        /**
         * Income Service
         */
        public DataTable getIncomeData(string id)
        {
            return incDAO.getDataList(xml, id);
        }

        public DataTable getIncomeDataByMonth(string id, int month, int year)
        {
            List<FinanceInfo> list = incDAO.getIncomeListByMonth(xml, id, month, year);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (FinanceInfo info in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = info.Id;
                row["Amount"] = info.Amount.ToString();
                row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                row["Description"] = info.Description;
            }
            return dt;
        }

        public DataTable getIncomeDataByRange(string id, DateTime dateFrom, DateTime dateTo)
        {
            List<FinanceInfo> list = incDAO.getList(xml, id);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense >= dateFrom && info.DateExpense <= dateTo)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                    row["Description"] = info.Description; 
                }
            }
            return dt;
        }

        public DataTable getIncomeDataByDate(string id, DateTime date)
        {
            List<FinanceInfo> list = incDAO.getList(xml, id);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense == date)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                    row["Description"] = info.Description;
                }
            }
            return dt;
        }

        public Income getIncomeById(string accId, string id)
        {
            return incDAO.getById(xml, id, accId);
        }

        public List<FinanceInfo> getIncomeList(string accId)
        {
            return incDAO.getList(xml, accId);
        }

        public bool addIncomeLog(FinanceInfo income, String id)
        {
            return incDAO.add(xml, income, id);
        }

        public bool modifyIncome(FinanceInfo income, String accId)
        {
            return incDAO.modify(xml, income, accId);
        }

        public bool removeIncome(FinanceInfo income, String accId)
        {
            return incDAO.remove(xml, income, accId);
        }

        /**
         * Payment Service
         */
        public DataTable getPaymentData(string id)
        {
            return payDAO.getDataList(xml, id);
        }

        public List<FinanceInfo> getPaymentList(string accId)
        {
            return payDAO.getList(xml, accId);
        }

        public DataTable getPaymentDataByMonth(string id, int month, int year)
        {
            List<FinanceInfo> list = payDAO.getPaymentListByMonth(xml, id, month, year);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (FinanceInfo info in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = info.Id;
                row["Amount"] = info.Amount.ToString();
                row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                row["Description"] = info.Description;
            }
            return dt;
        }

        public DataTable getPaymentDataByRange(string id, DateTime dateFrom, DateTime dateTo)
        {
            List<FinanceInfo> list = payDAO.getList(xml, id);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense >= dateFrom && info.DateExpense <= dateTo)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                    row["Description"] = info.Description;
                }
            }
            return dt;
        }

        public DataTable getPaymentDataByDate(string id, DateTime date)
        {
            List<FinanceInfo> list = payDAO.getList(xml, id);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("Description");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense == date)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                    row["Description"] = info.Description;
                }
            }
            return dt;
        }

        public DataTable getPaymentDataByDesc(DataTable dt, string keyword)
        {
            DataTable dtOut = new DataTable();

            foreach (DataRow rowNew in dt.Rows)
            {
                if (rowNew["Description"].ToString().Contains(keyword))
                {
                    DataRow row = rowNew;
                    dt.Rows.Add(row);
                    
                }
            }

            return dtOut;
        }

        public Payment getPaymentById(string id, string accId)
        {
            return payDAO.getById(xml, id, accId);
        }

        public bool addPaymentLog(FinanceInfo payment, String id)
        {
            return payDAO.add(xml, payment, id);
        }

        public bool modifyPayment(FinanceInfo payment, String accId)
        {
            return payDAO.modify(xml, payment, accId);
        }

        public bool removePayment(FinanceInfo payment, String accId)
        {
            return payDAO.remove(xml, payment, accId);
        }

        /**
         * Plan Service
         */
        public DataTable getPlanData(string id)
        {
            return planDAO.getDataList(xml, id);
        }

        public Plan getPlanById(string accId, string id)
        {
            return planDAO.getById(xml, id, accId);
        }
        
        public bool addPlanLog(FinanceInfo plan, String id)
        {
            return planDAO.add(xml, plan, id);
        }

        public bool modifyPlan(FinanceInfo plan, String accId)
        {
            return planDAO.modify(xml, plan, accId);
        }

        public bool removePlan(FinanceInfo plan, String accId)
        {
            return planDAO.remove(xml, plan, accId);
        }

        /**
         * Withdrawal Service
         */
        public List<string> getATMACBList()
        {
            return wdhDAO.getATMACBList(xml);
        }

        public DataTable getWithdrawalData(string id)
        {
            return wdhDAO.getDataList(xml, id);
        }

        public Withdrawal getWithdrawalById(string id, string accId)
        {
            return wdhDAO.getById(xml, id, accId);
        }

        public List<FinanceInfo> getWithdrawalList(string accId)
        {
            return wdhDAO.getList(xml, accId);
        }

        public bool addWithdrawalLog(FinanceInfo wdh, String id)
        {
            return wdhDAO.add(xml, wdh, id);
        }

        public bool modifyWithdrawal(FinanceInfo wdh, String accId)
        {
            return wdhDAO.modify(xml, wdh, accId);
        }

        public bool removeWithdrawal(FinanceInfo wdh, String accId)
        {
            return wdhDAO.remove(xml, wdh, accId);
        }

        public DataTable getWithdrawalDataByMonth(string id, int month, int year)
        {
            List<FinanceInfo> list = wdhDAO.getWithdrawalListByMonth(xml, id, month, year);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Date");
            dt.Columns.Add("ATM");
            foreach (FinanceInfo info in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = info.Id;
                row["Amount"] = info.Amount.ToString();
                row["Date"] = info.DateExpense.ToString("dd.MM.yyyy");
                row["ATM"] = info.Description;
            }
            return dt;
        }
    }
}
