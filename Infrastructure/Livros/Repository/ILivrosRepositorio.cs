using MandradeFrameworks.Repositorios.Persistence.Sql.ContextRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Livros.Repository
{
    public interface ILivrosRepositorio : IContextRepository
    {
        Task<Guid> ObterIdLivroPorNome(string nome);
    }
}
