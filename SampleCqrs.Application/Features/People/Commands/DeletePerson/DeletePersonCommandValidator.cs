using FluentValidation;

namespace SampleCqrs.Application.Features.People.Commands.DeletePerson
{
    public sealed class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
    {
        public DeletePersonCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().Length(36).WithMessage("Invalid person id");
        }
    }
}
