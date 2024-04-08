# NLW Unite Backend C#

> Evento da RocketSeat

## Tecnologias

- C#
- .Net
- Entity Framework

### Bibliotecas adicionais

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Sqlite

## DotNet

### Criando Estrutura do projeto

- mkdir PassIn
- cd PassIn
- dotnet new sln --name PassIn
- dotnet new gitignore
- dotnet new webapi --use-controllers --output PassIn.Api
- dotnet sln add PassIn.Api
- dotnet new classlib --output PassIn.Application
- dotnet sln add PassIn.Application
- dotnet new classlib --output PassIn.Communication
- dotnet sln add PassIn.Communication
- dotnet new classlib --output PassIn.Exceptions
- dotnet sln add PassIn.Exceptions
- dotnet new classlib --output PassIn.Infrastructure
- dotnet sln add PassIn.Infrastructure
- dotnet add PassIn.Api reference PassIn.Application
- dotnet add PassIn.Api reference PassIn.Communication
- dotnet add PassIn.Api reference PassIn.Exceptions
- dotnet add PassIn.Application reference PassIn.Communication
- dotnet add PassIn.Application reference PassIn.Exceptions
- dotnet add PassIn.Application reference PassIn.Infrastructure

### Instalando dependências

- dotnet add PassIn.Infrastructure package Microsoft.EntityFrameworkCore --version 8.0.3
- dotnet add PassIn.Infrastructure package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.3

## Rodar

### Requisitos

- .NET 8.0 sdk and runtime instalados

### Comandos

- dotnet run --project PassIn.Api

### Testar

- http://localhost:5210/swagger/index.html

## Melhorias implementadas

- Injeção de dependências
- Criar camada de Domínio
- Criar camada Repositories (Não acessar o DbContext diretamente)

### Comandos DotNet utilizados para Melhorias implementadas

- dotnet new classlib --output PassIn.Domain
- dotnet sln add PassIn.Domain
- dotnet add PassIn.Application reference PassIn.Domain
- dotnet add PassIn.Infrastructure reference PassIn.Domain
- dotnet remove PassIn.Application reference PassIn.Infrastructure
- dotnet add PassIn.Api reference PassIn.Infrastructure (for DI)

## Ideias de Melhorias

- Task (Assincronismo nas chamadas ao banco de dados)
- Teste de unidades
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
- Relação de um participante para um check-in (está um para muitos no banco)
- UUID na entidade por causa do SQLite (teria que mudar o banco)
- Localização de mensagens (internacionalização)
