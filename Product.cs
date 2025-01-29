namespace InventoryManagement;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public void Display()
    {
        Console.WriteLine($"{(IsSoldOut() ? "SOLD OUT\t" : "IN STOCK\t")}Name: {Name}\tPrice: {Price}\tQuantity: {Quantity}");
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public void Sell(int quantity)
    {
        if (Quantity - quantity > 0)
        {
            Quantity -= quantity;
            return;
        }
        Quantity = 0;
    }
    
    public void Restock(int quantity)
    {
        Quantity += quantity;
    }

    public bool IsSoldOut()
    {
        return Quantity == 0;
    }
}
