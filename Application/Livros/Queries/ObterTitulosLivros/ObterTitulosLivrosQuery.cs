using System.Collections.Generic;
using MediatR;

namespace Application.Livros.Queries.ObterTitulosLivros
{
    public class ObterTitulosLivrosQuery : IRequest<IEnumerable<string>>
    { }
}