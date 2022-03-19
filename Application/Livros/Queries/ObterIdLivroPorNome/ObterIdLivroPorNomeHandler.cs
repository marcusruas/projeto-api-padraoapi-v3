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

            Log.Information(nameof(ObterIdLivroPorNomeHandler), idRetorno.ToString());

            if (idRetorno == Guid.Empty)
            {
                Log.Warning(nameof(ObterIdLivroPorNomeHandler), Mensagens.LivroNaoEncontrado);
                _mensageria.RetornarMensagemFalhaValidacao(Mensagens.LivroNaoEncontrado);
            }

            return idRetorno;
        }
    }
}
