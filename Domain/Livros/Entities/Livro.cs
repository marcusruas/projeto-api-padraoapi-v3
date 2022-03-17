using MandradeFrameworks.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Livros.Entities
{
    public class Livro : Entity
    {
        public Livro() : base() { }
        public Livro(string nome, string descricao) : base()
        {
            Nome = nome;
            Descricao = descricao;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
