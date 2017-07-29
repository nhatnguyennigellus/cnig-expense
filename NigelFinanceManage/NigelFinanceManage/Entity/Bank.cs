using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Entity
{
    public class Bank
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int innerWdhFee;

        public int InnerWdhFee
        {
            get { return innerWdhFee; }
            set { innerWdhFee = value; }
        }

        private int outerWdhFee;

        public int OuterWdhFee
        {
            get { return outerWdhFee; }
            set { outerWdhFee = value; }
        }

        private string currency;

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
    }
}
