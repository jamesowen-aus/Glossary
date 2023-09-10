using Glossary.Web.Api.Modules.Glossary.Model;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Web.Api.Modules.Glossary.Services
{
    public class GlossaryContext : DbContext
    {
        public DbSet<Term> Terms { get; set;}
        public DbSet<Definition> Definitions { get; set; }

        private string _connectionString;
        
        public GlossaryContext() : base() 
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

#pragma warning disable CS8601 // Possible null reference assignment.
            _connectionString = configuration.GetConnectionString("WebApiDatabase");
#pragma warning restore CS8601 // Possible null reference assignment.

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ApplicationException("Connection String not found.");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

    }
}
