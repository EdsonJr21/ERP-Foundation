# ERP Foundation

Projeto desenvolvido para estudo de **C#**, **.NET**, **ASP.NET Core**, **Entity Framework Core** e **MySQL**, aplicando arquitetura em camadas, boas práticas e desenvolvimento de APIs REST.

---

## Tecnologias

* C#
* .NET
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* AutoMapper
* LINQ
* Git

---

## Recursos Implementados

* CRUD de Produtos
* CRUD de Fornecedores
* API REST
* DTOs
* AutoMapper
* Repository Pattern
* Service Layer
* Dependency Injection
* Middleware Global de Exceções
* Respostas de Erro Padronizadas
* Entity Framework Core + MySQL
* Migrations
* Data Annotations
* Fluent API
* Relacionamento entre Produtos e Fornecedores
* Async/Await
* LINQ

---

## Estrutura

```text
ERP-Foundation
├── ERPFoundation
│   ├── Application
│   ├── Domain
│   ├── Infrastructure
│   ├── Migrations
│   └── Presentation
│
├── ERPFoundation.API
│   ├── Controllers
│   ├── DTOs
│   ├── Middlewares
│   ├── Mappings
│   └── Responses
│
└── ERP-Foundation.sln
```

---

## Executar

```bash
git clone https://github.com/EdsonJr21/ERP-Foundation.git

dotnet restore

dotnet ef database update --project ERPFoundation

dotnet run --project ERPFoundation.API
```

---

## Roadmap

* FluentValidation
* Testes Unitários
* Testes de Integração
* Autenticação e Autorização (JWT)
* Clientes
* Pedidos
* Razor
* Blazor
* Docker
* CI/CD

---

**Desenvolvido por Edson Jr.**
