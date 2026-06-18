using System.Globalization;
using ERPFoundation.Application.Services.Interfaces;
using ERPFoundation.Domain.Models;

namespace ERPFoundation.Presentation.ConsoleUI;

public class ProdutoMenu
{
    private readonly IProdutoService _produtoService;

    public ProdutoMenu(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task ExibirAsync()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("=== Produtos ===");
            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Listar Produtos");
            Console.WriteLine("3 - Buscar Produto");
            Console.WriteLine("4 - Atualizar Produto");
            Console.WriteLine("5 - Remover Produto");
            Console.WriteLine("0 - Voltar");
            Console.Write("Digite uma opcao: ");

            var opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    await CadastrarProdutoAsync();
                    break;
                case "2":
                    await ListarProdutosAsync();
                    break;
                case "3":
                    await BuscarProdutoAsync();
                    break;
                case "4":
                    await AtualizarProdutoAsync();
                    break;
                case "5":
                    await RemoverProdutoAsync();
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

    private async Task CadastrarProdutoAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Cadastrar Produto ===");
        var rastreamentoId = Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        RastrearCadastroProduto(rastreamentoId, "Inicio da tentativa de cadastro.");

        Console.Write("SKU: ");
        var sku = Console.ReadLine() ?? "";

        Console.Write("Nome: ");
        var nome = Console.ReadLine() ?? "";

        var preco = LerDecimal("Preco: ");
        var quantidade = LerInteiro("Quantidade: ");
        var fornecedorId = LerInteiro("ID do fornecedor: ");

        RastrearCadastroProduto(
            rastreamentoId,
            $"Dados informados: SKU='{sku}', Nome='{nome}', Preco={preco}, Quantidade={quantidade}, FornecedorId={fornecedorId}.");

        try
        {
            var resultado = await _produtoService.CadastrarProdutoComDiagnosticoAsync(
                sku,
                nome,
                preco,
                quantidade,
                fornecedorId);

            RastrearCadastroProduto(
                rastreamentoId,
                resultado.Cadastrado
                    ? "Service confirmou o cadastro."
                    : $"Service recusou o cadastro. Motivo: {resultado.Motivo}");

            Console.WriteLine(resultado.Cadastrado
                ? "Produto cadastrado com sucesso."
                : $"Nao foi possivel cadastrar o produto. Motivo: {resultado.Motivo}");
        }
        catch (Exception ex)
        {
            RastrearCadastroProduto(rastreamentoId, "Erro inesperado durante o cadastro.");
            ExibirErroRastreamento(rastreamentoId, ex);
        }

        Pausar();
    }

    private async Task ListarProdutosAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Lista de Produtos ===");

        var produtos = await _produtoService.ListarProdutosAsync();
        ExibirProdutos(produtos);

        Pausar();
    }

    private async Task BuscarProdutoAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Produto ===");

        Console.Write("Nome ou SKU: ");
        var termo = Console.ReadLine() ?? "";

        var produtos = await _produtoService.BuscarProdutosAsync(termo);
        ExibirProdutos(produtos);

        Pausar();
    }

    private async Task AtualizarProdutoAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Atualizar Produto ===");

        var id = LerInteiro("ID do produto: ");

        Console.Write("Novo nome: ");
        var nome = Console.ReadLine() ?? "";

        var preco = LerDecimal("Novo preco: ");
        var quantidade = LerInteiro("Nova quantidade: ");

        var atualizado = await _produtoService.AtualizarProdutosAsync(
            id,
            nome,
            preco,
            quantidade);

        Console.WriteLine(atualizado
            ? "Produto atualizado com sucesso."
            : "Nao foi possivel atualizar o produto. Verifique os dados informados.");

        Pausar();
    }

    private async Task RemoverProdutoAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Remover Produto ===");

        var id = LerInteiro("ID do produto: ");

        var removido = await _produtoService.RemoverProdutosAsync(id);

        Console.WriteLine(removido
            ? "Produto removido com sucesso."
            : "Nao foi possivel remover o produto. O produto deve existir e estar com quantidade zero.");

        Pausar();
    }

    private static void ExibirProdutos(List<Produto> produtos)
    {
        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto encontrado.");
            return;
        }

        foreach (var produto in produtos)
        {
            Console.WriteLine(
                $"ID: {produto.Id} | SKU: {produto.Sku} | Nome: {produto.Nome} | " +
                $"Preco: {produto.Preco:C} | Quantidade: {produto.Quantidade} | " +
                $"Fornecedor ID: {produto.FornecedorId}");
        }
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

    private static decimal LerDecimal(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            var entrada = Console.ReadLine();

            if (decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.CurrentCulture, out var valor) ||
                decimal.TryParse(entrada, NumberStyles.Number, CultureInfo.InvariantCulture, out valor))
            {
                return valor;
            }

            Console.WriteLine("Informe um valor decimal valido.");
        }
    }

    private static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    private static void RastrearCadastroProduto(string rastreamentoId, string mensagem)
    {
        Console.WriteLine($"[ProdutoCadastro:{rastreamentoId}] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}");
    }

    private static void ExibirErroRastreamento(string rastreamentoId, Exception exception)
    {
        Console.WriteLine($"Nao foi possivel cadastrar o produto. Rastreamento: {rastreamentoId}");

        var erro = exception;
        var nivel = 0;

        while (erro != null)
        {
            Console.WriteLine($"[ProdutoCadastro:{rastreamentoId}] Erro nivel {nivel}: {erro.GetType().Name} - {erro.Message}");
            erro = erro.InnerException;
            nivel++;
        }
    }
}
