using Checkout.Kata.Services;
using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Tests.Services;

public class UnitPricingRuleTests
{
    private IPricingRule _pricingRule;
    private const int UNIT_PRICE = 10;

    [SetUp]
    public void Setup()
    {
        _pricingRule = new UnitPricingRule(UNIT_PRICE);
    }
    
    [Test]
    public void ShouldReturnZeroForZeroUnits()
    {
        int numberOfUnits = 0; int expected = 0;
        
        var totalPrice = _pricingRule.CalculatePrice(numberOfUnits);

        Assert.That(totalPrice, Is.EqualTo(expected));
    }
    
    [Test]
    public void ShouldReturnUnitPriceForSingleUnit()
    {
        int quantity = 1;
        var totalPrice = _pricingRule.CalculatePrice(quantity);
        
        Assert.That(totalPrice, Is.EqualTo(UNIT_PRICE));
    }
    
    [Theory]
    [TestCase(2, 50)]
    [TestCase(4, 100)]
    [TestCase(8, 10)]
    [TestCase(10, 100)]
    public void ShouldReturnTotalPriceForMultipleUnits(int quantity, int unitPrice)
    {
        _pricingRule = new UnitPricingRule(unitPrice);

        var totalPrice = _pricingRule.CalculatePrice(quantity);

        var expected = quantity * unitPrice;
        Assert.That(totalPrice, Is.EqualTo(expected));
    }
}