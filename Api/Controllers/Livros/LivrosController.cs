using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Livros.Queries.ObterPrimeiroLivro;
using Application.Livros.Queries.ObterTitulosLivros;
using Domain.Livros.Entities;
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

        // Método apenas para exemplificar um simples endpoint
        [HttpGet]
        [Route("titulos")]
        public async Task<RetornoApi<IEnumerable<string>>> ObterTitulosLivros() =>
            await ProcessarSolicitacao(new ObterTitulosLivrosQuery());

        // Método que usa uma classe herdada do SqlRepositorio (via DI)
        [HttpGet]
        [Route("primeiro-livro")]
        public async Task<RetornoApi<Livro>> ObterPrimeiroLivro() =>
            await ProcessarSolicitacao(new ObterPrimeiroLivroQuery());
    }
}