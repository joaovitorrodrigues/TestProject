using System.ComponentModel;

namespace TestProject.Domain.Enums
{
    public enum StatusType
    {
        [Description("Available")]
        Available = 0,
        [Description("Accepted")]
        Accepted = 1,
        [Description("Delivered")]
        Delivered = 2
    }
}
