using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    public class CreatePanelRequestModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }

        [Required]
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }

        [Required]
        public int TestSelectionId { get; set; }
        public List<int>? TestIds { set; get; }
        public bool? Visibility { set; get; }
    }
}
