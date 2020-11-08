using FluentValidation;
using Task5.Models;

namespace Task5.Validators
{
    public class PlayerValidator : AbstractValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} can not be empty!")
                .Length(1, 25);

            RuleFor(p => p.TeamId).NotNull().WithMessage("{PropertyName} can not be null!");

        }
    }
}
