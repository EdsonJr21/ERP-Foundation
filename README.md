# ERP Foundation

## Sobre o Projeto

ERP Foundation é um projeto de estudos desenvolvido com **C#**, **.NET**, **Entity Framework Core** e **MySQL**, com foco em desenvolvimento backend, arquitetura de software e boas práticas de mercado.

O projeto evolui gradualmente para uma base de ERP com gerenciamento de produtos e fornecedores por meio de uma aplicação em camadas e uma API REST.

## Tecnologias

* C#
* .NET
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* LINQ
* Git

## Funcionalidades

### Produtos

* Cadastro
* Consulta
* Atualização
* Remoção

### Fornecedores

* Cadastro
* Consulta
* Atualização
* Remoção

## Recursos Implementados

* Relacionamento entre Produtos e Fornecedores
* Data Annotations
* Fluent API
* Validação de regras de negócio
* Persistência com Entity Framework Core
* Operações assíncronas com Async/Await
* Consultas com LINQ
* API REST
* DTOs

## Arquitetura

### Camadas

* Domain
* Application
* Infrastructure
* Presentation
* API

### Padrões

* Repository Pattern
* Service Layer
* Dependency Injection

## Estrutura do Projeto

```text
ERP-Foundation
├── ERPFoundation
│   ├── Application
│   ├── Domain
│   ├── Infrastructure
│   ├── Migrations
│   ├── Presentation
│   └── Program.cs
├── ERPFoundation.API
│   ├── Controllers
│   ├── DTOs
│   └── Program.cs
├── ERP-Foundation.sln
└── README.md
```

## Como Executar

### Clonar o repositório

```bash
git clone https://github.com/EdsonJr21/ERP-Foundation.git
```

### Restaurar dependências

```bash
dotnet restore
```

### Aplicar migrations

```bash
dotnet ef database update --project ERPFoundation
```

### Executar a API

```bash
dotnet run --project ERPFoundation.API
```

## Status Atual

### Concluído

* CRUD de Produtos
* CRUD de Fornecedores
* Entity Framework Core + MySQL
* Migrations
* LINQ
* Async/Await
* Data Annotations
* Fluent API
* API REST
* DTOs
* Arquitetura em camadas

### Próximos Passos

* AutoMapper
* Tratamento global de exceções
* FluentValidation
* JWT
* Testes unitários
* Testes de integração
* Docker
* CI/CD
* Clientes
* Pedidos

## Autor

Desenvolvido por **Edson Jr.** para estudos de desenvolvimento backend com .NET.
