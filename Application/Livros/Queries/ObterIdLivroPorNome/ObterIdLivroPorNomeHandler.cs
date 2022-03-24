using Application.Recursos;
using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using Serilog;
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

            Log.ForContext("IdRetorno", idRetorno.ToString()).Information(nameof(ObterIdLivroPorNomeHandler));

            if (idRetorno == Guid.Empty)
            {
                Log.ForContext("MensagemRetorno", Mensagens.LivroNaoEncontrado).Warning(nameof(ObterIdLivroPorNomeHandler));
                _mensageria.RetornarMensagemFalhaValidacao(Mensagens.LivroNaoEncontrado);
            }

            return idRetorno;
        }
    }
}
