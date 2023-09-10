using Glossary.Web.Api.Modules.Glossary.Interfaces;
using Glossary.Web.Api.Modules.Glossary.Services;

namespace Glossary.Web.Api.Modules.Glossary.Endpoints
{
    public class DeleteGlossaryItem
    {
        public static async Task<IResult> Handle(int termId, IGlossaryService glossaryService)
        {
            try
            {
                var existingTerm = await glossaryService.GetById(termId);

                if (existingTerm != null)
                {
                    await glossaryService.DeleteById(termId);
                    return Results.Ok("Deleted");
                    
                }

                return Results.NotFound($"Not Found: TermId {termId}");

            } catch (Exception)
            {
                //TODO: Implement Logging
                return Results.Problem("Internal Application Error");
            }
        }
    }
}
