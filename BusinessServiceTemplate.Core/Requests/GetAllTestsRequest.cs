﻿using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetAllTestsRequest : IRequest<IList<TestDto>>
    {
    }
}
