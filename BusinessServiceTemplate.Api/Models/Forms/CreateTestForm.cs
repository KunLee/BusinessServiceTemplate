using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.Forms
{
    public class CreateTestForm
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }
        public List<int>? PanelIds { set; get; }
    }
}
