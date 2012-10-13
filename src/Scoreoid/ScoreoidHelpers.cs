using System;
using System.Collections.Generic;
using System.Reflection;

namespace Scoreoid
{
    public static class ScoreoidHelpers
    {
        public static void Inject<T>(this Dictionary<string, string> p, T o)
        {
            if (p == null)
                throw new ArgumentNullException("p", "p can not be null.");
            if (o == null)
                throw new ArgumentNullException("o", "o can not be null.");

            IEnumerable<PropertyInfo> properties = o.GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(o) != null)
                {
                    p.Add(property.Name, property.GetValue(o).ToString());
                }
            }
        }

        public static string TrimResponse(this string str, string expectedResponse)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentNullException("str", "str can not be null or empty.");
            if (string.IsNullOrEmpty(expectedResponse))
                throw new ArgumentNullException("expectedResponse", "expectedResponse can not be null or empty.");

            int startTagLength = expectedResponse.Length + 2;
            int endTagLength = expectedResponse.Length + 3;
            return
                str.Substring(startTagLength, str.Length - startTagLength - endTagLength).Replace("<![CDATA[",
                                                                                                  string.Empty).Replace(
                                                                                                      "]]>",
                                                                                                      string.Empty);
        }
    }
}