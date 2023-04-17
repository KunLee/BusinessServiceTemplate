using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.Forms
{
    public class CreatePanelForm
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { set; get; }
        public List<int>? TestIds { set; get; }
    }
}
