using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;

namespace ERPFoundation.Presentation.ConsoleUI;

public class FornecedorMenu
{
    private readonly IFornecedorService _fornecedorService;

    public FornecedorMenu(IFornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    public async Task ExibirAsync()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== Fornecedores ===");
            Console.WriteLine("1 - Cadastrar Fornecedor");
            Console.WriteLine("2 - Listar Fornecedores");
            Console.WriteLine("3 - Buscar Fornecedor por ID");
            Console.WriteLine("4 - Atualizar Fornecedor");
            Console.WriteLine("5 - Remover Fornecedor");
            Console.WriteLine("0 - Voltar");
            Console.Write("Digite uma opcao: ");

            var opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    await CadastrarFornecedorAsync();
                    break;
                case "2":
                    await ListarFornecedoresAsync();
                    break;
                case "3":
                    await BuscarFornecedorAsync();
                    break;
                case "4":
                    await AtualizarFornecedorAsync();
                    break;
                case "5":
                    await RemoverFornecedorAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opcao invalida.");
                    Pausar();
                    break;
            }
        }
    }

    private async Task CadastrarFornecedorAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Cadastrar Fornecedor ===");

        Console.Write("Nome: ");
        var nome = Console.ReadLine() ?? "";

        Console.Write("CNPJ: ");
        var cnpj = Console.ReadLine() ?? "";

        Console.Write("Endereco: ");
        var endereco = Console.ReadLine() ?? "";

        var fornecedor = new Fornecedor
        {
            Nome = nome,
            Cnpj = cnpj,
            Endereco = endereco
        };

        var cadastrado = await _fornecedorService.AdicionarFornecedorAsync(fornecedor);

        Console.WriteLine(cadastrado
            ? "Fornecedor cadastrado com sucesso."
            : "Nao foi possivel cadastrar o fornecedor. Verifique os dados informados.");

        Pausar();
    }

    private async Task ListarFornecedoresAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Lista de Fornecedores ===");

        var fornecedores = await _fornecedorService.ListarFornecedoresAsync();

        ExibirFornecedores(fornecedores);

        Pausar();
    }

    private async Task BuscarFornecedorAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Fornecedor ===");

        var id = LerInteiro("ID do fornecedor: ");

        var fornecedor = await _fornecedorService.BuscarPorIdAsync(id);

        if (fornecedor is null)
        {
            Console.WriteLine("Fornecedor nao encontrado.");
        }
        else
        {
            ExibirFornecedor(fornecedor);
        }

        Pausar();
    }

    private async Task AtualizarFornecedorAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Atualizar Fornecedor ===");

        var id = LerInteiro("ID do fornecedor: ");

        Console.Write("Novo nome: ");
        var nome = Console.ReadLine() ?? "";

        Console.Write("Novo CNPJ: ");
        var cnpj = Console.ReadLine() ?? "";

        Console.Write("Novo endereco: ");
        var endereco = Console.ReadLine() ?? "";

        var fornecedor = new Fornecedor
        {
            Id = id,
            Nome = nome,
            Cnpj = cnpj,
            Endereco = endereco
        };

        var atualizado = await _fornecedorService.AtualizarFornecedorAsync(fornecedor);

        Console.WriteLine(atualizado
            ? "Fornecedor atualizado com sucesso."
            : "Nao foi possivel atualizar o fornecedor.");

        Pausar();
    }

    private async Task RemoverFornecedorAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Remover Fornecedor ===");

        var id = LerInteiro("ID do fornecedor: ");

        var removido = await _fornecedorService.RemoverFornecedorAsync(id);

        Console.WriteLine(removido
            ? "Fornecedor removido com sucesso."
            : "Nao foi possivel remover o fornecedor.");

        Pausar();
    }

    private static void ExibirFornecedores(List<Fornecedor> fornecedores)
    {
        if (fornecedores.Count == 0)
        {
            Console.WriteLine("Nenhum fornecedor encontrado.");
            return;
        }

        foreach (var fornecedor in fornecedores)
        {
            ExibirFornecedor(fornecedor);
        }
    }

    private static void ExibirFornecedor(Fornecedor fornecedor)
    {
        Console.WriteLine(
            $"ID: {fornecedor.Id} | Nome: {fornecedor.Nome} | CNPJ: {fornecedor.Cnpj} | Endereco: {fornecedor.Endereco}");
    }

    private static int LerInteiro(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            var entrada = Console.ReadLine();

            if (int.TryParse(entrada, out var valor))
            {
                return valor;
            }

            Console.WriteLine("Informe um numero inteiro valido.");
        }
    }

    private static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
}
