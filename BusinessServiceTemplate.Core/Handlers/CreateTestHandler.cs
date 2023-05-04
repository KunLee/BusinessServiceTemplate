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
            var testToAdd = await ValidateRequestData(request);

            var result = await _testSelectionRepositoryManager.ScTestRepository.Create(testToAdd);

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<TestDto>(result);
        }

        private async Task<SC_Test> ValidateRequestData(CreateTestRequest request)
        {
            // Validate Duplicate records
            var isDuplicate = await _testSelectionRepositoryManager.ScTestRepository
                    .Any(x => x.Name.Equals(request.Name) &&
                                            x.Description == request.Description);
            if (isDuplicate) 
            {
                throw new ValidationException(ConstantStrings.DUPLICATE_REQUEST_DATA);
            }

            List<SC_Panel> sC_Panels = new();

            // * also works with range selection
            //if (request.PanelIds != null && request.PanelIds.Any())
            //{
            //    foreach (var panelId in request.PanelIds)
            //    {
            //        var panel = await _testSelectionRepositoryManager.ScPanelRepository.FindByCondition(x => x.Id == panelId);
            //        if (panel.Any())
            //        {
            //            sC_Panels.AddRange(panel);
            //        }
            //        else
            //        {
            //            throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
            //        }
            //    }
            //}

            if (request.PanelIds != null && request.PanelIds.Any())
            {
                foreach (var panelId in request.PanelIds)
                {
                    var panel = await _testSelectionRepositoryManager.ScPanelRepository.Find(panelId); // * do not call FindByIdWithTests Here

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

            return new SC_Test
            {
                Name = request.Name,
                Description = request.Description,
                Panels = sC_Panels
            };
        }
    }
}
