using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Entity
{
    public class Withdrawal : FinanceInfo
    {
        public static String PREFIX = "W";
        public static int INNER_FEE = 0;
        public static int OUTER_FEE = 1;

        private int extra;

        public int Extra
        {
            get { return extra; }
            set { extra = value; }
        }

        public Withdrawal()
        {

        }
    }
}
