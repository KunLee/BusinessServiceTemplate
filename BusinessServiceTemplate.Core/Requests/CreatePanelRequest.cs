using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class CreatePanelRequest : IRequest<PanelDto>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }
        public decimal? Price { get; set; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; }
        public int? CurrencyId { get; set; }
        public List<int>? TestIds { set; get; }
        public bool? Visibility { set; get; }
    }
}
