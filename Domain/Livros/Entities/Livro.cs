using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Livros.Entities
{
    public class Livro : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
