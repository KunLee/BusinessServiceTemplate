using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetPanelRequest : IRequest<PanelDto>
    {
        public GetPanelRequest(int Id)
        {
            this.Id = Id;
        }
        public int Id { get; set; }
    }
}
