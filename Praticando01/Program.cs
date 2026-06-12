using Praticando01.Data;
using Praticando01.Services;

namespace Praticando01;

class Program
{
    static async Task Main()
    {
        using var context = new AppDbContext();
        if (!await context.Database.CanConnectAsync())
        {
            Console.WriteLine("Falha ao conectar ao banco.");
            return;
        }

        var produtoService = ServiceConfigurator.Configurar(context);

        Console.WriteLine("\nPressione qualquer tecla para ir ao menu...");
        Console.ReadKey();

        while (true)
        {
            Console.Clear();

            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Listar Produtos");
            Console.WriteLine("3 - Buscar Produtos");
            Console.WriteLine("4 - Atualizar Produto");
            Console.WriteLine("5 - Remove Produto");
            Console.WriteLine("0 - Sair");

            Console.Write("Digite uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            if (opcao == "0")
            {
                break;
            }

            switch (opcao)
            {
                case "1":
                    Console.WriteLine("Sku: ");
                    string sku = Console.ReadLine() ?? "";

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine() ?? "";

                    Console.Write("Preço: ");
                    double.TryParse(Console.ReadLine(), out double preco);

                    Console.Write("Quantidade: ");
                    int.TryParse(Console.ReadLine(), out int quantidade);

                    bool cadastrado =
                        await produtoService.CadastrarProdutoAsync(
                            sku,
                            nome,
                            preco,
                            quantidade);

                    Console.WriteLine(
                        cadastrado
                            ? "Produto cadastrado com sucesso!"
                            : "Dados inválidos.");
                    break;
                case "2":
                    var produtos = await produtoService.ListarProdutosAsync();
                    foreach (var produto in produtos)
                    {
                        Console.WriteLine(
                            $"{produto.Id} - {produto.Nome} - R${produto.Preco} - Qtd: {produto.Quantidade}");
                    }

                    break;
                case "3":
                    Console.Write("Nome do produto: ");

                    string busca =
                        Console.ReadLine() ?? "";

                    var encontrados =
                        await produtoService.BuscarProdutosAsync(busca);

                    foreach (var produto in encontrados)
                    {
                        Console.WriteLine(
                            $"{produto.Id} - {produto.Nome}");
                    }

                    break;
                case "4":
                    Console.Write("Id: ");
                    int.TryParse(Console.ReadLine(), out int id);

                    Console.Write("Novo nome: ");
                    string novoNome = Console.ReadLine() ?? "";

                    Console.Write("Novo preço: ");
                    double.TryParse(Console.ReadLine(), out double novoPreco);

                    Console.Write("Nova quantidade: ");
                    int.TryParse(Console.ReadLine(), out int novaQuantidade);

                    bool atualizado =
                        await produtoService.AtualizarProdutosAsync(
                            id,
                            novoNome,
                            novoPreco,
                            novaQuantidade);

                    Console.WriteLine(
                        atualizado
                            ? "Produto atualizado!"
                            : "Falha na atualização.");
                    break;
                case "5":
                    Console.Write("Id: ");

                    int.TryParse(
                        Console.ReadLine(),
                        out int idRemover);

                    bool removido =
                        await produtoService.RemoverProdutosAsync(
                            idRemover);

                    Console.WriteLine(
                        removido
                            ? "Produto removido!"
                            : "Produto não encontrado.");
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            Console.ReadKey();
        }
    }
}