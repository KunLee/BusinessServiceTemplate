using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class DeletePanelHandler : IRequestHandler<DeletePanelRequest, PanelDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public DeletePanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelDto> Handle(DeletePanelRequest request, CancellationToken cancellationToken)
        {
            var panelDeleted = default(SC_Panel);
            var panels = await _testSelectionRepositoryManager.ScPanelRepository.FindByCondition(x => x.Id == request.Id);

            if (panels.Any()) 
            {
                var panelToDelete = panels.First();
                panelDeleted = await _testSelectionRepositoryManager.ScPanelRepository.Delete(panelToDelete);
                await _testSelectionRepositoryManager.Save();
            }
            
            return _mapper.Map<PanelDto>(panelDeleted);
        }
    }
}
