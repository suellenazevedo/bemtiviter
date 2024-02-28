using bemtiviter.DTOs;
using FluentValidation;

namespace bemtiviter.Validator
{
    public class TemaValidator : AbstractValidator<TemaDTO>
    {

        public TemaValidator()
        {
            RuleFor(t => t.Descricao)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(257);
        }

    }
}
