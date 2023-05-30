using congestion.calculator;
using System;

public interface ICongestionTaxCalculator
{
    int GetTax(Vehicle vehicle, DateTime[] dates);
}
