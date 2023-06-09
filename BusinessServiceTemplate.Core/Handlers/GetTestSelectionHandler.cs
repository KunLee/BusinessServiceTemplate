﻿using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class GetTestSelectionHandler : IRequestHandler<GetTestSelectionRequest, TestSelectionDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public GetTestSelectionHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager,
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestSelectionDto> Handle(GetTestSelectionRequest request, CancellationToken cancellationToken)
        {
            var testSelection = await _testSelectionRepositoryManager.ScTestSelectionRepository.FindById(request.Id);

            return _mapper.Map<TestSelectionDto>(testSelection);
        }
    }
}
