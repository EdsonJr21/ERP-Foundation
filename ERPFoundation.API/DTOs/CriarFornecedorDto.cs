namespace ERPFoundation.API.DTOs;

public record CriarFornecedorDto(
    string Nome,
    string Cnpj,
    string Endereco
);