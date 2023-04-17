using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class CreateTestRequest : IRequest<TestDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<int>? PanelIds { set; get; }
    }
}
