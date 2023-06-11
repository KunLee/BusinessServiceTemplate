using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.Shared.Common;
using BusinessServiceTemplate.Shared.Exceptions;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class DeleteCurrencyHandler : IRequestHandler<DeleteCurrencyRequest, CurrencyDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public DeleteCurrencyHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<CurrencyDto> Handle(DeleteCurrencyRequest request, CancellationToken cancellationToken)
        {
            var currency = await _testSelectionRepositoryManager.ScCurrencyRepository.Find(request.Id);

            if (currency != null)
            {
                await _testSelectionRepositoryManager.ScCurrencyRepository.Delete(currency);
                await _testSelectionRepositoryManager.Save();
            }
            else 
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }
            
            return _mapper.Map<CurrencyDto>(currency);
        }
    }
}
