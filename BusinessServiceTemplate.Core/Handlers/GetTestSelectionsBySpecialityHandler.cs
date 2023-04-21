using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetTestSelectionsBySpecialityHandler : IRequestHandler<GetTestSelectionsBySpecialityRequest, IList<TestSelectionDto>>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetTestSelectionsBySpecialityHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<IList<TestSelectionDto>> Handle(GetTestSelectionsBySpecialityRequest request, CancellationToken cancellationToken)
        {
            var testSelections = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindBySpecialityId(request.SpecialityId);

            return testSelections.Select(_mapper.Map<TestSelectionDto>).ToList();
        }
    }
}
