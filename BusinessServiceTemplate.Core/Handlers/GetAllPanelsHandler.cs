using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetAllPanelsHandler : IRequestHandler<GetAllPanelsRequest, IList<PanelDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllPanelsHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<IList<PanelDto>> Handle(GetAllPanelsRequest request, CancellationToken cancellationToken)
        {
            var panelList = await _testSelectionRepositoryManager.ScPanelRepository.FindAll();

            var completeList = panelList.Include(x => x.Tests);

            return completeList.Select(_mapper.Map<PanelDto>).ToList();
        }
    }
}
