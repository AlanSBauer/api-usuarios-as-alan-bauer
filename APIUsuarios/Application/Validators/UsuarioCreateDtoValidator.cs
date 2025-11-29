using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100)
                .WithMessage("O nome deve ter no máximo 100 caracteres");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório.")
                .EmailAddress()
                .WithMessage("Email inválido.");

            RuleFor(u => u.Senha)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .MinimumLength(6)
                .WithMessage("A senha deve ter no mínimo 6 caracteres.");

            RuleFor(u => u.DataNascimento)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória.");

            RuleFor(u => u.Telefone)
                .Matches(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?9?\d{4}-?\s?\d{4}$")
                .When(u => !string.IsNullOrWhiteSpace(u.Telefone))
                .WithMessage("Telefone inválido. Use um formato brasileiro válido como (00) 12345-6789.");
        }
    }
}