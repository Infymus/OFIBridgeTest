using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        private static AccessibleContextNode? FindNodeByNameAndIndexRecursive(string nodeName, AccessibleNode? parent, Role role, int indexInParent,
            int index, ref int matchCount, int depth)
        {
            if (parent == null) return null;
            var childNodes = parent.GetChildren();
            foreach (var child in childNodes)
            {
                if (child is not AccessibleContextNode contextNode) continue;
                contextNode.Refresh();
                var info = contextNode.GetInfo();
                if (info.indexInParent == indexInParent && info.name.StartsWith(nodeName) && info.role == role.GetStringValue()
                    || info.indexInParent == indexInParent && info.name.Contains(nodeName) && info.role == role.GetStringValue())
                {
                    if (matchCount == index)
                    {
                        return contextNode;
                    }
                    matchCount++;
                }
                var foundNode = FindNodeByNameAndIndexRecursive(nodeName, contextNode, role, indexInParent, index, ref matchCount, depth + 1);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
    }
}