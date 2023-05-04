using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using Microsoft.EntityFrameworkCore;
using BusinessServiceTemplate.Shared.Exceptions;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class UpdatePanelHandler : IRequestHandler<UpdatePanelRequest, PanelDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public UpdatePanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelDto> Handle(UpdatePanelRequest request, CancellationToken cancellationToken)
        {
            var recordToUpdate = await ValidateRequestData(request);
            await _testSelectionRepositoryManager.ScPanelRepository.Update(recordToUpdate);
            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<PanelDto>(recordToUpdate);
        }

        private async Task<SC_Panel> ValidateRequestData(UpdatePanelRequest request)
        {
            var recordFound = await _testSelectionRepositoryManager.ScPanelRepository.Find(request.Id);

            if (recordFound == null)
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            // Validate the Test Ids
            List<SC_Test> sC_Tests = new();

            if (request.TestIds != null && request.TestIds.Any())
            {
                foreach (var testId in request.TestIds)
                {
                    var test = await _testSelectionRepositoryManager.ScTestRepository.Find(testId);

                    if (test != null)
                    {
                        sC_Tests.Add(test);
                    }
                    else
                    {
                        throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
                    }
                }
            }

            // Validate the Test Selection Id
            var testSelection = await _testSelectionRepositoryManager.ScTestSelectionRepository.Find(request.TestSelectionId);

            if (testSelection is null)
            {
                throw new ValidationException(ConstantStrings.INVALID_REQUEST_DATA);
            }

            recordFound.Name = request.Name;
            recordFound.Description = request.Description;
            recordFound.DescriptionVisibility = request.DescriptionVisibility;
            recordFound.Price = request.Price;
            recordFound.TestSelection = testSelection;
            recordFound.Tests = sC_Tests;
            recordFound.Visibility = request.Visibility;

            return recordFound;
        }
    }
}
