using Domain.Livros.Entities;
using Domain.Livros.Specifications;
using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Livros.Queries.ListarLivrosComDescricao
{
    class ListarLivrosComDescricaoHandler : ApplicationRequestHandler<ListarLivrosComDescricaoQuery, IEnumerable<Livro>>
    {
        public ListarLivrosComDescricaoHandler(IServiceProvider services) : base(services) { }

        public override async Task<IEnumerable<Livro>> Handle(ListarLivrosComDescricaoQuery request, CancellationToken cancellationToken)
        {
            var livros = await ObterRepositorio<ILivrosRepositorio>()
                .ConsultaComSpecification(new LivrosComDescricaoSpecification());

            if (!livros.Any())
                _mensageria.RetornarMensagemFalhaValidacao("Não foi possível localizar nenhum livro para a pesquisa indicada.");

            return livros;
        }
    }
}
