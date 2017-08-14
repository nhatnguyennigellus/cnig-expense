using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Entity
{
    public class Error
    {
        public static string ERROR_PREFIX = "E";
        public static string MESSAGE_PREFIX = "M";
        public static int CODE_LENGTH = 5;

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
