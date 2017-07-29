using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Entity
{
    public class Account
    {
        public Account()
        {

        }

        public Account(String id, String name, String bank, int balance, int cashWithdraw, String currency, string pin)
        {
            this.id = id;
            this.name = name;
            this.bank = bank;
            this.balance = balance;
            this.cashWithdraw = cashWithdraw;
            this.currency = currency;
            this.pin = pin;
        }

        private String id;
        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String bank;
        public String Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        private int balance;
        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        private int cashWithdraw;
        public int CashWithdraw
        {
            get { return cashWithdraw; }
            set { cashWithdraw = value; }
        }

        private String currency;
        public String Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        private string pin;
        public string Pin
        {
            get { return pin; }
            set { pin = value; }
        }

    }
}
