using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    public class CreateTestRequestModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }
        public List<int>? PanelIds { set; get; }
    }
}
