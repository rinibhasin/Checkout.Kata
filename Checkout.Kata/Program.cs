using Checkout.Kata.Services;
using Checkout.Kata.Services.Interfaces;
using Checkout.Kata.Services;

public class Program
{
    public static void Main(string[] args)
    {
        IDictionary<string, IPricingRule> _rules;

        
        _rules = new Dictionary<string, IPricingRule>()
        {
            { "A", new SpecialPricingRule(3, 130, 50) },
            { "B", new SpecialPricingRule(2, 45, 30) },
            { "C", new UnitPricingRule(20) },
            { "D", new UnitPricingRule(15) },
          
        };

        Checkout.Kata.Services.Checkout checkout = new Checkout.Kata.Services.Checkout(_rules);
        
        
        checkout.Scan("A");
        checkout.Scan("A");
        checkout.Scan("A");

        checkout.GetTotalPrice();

        Console.ReadLine();
    }
}
