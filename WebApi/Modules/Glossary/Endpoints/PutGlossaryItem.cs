using FluentValidation;
using Glossary.Web.Api.Modules.Glossary.Dtos;
using Glossary.Web.Api.Modules.Glossary.Helpers;
using Glossary.Web.Api.Modules.Glossary.Interfaces;
using Glossary.Web.Api.Modules.Glossary.Services;

namespace Glossary.Web.Api.Modules.Glossary.Endpoints
{
    public class PutGlossaryItem
    {
        public static async Task<IResult> Handle(int termId, GlossaryItemDto item, IGlossaryService glossaryService, IValidator<GlossaryItemDto> validator, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(item);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            try
            {
                var existingTerm = await glossaryService.GetById(termId);

                if (existingTerm != null)
                {
                    var recordsAffected = await glossaryService.Update(GlossaryItemDtoX.FromDto(item, existingTerm));
                    return Results.Ok("Updated");
                    
                }

                return Results.NotFound(termId);

            } catch (Exception)
            {
                return Results.Problem("Internal Application Error");
            }
           
        }
    }
}
