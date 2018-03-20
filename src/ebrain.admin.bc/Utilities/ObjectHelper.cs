using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ebrain.admin.bc.Utilities
{
    public static class ObjectHelper
    {
        public static void SetDefault4AllValueTypeProperties(this object src, params string[] ExcludeFiles)
        {

        }

        /// <summary>
        /// Get branch of user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appContext"></param>
        /// <returns></returns>
        public static Guid? GetBranchOfCurrentUser(this Guid userId, ApplicationDbContext appContext)
        {
            var user = appContext.Users.FirstOrDefault(p => p.Id == userId.ToString());
            return user != null ? user.BranchId : Guid.Empty;
        }

        public static bool IsNullOrDefault<T>(this T? self) where T : struct { return !self.HasValue || self.Value.Equals(default(T)); }

        public static string ConvertArrayGuidToString(this Guid[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                ids = ids.Distinct().OrderBy(c => c).ToArray();
                StringBuilder sb = new StringBuilder();

                foreach (var id in ids)
                    sb.AppendFormat("{0},", id);
                var sReturn = sb.ToString();
                if (sReturn.EndsWith(","))
                {
                    var len = sReturn.Length;
                    sReturn = sReturn.Substring(0, len - 1);
                }
                return sReturn;
            }
            return string.Empty;
        }

        /// <summary>
        /// "Thu Feb 01 2018 00:00:00 GMT 0700(SE Asia Standard Time)";
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime BuildDateTimeFromSEFormat(this string dateString)
        {
            dateString = dateString.Substring(4, 12).Trim();
            return DateTime.ParseExact(dateString, "MMM dd yyyy", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// "Thu Feb 01 2018 00:00:00 GMT 0700(SE Asia Standard Time)";
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime BuildLastDateTimeFromSEFormat(this string dateString)
        {
            var date = BuildDateTimeFromSEFormat(dateString);
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
    }
}
