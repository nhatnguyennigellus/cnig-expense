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
            set { path = value; }
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string extension;

        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }
        private string dayRangeFilename;

        public string DayRangeFilename
        {
            get { return dayRangeFilename; }
            set { dayRangeFilename = value; }
        }
        private string dayRangeTitle;

        public string DayRangeTitle
        {
            get { return dayRangeTitle; }
            set { dayRangeTitle = value; }
        }
        private int dayRangeParams;

        public int DayRangeParams
        {
            get { return dayRangeParams; }
            set { dayRangeParams = value; }
        }
        private string monthlyFilename;

        public string MonthlyFilename
        {
            get { return monthlyFilename; }
            set { monthlyFilename = value; }
        }
        private string monthlyTitle;

        public string MonthlyTitle
        {
            get { return monthlyTitle; }
            set { monthlyTitle = value; }
        }
        private int monthlyParams;

        public int MonthlyParams
        {
            get { return monthlyParams; }
            set { monthlyParams = value; }
        }
        private string dateFilename;

        public string DateFilename
        {
            get { return dateFilename; }
            set { dateFilename = value; }
        }
        private string dateTitle;

        public string DateTitle
        {
            get { return dateTitle; }
            set { dateTitle = value; }
        }
        private int dateParams;

        public int DateParams
        {
            get { return dateParams; }
            set { dateParams = value; }
        }
        private string dateFormat;

        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }

    }
}
