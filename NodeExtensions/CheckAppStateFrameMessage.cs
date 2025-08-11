using NUnit.Framework;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Checks for internal frame oracle errors.
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="errorMessage"></param>
        /// <param name="throwExcept"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CheckAppStateFrameMessage(string findNodeName, string nodeContains, string errorMessage, bool throwExcept,
            AccessibleNode? parent, Role role = Role.InternalFrame, State[]? states = null, int index = 0)
        {
            DebugOutput($"CheckAppStateFrameMessage: '{findNodeName}' | Contains = '{nodeContains}'");
            var isAppState = FindNodeByRoleContains(findNodeName, nodeContains, role, parent);
            if (isAppState != null)
            {
                if (isAppState.GetParent().GetParent() is AccessibleContextNode successMessageDialog)
                {
                    DebugOutput($"| Found '{findNodeName}'");
                    if (throwExcept)
                        Assert.Fail($"{findNodeName}: {errorMessage}");
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