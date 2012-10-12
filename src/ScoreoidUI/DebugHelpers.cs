using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ScoreoidUI
{
    public static class DebugHelpers
    {
        public static string ToDebugString(this object o, string label)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(label);

            IEnumerable<PropertyInfo> properties = o.GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                builder.Append(property.Name);
                builder.Append(": ");
                if (property.GetValue(o) == null)
                {
                    builder.AppendLine("null");
                }
                else
                {
                    builder.AppendLine(property.GetValue(o).ToString());
                }
            }

            return builder.ToString();
        }
    }
}
