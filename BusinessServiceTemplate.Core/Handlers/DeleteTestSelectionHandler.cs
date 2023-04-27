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
            var testSelection = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindById(request.Id);

            if (testSelection != null)
            {
                await _testSelectionRepositoryManager.ScTestSelectionRepository.Delete(testSelection);
                await _testSelectionRepositoryManager.Save();
            }

            return _mapper.Map<TestSelectionDto>(testSelection);
        }
    }
}
