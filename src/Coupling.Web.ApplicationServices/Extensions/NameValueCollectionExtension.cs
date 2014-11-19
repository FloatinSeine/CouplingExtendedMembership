using System;
using System.Collections.Specialized;

namespace Coupling.Web.ApplicationServices.Extensions
{
    public static class NameValueCollectionExtension
    {
        public static string GetValue(this NameValueCollection coll, string key, string defaultValue)
        {
            return string.IsNullOrEmpty(coll[key]) ? defaultValue : coll[key];
        }

        public static int GetValue(this NameValueCollection coll, string key, int defaultValue)
        {
            return string.IsNullOrEmpty(coll[key]) ? defaultValue : Convert.ToInt32(coll[key]);
        }

        public static bool GetValue(this NameValueCollection coll, string key, bool defaultValue)
        {
            return string.IsNullOrEmpty(coll[key]) ? defaultValue : Convert.ToBoolean(coll[key]);
        }
    }
}
