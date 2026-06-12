namespace Praticando01.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sku { get; set; }
    public double Preco { get; set; }
    public int Quantidade { get; set; }

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