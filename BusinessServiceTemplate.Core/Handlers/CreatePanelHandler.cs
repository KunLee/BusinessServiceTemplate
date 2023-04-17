using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

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
                var tests = await _testSelectionRepositoryManager.ScTestRepository.FindByCondition(x => request.TestIds.Contains(x.Id));
                sC_Tests = tests.ToList();
            }
            
            var result = await _testSelectionRepositoryManager.ScPanelRepository.Create(new SC_Panel { 
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Tests = sC_Tests
            });

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<PanelDto>(result);
        }
    }
}
