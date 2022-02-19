using FluentValidation;

namespace SampleCqrs.Application.Features.People.Commands.CreatePerson
{
    public sealed class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Minimum length for the first name is 2");
            //
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Minimum length for the last name is 2");
            //
        }
    }
}
