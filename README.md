# ERP-Foundation

## Sobre o Projeto

ERP Foundation é um projeto de estudos desenvolvido em **C#**, **.NET**, **Entity Framework Core** e **MySQL**, com foco em desenvolvimento backend, arquitetura de software e boas práticas de programação.

O objetivo é evoluir gradualmente a aplicação para simular componentes presentes em sistemas ERP reais, explorando conceitos utilizados em aplicações corporativas.

---

## Tecnologias

* C#
* .NET
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* LINQ
* Git e GitHub

---

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

### Recursos Implementados

* Relacionamento entre Produtos e Fornecedores
* Validação de dados com Data Annotations
* Configuração de entidades com Fluent API
* Controle de unicidade de CNPJ
* Persistência de dados com Entity Framework Core
* Operações assíncronas com Async/Await
* Consultas utilizando LINQ

---

## Arquitetura

O projeto está organizado em camadas:

* **Domain** → Entidades do domínio
* **Application** → Serviços e regras de negócio
* **Infrastructure** → Persistência de dados e configurações
* **Presentation** → Interface Console
* **API** → Endpoints REST

### Padrões Utilizados

* Repository Pattern
* Service Layer
* Dependency Injection

---

## Estrutura do Projeto

```text
ERP-Foundation/
│
├── ERPFoundation/
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   ├── Presentation/
│   └── Migrations/
│
├── ERPFoundation.API/
│   ├── Controllers/
│   └── DTOs/
│
├── ERP-Foundation.sln
└── README.md
```

---

## Como Executar

### Clonar o repositório

```bash
git clone https://github.com/EdsonJr21/ERP-Foundation.git
```

### Restaurar dependências

```bash
dotnet restore
```

### Aplicar as migrations

```bash
dotnet ef database update --project ERPFoundation
```

### Executar a API

```bash
dotnet run --project ERPFoundation.API
```

---

## Status

### Concluído

* CRUD de Produtos
* CRUD de Fornecedores
* Entity Framework Core + MySQL
* Migrations
* LINQ
* Async/Await
* Data Annotations
* Fluent API
* Relacionamentos entre Entidades
* Arquitetura em Camadas
* Estrutura inicial da Web API

### Em Desenvolvimento

* Endpoints REST
* DTOs
* Integração da API com a camada de serviços

### Próximos Passos

* AutoMapper
* Tratamento global de exceções
* JWT Authentication
* Autorização por Roles
* Testes Unitários
* Testes de Integração
* Docker
* CI/CD
* Deploy

---

## Autor

Desenvolvido por **EdsonJr21** como projeto de estudos e evolução prática em desenvolvimento backend com .NET.
