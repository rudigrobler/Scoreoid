using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Scoreoid
{
    public static class ScoreoidHelpers
    {
        public static void Inject<T>(this Dictionary<string, string> p, T o)
        {
            var properties = o.GetType().GetRuntimeProperties();
            foreach (var property in properties)
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
            return str.Substring(startTagLength, str.Length - startTagLength - endTagLength).Replace("<![CDATA[", string.Empty).Replace("]]>", string.Empty);   
        }
    }
}
