namespace ERPFoundation.Presentation.ConsoleUI;

public class MainMenu
{
    private readonly ProdutoMenu _produtoMenu;
    private readonly FornecedorMenu _fornecedorMenu;

    public MainMenu(ProdutoMenu produtoMenu, FornecedorMenu fornecedorMenu)
    {
        _produtoMenu = produtoMenu;
        _fornecedorMenu = fornecedorMenu;
    }

    public async Task ExibirAsync()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== ERP Foundation ===");
            Console.WriteLine("1 - Gerenciar Produtos");
            Console.WriteLine("2 - Gerenciar Fornecedores");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite uma opção: ");

            var opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    await _produtoMenu.ExibirAsync();
                    break;
                case "2":
                    await _fornecedorMenu.ExibirAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}