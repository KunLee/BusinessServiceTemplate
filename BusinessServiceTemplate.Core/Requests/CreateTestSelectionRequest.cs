using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class CreateTestSelectionRequest : IRequest<TestSelectionDto>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool? DescriptionVisibility { set; get; }
        public int SpecialityId { set; get; }
    }
}
