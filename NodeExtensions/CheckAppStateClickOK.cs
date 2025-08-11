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
        /// Allows finding any node with a particular role. Assumes that the okButton parameter is a Role.PushButton.
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="okButton"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckAppStateClickOK(string findNodeName, string nodeContains, string okButton,
            AccessibleNode? parent, Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateClickOK: '{findNodeName}' | Contains = '{nodeContains}' | Button = '{okButton}'");
            var nodeFound = FindNodeByRoleContains(findNodeName, nodeContains, role, parent);
            if (nodeFound != null)
            {
                if (nodeFound.GetParent().GetParent() is AccessibleContextNode successMessageDialog)
                {
                    DebugOutput($"| Found '{findNodeName}'");

                    // Find the okButton
                    var OracleClickArea = FindNodeByRole(okButton, Role.PushButton, parent, states, index);

                    var rect = GetNodeRect(OracleClickArea);
                    if (rect == null)
                    {
                        DebugOutput($"| Warning - RECT is NULL, Cannot click '{okButton}'");
                        return false;
                    }

                    MouseHelper.Click(rect, ClickPoint.Center);

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