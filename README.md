# Scaffold de API
_Scaffold criado para definir o padrão de desenvolvimento de minhas APIS_

### Sobre o projeto
Api criada com o framework .NET Core na versão 3.1, utilizando pacotes criados com arquitetura netstandard2.1

### Configurações da API
Com o intuito de facilitar o fork do repositório de Scaffold, as configurações da API (middlewares básicos, serviços e implementação de pacotes etc.) foram colocadas em pacotes Nuget localizados no repositório abaixo:

*https://github.com/marcusruas/MandradeFrameworks-v2*

Até a data da implementação desta documentação (15/02/2022), os pacotes ainda não foram disponibilizados em domínio público, sendo necessário a instalação dos pacotes localmente.

## Configurações iniciais necessárias
Para começar a desenvolver neste Scaffold, é necessário somente algumas coisas:
- Alterar o nome da solution principal (ScaffoldApi.sln) para o nome da futura api;
- Alterar a variável NOME_API na classe Api.Configuration.DependencyInjection da camada de api para o nome da api desejada, bem como o nome do documento na invocação do serviço;

## Um pouco sobre a arquitetura da aplicação

### Api

### Application

### Communication

### Domain

### Infrastructure

### Tests