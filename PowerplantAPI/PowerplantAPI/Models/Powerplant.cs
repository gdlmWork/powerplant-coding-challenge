using System.ComponentModel.DataAnnotations;

namespace PowerplantAPI.Models
{
    public class Powerplant
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EnumDataType(typeof(PowerplantType))]
        public PowerplantType Type { get; set; }
        [Required]
        public decimal Efficiency { get; set; }
        [Required]
        public decimal Pmin { get; set; }
        [Required]
        public decimal Pmax { get; set; }


    }

    public enum PowerplantType
    {
        gasfired, turbojet, windturbine
    }
}
