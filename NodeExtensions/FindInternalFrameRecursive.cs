using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        // Specifically find an internal frame by framename
        // This works identically to FindNodeByRole() - except it targets just an "InternalFrame"
        public static AccessibleContextNode? FindInternalFrameRecursive(string frameName, AccessibleNode? parent, int index, ref int matchCount, int depth)
        {
            if (parent == null) return null;

            var childNodes = parent.GetChildren();
            foreach (var child in childNodes)
            {
                if (child is not AccessibleContextNode contextNode) continue;

                Role internalFrame = Role.InternalFrame;
                contextNode.Refresh();
                var info = contextNode.GetInfo();
                if (info.name.Contains(frameName) && info.role == internalFrame.GetStringValue())
                {
                    if (matchCount == index)
                    {
                        return contextNode;
                    }
                    matchCount++;
                }
                var foundNode = FindInternalFrameRecursive(frameName, contextNode, index, ref matchCount, depth + 1);
                if (foundNode != null)
                {
                    return foundNode;
                }
            }
            return null;
        }
    }
}