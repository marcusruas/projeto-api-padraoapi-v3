using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Livros.Queries.ObterIdLivroPorNome
{
    public class ObterIdLivroPorNomeQuery : IRequest<Guid>
    {
        public ObterIdLivroPorNomeQuery(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
