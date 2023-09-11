using Glossary.Web.Api.Modules.Glossary.Helpers;
using Glossary.Web.Api.Modules.Glossary.Interfaces;

namespace Glossary.Web.Api.Modules.Glossary.Endpoints
{
    public static class GetGlossaryItem
    {
        public static async Task<IResult> Handle(int termId, IGlossaryService glossaryService)
        {
            try
            {
                var term = await glossaryService.GetById(termId);
                if (term != null)
                {
                    return Results.Ok(GlossaryItemDtoX.ToDto(term));
                }

                return Results.NotFound($"Not Found: termId:{termId}");
                
            } 
            catch (Exception)
            {
                return Results.Problem("Internal Application Error");
            }
        }
    }
}
