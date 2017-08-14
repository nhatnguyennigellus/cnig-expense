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
        private static DiaryService instance = null;
        private XmlDataSource xml;
        AccountDAO accDAO = new AccountDAO();
        IncomeDAO incDAO = new IncomeDAO();
        PaymentDAO payDAO = new PaymentDAO();
        PlanDAO planDAO = new PlanDAO();
        WithdrawalDAO wdhDAO = new WithdrawalDAO();
        QuickEntryDAO qeDAO = new QuickEntryDAO();
        BankDAO bankDAO = new BankDAO();

        protected DiaryService()
        {
            xml = XmlDataSource.getInstance();
        }

        public static DiaryService getInstance()
        {
            if (instance == null)
            {
                instance = new DiaryService();
            }
            return instance;
        }
        public void chooseDatabase(int dbIdx)
        {
            if (dbIdx == XMLXpathConfig.SANDBOX)
            {
                xml.XmlPath = ConfigurationManager.AppSettings.Get("SandboxDb");
            }
            else if (dbIdx == XMLXpathConfig.HOUSE)
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

        public bool existedAccount(string id)
        {
            return accDAO.existedAccount(xml, id);
        }

        public bool addAccount(Account account)
        {
            return accDAO.addAccount(xml, account);
        }

        public bool addProfile(Account account)
        {
            return accDAO.addProfile(xml, account);
        }
         
        public bool modifyAccount(Account account)
        {
            return accDAO.modifyAccount(xml, account);
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
            dt.Columns.Add("Budget");
            foreach (FinanceInfo info in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = info.Id;
                row["Amount"] = info.Amount.ToString();
                row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                row["Description"] = info.Description;
                row["Budget"] = info.Budget.ToString() == "0" ? "Bank" : "Cash";
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
            dt.Columns.Add("Budget");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense >= dateFrom.AddDays(-1) 
                    && info.DateExpense <= dateTo.AddDays(1))
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                    row["Description"] = info.Description;
                    row["Budget"] = info.Budget.ToString() == "0" ? "Bank" : "Cash";
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
            dt.Columns.Add("Budget");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense.ToShortDateString().Equals(date.ToShortDateString()))
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                    row["Description"] = info.Description;
                    row["Budget"] = info.Budget.ToString() == "0" ? "Bank" : "Cash";
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
            dt.Columns.Add("Budget");
            foreach (FinanceInfo info in list)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
                row["ID"] = info.Id;
                row["Amount"] = info.Amount.ToString();
                row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                row["Description"] = info.Description;
                row["Budget"] = info.Budget.ToString() == "0" ? "Bank" : "Cash";
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
            dt.Columns.Add("Budget");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense >= dateFrom.AddDays(-1) 
                    && info.DateExpense <= dateTo.AddDays(1))
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                    row["Description"] = info.Description;
                    row["Budget"] = info.Budget.ToString() == "0" ? "Bank" : "Cash";
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
            dt.Columns.Add("Budget");
            foreach (FinanceInfo info in list)
            {
                if (info.DateExpense.ToShortDateString().Equals(date.ToShortDateString()))
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(row);
                    row["ID"] = info.Id;
                    row["Amount"] = info.Amount.ToString();
                    row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                    row["Description"] = info.Description;
                    row["Budget"] = info.Budget.ToString() == "0" ? "Bank" : "Cash";
                }
            }
            return dt;
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

        public List<string> getPayType()
        {
            return payDAO.getPayType(xml);
        }
        /**
         * Plan Service
         */
        public DataTable getPlanData(string id)
        {
            return planDAO.getDataList(xml, id);
        }

        public List<FinanceInfo> getPlanList(string accId)
        {
            return planDAO.getList(xml, accId);
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

        public void p2P(FinanceInfo src, FinanceInfo dest)
        {
            dest.DateExpense = DateTime.Now;
            dest.Amount = src.Amount;
            dest.Currency = src.Currency;
            dest.Description = src.Description;
            dest.Budget = src.Budget;
        }

        /**
         * Withdrawal Service
         */
        
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

        public bool addWithdrawalLog(Withdrawal wdh, String accId)
        {
            return wdhDAO.add(xml, wdh, accId);
        }

        public bool modifyWithdrawal(Withdrawal wdh, String accId)
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
                row["Date"] = info.DateExpense.ToString("yyyy.MM.dd");
                row["ATM"] = info.Description;
            }
            return dt;
        }

        /**
         * Quick Entry
         */
        public int generateQEId(string accId)
        {
            int idx = 0;

            List<QuickEntry> list = qeDAO.getList(xml, accId);
            if (list.Count > 0)
            {
                idx = list[list.Count - 1].Id + 1;
            }

            return idx;
        }

        public DataTable getQEData(string id)
        {
            return qeDAO.getDataList(xml, id);
        }

        public List<QuickEntry> getQEDataByType(string accId, string type)
        {
            return qeDAO.getDataListByType(xml, accId, type);
        }

        public QuickEntry getById(string accId, string id)
        {
            return qeDAO.getById(xml, id, accId);
        }

        public bool addQE(QuickEntry qe, string accId)
        {
            return qeDAO.add(xml, qe, accId);
        }

        public bool modifyQE(QuickEntry qe, string accId)
        {
            return qeDAO.modify(xml, qe, accId);
        }

        public bool removeQE(QuickEntry qe, string accId)
        {
            return qeDAO.remove(xml, qe, accId);
        }

        /**
         * ATM
         **/
        public List<String> getOtherATMList(Account account)
        {
            return wdhDAO.getATMOtherList(xml, account);
        }

        public List<String> getATMListByBank(String bank)
        {
            return wdhDAO.getATMListByBank(xml, bank);
        }

        public int getWdhFee(String bank, int io)
        {
            return wdhDAO.getWithdrawFee(xml, bank, io);
        }

        public bool addATM(string atmName, string bank)
        {
            return wdhDAO.addATM(xml, atmName, bank);
        }

        public bool addATM(string atmName, Account account)
        {
            return wdhDAO.addATM(xml, atmName, account);
        }

        public bool removeATM(string atmName, string bank)
        {
            return wdhDAO.removeATM(xml, atmName, bank);
        }
        public bool removeATM(string atmName, Account account)
        {
            return wdhDAO.removeATM(xml, atmName, account);
        }
        
        /*
         * Bank
         */
        public bool existedBank(string bank)
        {
            return bankDAO.existedBank(xml, bank);
        }

        public bool addBank(Bank bank)
        {
            return bankDAO.addBank(xml, bank);
        }
    }
}
