using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

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
            var result = await _testSelectionRepositoryManager.ScTestSelectionRepository.Create(new SC_TestSelection { 
                Name = request.Name,
                Description = request.Description,
                SpecialityId = request.SpecialityId
            });

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<TestSelectionDto>(result);
        }
    }
}
