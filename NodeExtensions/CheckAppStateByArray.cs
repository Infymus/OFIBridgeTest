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
        /// Same call - but allows passing an Index
        /// NOTE: This always goes in REVERESE as most of the nodes are either -1 or -2 from what AccessBridgeExplorer says they are.
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="indexInParent"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckAppStateByArray(string findNodeName, string nodeContains, int indexInParent,
            AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateByArray: '{findNodeName}' | Contains = '{nodeContains}' | Index = '{indexInParent}'");
            var isAppState = FindNodeByRoleContainsArray(findNodeName, nodeContains, indexInParent, role, parent);
            if (isAppState != null)
            {
                if (isAppState.GetParent().GetParent() is AccessibleContextNode successMessageDialog)
                {
                    DebugOutput($"| Found '{findNodeName}' | index = '{indexInParent}'");
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