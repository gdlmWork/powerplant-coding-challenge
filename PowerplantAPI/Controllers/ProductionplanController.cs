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
            try
            {
                List<ProductionPlan> productionplans = productionService.CalculateUnit(payload);
                if (productionplans != null)
                    return Ok(productionplans);
                else
                    throw new Exception("Couldn't calculate the energy amount!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
