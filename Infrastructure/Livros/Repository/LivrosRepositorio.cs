using Infrastructure.DBContexts;
using MandradeFrameworks.Repositorios.Persistence.Sql.ContextRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Livros.Repository
{
    public class LivrosRepositorio : StandardSqlRepository<LivrosContext>, ILivrosRepositorio
    {
        public LivrosRepositorio(IServiceProvider provider, LivrosContext context) 
        : base(provider, context) { }
    }
}
