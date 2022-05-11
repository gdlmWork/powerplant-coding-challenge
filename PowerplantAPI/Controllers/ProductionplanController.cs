using Microsoft.AspNetCore.Mvc;
using PowerplantAPI.Models;
using PowerplantAPI.Services;

namespace PowerplantAPI.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionplanController : ControllerBase
    {
        private IProductionService productionService;
        public ProductionplanController(IProductionService productionService)
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
