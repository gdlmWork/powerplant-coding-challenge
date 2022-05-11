using System.ComponentModel.DataAnnotations;

namespace PowerplantAPI.Models
{
    public class Payload
    {
        [Required]
        public int Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<Powerplant> Powerplants { get; set; }
    }
}
