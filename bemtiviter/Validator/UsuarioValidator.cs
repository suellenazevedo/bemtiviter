using bemtiviter.DTOs;
using FluentValidation;

namespace bemtiviter.Validator
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .MaximumLength(255)
                .MinimumLength(3);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Foto)
                .MaximumLength(5000);

        }

    }
}
