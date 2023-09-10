using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Glossary.Web.Api.Modules.Glossary.Model
{
    public class Term
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public Definition? Definition { get; set; }
       
    }


}
