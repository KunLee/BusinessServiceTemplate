using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetTestSelectionRequest : IRequest<TestSelectionDto>
    {
        public int Id { get; set; }
    }
}
