using Checkout.Kata.Services.Interfaces;

namespace TestProject1.Services;

    using NUnit;
    using Checkout.Kata.Services;
    
    public class CheckoutTests
    {
        private  Checkout _checkout;

        
        public CheckoutTests()
        {
            Setup();
        }
        
        [SetUp]
        public void Setup()
        {
            _checkout = new Checkout();
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
        

    }

