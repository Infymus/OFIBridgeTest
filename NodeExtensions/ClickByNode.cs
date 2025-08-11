using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Old Method left in place for AP tests.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool ClickByNode(this AccessibleContextNode? node)
        {
            if (node == null) return false;

            // Get coordinates
            var rect = node.GetNodeRect();
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);
            return true;
        }
    }
}