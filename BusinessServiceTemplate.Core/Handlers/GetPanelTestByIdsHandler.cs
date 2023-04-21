using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetPanelTestByIdsHandler : IRequestHandler<GetPanelTestByIdsRequest, PanelTestDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetPanelTestByIdsHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelTestDto> Handle(GetPanelTestByIdsRequest request, CancellationToken cancellationToken)
        {
            var panelTest = await _testSelectionRepositoryManager.ScPanelTestRepository.FindByIds(request.PanelId, request.TestId);
            return _mapper.Map<PanelTestDto>(panelTest);
        }
    }
}
