using System.ComponentModel.DataAnnotations;

namespace PowerplantAPI.Models
{
    public class Fuels
    {
        [Required]
        public decimal Gas { get; set; }

        [Required]
        public decimal Kerosine { get; set; }

        [Required]
        public decimal CO2 { get; set; }

        [Required]
        public decimal Wind { get; set; }
    }
}
