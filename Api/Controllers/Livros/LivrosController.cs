using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Livros.Commands;
using Application.Livros.Queries.ListarLivrosComDescricao;
using Application.Livros.Queries.ObterPrimeiroLivro;
using Application.Livros.Queries.ObterTitulosLivros;
using Domain.Livros.Entities;
using MandradeFrameworks.Autenticacao.Filters;
using MandradeFrameworks.Retornos.Controllers;
using MandradeFrameworks.Retornos.Models;
using MandradeFrameworks.SharedKernel.Usuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Livros
{
    [Route("livros/")]
    public class LivrosController : StandardController
    {
        public LivrosController(IMediator mediador) : base(mediador) { }

        // M�todo apenas para exemplificar um simples endpoint
        [HttpGet]
        [Route("titulos")]
        public async Task<RetornoApi<IEnumerable<string>>> ObterTitulosLivros() =>
            await ProcessarSolicitacao(new ObterTitulosLivrosQuery());

        // M�todo que usa uma classe herdada do SqlRepositorio (via DI)
        [HttpGet]
        [Route("primeiro-livro")]
        public async Task<RetornoApi<Livro>> ObterPrimeiroLivro() =>
            await ProcessarSolicitacao(new ObterPrimeiroLivroQuery());

        // M�todo que usa uma classe herdada do SqlRepositorio (via DI) utilizando 
        // a consulta com specification pattern
        [HttpGet]
        [Route("livros-com-descricao")]
        public async Task<RetornoApi<IEnumerable<Livro>>> ListarLivrosComDescricao() =>
            await ProcessarSolicitacao(new ListarLivrosComDescricaoQuery());

        // Cadastro b�sico de um livro, com valida��es e uso de mensageria
        [HttpPost]
        [EndpointRestrito]
        public async Task<RetornoApi<bool>> CadastrarLivro([FromBody] CadastrarLivroCommand command) =>
            await ProcessarSolicitacao(command);
    }
}