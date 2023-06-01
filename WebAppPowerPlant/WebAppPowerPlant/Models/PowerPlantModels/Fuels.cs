using System.Text.Json.Serialization;

namespace PowerPlantApplication.Models
{
    public class Fuels
    {
        [JsonPropertyName("co2(Euro/ton)")]
        public double Co2EuroTon { get; set; }

        [JsonPropertyName("gas(Euro/MWh)")]
        public double GasEuroMWh { get; set; }

        [JsonPropertyName("kerosine(Euro/MWh)")]
        public double KerosineEuroMWh { get; set; }

        [JsonPropertyName("wind(%)")]
        public double Wind { get; set; }
    }
}