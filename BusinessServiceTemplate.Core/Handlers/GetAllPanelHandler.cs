using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using System.Linq;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BusinessServiceTemplate.DataAccess.Data.Contexts;
using System.Security.Cryptography.X509Certificates;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetAllPanelHandler : IRequestHandler<GetAllPanelsRequest, IList<PanelDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly TestSelectionRepositoryContext _testSelectionRepositoryContext;
        private readonly IMapper _mapper;

        public GetAllPanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, TestSelectionRepositoryContext testSelectionRepositoryContext,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _testSelectionRepositoryContext = testSelectionRepositoryContext;
            _mapper = mapper;
        }
        public async Task<IList<PanelDto>> Handle(GetAllPanelsRequest request, CancellationToken cancellationToken)
        {
            var panelList = await _testSelectionRepositoryManager.ScPanelRepository.FindAll();

            var fullList = panelList.Include(x => x.Tests);

            //IList<PanelDto> toList = await fullList.ProjectTo<PanelDto>(_mapper.ConfigurationProvider).ToListAsync();

            IList<PanelDto> toList = fullList.Select(_mapper.Map<PanelDto>).ToList();

            return toList;
        }
    }
}
