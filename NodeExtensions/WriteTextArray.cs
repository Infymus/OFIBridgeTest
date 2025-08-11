using TestHelpers.KeyBoardMouse;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Writes out text at a node by name, role and index
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexInParent"></param>
        /// <param name="textOutput"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool WriteTextArray(string elementName, AccessibleNode? parent, Role role, int indexInParent, string textOutput,
            State[]? states = null, int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"WriteTextArray: '{elementName}' | index = '{indexInParent}' | Output = '{textOutput}'");

            var nodeFound = FindNodeByRoleContainsArray(elementName, "", indexInParent, role, parent);

            if (nodeFound == null) return false;
            DebugOutput($"| Found : '{elementName}' | index = '{indexInParent}'");
            // Get coordinates
            var rect = GetNodeRect(nodeFound);
            if (rect == null) return false;

            // Click the coordinates
            MouseHelper.Click(rect, ClickPoint.Center);

            // Delay for a second so the field can render
            OracleUtilities.Sleep(800);

            // Type the text
            KeyboardHelper.TypeText(textOutput);
            return true;
        }

    }
}
