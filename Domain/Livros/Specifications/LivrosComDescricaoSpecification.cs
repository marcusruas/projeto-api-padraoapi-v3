using Domain.Livros.Entities;
using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Livros.Specifications
{
    public class LivrosComDescricaoSpecification : BaseSpecification<Livro>
    {
        public LivrosComDescricaoSpecification() : base(x => !string.IsNullOrWhiteSpace(x.Descricao)) { }
    }
}
