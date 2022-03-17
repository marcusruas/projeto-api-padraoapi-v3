using Domain.Livros.Entities;
using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Livros.Specifications
{
    public class LivroPorNomeSpecification : BaseSpecification<Livro>
    {
        public LivroPorNomeSpecification(string nome) : base(x => x.Nome == nome) { }
    }
}
