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
            var recordToUpdate = await _testSelectionRepositoryManager.ScTestRepository.FindById(request.Id);

            if (recordToUpdate != null)
            {
                List<SC_Panel> sC_Panels = new();

                if (request.PanelIds != null && request.PanelIds.Any())
                {
                    var panels = await _testSelectionRepositoryManager.ScPanelRepository.FindByCondition(x => request.PanelIds.Contains(x.Id));
                    sC_Panels = panels.ToList();
                }

                recordToUpdate.Name = request.Name;
                recordToUpdate.Description = request.Description;
                recordToUpdate.DescriptionVisibility = request.DescriptionVisibility;
                recordToUpdate.Panels = sC_Panels;

                await _testSelectionRepositoryManager.ScTestRepository.Update(recordToUpdate);
                await _testSelectionRepositoryManager.Save();
            }

            return _mapper.Map<TestDto>(recordToUpdate);
        }
    }
}
