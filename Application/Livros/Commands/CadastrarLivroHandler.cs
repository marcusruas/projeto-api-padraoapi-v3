using Domain.Livros.Entities;
using Domain.Livros.Specifications;
using Infrastructure.Livros.Repository;
using MandradeFrameworks.Retornos.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Livros.Commands
{
    public class CadastrarLivroHandler : ApplicationRequestHandler<CadastrarLivroCommand, bool>
    {
        public CadastrarLivroHandler(IServiceProvider services) : base(services) { }

        bool _sucessoOperacao;

        public override async Task<bool> Handle(CadastrarLivroCommand request, CancellationToken cancellationToken)
        {
            await ValidarLivroExistente(request);
            await CadastrarNovoLivro(request);

            return _sucessoOperacao;
        }

        async Task ValidarLivroExistente(CadastrarLivroCommand request)
        {
            var livroExistente = await ObterRepositorio<ILivrosRepositorio>()
                .ConsultaComSpecification(new LivroPorNomeSpecification(request.Nome));

            if (livroExistente.Any())
                _mensageria.RetornarMensagemFalhaValidacao("Livro informado já existe. Não é possível prosseguir com o cadastro.");
        }

        async Task CadastrarNovoLivro(CadastrarLivroCommand request)
        {
            var livroNovo = new Livro(request.Nome, request.Descricao);
            var linhasAfetadas = await ObterRepositorio<ILivrosRepositorio>().AdicionarEntidade(livroNovo);

            _sucessoOperacao = linhasAfetadas > 0;

            if (_sucessoOperacao)
                _mensageria.AdicionarMensagemInformativa("Livro cadastrado com sucesso.");
        }
    }
}
