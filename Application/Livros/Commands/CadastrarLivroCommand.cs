using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Livros.Commands
{
    public class CadastrarLivroCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo para o nome é 100 caractéres")]
        public string Nome { get; set; }
        [MaxLength(500, ErrorMessage = "Tamanho máximo para a descrição é 100 caractéres")]
        public string Descricao { get; set; }
    }
}
