using System.Runtime.Serialization;

namespace PowerPlantApplication.Models
{
    public enum PowerPlantTypes
    {
        [EnumMember(Value = "gasfired")]
        GASFIRED,

        [EnumMember(Value = "turbojet")]
        TURBOJET,

        [EnumMember(Value = "windturbine")]
        WINDTURBINE
    }
}