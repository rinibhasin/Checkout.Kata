using Checkout.Kata.Services.Interfaces;

namespace Checkout.Kata.Services;

public class Checkout: ICheckout
{
    public Dictionary<string, int> _items;

    public Checkout()
    {
        _items = new Dictionary<string, int>();
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
}