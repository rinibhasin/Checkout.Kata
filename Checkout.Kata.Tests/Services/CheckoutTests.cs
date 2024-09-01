using Checkout.Kata.Services.Interfaces;

namespace TestProject1.Services;

    using NUnit;
    using Checkout.Kata.Services;
    
    public class CheckoutTests
    {
        private  Checkout _checkout;
        private IDictionary<string, IPricingRule> _rules;

        
        public CheckoutTests()
        {
            Setup();
        }
        
        [SetUp]
        public void Setup()
        {
            _rules = new Dictionary<string, IPricingRule>()
            {
                {"A", new UnitPricingRule(50)},
                {"B", new UnitPricingRule(30)},
                {"C", new UnitPricingRule(20)}


            };
            _checkout = new Checkout(_rules);
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
        [TestCase( 2)]
        [TestCase( 3)]
        [TestCase( 5)]
        public void ShouldIncreaseCountIfSameItemBoughtNTimes(int n)
        {
            for (int i =0; i <n; i++)
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
    }

