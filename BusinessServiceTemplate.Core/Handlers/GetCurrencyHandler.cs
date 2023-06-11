using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetCurrencyHandler : IRequestHandler<GetCurrencyRequest, CurrencyDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetCurrencyHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<CurrencyDto> Handle(GetCurrencyRequest request, CancellationToken cancellationToken)
        {
            var currency = await _testSelectionRepositoryManager.ScCurrencyRepository.Find(request.Id);
            return _mapper.Map<CurrencyDto>(currency);
        }
    }
}
