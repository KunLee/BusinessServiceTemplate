using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetAllTestsHandler : IRequestHandler<GetAllTestsRequest, IList<TestDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllTestsHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<IList<TestDto>> Handle(GetAllTestsRequest request, CancellationToken cancellationToken)
        {
            var panelList = await _testSelectionRepositoryManager.ScTestRepository.FindAll();

            var completeList = panelList.Include(x => x.Panels);

            return completeList.Select(_mapper.Map<TestDto>).ToList();
        }
    }
}
