using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class DeleteTestSelectionHandler : IRequestHandler<DeleteTestSelectionRequest, TestSelectionDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public DeleteTestSelectionHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestSelectionDto> Handle(DeleteTestSelectionRequest request, CancellationToken cancellationToken)
        {
            var testSelectionDeleted = default(SC_TestSelection);
            var testSelections = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindByCondition(x => x.Id == request.Id);

            if (testSelections.Any()) 
            {
                var testSelectionToDelete = testSelections.First();
                testSelectionDeleted = await _testSelectionRepositoryManager.ScTestSelectionRepository.Delete(testSelectionToDelete);
                await _testSelectionRepositoryManager.Save();
            }
            
            return _mapper.Map<TestSelectionDto>(testSelectionDeleted);
        }
    }
}
