# ERP-Foundation

Projeto desenvolvido para estudo de **C#**, **.NET**, **Entity Framework Core**, **MySQL** e **arquitetura de software**, evoluindo gradualmente para um sistema **ERP** com **API REST**, autenticação e testes automatizados.

---

## Objetivos

- Aprimorar conhecimentos em C#
- Aplicar Programação Orientada a Objetos (POO)
- Implementar arquitetura em camadas
- Trabalhar com Entity Framework Core e MySQL
- Utilizar Repository Pattern e Service Layer
- Aplicar consultas avançadas com LINQ
- Utilizar operações assíncronas com Async/Await
- Desenvolver APIs REST com ASP.NET Core
- Aplicar autenticação e autorização com JWT
- Utilizar testes automatizados
- Simular funcionalidades presentes em sistemas ERP reais

---

## Tecnologias

- C#
- .NET
- Entity Framework Core
- MySQL
- LINQ
- Async/Await
- Migrations
- Git
- GitHub

---

## Funcionalidades

### Produtos

- Cadastro de Produtos
- Atualização de Produtos
- Remoção de Produtos
- Consulta de Produtos

### Fornecedores

- Cadastro de Fornecedores
- Consulta de Fornecedores

### Infraestrutura

- Persistência de dados com Entity Framework Core
- Banco de dados MySQL
- Controle de schema com Migrations
- Operações assíncronas com Async/Await
- Consultas utilizando LINQ
- Arquitetura em camadas
- Repository Pattern
- Service Layer

---

## Estrutura do Projeto

```text
ERPFoundation/
│
├── Application/
│   ├── Configuration/
│   └── Services/
│       ├── Interfaces/
│       ├── ProdutoService.cs
│       └── FornecedorService.cs
│
├── Domain/
│   └── Models/
│       ├── Produto.cs
│       └── Fornecedor.cs
│
├── Infrastructure/
│   ├── Data/
│   └── Repositories/
│       ├── Interfaces/
│       ├── ProdutoRepository.cs
│       └── FornecedorRepository.cs
│
├── Migrations/
│
├── Docs/
│
└── Program.cs
```

---

## Conceitos Aplicados

- Programação Orientada a Objetos (POO)
- Arquitetura em Camadas
- Repository Pattern
- Service Layer
- Entity Framework Core
- LINQ
- Async/Await
- Migrations
- Tratamento de Exceções
- Persistência de Dados

---

## Documentação

- [Roadmap](./ERPFoundation/Docs/Roadmap.md)

---

## Configuração

Antes de executar o projeto, ajuste a Connection String no `AppDbContext` para o seu ambiente local.

Exemplo:

```json
"Server=localhost;Database=erpfoundation;User=root;Password=sua_senha;"
```

---

## Como Executar

1. Clone o repositório

```bash
git clone https://github.com/EdsonJr21/ERP-Foundation.git
```

2. Acesse a pasta do projeto

```bash
cd ERP-Foundation/ERPFoundation
```

3. Restaure os pacotes

```bash
dotnet restore
```

4. Execute as migrations

```bash
dotnet ef database update
```

5. Execute o projeto

```bash
dotnet run
```

---

## Status

🚧 Projeto em evolução contínua, utilizado como base prática para aprofundamento em desenvolvimento .NET e construção de um ERP.

### Implementado

- Entity Framework Core
- MySQL
- Repository Pattern
- Service Layer
- LINQ
- Async/Await
- Migrations
- Arquitetura em Camadas
- CRUD de Produtos
- CRUD de Fornecedores

### Próximos Passos

- Data Annotations
- Fluent API
- Relacionamentos entre Entidades
- Dependency Injection
- ASP.NET Core Web API
- JWT Authentication
- Testes Automatizados
- Docker
- Deploy

---

## Autor

Projeto desenvolvido por **EdsonJr21** como laboratório prático de estudos em desenvolvimento .NET.