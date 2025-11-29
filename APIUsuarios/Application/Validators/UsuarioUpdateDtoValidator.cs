using System.Data;
using Application.Dtos;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
    {
        public UsuarioUpdateDtoValidator(IUSuarioRepository repository)
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve ter no minimo 3 caracteres.")
                .MaximumLength(100)
                .WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.");

            RuleFor(u => u.Telefone)
                .Matches(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?9?\d{4}-?\s?\d{4}$")
                .When(u => !string.IsNullOrWhiteSpace(u.Telefone));

            RuleFor(u => u)
                .CustomAsync(async (dto, context, ct) =>
                {
                    var id = (int)context.RootContextData["Id"];

                    var user = await repository.GetByEmailAsync(dto.Email, ct);

                    if(user != null && user.Id != id)
                    {
                        context.AddFailure("Email", "Email já está sendo utilizado por outro usuário.");
                    }
                });
        }
    
    }
}