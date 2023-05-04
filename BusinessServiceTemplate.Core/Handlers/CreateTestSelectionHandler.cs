using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using Microsoft.EntityFrameworkCore;
using BusinessServiceTemplate.Shared.Exceptions;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class CreateTestSelectionHandler : IRequestHandler<CreateTestSelectionRequest, TestSelectionDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public CreateTestSelectionHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestSelectionDto> Handle(CreateTestSelectionRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequestData(request);

            var result = await _testSelectionRepositoryManager.ScTestSelectionRepository.Create(new SC_TestSelection { 
                Name = request.Name,
                Description = request.Description,
                SpecialityId = request.SpecialityId,
                DescriptionVisibility = request.DescriptionVisibility
            });

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<TestSelectionDto>(result);
        }

        private async Task ValidateRequestData(CreateTestSelectionRequest request)
        {
            // Validate Duplicate records
            var isDuplicate = await _testSelectionRepositoryManager.ScTestSelectionRepository
                    .Any(x => x.Name.Equals(request.Name) &&
                                            x.Description == request.Description &&
                                            x.DescriptionVisibility == request.DescriptionVisibility &&
                                            x.SpecialityId == request.SpecialityId);
            if (isDuplicate)
            {
                throw new ValidationException(ConstantStrings.DUPLICATE_REQUEST_DATA);
            }
        }
    }
}
