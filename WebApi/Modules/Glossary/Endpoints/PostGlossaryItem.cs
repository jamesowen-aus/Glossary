using FluentValidation;
using Glossary.Web.Api.Modules.Glossary.Dtos;
using Glossary.Web.Api.Modules.Glossary.Helpers;
using Glossary.Web.Api.Modules.Glossary.Interfaces;

namespace Glossary.Web.Api.Modules.Glossary.Endpoints
{
    public static class PostGlossaryItem
    {
        public static async Task<IResult> Handle(GlossaryItemDto item, IGlossaryService glossaryService, IValidator<GlossaryItemDto> validator)
        {
            var validationResult = await validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            try
            {
                var newTerm = GlossaryItemDtoX.FromDto(item);
                var newId = await glossaryService.Insert(newTerm);
                return Results.Created($"/glossaryitems/{newId}", GlossaryItemDtoX.ToDto(newTerm));

            } 
            catch (Exception)
            {
                return Results.Problem("Internal Application Error");
            }
            
        }
    }
}
