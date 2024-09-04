using Checkout.Kata.Services;
using Checkout.Kata.Services.Interfaces;
using Moq;

namespace Checkout.Kata.Tests.Services;

public class CheckoutTests
{
    private Kata.Services.Checkout _checkout;
    private IDictionary<string, IPricingRule> _rules;
    private Mock<IPricingRule> _mockPricingRuleE;
    private Mock<IPricingRule> _mockPricingRuleF;

    [SetUp]
    public void Setup()
    {
        _mockPricingRuleE = new Mock<IPricingRule>();
        _mockPricingRuleF = new Mock<IPricingRule>();
        _mockPricingRuleE.Setup(pr => pr.CalculatePrice(It.IsAny<int>())).Returns(130);
        _mockPricingRuleF.Setup(pr => pr.CalculatePrice(It.IsAny<int>())).Returns(100);

        _rules = new Dictionary<string, IPricingRule>()
        {
            { "A", new SpecialPricingRule(3, 130, 50) },
            { "B", new SpecialPricingRule(2, 45, 30) },
            { "C", new UnitPricingRule(20) },
            { "D", new UnitPricingRule(15) },
            { "E", _mockPricingRuleE.Object },
            { "F", _mockPricingRuleF.Object }
        };

        _checkout = new Kata.Services.Checkout(_rules);
    }

    [Test]
    public void ShouldScanItemAndStoreToDictionary()
    {
        _checkout.Scan("A");

        var item = _checkout._items.First();

        Assert.That(item.Key, Is.EqualTo("A"));
        Assert.That(item.Value, Is.EqualTo(1));
    }

    [Theory]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(5)]
    public void ShouldIncreaseCountIfSameItemBoughtNTimes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            _checkout.Scan("A");
        }

        var item = _checkout._items.First();
        Assert.That(item.Key, Is.EqualTo("A"));
        Assert.That(item.Value, Is.EqualTo(n));
    }

    [Test]
    public void ShouldAddMultipleItemsWhenScanned()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("B");

        var itemA = _checkout._items["A"];
        var itemB = _checkout._items["B"];
        
        Assert.That(itemA, Is.EqualTo(2));
        Assert.That(itemB, Is.EqualTo(3));
    }

    [Test]
    public void ShouldReturnTotalPriceAsZeroWhenNoItemsScanned()
    {
        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(0));

    }

    [Test]
    public void ShouldReturnCorrectTotalPriceWhenItemsScanned()
    {
        _checkout.Scan("A");
        _checkout.Scan("B");

        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(80));

    }

    [Test]
    public void ShouldReturnTotalPriceWhenMultipleItemsWithUnitPricing()
    {
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("C");

        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(100));
    }


    [Test]
    public void ShouldHandleSpecialPricing()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");

        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(130));
    }

    [Test]
    public void ShouldHandleSpecialPricingWithUnitPricing()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");

        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(180));
    }

    [Test]
    public void ShouldHandleSpecialPricingForMultipleItems()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");

        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(175));
    }

    [Test]
    public void ShouldHandleSpecialPricingAndUnitPricingForMultipleItems()
    {
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("A");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("B");
        _checkout.Scan("D");

        var totalprice = _checkout.GetTotalPrice();

        Assert.That(totalprice, Is.EqualTo(270));
    }

    [Test]
    public void ShouldApplyCorrectPricingRuleForItemE()
    {
        _checkout.Scan("E");

        var totalprice = _checkout.GetTotalPrice();

        _mockPricingRuleE.Verify(pr => pr.CalculatePrice(1), Times.Exactly(1));
        _mockPricingRuleF.Verify(pr => pr.CalculatePrice(1), Times.Never);
        Assert.That(totalprice, Is.EqualTo(130));
    }

    [Test]
    public void ShouldApplyCorrectPricingRuleForItemEAndF()
    {
        _checkout.Scan("E");
        _checkout.Scan("F");
        _checkout.Scan("F");

        var totalprice = _checkout.GetTotalPrice();

        _mockPricingRuleE.Verify(pr => pr.CalculatePrice(1), Times.Exactly(1));
        _mockPricingRuleF.Verify(pr => pr.CalculatePrice(2), Times.Exactly(1));
        Assert.That(totalprice, Is.EqualTo(230));
    }


    [Test]
    public void ShouldHandleExceptionWhenItemScannedDoesNotExist()
    {
        _checkout.Scan("E");
        _checkout.Scan("Invalid item");
        _checkout.Scan("F");
        _checkout.Scan("F");

        var totalprice = _checkout.GetTotalPrice();

        _mockPricingRuleE.Verify(pr => pr.CalculatePrice(1), Times.Exactly(1));
        _mockPricingRuleF.Verify(pr => pr.CalculatePrice(2), Times.Exactly(1));
        Assert.That(totalprice, Is.EqualTo(230));
    }

    [Test]
    public void ShouldLogErrorToConsoleWhenInvalidItemScanned()
    {
        _checkout.Scan("Invalid item");
        var originalOut = Console.Out;

        using (var sw = new StringWriter())
        {
            try
            {
                
                Console.SetOut(sw);
                _checkout.GetTotalPrice();
            
                var result = sw.ToString();

                Assert.That(result, Does.Contain("Please remove the last scanned item."));
                Assert.That(result, Does.Contain("The item 'Invalid item' does not exist."));
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}

