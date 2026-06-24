namespace ERPFoundation.Presentation.ConsoleUI;

public class MainMenu
{
    private readonly ProductMenu _productMenu;
    private readonly SupplierMenu _supplierMenu;

    public MainMenu(ProductMenu productMenu, SupplierMenu supplierMenu)
    {
        _productMenu = productMenu;
        _supplierMenu = supplierMenu;
    }

    public async Task ShowAsync()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== ERP Foundation ===");
            Console.WriteLine("1 - Manage Products");
            Console.WriteLine("2 - Manage Suppliers");
            Console.WriteLine("0 - Exit");
            Console.Write("Enter an option: ");

            var option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    await _productMenu.ShowAsync();
                    break;
                case "2":
                    await _supplierMenu.ShowAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
