using NUnit.Framework;
using TestHelpers.KeyBoardMouse;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Allows you to pass write to a node that may be in a range index, sometimes it moves around
        /// Throws exception if not found for debugging purposes.
        /// NOTE: starts from TOP and goes to BOTTOM.       
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="indexStart"></param>
        /// <param name="indexEnd"></param>
        /// <param name="textOutput"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <param name="timeoutMilliseconds"></param>
        /// <param name="pollingIntervalMilliseconds"></param>
        /// <returns></returns>
        public static bool WriteTextArrayMultiIndex(string elementName, AccessibleNode? parent, Role role,
            int indexStart, int indexEnd, string textOutput, State[]? states = null,
            int index = 0, int timeoutMilliseconds = Globals.MaxWaitTime, int pollingIntervalMilliseconds = Globals.MinPollingTime)
        {
            DebugOutput($"WriteTextArrayMultiIndex: '{elementName}' | start/end index = '{indexStart}/{indexEnd}' | Value = '{textOutput}'");

            for (int countIdx = indexStart; countIdx >= indexEnd; countIdx--)
            {
                DebugOutput($"| Index {countIdx} - Find: {elementName}");
                var nodeFound = FindNodeByRoleContainsArray(elementName, "", countIdx, role, parent);
                if (nodeFound != null)
                {
                    DebugOutput($"--Found at Index {countIdx}");
                    var rect = GetNodeRect(nodeFound);
                    MouseHelper.Click(rect, ClickPoint.Center);
                    OracleUtilities.Sleep(800);
                    KeyboardHelper.TypeText(textOutput);
                    return true;
                }
            }

            Assert.Fail($"Could not locate: {elementName} | start/end index = {indexStart}/{indexEnd}");
            return false;
        }
    }
}