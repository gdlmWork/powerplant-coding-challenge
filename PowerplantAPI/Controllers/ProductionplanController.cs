using Microsoft.AspNetCore.Mvc;
using PowerplantAPI.Models;
using PowerplantAPI.Services;

namespace PowerplantAPI.Controllers
{
    [ApiController]
    // Here we define the endpoint to call the API
    [Route("productionplan")]
    public class ProductionplanController : ControllerBase
    {
        private IProductionService productionService;
        public ProductionplanController(IProductionService productionService)
        {
            this.productionService = productionService;
        }

        // Here we wait for a POST request on the API along with a JSON file which defines the payload.
        // Based on this file we calculate the desired power production per plant
        // and we return the outcome in a JSON format.
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
