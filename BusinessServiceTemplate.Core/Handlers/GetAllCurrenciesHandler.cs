using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetAllCurrenciesHandler : IRequestHandler<GetAllCurrenciesRequest, IList<CurrencyDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllCurrenciesHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<IList<CurrencyDto>> Handle(GetAllCurrenciesRequest request, CancellationToken cancellationToken)
        {
            var currencyList = await _testSelectionRepositoryManager.ScCurrencyRepository.FindAll();

            return currencyList.Select(_mapper.Map<CurrencyDto>).ToList();
        }
    }
}
