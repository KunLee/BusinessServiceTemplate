using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetAllPanelsRequest : IRequest<IList<PanelDto>>
    {
    }
}
