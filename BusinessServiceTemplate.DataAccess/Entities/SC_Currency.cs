using BusinessServiceTemplate.Shared.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Currency : DbEntity<int>
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { set; get; }
        public string? Country { set; get; }
        public string? Shortcode { set; get; }
        public string? Symbol { set; get; }
        public bool? Active { set; get; }
        public virtual List<SC_Panel> Panels { get; } = new();
    }
}
