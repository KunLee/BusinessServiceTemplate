﻿using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetTestSelectionRequest : IRequest<TestSelectionDto>
    {
        public GetTestSelectionRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
