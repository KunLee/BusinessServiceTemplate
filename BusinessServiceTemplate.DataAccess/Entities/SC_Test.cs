using BusinessServiceTemplate.Shared.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Test : DbEntity<int>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { set; get; }
        public string? Description { set; get; }
        public List<SC_Panel> Panels { set; get; } = new();
    }
}
