using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using BusinessServiceTemplate.Shared.Exceptions;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class UpdateCurrencyHandler : IRequestHandler<UpdateCurrencyRequest, CurrencyDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public UpdateCurrencyHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<CurrencyDto> Handle(UpdateCurrencyRequest request, CancellationToken cancellationToken)
        {
            var recordToUpdate = await ValidateRequestData(request);
            await _testSelectionRepositoryManager.ScCurrencyRepository.Update(recordToUpdate);
            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<CurrencyDto>(recordToUpdate);
        }

        private async Task<SC_Currency> ValidateRequestData(UpdateCurrencyRequest request)
        {
            var recordFound = await _testSelectionRepositoryManager.ScCurrencyRepository.Find(request.Id);

            if (recordFound == null)
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            recordFound.Name = request.Name;
            recordFound.Shortcode = request.Shortcode;
            recordFound.Country = request.Country;
            recordFound.Active = request.Active;
            recordFound.Symbol = request.Symbol;

            return recordFound;
        }
    }
}
