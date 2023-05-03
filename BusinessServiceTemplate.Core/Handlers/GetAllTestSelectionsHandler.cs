using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetAllTestSelectionsHandler : IRequestHandler<GetAllTestSelectionsRequest, IList<TestSelectionDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetAllTestSelectionsHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<IList<TestSelectionDto>> Handle(GetAllTestSelectionsRequest request, CancellationToken cancellationToken)
        {
            var testSelectionList = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindAll();
            var fullSelectionList = testSelectionList.Include(x => x.Panels).ThenInclude(t => t.Tests);
            return fullSelectionList.Select(_mapper.Map<TestSelectionDto>).ToList();
        }
    }
}
