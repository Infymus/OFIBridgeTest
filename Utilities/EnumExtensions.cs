using System.Reflection;

namespace Tests.Utilities
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<StringValueAttribute>();

            return attribute == null ? value.ToString() : attribute.StringValue;
        }
    }

}
