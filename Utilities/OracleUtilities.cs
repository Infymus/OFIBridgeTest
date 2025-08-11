using System.Globalization;
using System.Reflection;

namespace Tests.Utilities
{

    public static class OracleUtilities
    {

        /// <summary>
        /// A local debugoutput only (no test case writing)
        /// </summary>
        /// <param name="inDebugData"></param>
        private static void DebugOutput(string inDebugData)
        {
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("MM-dd-yyyy @ hh:mm:ss tt");
            System.Diagnostics.Debug.WriteLine($"{formattedDate} : {inDebugData}");
        }

        /// <summary>
        /// Needed this to clean up the Oracle Forms currency which MAY have junk in the trunk
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
        public static string ParseOracleCurrency(string inString)
        {
            string result = inString.Replace(",", "");
            decimal number = decimal.Parse(result, CultureInfo.InvariantCulture);
            decimal roundedNumber = Math.Round(number, 2);
            result = roundedNumber.ToString("F2", CultureInfo.InvariantCulture);
            return result;
        }

        /// <summary>
        /// Allows adding oracle form currency amounts that have been parsed by ParseOracleCurrency.
        /// </summary>
        /// <param name="amounts"></param>
        /// <returns></returns>
        public static decimal SumCurrencyAmounts(params string[] amounts)
        {
            return amounts.Select(a => decimal.Parse(a, CultureInfo.InvariantCulture)).Sum();
        }

        /// <summary>
        /// Pulls an ENUM description from the Enum Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                System.ComponentModel.DescriptionAttribute[] attributes =
                    (System.ComponentModel.DescriptionAttribute[])field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// Returns a boolean value out of a enum where enum is TRUE/FALSE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static bool ConvertEnumToBool<T>(T enumValue) where T : Enum
        {
            // Check if the enum value represents True or False
            if (Enum.IsDefined(typeof(T), enumValue))
            {
                // Return true if the value is True, false otherwise
                return enumValue.ToString() == "True";
            }
            else return false;
        }

        /// <summary>
        /// Our overloaded sleep so we can debug out 
        /// </summary>
        /// <param name="sleepTime"></param>
        public static void Sleep(int sleepTime)
        {
            DebugOutput($"...Sleep({sleepTime / 1000.0})");
            Thread.Sleep(sleepTime);
        }

        /// <summary>
        /// Oracle requires Format of "03 MAR 2024 00:00:00" timestamp. So we pass a regular datetime and convert it.
        /// </summary>
        /// <param name="inDate"></param>
        /// <returns></returns>
        /// OLD: dd-MMM-yyyy HH:mm:s
        public static string convertDateTimeToOracle(DateTime inDate)
        {
            return inDate.ToString("dd-MMM-yyyy 00:00:00", CultureInfo.InvariantCulture).ToUpper();
        }

        /// <summary>
        /// For Accounting Credit/Debit Memos
        /// </summary>
        /// <param name="inDate"></param>
        /// <returns></returns>
        public static string convertDateToOracleCompact(DateTime inDate)
        {
            return inDate.ToString("ddMMMyy", CultureInfo.InvariantCulture).ToUpper();
        }

        /// <summary>
        /// Oracle date without the time
        /// </summary>
        /// <param name="inDate"></param>
        /// <returns></returns>
        public static string convertDateToOracle(DateTime inDate)
        {
            return inDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture).ToUpper();
        }

        /// <summary>
        /// Compares two strings for equality, ignoring case.
        /// </summary>
        /// <param name="stringA">The first string to compare.</param>
        /// <param name="stringB">The second string to compare.</param>
        /// <returns>True if the strings are equal ignoring case; otherwise, false.</returns>
        public static bool EqualsIgnoreCase(this string stringA, string stringB)
        {
            return string.Equals(stringA, stringB, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether a string contains another string, ignoring case.
        /// </summary>
        /// <param name="stringA">The string to search within.</param>
        /// <param name="stringB">The string to search for.</param>
        /// <returns>True if the stringA contains stringB ignoring case; otherwise, false.</returns>
        public static bool ContainsIgnoreCase(this string stringA, string stringB)
        {
            return stringA != null && stringA.IndexOf(stringB, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        // Allows us to parse out doubles and return them correctly (like quantities of "3282158.32899")
        public static double SafeStringToDouble(string input)
        {
            if (double.TryParse(input, out double result))
            {
                return result == 0 ? 0.0 : result;
            }
            return 0.0;
        }



    }
}
