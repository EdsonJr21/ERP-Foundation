# ERP Foundation

Projeto desenvolvido para estudo de **C#**, **.NET**, **ASP.NET Core**, **Entity Framework Core** e **MySQL**, aplicando arquitetura em camadas, boas práticas, API REST e testes automatizados.

---

## Tecnologias

* C#
* .NET
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* AutoMapper
* FluentValidation
* xUnit
* Moq
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
* FluentValidation
* Testes Unitários
* Entity Framework Core + MySQL
* Migrations
* Fluent API
* Data Annotations
* Relacionamento entre Produtos e Fornecedores
* Async/Await
* LINQ

---

## Estrutura

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
│   ├── Filters  
│   ├── Middlewares  
│   ├── Mappings  
│   ├── Responses  
│   └── Validators  
│  
├── ERPFoundation.Tests  
│   ├── Application  
│   ├── Builders  
│   └── Unit Tests  
│  
└── ERP-Foundation.sln

---

## Executar

git clone https://github.com/EdsonJr21/ERP-Foundation.git

dotnet restore

dotnet ef database update --project ERPFoundation

dotnet run --project ERPFoundation.API

---

## Roadmap

* Testes de Integração
* Arquitetura de Monólito Modular
* Autenticação e Autorização (JWT)
* Clientes
* Pedidos
* Docker
* CI/CD

---

**Desenvolvido por Edson Jr.**