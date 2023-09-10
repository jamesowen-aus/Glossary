using FluentValidation;

namespace Glossary.Web.Api.Modules.Glossary.Dtos
{

    public class GlossaryItemDto
    {
        public int Id { get; set; }

        public string? Term { get; set; }

        public string? Definition { get; set; }
    }

    public class GlossaryItemValidator : AbstractValidator<GlossaryItemDto>
    {
        public GlossaryItemValidator()
        {
            RuleFor(x => x.Term)
                .NotEmpty().WithMessage("A Term must be provided.")
                .MaximumLength(100);

            RuleFor(x => x.Definition)
                .NotEmpty().WithMessage("A Definition must be provided.")
                .MaximumLength(1000);
        }
    }
}
