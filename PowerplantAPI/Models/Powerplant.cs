using System.ComponentModel.DataAnnotations;

namespace PowerplantAPI.Models
{
    public class Powerplant
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public double Efficiency { get; set; }
        [Required]
        public double Pmin { get; set; }
        [Required]
        public double Pmax { get; set; }

        public double GetCostRatio(Fuels fuels)
        {
            double costratio = 0;
            switch (this.Type)
            {
                case "gasfired": costratio = fuels.Gas * Efficiency * Pmax; break;
                case "turbojet": costratio = fuels.Kerosine * Efficiency * Pmax; break;
            }
            return costratio;
        }
    }
}
