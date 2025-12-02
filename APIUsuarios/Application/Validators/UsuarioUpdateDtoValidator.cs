using FluentValidation;
using Application.Dtos;
using Application.Interfaces;

public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateDtoValidator(IUSuarioRepository repository)
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (dto, email, context, ct) =>
            {
                var idRota = (int)context.RootContextData["Id"];

                var existente = await repository.GetByEmailAsync(email, ct);

                if (existente == null) return true;

                return existente.Id == idRota;
            })
            .WithMessage("Email já cadastrado.");

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
            .When(u => !string.IsNullOrWhiteSpace(u.Telefone));
    }
}
