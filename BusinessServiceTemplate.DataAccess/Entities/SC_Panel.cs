using BusinessServiceTemplate.Shared.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Panel : DbEntity<int>
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; } // Required foreign key property for reference navigation

        [Required(ErrorMessage = "TestSelection is required.")]
        public SC_TestSelection TestSelection { get; set; } = null!; // Required reference navigation to principal
        public virtual List<SC_Test> Tests { set; get; } = new();
        public bool? Visibility { set; get; }
    }
}
