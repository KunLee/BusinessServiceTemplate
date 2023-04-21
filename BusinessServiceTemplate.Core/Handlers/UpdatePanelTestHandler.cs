using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using BusinessServiceTemplate.Shared.Exceptions;
using BusinessServiceTemplate.Shared.Common;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class UpdatePanelTestHandler : IRequestHandler<UpdatePanelTestRequest>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;

        public UpdatePanelTestHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
        }
        public async Task Handle(UpdatePanelTestRequest request, CancellationToken cancellationToken)
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
        }
    }
}
