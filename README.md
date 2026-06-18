# ERP-Foundation

## Sobre o Projeto

ERP Foundation é um projeto de estudo desenvolvido em **C#**, **.NET**, **Entity Framework Core** e **MySQL**, com foco em arquitetura de software e boas práticas de desenvolvimento backend.

O objetivo é evoluir gradualmente a aplicação de um sistema console para uma **API REST completa**, aplicando conceitos e padrões utilizados em sistemas ERP corporativos.

---

## Tecnologias

* C#
* .NET
* Entity Framework Core
* MySQL
* Git
* GitHub

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

### Regras Implementadas

* Relacionamento entre Produtos e Fornecedores
* Validação de dados com Data Annotations
* Configurações de entidades com Fluent API
* Controle de unicidade de CNPJ
* Persistência de dados com Entity Framework Core
* Operações assíncronas com Async/Await
* Consultas utilizando LINQ

---

## Arquitetura

O projeto segue uma arquitetura em camadas inspirada em aplicações corporativas:

* **Domain** → Entidades e regras centrais do sistema
* **Application** → Regras de negócio e serviços
* **Infrastructure** → Persistência de dados e configurações
* **Presentation** → Interface de interação com o usuário

### Padrões Aplicados

* Repository Pattern
* Service Layer
* Dependency Injection

---

## Estrutura do Projeto

```text
ERP-Foundation/
├─ ERPFoundation/
│  ├─ Application/
│  │  └─ Services/
│  ├─ Domain/
│  │  └─ Models/
│  ├─ Infrastructure/
│  │  ├─ Data/
│  │  ├─ DependencyInjection/
│  │  └─ Repositories/
│  ├─ Migrations/
│  ├─ Presentation/
│  │  └─ ConsoleUI/
│  ├─ Program.cs
│  └─ ERPFoundation.csproj
├─ ERP-Foundation.sln
└─ README.md
```

---

## Como Executar

### 1. Clone o repositório

```bash
git clone https://github.com/EdsonJr21/ERP-Foundation.git
```

### 2. Entre na pasta do projeto

```bash
cd ERP-Foundation
```

### 3. Entre na aplicação

```bash
cd ERPFoundation
```

### 4. Restaure os pacotes

```bash
dotnet restore
```

### 5. Configure a conexão com o banco de dados

Ajuste a Connection String no `AppDbContext` para o seu ambiente local.

Exemplo:

```text
Server=localhost;Database=erpfoundation;User=root;Password=sua_senha;
```

### 6. Execute as migrations

```bash
dotnet ef database update
```

### 7. Execute a aplicação

```bash
dotnet run
```

---

## Status

🚧 Projeto em evolução contínua, utilizado como laboratório prático para aprofundamento em desenvolvimento backend com .NET.

### Concluído

* CRUD de Produtos
* CRUD de Fornecedores
* Entity Framework Core
* MySQL
* Migrations
* LINQ
* Async/Await
* Dependency Injection
* Data Annotations
* Fluent API
* Relacionamentos entre Entidades
* Arquitetura em Camadas

### Próximas Etapas

* ASP.NET Core Web API
* DTOs
* AutoMapper
* Tratamento Global de Exceções
* JWT Authentication
* Autorização por Roles
* Testes Unitários
* Testes de Integração
* Docker
* CI/CD
* Deploy

---

## Autor

Desenvolvido por **EdsonJr21** como projeto de estudos para aprofundamento em desenvolvimento backend com .NET e construção gradual de um sistema ERP.
