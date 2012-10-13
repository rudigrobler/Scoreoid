using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ScoreoidUI
{
    public static class DebugHelpers
    {
        public static string ToDebugString(this object o, string label)
        {
            if (o == null)
                throw new ArgumentNullException("o", "o can not be null.");

            var builder = new StringBuilder();
            builder.AppendLine(label);

            IEnumerable<PropertyInfo> properties = o.GetType().GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                builder.Append(property.Name);
                builder.Append(": ");
                builder.AppendLine(property.GetValue(o) == null ? "null" : property.GetValue(o).ToString());
            }

            return builder.ToString();
        }
    }
}