using System;
using System.Collections.Specialized;

namespace Coupling.Web.ApplicationServices.Extensions
{
    public static class NameValueCollectionExtension
    {
        public static string GetValue(this NameValueCollection coll, string key, string defaultValue)
        {
            var s = coll.Get(key);
            return string.IsNullOrEmpty(s) ? defaultValue : s;
        }

        public static int GetValue(this NameValueCollection coll, string key, int defaultValue)
        {
            var s = coll.Get(key);
            return string.IsNullOrEmpty(s) ? defaultValue : Convert.ToInt32(s);
        }

        public static bool GetValue(this NameValueCollection coll, string key, bool defaultValue)
        {
            var s = coll.Get(key);
            return string.IsNullOrEmpty(s) ? defaultValue : Convert.ToBoolean(s);
        }
    }
}
