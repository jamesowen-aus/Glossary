using FluentValidation;
using Glossary.Web.Api.Modules.Glossary.Services;
using Glossary.Web.Api.Modules.Glossary.Dtos;
using Glossary.Web.Api.Modules.Glossary.Endpoints;
using Glossary.Web.Api.Modules.Glossary.Interfaces;

namespace Glossary.Web.Api.Modules.Glossary
{
    public static class GlossaryModule
    {
        public static IServiceCollection RegisterGlossaryModule(this IServiceCollection services)
        {
            // Register the services that are needed by the GlossaryModule
            services.AddScoped<IValidator<GlossaryItemDto>,GlossaryItemValidator>();
            services.AddScoped<IGlossaryService, GlossaryService>();

            return services;
        }

        public static IEndpointRouteBuilder MapGlossaryEndpoints(this IEndpointRouteBuilder endpoints)
        {
            //Map the Endpoint Implementation Classes to the App Route EndPoint
            endpoints.MapGet("/glossaryitems/{termId}", GetGlossaryItem.Handle)
                .WithOpenApi();
            
            endpoints.MapGet("/glossaryitems/", GetAllGlossaryItems.Handle)
                .WithOpenApi();

            endpoints.MapPost("/glossaryitems/", PostGlossaryItem.Handle)
                .WithOpenApi();

            endpoints.MapPut("/glossaryitems/{termId}", PutGlossaryItem.Handle)
                .WithOpenApi();

            endpoints.MapDelete("/glossaryitems/{termId}", DeleteGlossaryItem.Handle)
                .WithOpenApi();

            return endpoints;
        }
    }
}
