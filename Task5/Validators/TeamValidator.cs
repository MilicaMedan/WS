using FluentValidation;
using Task5.Models;

namespace Task5.Validators
{
    public class TeamValidator : AbstractValidator<Team>
    {
        public TeamValidator() {
            RuleFor(t => t.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} can not be empty!")
                .Length(1, 25);
        }
    }
}
