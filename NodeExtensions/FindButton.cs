using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Used specifically by BaseTest.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        public static AccessibleContextNode? FindButton(AccessibleContextNode node, string buttonName)
        {
            if (node.GetInfo().role == "push button" && node.GetInfo().name == buttonName)
            {
                return node;
            }

            foreach (var child in node.GetChildren())
            {
                if (child is not AccessibleContextNode childNode) continue;
                var found = FindButton(childNode, buttonName);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }
    }
}