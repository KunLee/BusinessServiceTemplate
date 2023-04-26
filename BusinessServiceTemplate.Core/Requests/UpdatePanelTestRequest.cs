using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class UpdatePanelTestRequest : IRequest<PanelTestDto>
    {
        public int PanelId { get; set; }
        public int TestId { get; set; }
        public bool Visibility { get; set; }
    }
}
