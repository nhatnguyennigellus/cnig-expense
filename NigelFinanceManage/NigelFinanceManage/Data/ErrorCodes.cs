using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NigelFinanceManage.Data
{
    public class ErrorCodes
    {
        /*
         * ERRORS
        */

        /// <summary>
        ///	No data
        /// </summary>
        public static string e0001 = "E0001";

        /// <summary>
        ///	Amount is required!
        /// </summary>
        public static string e0002 = "E0002";

        /// <summary>
        ///	ATM is required!
        /// </summary>
        public static string e0003 = "E0003";

        /// <summary>
        ///	ATM Name is required!
        /// </summary>
        public static string e0004 = "E0004";

        /// <summary>
        ///	Bank balance is required!
        /// </summary>
        public static string e0005 = "E0005";

        /// <summary>
        ///	Bank name is required!
        /// </summary>
        public static string e0006 = "E0006";

        /// <summary>
        ///	Cannot remove this ATM! Maybe this ATM name does not exist!
        /// </summary>
        public static string e0007 = "E0007";

        /// <summary>
        ///	Cash amount is required!
        /// </summary>
        public static string e0008 = "E0008";

        /// <summary>
        ///	Description is required!
        /// </summary>
        public static string e0009 = "E0009";

        /// <summary>
        ///	Error Description is required!
        /// </summary>
        public static string e0010 = "E0010";

        /// <summary>
        ///	Error occurred!
        /// </summary>
        public static string e0011 = "E0011";

        /// <summary>
        ///	Excel output configured failed!
        /// </summary>
        public static string e0012 = "E0012";

        /// <summary>
        ///	Existed Account!
        /// </summary>
        public static string e0013 = "E0013";

        /// <summary>
        ///	Field must be entered!
        /// </summary>
        public static string e0014 = "E0014";

        /// <summary>
        ///	Inner Wdh Fee is required!
        /// </summary>
        public static string e0015 = "E0015";

        /// <summary>
        ///	Invalid ID or PIN!
        /// </summary>
        public static string e0016 = "E0016";

        /// <summary>
        ///	Method name is required!
        /// </summary>
        public static string e0017 = "E0017";

        /// <summary>
        ///	Must not &lt; 0!
        /// </summary>
        public static string e0018 = "E0018";

        /// <summary>
        ///	Must not &lt;= 0!
        /// </summary>
        public static string e0019 = "E0019";

        /// <summary>
        ///	Name is required!
        /// </summary>
        public static string e0020 = "E0020";

        /// <summary>
        ///	No of Params is required!
        /// </summary>
        public static string e0021 = "E0021";

        /// <summary>
        ///	No row selected!
        /// </summary>
        public static string e0022 = "E0022";

        /// <summary>
        ///	Not enough budget!
        /// </summary>
        public static string e0023 = "E0023";

        /// <summary>
        ///	Outer Wdh Fee  is required!
        /// </summary>
        public static string e0024 = "E0024";

        /// <summary>
        ///	Path does not exist!
        /// </summary>
        public static string e0025 = "E0025";

        /// <summary>
        ///	PIN is required!
        /// </summary>
        public static string e0026 = "E0026";

        /// <summary>
        ///	PIN mismatch!
        /// </summary>
        public static string e0027 = "E0027";

        /// <summary>
        ///	Please enter number only!
        /// </summary>
        public static string e0028 = "E0028";

        /// <summary>
        ///	Username is required!
        /// </summary>
        public static string e0029 = "E0029";

        /// <summary>
        ///	XPath is required!
        /// </summary>
        public static string e0030 = "E0030";

        /// <summary>
        ///	File does not exist!
        /// </summary>
        public static string e0031 = "E0031";

        /*
         * MESSAGE
         */

        /// <summary>
        ///	Data loaded
        /// </summary>
        public static string m0001 = "M0001";

        /// <summary>
        ///	Account registered!
        /// </summary>
        public static string m0002 = "M0002";

        /// <summary>
        ///	Added new ATM!
        /// </summary>
        public static string m0003 = "M0003";

        /// <summary>
        ///	Bank added!
        /// </summary>
        public static string m0004 = "M0004";

        /// <summary>
        ///	Data loaded!
        /// </summary>
        public static string m0005 = "M0005";

        /// <summary>
        ///	DB XML Path updated!
        /// </summary>
        public static string m0006 = "M0006";

        /// <summary>
        ///	Error added!
        /// </summary>
        public static string m0007 = "M0007";

        /// <summary>
        ///	Error removed!
        /// </summary>
        public static string m0008 = "M0008";

        /// <summary>
        ///	Error updated!
        /// </summary>
        public static string m0009 = "M0009";

        /// <summary>
        ///	Excel output configured successfully!
        /// </summary>
        public static string m0010 = "M0010";

        /// <summary>
        ///	Income log added!
        /// </summary>
        public static string m0011 = "M0011";

        /// <summary>
        ///	Income log updated!
        /// </summary>
        public static string m0012 = "M0012";

        /// <summary>
        ///	Path exists!
        /// </summary>
        public static string m0013 = "M0013";

        /// <summary>
        ///	Payment info is rolled back to plan item!
        /// </summary>
        public static string m0014 = "M0014";

        /// <summary>
        ///	Payment log added!
        /// </summary>
        public static string m0015 = "M0015";

        /// <summary>
        ///	Payment log updated!
        /// </summary>
        public static string m0016 = "M0016";

        /// <summary>
        ///	Plan added!
        /// </summary>
        public static string m0017 = "M0017";

        /// <summary>
        ///	Plan implemented!
        /// </summary>
        public static string m0018 = "M0018";

        /// <summary>
        ///	Plan modified!
        /// </summary>
        public static string m0019 = "M0019";

        /// <summary>
        ///	Quick Entry added!
        /// </summary>
        public static string m0020 = "M0020";

        /// <summary>
        ///	Quick Entry modified!
        /// </summary>
        public static string m0021 = "M0021";

        /// <summary>
        ///	Quick Entry removed!
        /// </summary>
        public static string m0022 = "M0022";

        /// <summary>
        ///	Removed ATM
        /// </summary>
        public static string m0023 = "M0023";

        /// <summary>
        ///	Withdrawal Log added!
        /// </summary>
        public static string m0024 = "M0024";

        /// <summary>
        ///	Withdrawal Log modified!
        /// </summary>
        public static string m0025 = "M0025";

        /// <summary>
        ///	Withdrawal Log removed!
        /// </summary>
        public static string m0026 = "M0026";

        /// <summary>
        ///	Xpath added!
        /// </summary>
        public static string m0027 = "M0027";

        /// <summary>
        ///	Xpath updated!
        /// </summary>
        public static string m0028 = "M0028";

        /// <summary>
        ///	File exists!
        /// </summary>
        public static string m0029 = "M0029";

        /// <summary>
        ///	Plan removed!
        /// </summary>
        public static string m0030 = "M0030";

        /// <summary>
        ///	Xpath removed!
        /// </summary>
        public static string m0031 = "M0031";

    }
}
