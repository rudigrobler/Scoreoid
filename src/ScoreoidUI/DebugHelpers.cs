using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScoreoidUI
{
    public static class DebugHelpers
    {
        public static string ToDebugString(this object o, string label)
        {
            var builder = new StringBuilder();
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