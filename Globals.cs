namespace OFIBridgeTest.Tests
{
    /// <summary>
    /// These are globals for the automated tests. They determine how long to wait for a node to be found, text being typed or polling times.
    /// 
    /// Be careful in modifying these!
    ///
    /// Defaults known to work well:
    /// 
    /// MaxWaitTime = 15000;
    /// MinTypeTime = 50;
    /// MinPollingTime = 500;
    /// </summary>
    public static class Globals
    {
        // For Node Extensions
        public const int MaxWaitTime = 3500;
        public const int MinTypeTime = 25;
        public const int MinPollingTime = 200;
    }
}
