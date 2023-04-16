using AutoMapper;
using BusinessServiceTemplate.Api.Models.Forms;
using BusinessServiceTemplate.Api.Models.ViewModels;
using BusinessServiceTemplate.Core.Dtos;
using BusinessServiceTemplate.Core.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessServiceTemplate.Api.Controllers.TestSelection
{
    [ApiController]
    [Route("[controller]")]
    public class TestSelectionController : Controller
    {
        private readonly IMediator _mediator;

        public readonly IMapper _mapper;

        public TestSelectionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("panels")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IList<PanelViewModel>> GetAllPanels() 
        {
            var allPanels = await _mediator.Send(new GetAllPanelsRequest());

            return allPanels.Select(_mapper.Map<PanelViewModel>).ToList();
        }

        [HttpPost("panel")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<PanelViewModel> AddPanel([FromBody] CreatePanelForm form)
        {
            var panelDto = await _mediator.Send(new CreatePanelRequest() { 
                Name = form.Name,
                Description = form.Description,
                Price = form.Price,
                TestIds= form.TestIds
            });

            return _mapper.Map<PanelViewModel>(panelDto);
        }

        [HttpPut("panel/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<PanelViewModel> UpdatePanel(int id, [FromBody] UpdatePanelForm form)
        {
            var panelDto = await _mediator.Send(new UpdatePanelRequest()
            {
                Id = id,
                Name = form.Name,
                Description = form.Description,
                Price = form.Price,
                TestIds = form.TestIds
            });

            return _mapper.Map<PanelViewModel>(panelDto);
        }

        /// <summary>
        /// Deletes an existing Panel
        /// </summary>
        /// <param name="modelPackageConfigId">The ID of the Panel to delete</param>
        [HttpDelete("panel/{id}")]
        [ProducesResponseType(200)]
        public Task DeletePanel(int id)
        {
            return _mediator.Send(
                new DeletePanelRequest
                {
                    Id = id
                });
        }

        [HttpGet("tests")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public Task<IList<TestDto>> GetAllTests()
        {
            return _mediator.Send(new GetAllTestsRequest());
        }

        [HttpPost("test")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestViewModel> AddTest([FromBody] CreateTestForm form)
        {
            var testDto = await _mediator.Send(new CreateTestRequest()
            {
                Name = form.Name,
                Description = form.Description,
                PanelIds = form.PanelIds
            });

            return _mapper.Map<TestViewModel>(testDto);
        }

        [HttpPut("test/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestViewModel> UpdateTest(int id, [FromBody] UpdateTestForm form)
        {
            var testDto = await _mediator.Send(new UpdateTestRequest()
            {
                Id = id,
                Name = form.Name,
                Description = form.Description,
                PanelIds = form.PanelIds
            });

            return _mapper.Map<TestViewModel>(testDto);
        }

        /// <summary>
        /// Deletes an existing Test
        /// </summary>
        /// <param name="modelPackageConfigId">The ID of the test to delete</param>
        [HttpDelete("test/{id}")]
        [ProducesResponseType(200)]
        public Task DeleteTest(int id)
        {
            return _mediator.Send(
                new DeleteTestRequest
                {
                    Id = id
                });
        }
    }
}
