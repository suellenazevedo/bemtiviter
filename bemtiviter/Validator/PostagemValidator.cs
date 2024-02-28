using bemtiviter.DTOs;
using FluentValidation;

namespace bemtiviter.Validator
{
    public class PostagemValidator : AbstractValidator<PostagemDTO>
    {
        public PostagemValidator() 
        {
            RuleFor(p => p.Titulo)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(p => p.Texto)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(280);

            RuleFor(p => p.TemaId)
                .NotEmpty();

            RuleFor(p => p.UsuarioId)
                .NotEmpty();


        }
    }
}
