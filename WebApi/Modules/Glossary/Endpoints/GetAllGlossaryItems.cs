using Glossary.Web.Api.Modules.Glossary.Helpers;
using Glossary.Web.Api.Modules.Glossary.Interfaces;

namespace Glossary.Web.Api.Modules.Glossary.Endpoints
{
    public static class GetAllGlossaryItems
    {
        public static async Task<IResult> Handle(IGlossaryService glossaryService, CancellationToken cancellationToken)
        {
            try
            {
                var terms = await glossaryService.GetAll(cancellationToken);
                return Results.Ok(GlossaryItemDtoX.ToDto(terms));
            } 
            catch (Exception)
            {
                return Results.Problem("Internal Application Error");
            }
        }
    }
}
