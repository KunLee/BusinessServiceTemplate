using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetPanelHandler : IRequestHandler<GetPanelRequest, PanelDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetPanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelDto> Handle(GetPanelRequest request, CancellationToken cancellationToken)
        {
            var panel = await _testSelectionRepositoryManager.ScPanelRepository.FindByIdWithTests(request.Id);
            return _mapper.Map<PanelDto>(panel);
        }
    }
}
