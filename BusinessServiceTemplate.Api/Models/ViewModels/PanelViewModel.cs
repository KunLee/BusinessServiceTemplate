using BusinessServiceTemplate.Core.Dtos;

namespace BusinessServiceTemplate.Api.Models.ViewModels
{
    public class PanelViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string? Description { set; get; }
        public decimal? Price { set; get; }
        public List<int> Tests { get; } = new();
    }
}
