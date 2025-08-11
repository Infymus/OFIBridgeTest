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
        /// Checks to see if a node has appeared and if so returns True
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="parent"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckAppState(string findNodeName, string nodeContains, AccessibleNode? parent,
            Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppState: '{findNodeName}' | Contains = '{nodeContains}' | Role = '{role}'");
            var isAppState = FindNodeByRoleContains(findNodeName, nodeContains, role, parent, states);
            if (isAppState != null)
            {
                if (isAppState.GetParent().GetParent() is AccessibleContextNode successMessageDialog)
                {
                    DebugOutput($"| Found '{findNodeName}'");
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}