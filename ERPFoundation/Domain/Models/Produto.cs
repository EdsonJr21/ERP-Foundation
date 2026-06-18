using System.ComponentModel.DataAnnotations;

namespace ERPFoundation.Domain.Models;

public class Produto
{
    public int Id { get; set; }
    [Required] [StringLength(100)] public string Nome { get; set; } = string.Empty;
    [Required] [StringLength(50)] public string Sku { get; set; } = string.Empty;
    [Range(0.01, 999999)] public decimal Preco { get; set; }
    [Range(0, int.MaxValue)] public int Quantidade { get; set; }

    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; } = null!;

    public Produto()
    {
    }

    public Produto(int id, string sku, string nome, decimal preco, int quantidade)
    {
        Id = id;
        Sku = sku;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
    }

    public Produto(string sku, string nome, decimal preco, int quantidade, int fornecedorId)
    {
        Sku = sku;
        Nome = nome;
        Preco = preco;
        Quantidade = quantidade;
        FornecedorId = fornecedorId;
    }
}
