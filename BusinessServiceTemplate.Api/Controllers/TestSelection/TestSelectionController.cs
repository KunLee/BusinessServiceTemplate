using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessServiceTemplate.Api.Controllers.TestSelection
{
    [ApiController]
    [Route("[controller]")]
    public class TestSelectionController : Controller
    {
        private readonly IMediator _mediator;

        public TestSelectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all/panels")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Task<IList<PanelDto>> GetAllPanels() 
        {
            return _mediator.Send(new GetAllPanelRequest());
        }
    }
}
