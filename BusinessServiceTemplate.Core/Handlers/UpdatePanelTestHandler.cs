using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using BusinessServiceTemplate.Core.Dtos;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.Shared.Common;
using BusinessServiceTemplate.Shared.Exceptions;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class UpdatePanelTestHandler : IRequestHandler<UpdatePanelTestRequest, PanelTestDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public UpdatePanelTestHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelTestDto> Handle(UpdatePanelTestRequest request, CancellationToken cancellationToken)
        {
            var recordToUpdate = await _testSelectionRepositoryManager.ScPanelTestRepository
                .FindByIds(request.PanelId, request.TestId);

            if (recordToUpdate != null)
            {
                recordToUpdate.Visibility = request.Visibility;

                _testSelectionRepositoryManager.ScPanelTestRepository.UpdateChanges(recordToUpdate);

                await _testSelectionRepositoryManager.Save();
            }
            else
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            return _mapper.Map<PanelTestDto>(recordToUpdate);
        }
    }
}
