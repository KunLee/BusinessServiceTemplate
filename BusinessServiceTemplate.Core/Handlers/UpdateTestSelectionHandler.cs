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
            SC_TestSelection? updatedTestSelection = null;

            var recordToUpdate = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindByCondition(x => x.Id == request.Id);
            
            if (recordToUpdate.Any()) 
            {
                var record = recordToUpdate.FirstOrDefault();
                record.Name = request.Name;
                record.Description = request.Description;
                record.SpecialityId = request.SpecialityId;

                updatedTestSelection = await _testSelectionRepositoryManager.ScTestSelectionRepository.Update(record);
                await _testSelectionRepositoryManager.Save();
            }

            return _mapper.Map<TestSelectionDto>(updatedTestSelection);
        }
    }
}
