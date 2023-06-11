using BusinessServiceTemplate.Shared.Enumerations;

namespace BusinessServiceTemplate.Api.Security
{
    public enum SecurityOperation
    {
        [EnumMetadata("Full APIs access roles", "Full APIs operations")]
        FullAccess = 1,
        [EnumMetadata("Read only APIs access roles", "Read only operations, mainly apply to Get APIs")]
        ReadOnlyAccess = 2,
        [EnumMetadata("Panels access roles", "Access to Panel APIs")]
        PanelAccess = 3,
        [EnumMetadata("Tests access roles", "Access to Test APIs")]
        TestAccess = 4,
        [EnumMetadata("TestSelection access roles", "Access to Test Selection APIs")]
        TestSelectionAccess = 5,
        [EnumMetadata("Write access permission", "Write access to Test Selection APIs")]
        WriteAccess = 6,
        [EnumMetadata("Read access permission", "Read access to Test Selection APIs")]
        ReadAccess = 7,
    }
}
