using OFIBridgeTest.Tests.NodeExtensions;
using TestCases.TestMethods;
using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace Tests.TestMethods
{
    public partial class OracleFormAreas : BaseTest
    {
        public static void SampleTestMethod(AccessibleNode? parent)
        {
            DebugOutputSep();
            DebugOutput("SAMPLE TEST METHOD HELPER");
            DebugOutputSep();

            // Click on the Order Information Tab
            NodeExtensions.Click("Order Information", parent, Role.PageTab);

            // Click on the Main Tab inside the Order Information Tab
            NodeExtensions.Click("Main", parent, Role.PageTab);

            NodeExtensions.WriteTextArray("SalespersonList of Values", parent, Role.Text, 31, "SOME SALES PERSON NAME");

            KeyboardHelper.PressTAB(1);

            if (NodeExtensions.CheckAppState("FRM-40212", "", parent, Role.Label))
            {
                DebugOutput($"FRM-40212: Invalid value for field SALES PERSON NAME.");
                KeyboardHelper.ClearInputText(55);
                KeyboardHelper.PressTAB(1);
            }

            // Book Order button Click
            NodeExtensions.Click("Book Order alt B", parent, Role.PushButton);

            // Wait for Oracle to process 
            NodeExtensions.WaitForOracleMessage("Actions alt A", "", parent, Role.PushButton, false, 20);

            // Screen Shot
            TakeScreenShotAttach("SampleTestMethod");
        }
    }
}
