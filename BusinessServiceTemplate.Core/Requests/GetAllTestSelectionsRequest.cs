using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetAllTestSelectionsRequest : IRequest<IList<TestSelectionDto>>
    {
    }
}
