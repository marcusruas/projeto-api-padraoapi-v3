using Application.Recursos;
using Domain.Livros.Entities;
using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Livros.Queries.ObterPrimeiroLivro
{
    public class ObterPrimeiroLivroHandler : ApplicationRequestHandler<ObterPrimeiroLivroQuery, Livro>
    {
        public ObterPrimeiroLivroHandler(IServiceProvider services) : base(services) { }

        public override async Task<Livro> Handle(ObterPrimeiroLivroQuery request, CancellationToken cancellationToken)
        {
            var livro = await ObterRepositorio<ILivrosRepositorio>().FirstOrDefaultAsync<Livro>();

            if (livro is null)
                _mensageria.RetornarMensagemFalhaValidacao(Mensagens.NenhumLivroCadastrado);

            return livro;
        }
    }
}
