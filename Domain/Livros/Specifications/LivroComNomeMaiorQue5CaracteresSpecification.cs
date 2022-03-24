using Domain.Livros.Entities;
using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Livros.Specifications
{
    public class LivroComNomeMaiorQue5CaracteresSpecification : BaseSpecificationPaginated<Livro>
    {
        public LivroComNomeMaiorQue5CaracteresSpecification(int pagina, int quantidadeRegistros) 
        : base(x => x.Nome.Length > 5, pagina, quantidadeRegistros) { }
    }
}
