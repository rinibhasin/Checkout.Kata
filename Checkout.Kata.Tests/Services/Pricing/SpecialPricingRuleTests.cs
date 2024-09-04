using Checkout.Kata.Services;
using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Tests.Services;

public class SpecialPricingRuleTests
{
    private IPricingRule _pricingRule;
    private const int UNIT_PRICE = 20;
    private const int THRESHOLD_QUANTITY = 3;
    private const int DISCOUNTED_PRICE = 15;
    
    [SetUp]
    public void Setup()
    {
        _pricingRule = new SpecialPricingRule(THRESHOLD_QUANTITY, DISCOUNTED_PRICE, UNIT_PRICE);
    }
    
    [Test]
    public void ShouldReturnZeroForZeroUnits()
    {
        int numberOfUnits = 0; int expected = 0;
        
        var totalPrice = _pricingRule.CalculatePrice(numberOfUnits);

        Assert.That(totalPrice, Is.EqualTo(expected));
    }
    
    [Test]
    public void ShouldApplyUnitPriceIfThresholdQuantityNotPurchased()
    {
        int numberOfUnits = 2; int expected = numberOfUnits* UNIT_PRICE;
        
        var totalPrice = _pricingRule.CalculatePrice(numberOfUnits);

        Assert.That(totalPrice, Is.EqualTo(expected));
    }
    
    [Test]
    public void ShouldApplySpecialPriceIfThresholdQuantityPurchased()
    {
        var totalPrice = _pricingRule.CalculatePrice(THRESHOLD_QUANTITY);

        Assert.That(totalPrice, Is.EqualTo(DISCOUNTED_PRICE));
    }
    
    [Test]
    public void ShouldApplySpecialPriceTwiceIfTwiceThresholdQuantityPurchased()
    {
        var totalPrice = _pricingRule.CalculatePrice(THRESHOLD_QUANTITY*2);

        Assert.That(totalPrice, Is.EqualTo(DISCOUNTED_PRICE*2));
    }
    
    [Test]
    public void ShouldApplySpecialPriceAndUnitPriceIfOneMoreThanThresholdQuantityPurchased()
    {
        var totalPrice = _pricingRule.CalculatePrice(THRESHOLD_QUANTITY+1);

        Assert.That(totalPrice, Is.EqualTo(DISCOUNTED_PRICE+UNIT_PRICE));
    }

    [Theory]
    [TestCase(3)]
    [TestCase(6)]
    [TestCase(9)]
    public void ShouldApplySpecialPriceNTimesIfNThresholdQuantityPurchased(int n)
    {
        var actual = _pricingRule.CalculatePrice(THRESHOLD_QUANTITY*n);
        
        Assert.That(actual, Is.EqualTo(DISCOUNTED_PRICE*n));
    }
}