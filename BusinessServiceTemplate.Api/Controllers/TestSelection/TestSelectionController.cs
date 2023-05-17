using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BusinessServiceTemplate.Api.Models.RequestModels;
using BusinessServiceTemplate.Api.Models.ResponseModels;
using BusinessServiceTemplate.Core.Requests;
using Microsoft.AspNetCore.Authorization;
using BusinessServiceTemplate.Api.Security;

namespace BusinessServiceTemplate.Api.Controllers.TestSelection
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(nameof(SecurityOperation.FullAccess))]
    public class TestSelectionController : Controller
    {
        private readonly IMediator _mediator;

        public readonly IMapper _mapper;

        public TestSelectionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a specific Test Panel by Id
        /// </summary>
        /// <returns>The panel</returns>
        [HttpGet("panels/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(nameof(SecurityOperation.PanelAccess))]
        public async Task<ActionResult<PanelResponseModel>> GetPanel(int id)
        {
            var panel = await _mediator.Send(new GetPanelRequest(id));

            return panel == null ? NotFound() : _mapper.Map<PanelResponseModel>(panel);
        }

        /// <summary>
        /// Get All of the Test Panels
        /// </summary>
        /// <returns>The list of the panels</returns>
        [HttpGet("panels")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<PanelResponseModel>))]
        [Authorize(nameof(SecurityOperation.PanelAccess))]
        public async Task<IList<PanelResponseModel>> GetAllPanels() 
        {
            var allPanels = await _mediator.Send(new GetAllPanelsRequest());

            return allPanels.Select(_mapper.Map<PanelResponseModel>).ToList();
        }

        /// <summary>
        /// Add a new Test Panel
        /// </summary>
        /// <param name="requestModel">The data describing the Panel details</param>
        /// <returns>The added Panel object</returns>
        [HttpPost("panels")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(nameof(SecurityOperation.PanelAccess))]
        public async Task<ActionResult<PanelResponseModel>> AddPanel([FromBody] CreatePanelRequestModel requestModel)
        {
            var panelDto = await _mediator.Send(new CreatePanelRequest() { 
                Name = requestModel.Name,
                Description = requestModel.Description,
                Price = requestModel.Price,
                PriceVisibility = requestModel.PriceVisibility,
                DescriptionVisibility = requestModel.DescriptionVisibility,
                TestSelectionId = requestModel.TestSelectionId,
                TestIds= requestModel.TestIds,
                Visibility = requestModel.Visibility
            });

            return CreatedAtAction(nameof(GetPanel), new { id = panelDto.Id }, _mapper.Map<PanelResponseModel>(panelDto));
        }

        /// <summary>
        /// Update an existing Test Panel
        /// </summary>
        /// <param name="requestModel">The data describing the Panel details to update</param>
        /// <returns>The updated Panel object</returns>
        [HttpPut("panels")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(nameof(SecurityOperation.PanelAccess))]
        public async Task<ActionResult<PanelResponseModel>> UpdatePanel([FromBody] UpdatePanelRequestModel requestModel)
        {
            var panelDto = await _mediator.Send(new UpdatePanelRequest()
            {
                Id = requestModel.Id,
                Name = requestModel.Name,
                Description = requestModel.Description,
                DescriptionVisibility = requestModel.DescriptionVisibility,
                PriceVisibility = requestModel.PriceVisibility,
                TestSelectionId = requestModel.TestSelectionId,
                Price = requestModel.Price,
                TestIds = requestModel.TestIds,
                Visibility = requestModel.Visibility
            });

            return panelDto == null ? NotFound() : CreatedAtAction(nameof(GetPanel), new { id = panelDto.Id }, _mapper.Map<PanelResponseModel>(panelDto));
        }

        /// <summary>
        /// Delete an existing Test Panel
        /// </summary>
        /// <param name="id">The ID of the Test Panel</param>
        /// <returns></returns>
        [HttpDelete("panels/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(nameof(SecurityOperation.PanelAccess))]
        public async Task<ActionResult<PanelResponseModel>> DeletePanel(int id)
        {
            var panel = await _mediator.Send(
                new DeletePanelRequest
                {
                    Id = id
                });
            return panel == null ? NotFound() : _mapper.Map<PanelResponseModel>(panel);
        }

        /// <summary>
        /// Get All of the Tests
        /// </summary>
        /// <returns>The list of the tests</returns>
        [HttpGet("tests")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(nameof(SecurityOperation.TestAccess))]
        public async Task<IList<TestResponseModel>> GetAllTests()
        {
            var list = await _mediator.Send(new GetAllTestsRequest());
            return list.Select(_mapper.Map<TestResponseModel>).ToList();
        }

        /// <summary>
        /// Get a specific Test by Id
        /// </summary>
        /// <returns>A specific test returned</returns>
        [HttpGet("tests/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(nameof(SecurityOperation.TestAccess))]
        public async Task<ActionResult<TestResponseModel>> GetTest(int id)
        {
            var test = await _mediator.Send(new GetTestRequest(id));

            return test == null ? NotFound() : _mapper.Map<TestResponseModel>(test);
        }

        /// <summary>
        /// Add a new Test with full details
        /// </summary>
        /// <param name="requestModel">The data describing the Test details</param>
        /// <returns>The added Test object</returns>
        [HttpPost("tests")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(nameof(SecurityOperation.TestAccess))]
        public async Task<ActionResult<TestResponseModel>> AddTest([FromBody] CreateTestRequestModel requestModel)
        {
            var testDto = await _mediator.Send(new CreateTestRequest()
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
                DescriptionVisibility = requestModel.DescriptionVisibility,
                PanelIds = requestModel.PanelIds
            });
            return CreatedAtAction(nameof(GetTest), new { id = testDto.Id }, _mapper.Map<TestResponseModel>(testDto));
        }

        /// <summary>
        /// Update an existing Test
        /// </summary>
        /// <param name="requestModel">The data describing the Test details to update</param>
        /// <returns>The updated Test object</returns>
        [HttpPut("tests")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(nameof(SecurityOperation.TestAccess))]
        public async Task<ActionResult<TestResponseModel>> UpdateTest([FromBody] UpdateTestRequestModel requestModel)
        {
            var testDto = await _mediator.Send(new UpdateTestRequest()
            {
                Id = requestModel.Id,
                Name = requestModel.Name,
                Description = requestModel.Description,
                DescriptionVisibility = requestModel.DescriptionVisibility,
                PanelIds = requestModel.PanelIds
            });
            return testDto == null ? NotFound() : CreatedAtAction(nameof(GetTest), new { id = testDto.Id }, _mapper.Map<TestResponseModel>(testDto));
        }

        /// <summary>
        /// Deletes an existing Test
        /// </summary>
        /// <param name="id">The ID of the test to delete</param>
        [HttpDelete("test/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(nameof(SecurityOperation.TestAccess))]
        public async Task<ActionResult<TestResponseModel>> DeleteTest(int id)
        {
            var test = await _mediator.Send(
                        new DeleteTestRequest
                        {
                            Id = id
                        });

            return test == null ? NotFound() : _mapper.Map<TestResponseModel>(test);
        }

        /// <summary>
        /// Get All of the TestSelections
        /// </summary>
        /// <returns>The list of the test selections</returns>
        [HttpGet("testselections")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<TestSelectionResponseModel>> GetAllTestSelections()
        {
            var list = await _mediator.Send(new GetAllTestSelectionsRequest());
            return list.Select(_mapper.Map<TestSelectionResponseModel>).ToList();
        }

        /// <summary>
        /// Get A specific TestSelections
        /// </summary>
        /// <returns>The list of the test selections</returns>
        [HttpGet("testselections/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestSelectionResponseModel>> GetTestSelection(int id)
        {
            var testSelection = await _mediator.Send(new GetTestSelectionRequest(id));
            return testSelection == null ? NotFound() : _mapper.Map<TestSelectionResponseModel>(testSelection);
        }

        /// <summary>
        /// Get TestSelections via SpecialityId
        /// </summary>
        /// <returns>The list of the test selections</returns>
        [HttpGet("testselections/speciality/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<TestSelectionResponseModel>> GetTestSelectionsBySpecialityId(int id)
        {
            var testSelections = await _mediator.Send(new GetTestSelectionsBySpecialityRequest(id));

            return testSelections.Select(_mapper.Map<TestSelectionResponseModel>).ToList();
        }

        /// <summary>
        /// Add a new Test Selection with full details
        /// </summary>
        /// <param name="requestModel">The data describing the Test Selection details</param>
        /// <returns>The added Test Selection object</returns>
        [HttpPost("testselections")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TestSelectionResponseModel>> AddTestSelection([FromBody] CreateTestSelectionRequestModel requestModel)
        {
            var testSelectionDto = await _mediator.Send(new CreateTestSelectionRequest()
            {
                Name = requestModel.Name,
                Description = requestModel.Description,
                SpecialityId= requestModel.SpecialityId,
                DescriptionVisibility = requestModel.DescriptionVisibility
            });

            return CreatedAtAction(nameof(GetTestSelection), new { id = testSelectionDto.Id }, _mapper.Map<TestSelectionResponseModel>(testSelectionDto)); 
        }

        /// <summary>
        /// Update an existing Test Selection
        /// </summary>
        /// <param name="requestModel">The data describing the Test Selection details to update</param>
        /// <returns>The updated Test Selection object</returns>
        [HttpPut("testselections")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestSelectionResponseModel>> UpdateTestSelection([FromBody] UpdateTestSelectionRequestModel requestModel)
        {
            var testSelectionDto = await _mediator.Send(new UpdateTestSelectionRequest()
            {
                Id = requestModel.Id,
                Name = requestModel.Name,
                Description = requestModel.Description,
                DescriptionVisibility = requestModel.DescriptionVisibility,
                SpecialityId= requestModel.SpecialityId
            });

            return testSelectionDto == null ? NotFound() : CreatedAtAction(nameof(GetTestSelection), new { id = testSelectionDto.Id }, _mapper.Map<TestSelectionResponseModel>(testSelectionDto));
        }

        /// <summary>
        /// Deletes an existing Test Selection
        /// </summary>
        /// <param name="id">The ID of the test selection to delete</param>
        [HttpDelete("testselections/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestSelectionResponseModel>> DeleteTestSelection(int id)
        {
            var testSelection = await _mediator.Send(
                                new DeleteTestSelectionRequest
                                {
                                    Id = id
                                });
            return testSelection == null ? NotFound() : _mapper.Map<TestSelectionResponseModel>(testSelection);
        }

        /// <summary>
        /// Update the visibility of a mapping between a specific test to panel
        /// </summary>
        /// <param name="updatePanelTestForm"></param>
        /// <returns></returns>
        [HttpPut("paneltest")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PanelTestResponseModel>> UpdatePanelTestVisibility([FromBody] UpdatePanelTestRequestModel updatePanelTestRequestModel)
        {
            var panelTest = await _mediator.Send(new UpdatePanelTestRequest()
            {
                PanelId = updatePanelTestRequestModel.PanelId,
                TestId = updatePanelTestRequestModel.TestId,
                Visibility= updatePanelTestRequestModel.Visibility
            });

            return panelTest == null ? NotFound() : _mapper.Map<PanelTestResponseModel>(panelTest);
        }

        /// <summary>
        /// Get a specific Panel Test by PanelId and TestId
        /// </summary>
        /// <returns>The panel test mapping record</returns>
        [HttpGet("paneltest")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PanelTestResponseModel>> GetPanelTestByIds([FromQuery] int panelId, [FromQuery] int testId)
        {
            var panelTest = await _mediator.Send(new GetPanelTestByIdsRequest { 
                PanelId = panelId,
                TestId = testId
            });

            return panelTest == null ? NotFound() : _mapper.Map<PanelTestResponseModel>(panelTest);
        }
    }
}
