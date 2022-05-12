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
            // First we order by windturbines because they generate power at 'zero' cost.
            // Then we order on the cost ratio for the remaining powerplants with cheapest option first.
            powerplants = powerplants.OrderByDescending(p => p.PlantType == PowerType.windturbine).ThenByDescending(p => p.GetCostRatio(fuels)).ToList();
            List <ProductionPlan> productionPlans = new List<ProductionPlan>();
            int ppNextIndex = 0;

            foreach (var powerplant in powerplants)
            {
                ppNextIndex++;
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
                            // Compensate the remaining load over multiple plants to make up for the 
                            // fact that some plants have a min power production.
                            double tempLoad = loadRequired - powerplant.Pmax;
                            if ( tempLoad < powerplants[ppNextIndex].Pmin )
                            {
                                tempLoad = powerplant.Pmax - (powerplants[ppNextIndex].Pmin - tempLoad);
                                loadRequired = powerplants[ppNextIndex].Pmin;
                                productionPlans.Add(new ProductionPlan(powerplant.Name, Math.Round(tempLoad, 2)));
                            }
                            else
                            {
                                loadRequired = tempLoad;
                                productionPlans.Add(new ProductionPlan(powerplant.Name, powerplant.Pmax));
                            }
                        }
                        else
                        {
                            productionPlans.Add(new ProductionPlan(powerplant.Name, Math.Round(loadRequired, 2)));
                            loadRequired = 0;
                        }
                    }
                    else
                    {
                        double windpower = powerplant.Pmax * (fuels.Wind / 100); 
                        if (windpower < loadRequired)
                        {
                            loadRequired -= windpower;
                            productionPlans.Add(new ProductionPlan(powerplant.Name, Math.Round(windpower, 2)));
                        }
                        else
                        {
                            double rest = loadRequired / (fuels.Wind / 100);
                            loadRequired -= windpower;
                            productionPlans.Add(new ProductionPlan(powerplant.Name, Math.Round(rest, 2)));
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
