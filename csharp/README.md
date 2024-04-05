# NLW Unite Backend C#

> Evento da RocketSeat

## Tecnologias

- C#
- .Net
- Entity Framework

### Bibliotecas adicionais

-

## Links úteis

-

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