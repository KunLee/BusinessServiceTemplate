using BusinessServiceTemplate.Shared.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Panel : DbEntity<int>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { set; get; }
        public string? Description { set; get; }
        public decimal? Price { set; get; }
        public List<SC_Test> Tests { set; get; } = new();
    }
}
