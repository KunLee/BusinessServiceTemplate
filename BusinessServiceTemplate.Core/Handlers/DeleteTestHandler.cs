using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class DeleteTestHandler : IRequestHandler<DeleteTestRequest, TestDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public DeleteTestHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestDto> Handle(DeleteTestRequest request, CancellationToken cancellationToken)
        {
            var testDeleted = default(SC_Test);
            var tests = await _testSelectionRepositoryManager.ScTestRepository.FindByCondition(x => x.Id == request.Id);

            if (tests.Any()) 
            {
                var testToDelete = tests.First();
                testDeleted = await _testSelectionRepositoryManager.ScTestRepository.Delete(testToDelete);
                await _testSelectionRepositoryManager.Save();
            }
            
            return _mapper.Map<TestDto>(testDeleted);
        }
    }
}
