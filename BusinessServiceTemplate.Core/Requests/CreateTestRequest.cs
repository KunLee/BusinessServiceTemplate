using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class CreateTestRequest : IRequest<TestDto>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }
        public List<int>? PanelIds { set; get; }
    }
}
