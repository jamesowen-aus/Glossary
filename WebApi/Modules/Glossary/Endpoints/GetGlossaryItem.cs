using Glossary.Web.Api.Modules.Glossary.Helpers;
using Glossary.Web.Api.Modules.Glossary.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Glossary.Web.Api.Modules.Glossary.Endpoints
{
    public static class GetGlossaryItem
    {
        public static async Task<IResult> Handle(int termId, IGlossaryService glossaryRepository)
        {
            try
            {
                var term = await glossaryRepository.GetById(termId);
                if (term != null)
                {
                    return Results.Ok(GlossaryItemDtoX.ToDto(term));
                }

                return Results.NotFound($"Not Found: termId:{termId}");
                
            } catch (Exception)
            {
                //TODO: Implement Logging
                return Results.Problem("Internal Application Error");
            }
        }
    }
}
