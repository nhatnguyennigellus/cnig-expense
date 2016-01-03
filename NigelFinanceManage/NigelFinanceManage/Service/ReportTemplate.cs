using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using NigelFinanceManage.Data;

namespace NigelFinanceManage.Service
{
    public abstract class ReportTemplate
    {
        public void generateReport()
        {
            printTitle();
            printData();
            printTotal();
            save();
        }

        public abstract void printTitle();
        public abstract void printData();
        public abstract void printTotal();
        public abstract void save();
    }
}
