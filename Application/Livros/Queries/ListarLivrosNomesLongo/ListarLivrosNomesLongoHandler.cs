using Domain.Livros.Entities;
using Domain.Livros.Specifications;
using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Livros.Queries.ListarLivrosNomesLongo
{
    public class ListarLivrosNomesLongoHandler 
    : ApplicationRequestHandler<ListarLivrosNomesLongoQuery, ListaPaginada<Livro>>
    {
        public ListarLivrosNomesLongoHandler(IServiceProvider services) : base(services) { }

        public override async Task<ListaPaginada<Livro>> Handle(ListarLivrosNomesLongoQuery request, CancellationToken cancellationToken)
        => await ObterRepositorio<ILivrosRepositorio>()
            .ConsultaComSpecification(new LivroComNomeMaiorQue5CaracteresSpecification(request.Pagina, request.QuantidadeRegistros));
    }
}
