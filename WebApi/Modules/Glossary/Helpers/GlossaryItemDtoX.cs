using Glossary.Web.Api.Modules.Glossary.Dtos;
using Glossary.Web.Api.Modules.Glossary.Model;

namespace Glossary.Web.Api.Modules.Glossary.Helpers
{
    public static class GlossaryItemDtoX
    {
        public static Term FromDto(GlossaryItemDto dto, Term? term = null)
        {
            term = term ?? new Term();
            term.Id = dto.Id;
            term.Name = dto.Term;

            term.Definition = term.Definition ?? new Definition();
            term.Definition.Description = dto.Definition;
            return term;
        }

        public static GlossaryItemDto ToDto(Term term)
        {
            return new GlossaryItemDto()
            {
                Id = term.Id,
                Term = term.Name,
                Definition = term.Definition?.Description
            };
        }

        public static IEnumerable<GlossaryItemDto> ToDto(IEnumerable<Term> terms)
        {
            return terms.Select(t => ToDto(t)).ToList();
        }
    }
}
