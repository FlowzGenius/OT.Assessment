using FluentValidation;

namespace OT.Assessment.Core.Commands.ProcessWager;

public class ProcessWagerCommandValidator : AbstractValidator<ProcessWagerCommand>
{
    public ProcessWagerCommandValidator()
    {
        RuleFor(x => x.WagerId).NotEmpty();
        RuleFor(x => x.Theme).NotEmpty();
        RuleFor(x => x.Provider).NotEmpty();
        RuleFor(x => x.GameName).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.CreationDate).NotEmpty();
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
    }
}

