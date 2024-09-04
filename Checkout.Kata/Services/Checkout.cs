using Checkout.Kata.Errors;
using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Services;

public class Checkout: ICheckout
{
    public IDictionary<string, int> _items;
    private IDictionary<string, IPricingRule> _rules;

    public Checkout(IDictionary<string, IPricingRule> rules)
    {
        _items = new Dictionary<string, int>();
        _rules = rules;
    }

    public void Scan(string item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item]++;
        }
        else
        {
            _items.Add(item, 1);
        }
    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;
        foreach (var item in _items)
        {
            try
            {
                var pricingRule = FindItem(item.Key);
                totalPrice += pricingRule.CalculatePrice(item.Value);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Please remove the last scanned item.\n");
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine($"The total price is: {totalPrice}");
        return totalPrice;
    }


    private IPricingRule FindItem(string sKU)
    {
        if (_rules.TryGetValue(sKU, out var pricingRule))
        {
            return pricingRule;
        }

        throw new ItemNotFoundException(sKU);
    }
}