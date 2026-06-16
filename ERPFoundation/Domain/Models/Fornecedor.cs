namespace ERPFoundation.Domain.Models;

public class Fornecedor
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;

    public List<Produto> Produtos { get; set; } = new();
}