using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetPanelTestByIdsRequest : IRequest<PanelTestDto>
    {
        public int PanelId { get; set; }
        public int TestId { get; set; }
    }
}
