using System.Text.Json.Serialization;

namespace PowerPlantApplication.Models
{
    public class PowerPlant
    {
        public double Efficiency { get; set; }
        public string Name { get; set; }
        public int Pmax { get; set; }
        public int Pmin { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PowerPlantTypes Type { get; set; }
    }
}