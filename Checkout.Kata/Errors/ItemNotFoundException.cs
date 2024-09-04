namespace Checkout.Kata.Errors;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string sKU)
        : base($"The item '{sKU}' does not exist.")
    {
    }
}
