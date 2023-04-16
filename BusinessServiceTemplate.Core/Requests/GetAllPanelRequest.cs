using BusinessServiceTemplate.Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.Core.Requests
{
    public class GetAllPanelRequest : IRequest<IList<PanelDto>>
    {
    }
}
