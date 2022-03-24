# Tutoriais

Abaixo há algumas instruções para o uso do Scaffold.

# Api

### Criando um controller padronizado

- Ao criar um endpoint na camada de API, faça a mesma herdar a classe abstrata StandardController.
- A classe acima oferece diversos benefícios, como:
    - Mediador;
    - Formatação e padronização de retornos de endpoint;
    - Geração de padrão de retorno mesmo mediante exceptions;

### Utilizando um request handler no controller.

- Crie um endpoint no controller;
- De retorno do endpoint, execute o método ProcessarSolicitacao(TRequest request);
- O método acima formata o resultado do request e retorna um json padronizado;
- Seu endpoint deve se parecer com o código abaixo;

```
[HttpGet]
// TODOS OS SEUS ENDPOINTS DEVEM RETORNAR O RETORNO DO REQUEST "WRAPPED" NA CLASSE RETORNOAPI!!!
public async Task<RetornoApi<Registro>> ObterRegistro() =>
    await ProcessarSolicitacao(new ObterRegistroRequest());
```


### Utilizando Autenticação.

- Caso você queira que seja obrigatório autenticação no endpoint, basta adicionar o atributo "EndpointRestrito" ao endpoint, assim será obrigatório passar um JSON Web Token via header "Authorization" da requisição;
- Caso o endpoint possua o atributo "EndpointRestrito", será injetado via injeção de dependência o objeto IUsuarioAutenticado na aplicação. Ele já é por padrão uma propriedade na classe ApplicationRequestHandler;
- Seu endpoint deve se parecer com isso.

```
[HttpGet]
[EndpointRestrito]
// TODOS OS SEUS ENDPOINTS DEVEM RETORNAR O RETORNO DO REQUEST "WRAPPED" NA CLASSE RETORNOAPI!!!
public async Task<RetornoApi<Registro>> ObterRegistro() =>
    await ProcessarSolicitacao(new ObterRegistroRequest());
```

- Vale lembrar que caso o usuário não passe o token nesse endpoint, será retornado um json padrão com uma mensagem de erro.

### Utilizando autenticação com permissões específicas.

- O atributo "EndpointRestrito possui duas sobrecargas para passar uma ou mais permissões que o usuário precise ter para acessar tal método, se ele não tiver uma ou mais o método retornará erro.
- As permissões do usuário são obtidas via token.
- Seu endpoint deve se parecer com isso.

```
[HttpGet]
[EndpointRestrito("PERMISSAO_CONSULTA_REGISTRO")]
// TODOS OS SEUS ENDPOINTS DEVEM RETORNAR O RETORNO DO REQUEST "WRAPPED" NA CLASSE RETORNOAPI!!!
public async Task<RetornoApi<Registro>> ObterRegistro() =>
    await ProcessarSolicitacao(new ObterRegistroRequest());
```


### Validar modelo passado

- Caso um modelo seja passado para o endpoint e possua DataAnnotations (Required, MaxLength etc), caso os valroes passados sejam inválidos, para cada valor inválido será adicionado uma mensagem de erro e logo após a validação será retornado um json padronizado com os erros;

# Application

### Criando um Request Handler.

- Criar uma área na camada de Application (ex: Application.Exemplo) e dentro as pastas Commands e Queries;
- Para criar um request handler, crie uma nova pasta com o nome da requisição (ex: ObterRegistro);
- Dentro, criar uma classe com o nome do namespace atual + "Request" (ex: ObterRegistroRequest);
- Fazer a classe de request herdar da interface IRequest<T> em que T é o retorno do request;
- Criar uma classe de handler, utilizando o namespace atual + "Handler" (ex: ObterRegistroHandler);
- Fazer o handler herdar da classe ApplicationRequestHandler<TRequest, TResponse>;
- TRequest é o tipo da classe que você criou de request, TResponse é o que ela retorna; 
- Suas classes devem se parecer com isso

```
Request

public class ObterRegistroRequest : IRequest<Registro>
{
    public Guid RegistroId { get; set; }
    // Aqui ficam os parâmetros para sua solicitação
}

Handler

public class ObterRegistroHandler : ApplicationRequestHandler<ObterRegistroRequest, Registro>
{
    public ObterRegistroHandler(IServiceProvider services) : base(services) { }

    public override async Task<Registro> Handle(ObterRegistroRequest request, CancellationToken cancellationToken)
    {
        ...
        // Aqui fica a implementação do seu código
    }
}
```

# Infrastructure

## Criando um DBContext

- Gerar uma classe que sirva como context;
- Fazer a classe herdar a classe abstrata StandardContext<TContext> (nesse caso, se você criar uma classe de nome "ExemploContext", você coloca como tipo a própria classe, como StandardContext<ExemploContext>);
- Gerar um override do método OnModelCreating da seguinte forma;

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.AplicarModelBuilders();
```

- Esse override serve para você registrar os mapeamentos das entidades para tabelas;
- Registrar o context criado da seguinte forma na classe Startup da camada de Api.

```
services.AddDbContextSqlServer<LivrosContext>(Configuration, NOME_DO_BANCO);
// Adicionar a connection string do DBContext em "ConnectionStrings:NOME_DO_BANCO" no arquivo de AppSettings.
```

- Gerar Migration do context (dotnet ef migrations add PrimeiraMigration --startup-project ../Api);
- Dar update na tabela. (dotnet ef database update --startup-project ../Api);
- Caso você queira criar algum mapeamento, colocar as classes de mapeamento herdadas da interface IEntityTypeConfiguration<T> na seguinte hierarquia: Infrastructure.Area.Maps;

## Criando um repositório

### Para uso de EF Core e Dapper
- Antes de tudo, primeiro é necessário criar um DBContext para a sua área da aplicação. Por motivos de exemplo, estarei criando as implementações/interfaces assumindo a existência de um DBContext de nome "ExemploContext";
- Criar uma classe de implementação (Com sufixo "Repository", ex: ExemploRepository);
- Fazer a classe de implementação herdar da classe abstrata "StandardSqlRepository<TContext>";
- Criar uma interface para a implementação (Com sufixo "Repository" e prefixo "I", ex: IExemploRepository);
- Fazer a interface herdar da interface IContextRepository, para que assim a mesma possa conceder acesso aos métodos de uso do EF Core para outras camadas;
- A interface IContextRepository também torna possível resgatar o seu repositório pelo método ObterRepositorio<T> da classe abstrata ApplicationRequestHandler;
- Realizar a injeção de dependência do repositório na classe Infrastructure.Configuration.RepositoryDependencyInjection, no método *public static void AdicionarRepositorios(this IServiceCollection services)*;
- Ao final do processo, suas classes deverão se parecer com as classes abaixo;

```
Implementação:

public class ExemploRepositorio : StandardSqlRepository<ExemploContext>, IExemploRepositorio
{
    public ExemploRepositorio(IServiceProvider provider, ExemploContext context)
    : base(provider, context) { }

    ...
    //Aqui você pode criar métodos customizados de consulta via Dapper e adiciona-los a interface.
    ...
}

Interface:

public interface IExemploRepositorio : IContextRepository
{
}

Injeção de dependencia:

services.AddScoped<IExemploRepositorio, ExemploRepositorio>();

```

### Para uso apenas de Dapper
- Criar uma classe de implementação (Com sufixo "Repository", ex: ExemploRepository);
- Fazer a classe de implementação herdar da classe abstrata "StandardSqlRepository";
- Criar uma interface para a implementação (Com sufixo "Repository" e prefixo "I", ex: IExemploRepository);
- Fazer a interface herdar da interface IRepository, para que assim a mesma possa conceder acesso aos métodos de uso do EF Core para outras camadas;
- A interface IRepository também torna possível resgatar o seu repositório pelo método ObterRepositorio<T> da classe abstrata ApplicationRequestHandler;
- Realizar a injeção de dependência do repositório na classe Infrastructure.Configuration.RepositoryDependencyInjection, no método *public static void AdicionarRepositorios(this IServiceCollection services)*;
- Ao final do processo, suas classes deverão se parecer com as classes abaixo.

```
Implementação:

public class ExemploRepositorio : StandardSqlRepository, IExemploRepositorio
{
    public ExemploRepositorio(IServiceProvider provider) : base(provider) { }

    ...
    //Aqui você pode criar métodos customizados de consulta via Dapper e adiciona-los a interface.
    ...
}

Interface:

public interface IExemploRepositorio : IRepository
{
}

Injeção de dependencia:

services.AddScoped<IExemploRepositorio, ExemploRepositorio>();

```

### Obter uma query SQL via arquivo

- Salvar o arquivo com a query em questão na hierarquia de pasta Infrastructure.Exemplo.Repository.SQL;
- Adicionar o seguinte snippet ao seu Infrastructure.csproj para que os arquivos SQL sejam copiados para o build

```
// Vale lembrar que o "Exemplo" aqui é qualquer nome que você quiser.
<ItemGroup>
    <None Update="Exemplo\Repository\SQL\*.sql">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

- Está pronto. Repositórios que herdam tanto do StandardSqlRepository quanto StandardSqlRepository<TContext> possuem um método chamado ObterConteudoArquivoSql(string nomeArquivo) que pode ser usado para ler o arquivo e obter a query em uma string.
- Você pode obter o conteúdo do arquivo da seguinte forma:

```
// Assume-se que tem um arquivo chamado "querySql.sql" na pasta SQL do repositório.
string querySql = ObterConteudoArquivoSql("querySql");
```

### Executar uma query via Dapper

- Repositórios que herdam tanto do StandardSqlRepository quanto StandardSqlRepository<TContext> possuem um método chamado ExecutarComandoSql(Func<SqlConnection, Task> funcao, string nomeBanco). Nele você pode passar uma função para retorno e execução de dados, como no exemplo abaixo;
- Vale lembrar que o segundo parâmetro da função acima, nomeBanco, deve ter o mesmo nome da chave "ConnectionStrings:NOME_BANCO" do appsettings.

```
Guid idRetorno;
await ExecutarComandoSql(
    async conexao => idRetorno = await conexao.QueryFirstOrDefaultAsync<Guid>(querySql, parametros), 
    "Livros" //Nome do banco. No appsettings há uma connection string com esse nome.
);

```

# Gostaria de ver uma implementação desse scaffold?

Consulte a url https://github.com/marcusruas/exemplo-implementacao-padraoapi-v3 para exemplos das implementações acima e muito mais, como Mensageria, SharedKernel entre outras