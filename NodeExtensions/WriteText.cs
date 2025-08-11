using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Writes text out with optional FindGroupName
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="parent"></param>
        /// <param name="textOutput"></param>
        /// <param name="findGroupName"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool WriteText(string findNodeName, AccessibleNode? parent, string textOutput,
            string findGroupName = null, State[]? states = null, int index = 0)
        {
            DebugOutput($"WriteText: '{findNodeName}' | Value = '{textOutput}'");
            var ReadOracleEditBox = FindNodeByRole(findNodeName, Role.Text, parent, states, index);
            if (ReadOracleEditBox == null) return false;

            // Get coordinates
            var rect = GetNodeRect(ReadOracleEditBox);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);

            // Type the text
            KeyboardHelper.TypeText(textOutput);
            return true;
        }
    }
}
