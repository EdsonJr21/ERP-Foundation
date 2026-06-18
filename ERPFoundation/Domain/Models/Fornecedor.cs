using System.ComponentModel.DataAnnotations;

namespace ERPFoundation.Domain.Models;

public class Fornecedor
{
    public int Id { get; set; }
    [Required] [StringLength(100)] public string Nome { get; set; } = string.Empty;
    [Required] [StringLength(18)] public string Cnpj { get; set; } = string.Empty;
    [StringLength(200)] public string Endereco { get; set; } = string.Empty;

    public List<Produto> Produtos { get; set; } = new();
}