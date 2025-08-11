using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        public static bool ClickByArrayIndexAndDimensions(string findNodeName, string nodeContains, int indexInParent,
            int nWidth, int nHeight, AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"ClickByArrayIndexAndDimensions: '{findNodeName}' | Contains = '{nodeContains}' | Index = '{indexInParent}'");
            var nodeFound = FindNodeByArrayIndexAndDimensions(findNodeName, nodeContains, indexInParent, nWidth, nHeight, role, parent);
            if (nodeFound != null)
            {
                //                if (nodeFound.GetParent().GetParent() is AccessibleContextNode successMessageDialog)
                //{
                DebugOutput($"| Found '{nodeFound}' | index = '{indexInParent}'");
                // Get coordinates
                var rect = GetNodeRect(nodeFound);
                if (rect == null) return false;

                // Click the coordinates
                MouseHelper.Click(rect, ClickPoint.Center);

                return true;
                //}
            }
            return false;
        }
    }
}