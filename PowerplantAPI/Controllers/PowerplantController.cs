using Microsoft.AspNetCore.Mvc;
using PowerplantAPI.Models;
using PowerplantAPI.Services;

namespace PowerplantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerplantController : ControllerBase
    {
        private IProductionService productionService;
        public PowerplantController(IProductionService productionService)
        {
            this.productionService = productionService;
        }

        [HttpPost]
        public IActionResult Generate(Payload payload)
        {
            List<ProductionPlan> productionplans = productionService.Calculate(payload);
            return Ok(productionplans);
        }
    }
}
