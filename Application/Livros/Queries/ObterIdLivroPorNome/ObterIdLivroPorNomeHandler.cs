using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Livros.Queries.ObterIdLivroPorNome
{
    public class ObterIdLivroPorNomeHandler : ApplicationRequestHandler<ObterIdLivroPorNomeQuery, Guid>
    {
        public ObterIdLivroPorNomeHandler(IServiceProvider services) : base(services) { }

        public override async Task<Guid> Handle(ObterIdLivroPorNomeQuery request, CancellationToken cancellationToken)
        {
            var idRetorno = await ObterRepositorio<ILivrosRepositorio>()
                .ObterIdLivroPorNome(request.Nome);

            if (idRetorno == Guid.Empty)
                _mensageria.RetornarMensagemFalhaValidacao("Não foi possível encontrar um livro com esse nome.");

            return idRetorno;
        }
    }
}
