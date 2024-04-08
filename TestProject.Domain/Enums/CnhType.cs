using System.ComponentModel;

namespace TestProject.Domain.Enums
{
    public enum CnhType
    {
        [Description("A")]
        Motorcycle = 1,
        [Description("B")]
        Car = 2,
        [Description("AB")]
        Both = 3
    }
}
