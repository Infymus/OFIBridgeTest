using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        private static AccessibleContextNode? FindNodeByArrayRecursiveByIndexAndDimensions(string nodeName, AccessibleNode? parent, Role role,
                    int indexInParent, int nHeight, int nWidth, int index, ref int matchCount, int depth)
        {
            if (parent == null) return null;
            var childNodes = parent.GetChildren();
            foreach (var child in childNodes)
            {
                if (child is not AccessibleContextNode contextNode) continue;
                contextNode.Refresh();
                var info = contextNode.GetInfo();
                if (info.indexInParent == indexInParent &&
                    (info.name.StartsWith(nodeName) || info.name.Contains(nodeName)) &&
                    info.role == role.GetStringValue() &&
                    info.height == nHeight &&
                    info.width == nWidth)
                {
                    if (matchCount == index)
                    {
                        return contextNode;
                    }
                    matchCount++;
                    DebugOutput($"| MatchCount = {matchCount} | depth = {depth} | index = {index}");
                }
                ;
                var foundNode = FindNodeByArrayRecursiveByIndexAndDimensions(nodeName, contextNode, role, indexInParent, nHeight, nWidth, index, ref matchCount, depth + 1);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
    }
}
