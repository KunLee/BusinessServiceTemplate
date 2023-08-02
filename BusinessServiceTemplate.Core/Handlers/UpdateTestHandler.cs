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
            var recordToUpdate = await ValidateRequestData(request);

            await _testSelectionRepositoryManager.ScTestRepository.Update(recordToUpdate);
            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<TestDto>(recordToUpdate);
        }

        private async Task<SC_Test> ValidateRequestData(UpdateTestRequest request)
        {
            // Use FindByIdWithPanels instead of Find, otherwise the virtual Panels updates and deletes would have unexpected issues.
            var recordFound = await _testSelectionRepositoryManager.ScTestRepository.FindByIdWithPanels(request.Id);

            if (recordFound == null)
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            // Validate the Panel Ids
            List<SC_Panel> sC_Panels = new();

            if (request.PanelIds != null && request.PanelIds.Any())
            {
                foreach (var panelId in request.PanelIds)
                {
                    var panel = await _testSelectionRepositoryManager.ScPanelRepository.Find(panelId);

                    if (panel != null)
                    {
                        sC_Panels.Add(panel);
                    }
                    else
                    {
                        throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
                    }
                }
            }

            recordFound.Name = request.Name;
            recordFound.Description = request.Description;
            recordFound.DescriptionVisibility = request.DescriptionVisibility;
            recordFound.Panels = sC_Panels;

            return recordFound;
        }
    }
}
