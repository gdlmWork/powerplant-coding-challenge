using PowerplantAPI.Models;

namespace PowerplantAPI.Services
{
    public class ProductionService : IProductionService
    {
        public List<ProductionPlan> CalculateUnit(Payload payload)
        {
            double loadRequired = payload.Load;
            Fuels fuels = payload.Fuels;
            List<Powerplant> powerplants = payload.Powerplants;
            List<ProductionPlan> productionPlans = new List<ProductionPlan>();

            // First we order by windturbines because the generate power at 'zero' cost.
            // Then we order on the cost ratio for the remaining powerplants with cheapest option first.
            foreach (var powerplant in powerplants.OrderByDescending(p => p.PlantType == PowerType.windturbine).ThenByDescending(p => p.GetCostRatio(fuels)))
            {
                if (loadRequired > 0)
                {
                    if (powerplant.PlantType != PowerType.windturbine)
                    {
                        // If the remaining load needed is bigger than or equal to the max power production of the powerplant
                        // we operate this powerplant at max power.
                        // But when the remaining load is less than the min power production of the plant, we need to skip
                        // and use the next plant (always considering costs) that can generate the needed power.
                        if (powerplant.Pmax <= loadRequired)
                        {
                            loadRequired -= powerplant.Pmax;
                            productionPlans.Add(new ProductionPlan(powerplant.Name, powerplant.Pmax));
                        }
                        else if (loadRequired < powerplant.Pmin)
                        {
                            productionPlans.Add(new ProductionPlan(powerplant.Name, 0));
                        }
                        else
                        {
                            productionPlans.Add(new ProductionPlan(powerplant.Name, loadRequired));
                            loadRequired = 0;
                        }
                    }
                    else
                    {
                        double windpower = powerplant.Pmax * (fuels.Wind / 100); 
                        if (windpower < loadRequired)
                        {
                            loadRequired -= windpower;
                            productionPlans.Add(new ProductionPlan(powerplant.Name, windpower));
                        }
                        else
                        {
                            double rest = loadRequired / (fuels.Wind / 100);
                            loadRequired -= windpower;
                            productionPlans.Add(new ProductionPlan(powerplant.Name, rest));
                        }
                    }

                }
                else
                {
                    productionPlans.Add(new ProductionPlan(powerplant.Name, 0));
                }
            }
            return productionPlans;
        }
    }
}
