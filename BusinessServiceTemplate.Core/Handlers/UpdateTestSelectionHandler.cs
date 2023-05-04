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
    public class UpdateTestSelectionHandler : IRequestHandler<UpdateTestSelectionRequest, TestSelectionDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public UpdateTestSelectionHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestSelectionDto> Handle(UpdateTestSelectionRequest request, CancellationToken cancellationToken)
        {
            var recordToUpdate = await ValidateRequestData(request);
            await _testSelectionRepositoryManager.ScTestSelectionRepository.Update(recordToUpdate);
            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<TestSelectionDto>(recordToUpdate);
        }

        private async Task<SC_TestSelection> ValidateRequestData(UpdateTestSelectionRequest request)
        {
            var recordFound = await _testSelectionRepositoryManager.ScTestSelectionRepository.Find(request.Id);

            if (recordFound == null)
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            recordFound.Name = request.Name;
            recordFound.Description = request.Description;
            recordFound.DescriptionVisibility = request.DescriptionVisibility;
            recordFound.SpecialityId = request.SpecialityId;

            return recordFound;
        }
    }
}
