using congestion.calculator;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        [HttpPost("[action]")]
        public int GetTax(GetTaxDto dto)
        {
            ICongestionTaxCalculator congestionTaxCalculator;

            if (dto.City == City.Gothenburg)
                congestionTaxCalculator = new GothenburgCongestionTaxCalculator();
            else
                throw new Exception("nor implemented");

            return congestionTaxCalculator.GetTax(new Vehicle(dto.vehicle), dto.dates);
        }

    }
}