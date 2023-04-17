using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Exceptions;
using BusinessServiceTemplate.Shared.Common;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class CreateTestHandler : IRequestHandler<CreateTestRequest, TestDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public CreateTestHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestDto> Handle(CreateTestRequest request, CancellationToken cancellationToken)
        {
            List<SC_Panel> sC_Panels = new();

            if (request.PanelIds != null && request.PanelIds.Any()) 
            {
                foreach (var panelId in request.PanelIds) 
                {
                    var panel = await _testSelectionRepositoryManager.ScPanelRepository.FindByCondition(x => x.Id == panelId);
                    if (panel.Any())
                    {
                        sC_Panels.AddRange(panel);
                    }
                    else 
                    {
                        throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
                    }
                }
            }
            
            var result = await _testSelectionRepositoryManager.ScTestRepository.Create(new SC_Test { 
                Name = request.Name,
                Description = request.Description,
                Panels = sC_Panels
            });

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<TestDto>(result);
        }
    }
}
