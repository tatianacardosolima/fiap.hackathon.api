﻿using System.ComponentModel.DataAnnotations;

namespace Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.Requests
{
    public class TokenServicoRequest
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        [Required(ErrorMessage = "O campo ClientId é obrigatório.")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Segredo do cliente utilizado para autenticação.
        /// </summary>
        [Required(ErrorMessage = "O campo ClientSecret é obrigatório.")]
        public string ClientSecret { get; set; } = string.Empty;
    }
}
