using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

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
            var recordToUpdate = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindById(request.Id);

            if (recordToUpdate != null)
            {
                recordToUpdate.Name = request.Name;
                recordToUpdate.Description = request.Description;
                recordToUpdate.DescriptionVisibility = request.DescriptionVisibility;
                recordToUpdate.SpecialityId = request.SpecialityId;

                await _testSelectionRepositoryManager.ScTestSelectionRepository.Update(recordToUpdate);
                await _testSelectionRepositoryManager.Save();
            }
            return _mapper.Map<TestSelectionDto>(recordToUpdate);
        }
    }
}
