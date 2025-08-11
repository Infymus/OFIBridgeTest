using System.Diagnostics;
using NUnit.Framework;
using OFIBridgeTest.Tests.NodeExtensions;
using TestHelpers.Enums;
using TestHelpers.KeyBoardMouse;
using WindowsAccessBridgeInterop;

namespace Tests.Utilities
{

    /// <summary>
    /// These are roles within your organization if you have them in Oracle Forms. 
    /// </summary>
    public enum NavigatorEnums
    {
        [System.ComponentModel.Description("Some Kind of Oracle Role")]
        SomeKindOfOracleRole
    }

    /// <summary>
    /// Menu Enumerations
    /// </summary>
    public enum oMenu
    {
        [System.ComponentModel.Description("File ALT F")]
        File,
        [System.ComponentModel.Description("text")]
        Edit,
        [System.ComponentModel.Description("View ALT V")]
        View,
        [System.ComponentModel.Description("text")]
        Folder,
        [System.ComponentModel.Description("Tools ALT T")]
        Tools,
        [System.ComponentModel.Description("text")]
        Reports,
        [System.ComponentModel.Description("Actions")]
        Actions,
        [System.ComponentModel.Description("")]
        Find
    }

    /// <summary>
    /// Submenu Enumerations
    /// </summary>
    public enum oSubMenu
    {
        [System.ComponentModel.Description("Save mnemonic S")]
        Save,
        [System.ComponentModel.Description("Save Simulated Job")]
        SaveSimulatedJob,
        [System.ComponentModel.Description("Requests mnemonic R")]
        Requests,
        [System.ComponentModel.Description("Adjust mnemonic d")]
        ActionAdjust,
        [System.ComponentModel.Description("Applications mnemonic p")]
        ActionApplication,
        [System.ComponentModel.Description("Perform Full Cycle Count mnemonic F")]
        PerformCycleCount,
        [System.ComponentModel.Description("Close 1 mnemonic C")]
        CloseJob,
        [System.ComponentModel.Description("Close Form mnemonic C")]
        CloseForm
    }

    /// <summary>
    /// This class allows you to open Oracle Forms areas. Depending on your Oracle Forms, this will
    /// allow you to open File, View, Text, Tools, Actions, Etc. 
    /// Model this to map to your Oracle Forms.
    /// </summary>
    public static class OracleFormsExtensions
    {
        private static void DebugOutput(string inDebugData)
        {
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("MM-dd-yyyy @ hh:mm:ss tt");
            Debug.WriteLine($"{formattedDate} : {inDebugData}");
        }

        /// <summary>
        /// Changes the Oracle Forms Responsibility by clicking the HAT
        /// </summary>
        /// <param name="inCommand"></param>
        /// <param name="parent"></param>
        public static void RunOracleHatNav(NavigatorEnums inCommand, AccessibleNode? parent)
        {
            Debug.WriteLine($"Navigate: {inCommand}");
            // Can't get to the HAT with Oracle Forms (it's not a node) - so we HACK by clicking it elsewhere.
            // Tools is @ 169,51 - need to get to Hat @ 176,83 so we add +/+
            NodeExtensions.ClickOffSetArrayRect("Tools ALT T", parent, Role.Menu, 4, ClickPoint.Offset, +9, +34, 0, 0);
            NodeExtensions.WriteTextArray("Find", parent, Role.Text, 1, OracleUtilities.GetEnumDescription(inCommand));
            NodeExtensions.Click("Find ALT F", parent, Role.PushButton);

            // Handle menus that have more than 1 of the same name
            if (inCommand == NavigatorEnums.SomeKindOfOracleRole)
            {
                KeyboardHelper.PressTAB(1, 100);
                KeyboardHelper.ClickDirection(clickDirs.Down);
                KeyboardHelper.PressEnter();
            }
            NodeExtensions.CheckAppStateClickOK("Form Functions", "", "OK ALT O", parent, Role.InternalFrame);
            NodeExtensions.WaitForOracleMessage("Navigator", "", parent, Role.InternalFrame, true);
        }

        /// <summary>
        /// Maximizes the Oracle Java Window
        /// </summary>
        /// <param name="parent"></param>
        public static void OracleFormsMaximizeJava(AccessibleNode? parent)
        {
            NodeExtensions.WaitForOracleMessage("Navigator", "", parent, Role.InternalFrame, true);
            SendKeys.Send("% ");
            SendKeys.Send("x");
        }

        /// <summary>
        /// Opens any Oracle Menu
        /// </summary>
        /// <param name="inMenu"></param>
        /// <param name="inSubMenu"></param>
        /// <param name="parent"></param>
        public static void ClickOracleMenu(oMenu inMenu, oSubMenu inSubMenu, AccessibleNode? parent)
        {
            DebugOutput($"ClickOracleMenu({inMenu.ToString()},{inSubMenu.ToString()}");
            NodeExtensions.Click(OracleUtilities.GetEnumDescription(inMenu), parent, Role.Menu);
            NodeExtensions.Click(OracleUtilities.GetEnumDescription(inSubMenu), parent, Role.MenuItem);
        }

        /// <summary>
        /// Clicks the Oracle Menu Bar. These do not have nodes you can click so they are all offset from a visible node.
        /// </summary>
        /// <param name="inMenu"></param>
        /// <param name="parent"></param>
        public static void ClickOracleMenuBar(oMenu inMenu, AccessibleNode? parent)
        {
            Debug.WriteLine($"ClickOracleMenuBar({inMenu.ToString()})");
            switch (inMenu)
            {
                case oMenu.Find:
                    NodeExtensions.ClickOffSetArrayRect("Edit ALT E", parent, Role.Menu, 1, ClickPoint.Offset, +10, +35, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// Runs Close Form and checks for possible close decisions
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="checkClose"></param>
        public static void ClickCloseForm(AccessibleNode? parent, bool checkClose)
        {
            NodeExtensions.WaitForOracleMessage("Navigator", "", parent, Role.InternalFrame, true);
            DebugOutput($"ClickCloseForm()");
            State[] returnState = NodeExtensions.ReturnStateByArray("Close Form mnemonic C", "", 14, parent, Role.MenuItem);
            if (returnState.Contains(State.Enabled))
            {
                ClickOracleMenu(oMenu.File, oSubMenu.CloseForm, parent);
                if (checkClose)
                {
                    NodeExtensions.CheckAppStateClickOK("Forms", "Close this form?", "Yes ALT Y", parent, Role.InternalFrame);
                    NodeExtensions.CheckAppStateClickOK("Decision", "You have changes pending", "Discard ALT D", parent, Role.InternalFrame);
                }
            }
        }

        /// <summary>
        /// Opens an Oracle Forms Window from the Navigator
        /// </summary>
        /// <param name="inNavigator"></param>
        /// <param name="inMenuItem"></param>
        /// <param name="parent"></param>
        public static void RunOracleMenuNav(NavigatorEnums inNavigator, string inMenuItem, AccessibleNode? parent)
        {
            DebugOutput($"RunOracleMenuNav: {inNavigator} - {inMenuItem}");

            // Wait for Oracle FRAME - This MUST be there or we will cascade fail
            NodeExtensions.WaitForOracleMessage("Navigator", "", parent, Role.InternalFrame, true);

            // Click the Navigation Window
            NodeExtensions.Click("Navigator", parent, Role.InternalFrame);

            // Added this KeyBoard Simulator Nuget Package because it was the only thing I could do to get CTRL- command to work
            DebugOutput($"| CTRL-L");
            SendKeys.Send("^L");
            SendKeys.Send("^L");

            // Wait for Oracle FRAME
            if (!NodeExtensions.WaitForOracleMessage("Form Functions", "", parent, Role.InternalFrame, false, 5))
            {
                NodeExtensions.Click("Navigator", parent, Role.InternalFrame);
                SendKeys.Send("^L");
                SendKeys.Send("^L");
            }

            // If it didn't pop, let's try AGAIN
            if (!NodeExtensions.CheckAppState("Form Functions", "", parent, Role.InternalFrame))
            {
                DebugOutput($"| CTRL-L");
                NodeExtensions.Click("Navigator", parent, Role.InternalFrame);
                SendKeys.Send("^L");
                SendKeys.Send("^L");
                NodeExtensions.WaitForOracleMessage("Form Functions", "", parent, Role.InternalFrame, false, 5);
            }

            // We still can't get to it. We can't proceed.
            if (!NodeExtensions.CheckAppState("Form Functions", "", parent, Role.InternalFrame))
            {
                Assert.Fail($"Unable to Navigate to '{inMenuItem}' - Oracle not responding to keyboard input.");
            }

            // Find window + Enter Command + Click Find
            NodeExtensions.WriteTextArray("Find", parent, Role.Text, 1, inMenuItem);

            // Wait for Oracle FRAME
            NodeExtensions.WaitForOracleMessage("Navigator", "", parent, Role.InternalFrame, true);

            // Click Find
            NodeExtensions.Click("Find ALT F", parent, Role.PushButton);

            // Wait for Oracle FRAME
            NodeExtensions.WaitForOracleMessage("Navigator", "", parent, Role.InternalFrame, true);

            // Handle menus that have more than 1 of the same name
            if (
                inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Some Menu Item" ||
                inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Some Other Menu Item"
                )
            {
                // Some menus have multiple areas, handle those, default at 1
                int keyDownTimes = 1;
                if (inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Some Menu Item")
                    keyDownTimes = 3;
                if (inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Customer Search" ||
                    inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Some Other Role")
                    keyDownTimes = 3;

                // Move to correct area and hit Enter
                KeyboardHelper.ClickDirection(clickDirs.Down, keyDownTimes);
                KeyboardHelper.PressEnter();
            }

            // Just press ENTER
            if (
                inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Some Menu Item" ||
                inNavigator == NavigatorEnums.SomeKindOfOracleRole && inMenuItem == "Some Other Menu Item"
                )
            {
                KeyboardHelper.PressEnter();
            }
        }

        /// <summary>
        /// This handles any form that requires sending a F11, fill in data, then CTRL-F11
        /// </summary>
        /// <param name="inCellData"></param>
        /// <param name="inControlData"></param>
        /// <param name="inRole"></param>
        /// <param name="parent"></param>
        public static void HandleCellInput(string inCellData, string inControlData, Role inRole, AccessibleNode? parent)
        {
            NodeExtensions.WaitForOracleMessage(inControlData, "", parent, inRole, true);
            SendKeys.Send("{F11}");
            NodeExtensions.WaitForOracleMessage(inControlData, "", parent, inRole, true);
            KeyboardHelper.TypeText(inCellData);
            SendKeys.Send("^{F11}");
            NodeExtensions.WaitForOracleMessage(inControlData, "", parent, inRole, true);
        }

        /// <summary>
        /// Handle the Find Dialogs
        /// </summary>
        /// <param name="inFrame"></param>
        /// <param name="inFindString"></param>
        /// <param name="parent"></param>
        public static void FindDialog(string inFrame, string inFindString, AccessibleNode? parent)
        {
            // If the InternalFrame appears deal with it, otherwise there was only one and it auto-populates
            if (NodeExtensions.CheckAppState(inFrame, "", parent, Role.InternalFrame))
            {
                // If we are supposed to put text in, we add that
                if (inFindString != "")
                {
                    NodeExtensions.WriteTextArray(" Find", parent, Role.Text, 1, inFindString);
                    NodeExtensions.Click("Find ALT F", parent, Role.PushButton);

                    // Check for multiples and select the first one by hitting OK
                    if (NodeExtensions.CheckAppState(inFrame, "", parent, Role.InternalFrame))
                        NodeExtensions.Click("OK ALT O", parent, Role.PushButton);
                }
                else
                {
                    // We just want to click on the default
                    NodeExtensions.Click("OK ALT O", parent, Role.PushButton);
                }
            }
        }

    }
}
