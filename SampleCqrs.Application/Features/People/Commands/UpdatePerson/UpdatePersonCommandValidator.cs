using FluentValidation;

namespace SampleCqrs.Application.Features.People.Commands.UpdatePerson
{
    public sealed class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            //
            RuleFor(x => x.Id).NotEmpty().Length(36).WithMessage("Invalid person Id");
            //
            RuleFor(x => x.RowVersion).NotEmpty().Length(36).WithMessage("Invalid data version");
            //
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
