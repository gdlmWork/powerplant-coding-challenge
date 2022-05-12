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

        // Changing the string input value for Type to an enum of Powertype
        // for easier validation and compartations in later code.
        public PowerType PlantType
        {
            get
            {
                switch (this.Type)
                {
                    case "gasfired": return PowerType.gasfired;
                    case "turbojet": return PowerType.turbojet;
                    case "windturbine": return PowerType.windturbine;
                    default: return PowerType.gasfired;
                }
            }
        }

        // Deciding which PowerType is cheapest, taking efficiency in consideration,
        // to define the merit-order in ProductionService
        public double GetCostRatio(Fuels fuels)
        {
            double costratio = 0;
            switch (PlantType)
            {
                case PowerType.gasfired: costratio = fuels.Gas * Efficiency * Pmax; break;
                case PowerType.turbojet: costratio = fuels.Kerosine * Efficiency * Pmax; break;
            }
            return costratio;
        }
    }
}
