using System.Globalization;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;

namespace ERPFoundation.Presentation.ConsoleUI;

public class ProductMenu
{
    private readonly IProductService _productService;

    public ProductMenu(IProductService productService)
    {
        _productService = productService;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== Products ===");
            Console.WriteLine("1 - Create Product");
            Console.WriteLine("2 - List Products");
            Console.WriteLine("3 - Search Product");
            Console.WriteLine("4 - Update Product");
            Console.WriteLine("5 - Remove Product");
            Console.WriteLine("0 - Back");
            Console.Write("Enter an option: ");

            var option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    await CreateProductAsync();
                    break;
                case "2":
                    await ListProductsAsync();
                    break;
                case "3":
                    await SearchProductAsync();
                    break;
                case "4":
                    await UpdateProductAsync();
                    break;
                case "5":
                    await RemoveProductAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
    }

    private async Task CreateProductAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Product ===");

        Console.Write("SKU: ");
        var sku = Console.ReadLine() ?? "";

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";

        var price = ReadDecimal("Price: ");
        var quantity = ReadInteger("Quantity: ");
        var supplierId = ReadInteger("Supplier ID: ");

        var created = await _productService.CreateProductAsync(new Product(
            sku,
            name,
            price,
            quantity,
            supplierId));

        Console.WriteLine(created
            ? "Product created successfully."
            : "Could not create the product. Check the provided data.");

        Pause();
    }

    private async Task ListProductsAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Product List ===");

        var products = await _productService.ListProductsAsync();
        ShowProducts(products);

        Pause();
    }

    private async Task SearchProductAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Search Product ===");

        Console.Write("Name or SKU: ");
        var term = Console.ReadLine() ?? "";

        var products = await _productService.SearchProductsAsync(term);
        ShowProducts(products);

        Pause();
    }

    private async Task UpdateProductAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Update Product ===");

        var id = ReadInteger("Product ID: ");

        Console.Write("New name: ");
        var name = Console.ReadLine() ?? "";

        var price = ReadDecimal("New price: ");
        var quantity = ReadInteger("New quantity: ");
        
        var updated = await _productService.UpdateProductsAsync(new Product
        {
            Id = id,
            Name = name,
            Price = price,
            Quantity = quantity
        });

        Console.WriteLine(updated
            ? "Product updated successfully."
            : "Could not update the product. Check the provided data.");

        Pause();
    }

    private async Task RemoveProductAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Product ===");

        var id = ReadInteger("Product ID: ");

        var removed = await _productService.RemoveProductsAsync(id);

        Console.WriteLine(removed
            ? "Product removed successfully."
            : "Could not remove the product. The product must exist and have zero quantity.");

        Pause();
    }

    private static void ShowProducts(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("No product found.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine(
                $"ID: {product.Id} | SKU: {product.Sku} | Name: {product.Name} | " +
                $"Price: {product.Price:C} | Quantity: {product.Quantity} | " +
                $"Supplier ID: {product.SupplierId}");
        }
    }

    private static int ReadInteger(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var value))
            {
                return value;
            }

            Console.WriteLine("Enter a valid integer.");
        }
    }

    private static decimal ReadDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out var value) ||
                decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            Console.WriteLine("Enter a valid decimal value.");
        }
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
