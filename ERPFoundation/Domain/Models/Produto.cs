namespace ERPFoundation.Domain.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    
    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;

    public Produto()
    {
    }

    public Produto(int id, string sku, string nome, double preco, int quantidade)
    {
        Id = id;
        Sku = sku;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }

    public Produto(string sku, string nome, double preco, int quantidade)
    {
        Sku = sku;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }
}