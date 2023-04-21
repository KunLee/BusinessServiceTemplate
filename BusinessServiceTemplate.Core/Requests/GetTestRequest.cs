using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetTestRequest : IRequest<TestDto>
    {
        public GetTestRequest(int Id)
        {
            this.Id = Id;
        }
        public int Id { get; set; }
    }
}
