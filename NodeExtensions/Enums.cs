using Tests.Utilities;

namespace OFIBridgeTest.Tests.NodeExtensions
{
    public enum Role
    {
        [StringValue("text")]
        Text,

        [StringValue("push button")]
        PushButton,

        [StringValue("page tab")]
        PageTab,

        [StringValue("check box")]
        CheckBox,

        [StringValue("scroll bar")]
        ScrollBar,

        [StringValue("label")]
        Label,

        [StringValue("Edit")]
        Edit,

        [StringValue("internal frame")]
        InternalFrame,

        [StringValue("menu item")]
        MenuItem,

        [StringValue("menu")]
        Menu,

        [StringValue("menu bar")]
        MenuBar,

        [StringValue("desktop pane")]
        DesktopPane,

        [StringValue("radio button")]
        RadioButton,

        [StringValue("dialog")]
        Dialog,

        [StringValue("combo box")]
        ComboBox,

        [StringValue("frame")]
        Frame,

        [StringValue("panel")]
        Panel
    }

}
