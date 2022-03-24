using Domain.Livros.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Livros.Queries.ListarLivrosComDescricao
{
    public class ListarLivrosComDescricaoQuery : IRequest<IEnumerable<Livro>>
    {
    }
}
