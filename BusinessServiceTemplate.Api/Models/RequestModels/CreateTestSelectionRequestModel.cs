using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    public class CreateTestSelectionRequestModel
    {
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public int SpecialityId { set; get; }
    }
}
