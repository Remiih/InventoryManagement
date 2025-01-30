using System.Drawing;
using Pastel;

namespace InventoryManagement;

class Program
{
    static List<Product> products = new List<Product>();
    public static void Main(string[] args)
    {
        int choice = 0;
        do 
        {
            Console.WriteLine($"\n\n{"Inventory Management System".Pastel(Color.LightGreen)}");
            Console.Write($"{"1. Add Product\n2. Sell Product\n3. Restock Product\n4. Display Products\n5. Update Product Price\n6. Delete Product\n7. Exit".Pastel(Color.White)}\n\nInput: ");
            choice = GetValidInt();
            switch (choice)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    SellProduct();
                    break;
                case 3:
                    RestockProduct();
                    break;
                case 4:
                    DisplayProducts();
                    break;
                case 5:
                    UpdateProductPrice();
                    break;
                case 6:
                    DeleteProduct();
                    break;
                case 7:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            } 
        } while (choice != 7);
    }

    private static void AddProduct()
    {
        decimal price = 0;
        int quantity = 0;
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        if (products.Any(p => p.Name == name))
        {
            Console.WriteLine("A product with this name already exists. Please try again.");
            return;
        }
        else if (name == "")
        {
            Console.WriteLine("Product name cannot be empty. Please try again.");    
            return;
        }
        Console.Write("Enter product price: ");
        price = GetValidPrice();
        Console.Write("Enter product quantity: ");
        quantity = GetValidInt();
        Product product = new Product(name, price, quantity);
        products.Add(product);
        Console.WriteLine("Product added successfully.");
    }

    private static void SellProduct()
    {
        int quantity = 0;
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter quantity to sell: ");
        quantity = GetValidInt();
        foreach (Product product in products)
        {
            if (product.Name == name)
            {
                product.Sell(quantity);
                Console.WriteLine($"{quantity} of {name} sold successfully.");
                return;
            }
        }
        Console.WriteLine("Product not found.");
    }

    private static void RestockProduct()
    {
        int quantity = 0;
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter quantity to restock: ");
        quantity = GetValidInt();
        foreach (Product product in products)
        {
            if (product.Name == name)
            {
                product.Restock(quantity);
                Console.WriteLine($"{quantity} of {name} restocked successfully.");
                return;
            }
        }
        Console.WriteLine("Product not found.");
    }

    private static void DisplayProducts()
    {
        int pageSize = 10;
        int pages = (int)Math.Ceiling(products.Count / (double)pageSize);
        int page = 0;
        int choice = 0;
        do
        {
            Console.WriteLine($"Page {page + 1} of {pages}:");
            for (int i = page * pageSize; i < Math.Min(page * pageSize + pageSize, products.Count); i++)
            {
                products[i].Display();
            }
            Console.Write("\n1. Previous Page\n2. Next Page\n3. Exit\n\nInput: ");
            choice = GetValidInt();
            switch (choice)
            {
                case 1:
                    if (page == 0)
                    {
                        Console.WriteLine("You are already on the first page.");
                        break;
                    }
                    page--;
                    break;
                case 2:
                    if (page+1 == pages) 
                    {
                        Console.WriteLine("You are already on the last page.");
                        break;
                    }
                    page++;
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (choice != 3);
    }
    
    private static void UpdateProductPrice()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter new price: ");
        decimal price = GetValidPrice();
        foreach (Product product in products)
        {
            if (product.Name == name)
            {
                product.UpdatePrice(price);
                Console.WriteLine($"Price of {name} updated successfully.");
                return;
            }
        }
        Console.WriteLine("Product not found.");
    }

    private static void DeleteProduct()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        foreach (Product product in products)
        {
            if (product.Name == name)
            {
                products.Remove(product);
                Console.WriteLine($"{name} deleted successfully.");
                return;
            }
        }
        Console.WriteLine("Product not found.");
    }

    private static decimal GetValidPrice() 
    {   
        while (true)
        {
            try
            {
                decimal price = Convert.ToDecimal(Console.ReadLine());
                return price;
            }
            catch (FormatException)
            {
                Console.Write("Invalid input. Please try again.\nInput: ");
            }
        }
    }

    private static int GetValidInt()
    {
        while (true)
        {
            try
            {
                int quantity = Convert.ToInt32(Console.ReadLine());
                return quantity;
            }
            catch (FormatException)
            {
                Console.Write("Invalid format. Please try again.\nInput: ");
            }
        }
    }
}