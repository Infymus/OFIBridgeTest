namespace Tests.Utilities
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class StringValueAttribute : Attribute
    {
        public string StringValue { get; }

        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
    }
}
