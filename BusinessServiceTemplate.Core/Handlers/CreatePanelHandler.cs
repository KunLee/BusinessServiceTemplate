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
    public class CreatePanelHandler : IRequestHandler<CreatePanelRequest, PanelDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public CreatePanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelDto> Handle(CreatePanelRequest request, CancellationToken cancellationToken)
        {
            List<SC_Test> sC_Tests = new();

            if (request.TestIds != null && request.TestIds.Any())
            {
                foreach (var testId in request.TestIds)
                {
                    var panel = await _testSelectionRepositoryManager.ScTestRepository.FindByCondition(x => x.Id == testId);

                    if (panel.Any())
                    {
                        sC_Tests.AddRange(panel);
                    }
                    else
                    {
                        throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
                    }
                }
            }

            var testSelection = await _testSelectionRepositoryManager.ScTestSelectionRepository.Find(request.TestSelectionId);

            if (testSelection is null) 
            {
                throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
            }

            var result = await _testSelectionRepositoryManager.ScPanelRepository.Create(new SC_Panel { 
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                TestSelection = testSelection,
                Tests = sC_Tests,
                Visibility= request.Visibility
            });

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<PanelDto>(result);
        }
    }
}
