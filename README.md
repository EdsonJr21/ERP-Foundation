# ERP Foundation

API REST desenvolvida para estudo de **C#**, **.NET** e **ASP.NET Core**, simulando um sistema ERP e aplicando conceitos utilizados em aplicações reais, como arquitetura em camadas, Entity Framework Core, validações e testes automatizados.

## Objetivo

Este projeto foi desenvolvido para consolidar conhecimentos em desenvolvimento backend com .NET, aplicando boas práticas, padrões de arquitetura e recursos amplamente utilizados em aplicações corporativas.

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
* Git

## Arquitetura

* Layered Architecture
* Repository Pattern
* Service Layer
* Dependency Injection
* DTOs
* Fluent API

## Funcionalidades

* CRUD de Produtos
* CRUD de Fornecedores
* Relacionamento entre Produtos e Fornecedores
* Validação com FluentValidation
* Middleware global para tratamento de exceções
* Mapeamento com AutoMapper
* Migrations do Entity Framework Core
* Testes Unitários

## Como executar

```bash
git clone https://github.com/EdsonJr21/ERP-Foundation.git

cd ERP-Foundation

dotnet restore

dotnet ef database update --project ERPFoundation

dotnet run --project ERPFoundation.API
```

Após executar a aplicação, acesse o **Swagger** para testar os endpoints da API.

## Roadmap

* [x] CRUD de Produtos
* [x] CRUD de Fornecedores
* [x] AutoMapper
* [x] FluentValidation
* [x] Middleware Global de Exceções
* [x] Testes Unitários
* [ ] Testes de Integração
* [ ] Autenticação e Autorização (JWT)
* [ ] Clientes
* [ ] Pedidos
* [ ] Arquitetura de Monólito Modular
* [ ] Docker
* [ ] CI/CD

---

Desenvolvido por **Edson Jr.**
