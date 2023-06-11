using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetCurrencyRequest : IRequest<CurrencyDto>
    {
        public GetCurrencyRequest(int Id)
        {
            this.Id = Id;
        }
        public int Id { get; set; }
    }
}
