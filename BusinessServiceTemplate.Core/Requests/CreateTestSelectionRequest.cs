using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class CreateTestSelectionRequest : IRequest<TestSelectionDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SpecialityId { set; get; }
    }
}
