using FluentValidation; 
using Fiap.Hackathon.Medicos.Domain.Entities;
using Fiap.Hackathon.Medicos.Domain.Extensions;

namespace Fiap.Hackathon.Medicos.Application.Validators
{
   public class MedicoValidator : AbstractValidator<Medico> 
    {

        public MedicoValidator (bool needValidate)
        {
           RuleFor(x => x.Nome)
                .NotNull()
                   .WithMessage("O campo Nome é obrigatório.")
                .NotEmpty()
                   .WithMessage("O campo Nome é obrigatório.")
               .MaximumLength(150)
                   .WithMessage("O campo Nome permite até 150 caracteres.");

           RuleFor(x => x.CRM)
                .NotNull()
                   .WithMessage("O campo CRM é obrigatório.")
                .NotEmpty()
                   .WithMessage("O campo CRM é obrigatório.")
               .MaximumLength(20)
                   .WithMessage("O campo CRM permite até 20 caracteres.")
               .Must(crm => crm.EhCrmValido()).WithMessage("CRM inválido.");

            RuleFor(x => x.Email)
                .NotNull()
                   .WithMessage("O campo Email é obrigatório.")
                .NotEmpty()
                   .WithMessage("O campo Email é obrigatório.")
               .MaximumLength(90)
                   .WithMessage("O campo Email permite até 90 caracteres.")
               .Must(email=> email.EhEmailValido()).WithMessage("E-mail inválido."); ;
            if (needValidate)
            { 
                RuleFor(x => x.Senha)
                .NotNull()
                   .WithMessage("O campo Senha é obrigatório.")
                .NotEmpty()
                   .WithMessage("O campo Senha é obrigatório.")
               .MaximumLength(128)
                   .WithMessage("O campo Senha permite até 128 caracteres.");
            
            
                RuleFor(x => x.CPF)
                     .NotNull()
                        .WithMessage("O campo CPF é obrigatório.")
                     .NotEmpty()
                        .WithMessage("O campo CPF é obrigatório.")
                    .MaximumLength(11)
                        .WithMessage("O campo CPF permite até 11 caracteres.")
                    .Must(cpf => cpf.EhCpfValido()).WithMessage("CPF inválido.");
            }
            RuleFor(x => x.Especialidade)
                .NotNull()
                   .WithMessage("O campo Especialidade é obrigatório.")
                .NotEmpty()
                   .WithMessage("O campo Especialidade é obrigatório.")
               .MaximumLength(100)
                   .WithMessage("O campo Especialidade permite até 100 caracteres.");


        }

    }
}

