using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class UpdateTestHandler : IRequestHandler<UpdateTestRequest, TestDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public UpdateTestHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
                IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestDto> Handle(UpdateTestRequest request, CancellationToken cancellationToken)
        {   
            SC_Test? updatedTest = null;

            var recordToUpdate = await _testSelectionRepositoryManager.ScTestRepository.FindByCondition(x => x.Id == request.Id);
            
            if (recordToUpdate.Any()) 
            {
                List<SC_Panel> sC_Panels = new();

                if (request.PanelIds != null && request.PanelIds.Any())
                {
                    var panels = await _testSelectionRepositoryManager.ScPanelRepository.FindByCondition(x => request.PanelIds.Contains(x.Id));
                    sC_Panels = panels.ToList();
                }

                var record = recordToUpdate.FirstOrDefault();
                record.Name = request.Name;
                record.Description = request.Description;
                record.Panels= sC_Panels;

                updatedTest = await _testSelectionRepositoryManager.ScTestRepository.Update(record);

                await _testSelectionRepositoryManager.Save();
            }

            return _mapper.Map<TestDto>(updatedTest);
        }
    }
}
