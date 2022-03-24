using Domain.Livros.Entities;
using MandradeFrameworks.Repositorios.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using MandradeFrameworks.Repositorios.Configuration;
using System.Text;

namespace Infrastructure.DBContexts
{
    public class LivrosContext : StandardContext<LivrosContext>
    {
        public LivrosContext(DbContextOptions<LivrosContext> options) : base(options) { }

        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.AplicarModelBuilders();
    }
}
