using NUnit.Framework;
using WindowsAccessBridgeInterop;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public static partial class NodeExtensions
    {
        /// <summary>
        /// Waits for a passed Oracle Message to appear such as FRM-40400.
        /// Checking nodes will throw an exception if Oracle Forms is not responding (spinning |)
        /// So we catch the error and wait for the form to respond.
        /// </summary>
        /// <param name="findNodeName"></param>
        /// <param name="nodeContains"></param>
        /// <param name="parent"></param>
        /// <param name="role"></param>
        /// <param name="throwError"></param>
        /// <param name="inMaxAttempts"></param>
        /// <param name="states"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool WaitForOracleMessage(string findNodeName, string nodeContains, AccessibleNode? parent,
            Role role, bool throwError, int inMaxAttempts = 25, State[]? states = null, int index = 0)
        {
            DebugOutput($"WaitForOracleMessage : Checking for: '{findNodeName}'");

            bool wasSaved = false;
            int tryAttempts = 1;
            int MaxAttempts = inMaxAttempts;
            do
            {
                DebugOutput($"| WaitForOracleMessage - Attempt '{tryAttempts}' of '{MaxAttempts}'");
                try
                {
                    // This can throw an exception because when Oracle Forms is not responding, we get a node error
                    if (CheckAppState(findNodeName, nodeContains, parent, role))
                    {
                        wasSaved = true;
                        break;
                    }
                }
                catch
                {
                    // Catch the error, but sleep and try again
                }

            } while (tryAttempts++ <= MaxAttempts);
            if (!wasSaved)
                DebugOutput($"| Node NOT Found > Check Nodes...");
            if (!wasSaved && throwError)
                Assert.Fail($"WaitForOracleMessage Failed: Oracle Forms did not return with '{findNodeName}' in '{MaxAttempts}' attempts");

            return wasSaved;
        }
    }
}