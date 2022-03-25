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
- Alterar a variável NOME_API na classe Api.Configuration.ApiConfiguration da camada de api para o nome da api desejada, bem como o nome do documento na invocação do serviço;
- Alterar a variável TABELA_LOGS na classe Api.Configuration.ApiConfiguration da camada de api para o nome da tabela desejada.

## Um pouco sobre a arquitetura da aplicação

### Api

Aqui fica endpoints da aplicação

### Application

Aqui fica os requests e requestsHandlers da aplicação, bem como implementações de hosted services.

### Communication

Aqui fica qualquer integração com apis externas, você pode criar handlers para comunicação com apis para que não seja necessário um serviço como um todo

### Domain

Domínio da aplicação, bem como seus objetos de valor, specifications etc.

### Infrastructure

Repositórios da aplicação

Para uso do repositório, é necessário manter a arquitetura da pasta "Areas" afim de poder usufruir da leitura de queries SQL em arquivos. Você pode criar uma nova pasta com um nome diferente, mas manter a arquitetura.

### Tests

Testes da aplicação