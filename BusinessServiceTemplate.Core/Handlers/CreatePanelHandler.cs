using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using BusinessServiceTemplate.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class CreatePanelHandler : IRequestHandler<CreatePanelRequest, PanelDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public CreatePanelHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<PanelDto> Handle(CreatePanelRequest request, CancellationToken cancellationToken)
        {
            var panelToAdd = await ValidateRequestData(request);

            var result = await _testSelectionRepositoryManager.ScPanelRepository.Create(panelToAdd);

            await _testSelectionRepositoryManager.Save();

            return _mapper.Map<PanelDto>(result);
        }

        private async Task<SC_Panel> ValidateRequestData(CreatePanelRequest request) 
        {
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

            // Validate Duplicate records
            var panels = await _testSelectionRepositoryManager.ScPanelRepository
                    .FindByCondition(x => x.Name.Equals(request.Name) &&
                                            x.PriceVisibility == request.PriceVisibility &&
                                            Decimal.Compare(x.Price.HasValue ? x.Price.Value : 0, request.Price.HasValue ? request.Price.Value : 0) == 0 &&
                                            x.TestSelectionId == request.TestSelectionId &&
                                            x.Description == request.Description &&
                                            x.DescriptionVisibility == request.DescriptionVisibility &&
                                            x.Visibility == request.Visibility);

            var panelList = panels.Include(t => t.Tests).ToList();

            foreach (var panel in panelList)
            {
                if (request.TestIds != null && request.TestIds.SequenceEqual(panel.Tests.Select(x => x.Id)))
                {
                    throw new ValidationException(ConstantStrings.DUPLICATE_REQUEST_DATA);
                }
                else if (request.TestIds == null && !panel.Tests.Any())
                {
                    throw new ValidationException(ConstantStrings.DUPLICATE_REQUEST_DATA);
                }
            }

            return new SC_Panel
            {
                Name = request.Name,
                Description = request.Description,
                DescriptionVisibility = request.DescriptionVisibility,
                PriceVisibility = request.PriceVisibility,
                Price = request.Price,
                TestSelection = testSelection,
                Tests = sC_Tests,
                Visibility = request.Visibility
            };
        }
    }
}
