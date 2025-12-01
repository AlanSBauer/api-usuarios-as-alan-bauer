using Application.Dtos;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateDtoValidator(IUSuarioRepository repository)
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, ct) =>
                {
                    var emailVerificacao = await repository.EmailExistsAsync(email.ToLower(), ct);
                    return !emailVerificacao;
                })
                .WithMessage("Email já cadastrado.");

            RuleFor(u => u.Senha)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(u => u.DataNascimento)
                .NotEmpty()
                .Must(data =>
                {
                    int idade = DateTime.Today.Year - data.Year;
                    if (data.Date > DateTime.Today.AddYears(-idade))
                        idade--;
                    return idade >= 18;
                })
                .WithMessage("Usuário deve ter pelo menos 18 anos.");

            RuleFor(u => u.Telefone)
                .Matches(@"^(\+55\s?)?(\(?\d{2}\)?\s?)?9?\d{4}-?\s?\d{4}$")
                .WithMessage("Utilize o formato correto. Como: (00) 12345-6789")
                .When(u => !string.IsNullOrWhiteSpace(u.Telefone))
                .WithMessage("Telefone inválido.");
        }
    }
}
