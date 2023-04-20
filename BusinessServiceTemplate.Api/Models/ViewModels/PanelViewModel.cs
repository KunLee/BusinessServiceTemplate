using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.ViewModels
{
    public class PanelViewModel
    {
        public int Id { set;  get; }
        public string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; }
        public List<int> Tests { set; get; } = new();
    }
}
