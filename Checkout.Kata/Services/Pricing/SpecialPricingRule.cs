using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Services;

public class SpecialPricingRule : IPricingRule
{
    private readonly int _thresholdQuantity;
    private readonly int _discountedPrice;
    private readonly int _unitPrice;

    public SpecialPricingRule(int thresholdQuantity, int discountedPrice, int unitPrice)
    {
        _thresholdQuantity = thresholdQuantity;
        _discountedPrice = discountedPrice;
        _unitPrice = unitPrice;
    }

    public int CalculatePrice(int quantity)
    {
        int totalPrice = 0;
        while (quantity >= _thresholdQuantity)
        {
            quantity -= _thresholdQuantity;
            totalPrice += _discountedPrice;
        }

        if (quantity >= 1)
        {
            totalPrice += quantity * _unitPrice;
        }
        
        return totalPrice;
    }
}