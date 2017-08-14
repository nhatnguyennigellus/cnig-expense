using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Entity
{
    public class XMLXpathConfig
    {
        public static int SANDBOX = 0;
        public static int HOUSE = 1;

        private string method;

        public string Method
        {
            get { return method; }
            set { method = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        private int nofParams;

        public int NofParams
        {
            get { return nofParams; }
            set { nofParams = value; }
        }

        private string dao;

        public string Dao
        {
            get { return dao; }
            set { dao = value; }
        }
    }
}
