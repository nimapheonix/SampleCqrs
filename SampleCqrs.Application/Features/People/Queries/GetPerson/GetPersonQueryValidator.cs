using FluentValidation;

namespace SampleCqrs.Application.Features.People.Queries.GetPerson
{
    public sealed class GetPersonQueryValidator : AbstractValidator<GetPersonQuery>
    {
        public GetPersonQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().Length(36).WithMessage("Invalid person id");
        }
    }
}
