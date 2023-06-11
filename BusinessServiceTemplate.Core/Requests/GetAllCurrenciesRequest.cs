using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetAllCurrenciesRequest : IRequest<IList<CurrencyDto>>
    {
    }
}
