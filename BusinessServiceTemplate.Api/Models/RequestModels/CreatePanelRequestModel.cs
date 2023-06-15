using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    public class CreatePanelRequestModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; }
        public int? CurrencyId { get; set; }
        public List<int>? TestIds { set; get; }
        public bool? Visibility { set; get; }
    }
}
