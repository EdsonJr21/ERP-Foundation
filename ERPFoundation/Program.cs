using ERPFoundation.Infrastructure.DependencyInjection;
using ERPFoundation.Presentation.ConsoleUI;
using Microsoft.Extensions.DependencyInjection;

namespace ERPFoundation;

class Program
{
    static async Task Main()
    {
        var provider = ServiceConfiguration.Configure();
        var mainMenu = provider.GetRequiredService<MainMenu>();
        await mainMenu.ShowAsync();
    }
}