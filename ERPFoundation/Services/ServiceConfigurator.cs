namespace ERP;

public class ServiceConfigurator
{
    public static IProdutoService Configurar(AppDbContext context)
    {
        IProdutoRepository repository = new ProdutoRepository(context);
        return new ProdutoService(repository);
    }
}