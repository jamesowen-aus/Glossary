using System.ComponentModel.DataAnnotations;

namespace Glossary.Web.Api.Modules.Glossary.Model
{
    public class Definition
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Description { get; set; }

        public int TermId { get; set; }
        
    }
}
