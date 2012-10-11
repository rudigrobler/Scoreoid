using System.Collections.Generic;
using System.Reflection;

namespace Scoreoid
{
    public static class ScoreoidHelpers
    {
        public static void Inject<T>(this Dictionary<string, string> p, T o)
        {
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