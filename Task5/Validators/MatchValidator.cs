using FluentValidation;
using System;
using Task5.Models;

namespace Task5.Validators
{
    public class MatchValidator : AbstractValidator<Match>
    {
        public MatchValidator()
        {

            RuleFor(m => m.GuestTeamId).NotNull().WithMessage("{PropertyName} can not be null!");
            RuleFor(m => m.HostTeamId).NotNull().WithMessage("{PropertyName} can not be null!");
            RuleFor(m => m.Time).NotNull().WithMessage("{PropertyName} can not be null!")
                .Must(IsInFuture).WithMessage("{PropertyName} must be in future!");
            RuleFor(m => m.StatussId).NotNull().WithMessage("{PropertyName} can not be null!");
            RuleFor(m => m).Must(IsValidTeams).WithMessage("Guest team and host team can not be same team!");

        }

        private bool IsValidTeams(Match m)
        {
            if (m.GuestTeamId == m.HostTeamId) {
                return false;
            }
            return true;
        }

        private bool IsInFuture(DateTime time)
        {
            if (time < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
