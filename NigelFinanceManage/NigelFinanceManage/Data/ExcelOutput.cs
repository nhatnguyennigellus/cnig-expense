using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using System.Configuration;

namespace NigelFinanceManage.Data
{
    public class ExcelOutput
    {
        private string path;
        public string Path
        {
            get { return path; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public ExcelOutput(string fileName)
        {
            path = ConfigurationManager.AppSettings.Get("ExcelPath")
                + fileName;
        }
    }
}
