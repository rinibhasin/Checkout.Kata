namespace Checkout.Kata.Services.Interfaces;

public interface IPricingRule
{
    int CalculatePrice(int quantity);
}