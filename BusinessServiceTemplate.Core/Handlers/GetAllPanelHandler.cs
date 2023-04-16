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

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetAllPanelHandler : IRequestHandler<GetAllPanelRequest, IList<PanelDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllPanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper, TestSelectionRepositoryContext context)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<IList<PanelDto>> Handle(GetAllPanelRequest request, CancellationToken cancellationToken)
        {
            var list = await _testSelectionRepositoryManager.ScPanelRepository.FindAll();
            IList<PanelDto> toList = await list.ProjectTo<PanelDto>(_mapper.ConfigurationProvider).ToListAsync();

            return toList;
        }
    }
}
