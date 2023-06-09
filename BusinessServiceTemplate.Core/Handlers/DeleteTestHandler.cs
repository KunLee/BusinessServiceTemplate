﻿using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using BusinessServiceTemplate.DataAccess;
using MediatR;
using AutoMapper;
using BusinessServiceTemplate.DataAccess.Entities;
using BusinessServiceTemplate.Shared.Common;
using BusinessServiceTemplate.Shared.Exceptions;

namespace BusinessServiceTemplate.Core.Handlers
{
    public class DeleteTestHandler : IRequestHandler<DeleteTestRequest, TestDto>
    {
        private readonly ITestSelectionRepositoryManager _testSelectionRepositoryManager;
        private readonly IMapper _mapper;

        public DeleteTestHandler(ITestSelectionRepositoryManager testSelectionRepositoryManager, 
            IMapper mapper)
        {
            _testSelectionRepositoryManager = testSelectionRepositoryManager;
            _mapper = mapper;
        }
        public async Task<TestDto> Handle(DeleteTestRequest request, CancellationToken cancellationToken)
        {
            var test = await _testSelectionRepositoryManager.ScTestRepository.Find(request.Id);

            if (test != null)
            {
                await _testSelectionRepositoryManager.ScTestRepository.Delete(test);
                await _testSelectionRepositoryManager.Save();
            }
            else
            {
                throw new ValidationException(ConstantStrings.NO_REQUESTED_RECORD);
            }

            return _mapper.Map<TestDto>(test);
        }
    }
}
