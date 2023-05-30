using congestion.calculator;

namespace WebApplication2.Controllers;

public class GetTaxDto
{
    public City City { get; set; }
    public Vehicle vehicle { get; set; }
    public DateTime[] dates { get; set; }
}

public enum City
{
    Gothenburg,
    Tehran
}