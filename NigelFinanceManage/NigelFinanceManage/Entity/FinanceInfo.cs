using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Entity
{
    public class FinanceInfo
    {
        public static int ID_SUFFIX_LENGTH = 6;

        private String id;
        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private String currency;
        public String Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        private DateTime dateExpense;
        public DateTime DateExpense
        {
            get { return dateExpense; }
            set { dateExpense = value; }
        }

        private String description;
        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public FinanceInfo() {

        }

        public void ExpenseInfo(String id, int amount, String currency, DateTime dateExpense, String description) {
            this.id = id;
            this.amount = amount;
            this.currency = currency;
            this.dateExpense = dateExpense;
            this.description = description;
        }
    }
}
