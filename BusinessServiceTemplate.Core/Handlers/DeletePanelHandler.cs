﻿using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

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
            var panel = await _testSelectionRepositoryManager.ScPanelRepository.FindById(request.Id);

            if (panel != null) 
            {
                await _testSelectionRepositoryManager.ScPanelRepository.Delete(panel);
                await _testSelectionRepositoryManager.Save();
            }
            
            return _mapper.Map<PanelDto>(panel);
        }
    }
}
