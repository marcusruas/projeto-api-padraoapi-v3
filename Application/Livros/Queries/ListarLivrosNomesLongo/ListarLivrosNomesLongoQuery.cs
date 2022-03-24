using Domain.Livros.Entities;
using MandradeFrameworks.SharedKernel.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Livros.Queries.ListarLivrosNomesLongo
{
    public class ListarLivrosNomesLongoQuery : IRequest<ListaPaginada<Livro>>
    {
        public ListarLivrosNomesLongoQuery(int pagina, int quantidadeRegistros)
        {
            Pagina = pagina;
            QuantidadeRegistros = quantidadeRegistros;
        }

        public int Pagina { get; set; }
        public int QuantidadeRegistros { get; set; }
    }
}
