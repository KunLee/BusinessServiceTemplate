using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class UpdatePanelTestRequest : IRequest
    {
        public int PanelId { get; set; }
        public int TestId { get; set; }
        public bool Visibility { get; set; }
    }
}
