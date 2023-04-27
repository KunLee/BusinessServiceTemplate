using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    public class CreateTestSelectionRequestModel
    {
        [Required]
        public string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }

        [Required]
        public int SpecialityId { set; get; }
    }
}
