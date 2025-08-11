using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Handles popup Find Windows and enters the correct value.
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="writeValue"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckAppStateFindDialog(string findNodeName, string nodeContains, string writeValue, AccessibleNode? parent,
            Role role, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateFindDialog: '{findNodeName}' | Contains = '{nodeContains}'");
            var isAppState = FindNodeByRoleContains(findNodeName, nodeContains, role, parent);
            if (isAppState != null)
            {
                if (isAppState.GetParent().GetParent() is AccessibleContextNode successMessageDialog)
                {
                    DebugOutput($"| Found '{findNodeName}'");

                    // Clear the Input
                    var OracleClickArea = FindNodeByRole(" Find", Role.Text, parent, states, index);
                    if (OracleClickArea != null)
                    {
                        var rect = GetNodeRect(OracleClickArea);
                        if (rect != null)
                        {
                            MouseHelper.Click(rect, ClickPoint.TextEnd);
                            KeyboardHelper.ClearInputText(20);
                        }
                    }

                    // Write the Find Value
                    WriteTextArray(" Find", parent, Role.Text, 1, writeValue);
                    Click("Find ALT F", parent, Role.PushButton);

                    // Handle Multiples and click OK if found
                    if (CheckAppState(findNodeName, "", parent, Role.InternalFrame))
                        Click("OK ALT O", parent, Role.PushButton);

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