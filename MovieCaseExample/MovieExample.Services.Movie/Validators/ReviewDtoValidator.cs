using FluentValidation;
using MovieExample.Services.Movie.Dtos;

namespace MovieExample.Services.Movie.Validators
{
    public class ReviewDtoValidator : AbstractValidator<ReviewDto>
    {
        public ReviewDtoValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 10).WithMessage("{PropertyName} must be between 1 and 10");
        }
    }
}
