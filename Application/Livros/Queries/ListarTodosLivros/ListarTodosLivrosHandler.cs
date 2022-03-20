using Domain.Livros.Entities;
using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Livros.Queries.ListarTodosLivros
{
    public class ListarTodosLivrosHandler : ApplicationRequestHandler<ListarTodosLivrosQuery, ListaPaginada<Livro>>
    {
        public ListarTodosLivrosHandler(IServiceProvider services) : base(services) { }

        public override async Task<ListaPaginada<Livro>> Handle(ListarTodosLivrosQuery request, CancellationToken cancellationToken)
            => await ObterRepositorio<ILivrosRepositorio>()
                .ToListAsync<Livro>(request.Pagina, request.QuantidadeRegistros);
    }
}
