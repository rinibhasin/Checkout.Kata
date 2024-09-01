using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Services;

public class Checkout: ICheckout
{
    public IDictionary<string, int> _items;
    private IDictionary<string, IPricingRule> _rules;

    public Checkout(IDictionary<string,IPricingRule> rules)
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
            _items.Add(item,1);
        }
    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;
        foreach (var item in _items)
        {
            if (_rules.TryGetValue(item.Key, out var pricingRule))
            {
                totalPrice += pricingRule.CalculatePrice(item.Value);
            }

            
        }
        return totalPrice;
    }
    
}