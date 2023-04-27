using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;

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
            var recordToUpdate = await _testSelectionRepositoryManager.ScPanelRepository.FindById(request.Id);

            if (recordToUpdate != null)
            {
                List<SC_Test> sC_Tests = new();

                if (request.TestIds != null && request.TestIds.Any())
                {
                    var tests = await _testSelectionRepositoryManager.ScTestRepository.FindByCondition(x => request.TestIds.Contains(x.Id));
                    sC_Tests = tests.ToList();
                }

                recordToUpdate.Name = request.Name;
                recordToUpdate.Description = request.Description;
                recordToUpdate.DescriptionVisibility = request.DescriptionVisibility;
                recordToUpdate.Price = request.Price;
                recordToUpdate.Tests = sC_Tests;
                recordToUpdate.Visibility = request.Visibility;

                await _testSelectionRepositoryManager.ScPanelRepository.Update(recordToUpdate);
                await _testSelectionRepositoryManager.Save();
            }

            return _mapper.Map<PanelDto>(recordToUpdate);
        }
    }
}
