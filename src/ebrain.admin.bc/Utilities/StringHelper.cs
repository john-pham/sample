using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ebrain.admin.bc.Utilities
{
    public static class StringHelper
    {
        #region IntParseFast
        /// <summary>
        /// http://www.dotnetperls.com/int-parse-optimization
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IntParseFast(this string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char letter = value[i];
                result = 10 * result + (letter - 48);
            }
            return result;
        }
        /// <summary>
        /// test is number with 1000000 items
        /// IsDigitsOnly(Code below) : 384588
        /// TryParse:     639583
        /// Regex:        1329571
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static int IntParseFastWithCheck(string value)
        {
            if (value != null)
                value = value.Trim();
            return IntParseFast(value);
        }
        #endregion
              
        /// <summary>
        /// http://www.dotnetperls.com/int-parse-optimization
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long LongParseFast(this string value)
        {
            long result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char letter = value[i];
                result = 10 * result + (letter - 48);
            }
            return result;
        }

        /// <returns></returns>
        public static long longParseFastWithCheck(this string value)
        {
            if (value == null)
                return 0;
            if (value != null)
                value = value.Trim();

            return LongParseFast(value);
        }
        /// <summary>
        /// Dùng để cast những đối tượng còn nghi ngờ không phải số
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool CheckStringIsNumber(this string value)
        {
            if (value != null)
                value = value.Trim();

            #region Check In Valid
            if (string.IsNullOrEmpty(value))
                return false;

            #region test is number with 1000000 items
            if (IsDigitsOnly(value) == false)
                return false;
            #endregion
            #endregion
            return true;
        }
    }
}
