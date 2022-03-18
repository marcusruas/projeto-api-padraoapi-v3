using Dapper;
using Infrastructure.DBContexts;
using MandradeFrameworks.Mensagens;
using MandradeFrameworks.Repositorios.Persistence;
using MandradeFrameworks.Repositorios.Persistence.Sql.ContextRepository;
using MandradeFrameworks.SharedKernel.Extensions;
using MandradeFrameworks.SharedKernel.Usuario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Livros.Repository
{
    public class LivrosRepositorio : StandardSqlRepository<LivrosContext>, ILivrosRepositorio
    {
        public LivrosRepositorio(IServiceProvider provider, LivrosContext context)
        : base(provider, context) { }

        public async Task<Guid> ObterIdLivroPorNome(string nome)
        {

            Guid idRetorno = Guid.Empty;
            string querySql = ObterConteudoArquivoSql("selectIdPorNome");

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@NOME", nome);


            await ExecutarComandoSql(
                async conexao => idRetorno = await conexao.QueryFirstOrDefaultAsync<Guid>(querySql, parametros), 
                "Livros"
            );

            return idRetorno;
        }
    }
}
