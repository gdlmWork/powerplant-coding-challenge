using PowerplantAPI.Models;

namespace PowerplantAPI.Services
{
    public class ProductionService : IProductionService
    {
        public List<ProductionPlan> Calculate(Payload payload)
        {
            double loadRequired = payload.Load;
            Fuels fuels = payload.Fuels;
            List<Powerplant> powerplants = payload.Powerplants;
            List<ProductionPlan> productionPlans = new List<ProductionPlan>();

            foreach (var powerplant in powerplants.OrderByDescending(p => p.Type == "windturbine").ThenByDescending(p => p.GetCostRatio(fuels)))
            {
                if (loadRequired > 0)
                {
                    if (powerplant.Type != "windturbine")
                    {
                        if (powerplant.Pmax < loadRequired)
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
