using NUnit.Framework;
using System.Diagnostics;
using TestHelpers.Enums;
using TestHelpers.KeyBoardMouse;
using Tests.Utilities;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// This method looking for a node between an index area and if it is found
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="endIdx"></param>
        /// <param name="startIdx"></param>
        /// <param name="checkParse"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int CheckAppStateByArrayIndex(string findNodeName, string nodeContains, int startIdx, int endIdx,
            AccessibleNode? parent, Role role, bool throwError, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateByArrayIndexClick: '{findNodeName}' | Contains = '{nodeContains}' | Index Start/End = '{startIdx}/{endIdx}'");
            for (int countIdx = startIdx; countIdx >= endIdx; countIdx--)
            {
                DebugOutput($"| Check Index {countIdx}");
                if (CheckAppStateByArray(findNodeName, nodeContains, countIdx, parent, role, states))
                {
                    DebugOutput($"| Found '{findNodeName}' @ Index '{countIdx}'");
                    return countIdx;
                }
            }
            if (throwError)
                Assert.Fail($"CheckAppStateByArrayIndexClick - Node with role '{role}' and name containing '{findNodeName}' at Index ({startIdx} thru {endIdx}) not found.");
            return 0;
        }
    }
}