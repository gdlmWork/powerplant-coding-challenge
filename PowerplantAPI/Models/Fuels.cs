using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PowerplantAPI.Models
{
    public class Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        [Required]
        public double Gas { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        [Required]
        public double Kerosine { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        [Required]
        public double CO2 { get; set; }
        [JsonPropertyName("wind(%)")]
        [Required]
        public double Wind { get; set; }
    }
}
