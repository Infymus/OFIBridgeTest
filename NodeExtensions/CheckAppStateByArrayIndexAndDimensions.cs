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
        /// Recursive to find by Dimensions (X/Y are useless don't use them)
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="indexInParent"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckAppStateByArrayIndexAndDimensions(string findNodeName, string nodeContains, int indexInParent,
            int nWidth, int nHeight, AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateByArrayIndexAndDimensions: '{findNodeName}' | Contains = '{nodeContains}' | Index = '{indexInParent}'");
            var isAppState = FindNodeByArrayIndexAndDimensions(findNodeName, nodeContains, indexInParent, nWidth, nHeight, role, parent);
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