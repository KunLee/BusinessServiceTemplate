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
            SC_Panel? updatedPanel = null;

            var recordToUpdate = await _testSelectionRepositoryManager.ScPanelRepository.FindByCondition(x => x.Id == request.Id);
            
            if (recordToUpdate.Any()) 
            {
                List<SC_Test> sC_Tests = new();

                if (request.TestIds != null && request.TestIds.Any())
                {
                    var tests = await _testSelectionRepositoryManager.ScTestRepository.FindByCondition(x => request.TestIds.Contains(x.Id));
                    sC_Tests = tests.ToList();
                }

                var record = recordToUpdate.FirstOrDefault();
                record.Name = request.Name;
                record.Description = request.Description;
                record.Price = request.Price;
                record.Tests= sC_Tests;

                updatedPanel = await _testSelectionRepositoryManager.ScPanelRepository.Update(record);

                await _testSelectionRepositoryManager.Save();
            }

            return _mapper.Map<PanelDto>(updatedPanel);
        }
    }
}
