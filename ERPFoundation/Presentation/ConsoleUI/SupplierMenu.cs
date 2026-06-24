using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;

namespace ERPFoundation.Presentation.ConsoleUI;

public class SupplierMenu
{
    private readonly ISupplierService _supplierService;

    public SupplierMenu(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== Suppliers ===");
            Console.WriteLine("1 - Create Supplier");
            Console.WriteLine("2 - List Suppliers");
            Console.WriteLine("3 - Search Supplier by ID");
            Console.WriteLine("4 - Update Supplier");
            Console.WriteLine("5 - Remove Supplier");
            Console.WriteLine("0 - Back");
            Console.Write("Enter an option: ");

            var option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    await CreateSupplierAsync();
                    break;
                case "2":
                    await ListSuppliersAsync();
                    break;
                case "3":
                    await SearchSupplierAsync();
                    break;
                case "4":
                    await UpdateSupplierAsync();
                    break;
                case "5":
                    await RemoveSupplierAsync();
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

    private async Task CreateSupplierAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Supplier ===");

        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Tax ID: ");
        var taxId = Console.ReadLine() ?? "";

        Console.Write("Address: ");
        var address = Console.ReadLine() ?? "";

        var supplier = new Supplier
        {
            Name = name,
            TaxId = taxId,
            Address = address
        };

        var created = await _supplierService.AddSupplierAsync(supplier);

        Console.WriteLine(created
            ? "Supplier created successfully."
            : "Could not create the supplier. Check the provided data.");

        Pause();
    }

    private async Task ListSuppliersAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Supplier List ===");

        var suppliers = await _supplierService.ListSuppliersAsync();

        ShowSuppliers(suppliers);

        Pause();
    }

    private async Task SearchSupplierAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Search Supplier ===");

        var id = ReadInteger("Supplier ID: ");

        var supplier = await _supplierService.GetByIdAsync(id);

        if (supplier is null)
        {
            Console.WriteLine("Supplier not found.");
        }
        else
        {
            ShowSupplier(supplier);
        }

        Pause();
    }

    private async Task UpdateSupplierAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Update Supplier ===");

        var id = ReadInteger("Supplier ID: ");

        Console.Write("New name: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("New Tax ID: ");
        var taxId = Console.ReadLine() ?? "";

        Console.Write("New address: ");
        var address = Console.ReadLine() ?? "";

        var supplier = new Supplier
        {
            Id = id,
            Name = name,
            TaxId = taxId,
            Address = address
        };

        var updated = await _supplierService.UpdateSupplierAsync(supplier);

        Console.WriteLine(updated
            ? "Supplier updated successfully."
            : "Could not update the supplier.");

        Pause();
    }

    private async Task RemoveSupplierAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Supplier ===");

        var id = ReadInteger("Supplier ID: ");

        var removed = await _supplierService.RemoveSupplierAsync(id);

        Console.WriteLine(removed
            ? "Supplier removed successfully."
            : "Could not remove the supplier.");

        Pause();
    }

    private static void ShowSuppliers(List<Supplier> suppliers)
    {
        if (suppliers.Count == 0)
        {
            Console.WriteLine("No supplier found.");
            return;
        }

        foreach (var supplier in suppliers)
        {
            ShowSupplier(supplier);
        }
    }

    private static void ShowSupplier(Supplier supplier)
    {
        Console.WriteLine(
            $"ID: {supplier.Id} | Name: {supplier.Name} | Tax ID: {supplier.TaxId} | Address: {supplier.Address}");
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

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
