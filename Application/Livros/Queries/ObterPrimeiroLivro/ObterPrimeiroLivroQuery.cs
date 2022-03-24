using Domain.Livros.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Livros.Queries.ObterPrimeiroLivro
{
    public class ObterPrimeiroLivroQuery : IRequest<Livro>
    {
    }
}
