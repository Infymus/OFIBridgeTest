namespace Tests.Utilities
{
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException(string message) : base(message) { }
    }

    public class NodeNotEditableException : Exception
    {
        public NodeNotEditableException(string message) : base(message) { }
    }
}
