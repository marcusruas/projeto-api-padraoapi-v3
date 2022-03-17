using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MandradeFrameworks.Retornos.Handlers;

namespace Application.Livros.Queries.ObterTitulosLivros
{
    public class ObterTitulosLivrosHandler : ApplicationRequestHandler<ObterTitulosLivrosQuery, IEnumerable<string>>
    {
        public ObterTitulosLivrosHandler(IServiceProvider services) : base(services) { }

        public override Task<IEnumerable<string>> Handle(ObterTitulosLivrosQuery request, CancellationToken cancellationToken)
        {
            var titulos = new List<string> { "Cujo", "IT - A coisa", "Carrie a estranha", "Mr. Mercedes", "O Iluminado" };

            return Task.FromResult<IEnumerable<string>>(titulos);
        }
    }
}