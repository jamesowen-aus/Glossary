using Glossary.Web.Api.Modules.Glossary;
using Glossary.Web.Api.Modules.Glossary.Services;
using Glossary.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GlossaryContext>();
builder.Services.RegisterGlossaryModule();

var app = builder.Build();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
}


app.MapGlossaryEndpoints();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseStatusCodePages();

app.MapFallbackToFile("index.html");

app.Run();
