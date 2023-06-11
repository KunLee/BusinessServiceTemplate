using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Exceptions;
using BusinessServiceTemplate.Shared.Common;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class CreateCurrencyHandler : IRequestHandler<CreateCurrencyRequest, CurrencyDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public CreateCurrencyHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<CurrencyDto> Handle(CreateCurrencyRequest request, CancellationToken cancellationToken)
        {
            var currencyToAdd = await ValidateRequestData(request);

            var result = await _testSelectionRepositoryManager.ScCurrencyRepository.Create(currencyToAdd);

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<CurrencyDto>(result);
        }

        private async Task<SC_Currency> ValidateRequestData(CreateCurrencyRequest request)
        {
            // Validate Duplicate records
            var isDuplicate = await _testSelectionRepositoryManager.ScCurrencyRepository
                    .Any(x => x.Name.Equals(request.Name) &&
                                            x.Country == request.Country);
            if (isDuplicate) 
            {
                throw new ValidationException(ConstantStrings.DUPLICATE_REQUEST_DATA);
            }

            return new SC_Currency
            {
                Name = request.Name,
                Country = request.Country,
                Shortcode = request.Shortcode,
                Symbol = request.Symbol,
                Active = request.Active
            };
        }
    }
}
