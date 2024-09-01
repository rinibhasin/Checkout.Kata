using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Services;

public class UnitPricingRule : IPricingRule
{
    private readonly int _unitPrice;

    public UnitPricingRule(int unitPrice)
    {
        _unitPrice = unitPrice;
    }
    public int CalculatePrice(int quantity)
    {
        return _unitPrice * quantity;
    }
}