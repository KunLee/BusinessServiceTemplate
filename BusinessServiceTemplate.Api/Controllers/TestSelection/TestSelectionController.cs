using AutoMapper;
using BusinessServiceTemplate.Api.Models.Forms;
using BusinessServiceTemplate.Api.Models.ViewModels;
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
        [ProducesResponseType(200)]
        public async Task<PanelViewModel> GetPanel(int id)
        {
            var panel = await _mediator.Send(new GetPanelRequest(id));

            return _mapper.Map<PanelViewModel>(panel);
        }

        /// <summary>
        /// Get All of the Test Panels
        /// </summary>
        /// <returns>The list of the panels</returns>
        [HttpGet("panels")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IList<PanelViewModel>> GetAllPanels() 
        {
            var allPanels = await _mediator.Send(new GetAllPanelsRequest());

            return allPanels.Select(_mapper.Map<PanelViewModel>).ToList();
        }

        /// <summary>
        /// Add a new Test Panel
        /// </summary>
        /// <param name="form">The data describing the Panel details</param>
        /// <returns>The added Panel object</returns>
        [HttpPost("panel")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<PanelViewModel> AddPanel([FromBody] CreatePanelForm form)
        {
            var panelDto = await _mediator.Send(new CreatePanelRequest() { 
                Name = form.Name,
                Description = form.Description,
                Price = form.Price,
                PriceVisibility = form.PriceVisibility,
                DescriptionVisibility = form.DescriptionVisibility,
                TestSelectionId = form.TestSelectionId,
                TestIds= form.TestIds,
                Visibility = form.Visibility
            });

            return _mapper.Map<PanelViewModel>(panelDto);
        }

        /// <summary>
        /// Update an existing Test Panel
        /// </summary>
        /// <param name="id">The ID of the Test Panel</param>
        /// <param name="form">The data describing the Panel details to update</param>
        /// <returns>The updated Panel object</returns>
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
                DescriptionVisibility = form.DescriptionVisibility,
                PriceVisibility = form.PriceVisibility,
                TestSelectionId = form.TestSelectionId,
                Price = form.Price,
                TestIds = form.TestIds,
                Visibility = form.Visibility
            });

            return _mapper.Map<PanelViewModel>(panelDto);
        }

        /// <summary>
        /// Delete an existing Test Panel
        /// </summary>
        /// <param name="id">The ID of the Test Panel</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get All of the Tests
        /// </summary>
        /// <returns>The list of the tests</returns>
        [HttpGet("tests")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IList<TestViewModel>> GetAllTests()
        {
            var list = await _mediator.Send(new GetAllTestsRequest());
            return list.Select(_mapper.Map<TestViewModel>).ToList();
        }

        /// <summary>
        /// Get a specific Test by Id
        /// </summary>
        /// <returns>A specific test returned</returns>
        [HttpGet("tests/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestViewModel> GetTest(int id)
        {
            var test = await _mediator.Send(new GetTestRequest(id));

            return _mapper.Map<TestViewModel>(test);
        }

        /// <summary>
        /// Add a new Test with full details
        /// </summary>
        /// <param name="form">The data describing the Test details</param>
        /// <returns>The added Test object</returns>
        [HttpPost("test")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestViewModel> AddTest([FromBody] CreateTestForm form)
        {
            var testDto = await _mediator.Send(new CreateTestRequest()
            {
                Name = form.Name,
                Description = form.Description,
                DescriptionVisibility = form.DescriptionVisibility,
                PanelIds = form.PanelIds
            });

            return _mapper.Map<TestViewModel>(testDto);
        }

        /// <summary>
        /// Update an existing Test
        /// </summary>
        /// <param name="id">The ID of the Test</param>
        /// <param name="form">The data describing the Test details to update</param>
        /// <returns>The updated Test object</returns>
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
                DescriptionVisibility = form.DescriptionVisibility,
                PanelIds = form.PanelIds
            });

            return _mapper.Map<TestViewModel>(testDto);
        }

        /// <summary>
        /// Deletes an existing Test
        /// </summary>
        /// <param name="id">The ID of the test to delete</param>
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

        /// <summary>
        /// Get All of the TestSelections
        /// </summary>
        /// <returns>The list of the test selections</returns>
        [HttpGet("testselections")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IList<TestSelectionViewModel>> GetAllTestSelections()
        {
            var list = await _mediator.Send(new GetAllTestSelectionsRequest());
            return list.Select(_mapper.Map<TestSelectionViewModel>).ToList();
        }

        /// <summary>
        /// Get A specific TestSelections
        /// </summary>
        /// <returns>The list of the test selections</returns>
        [HttpGet("testselections/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestSelectionViewModel> GetTestSelection(int id)
        {
            var testSelection = await _mediator.Send(new GetTestSelectionRequest(id));
            return _mapper.Map<TestSelectionViewModel>(testSelection);
        }

        /// <summary>
        /// Get TestSelections via SpecialityId
        /// </summary>
        /// <returns>The list of the test selections</returns>
        [HttpGet("testselections/speciality/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<IList<TestSelectionViewModel>> GetTestSelectionsBySpecialityId(int id)
        {
            var testSelections = await _mediator.Send(new GetTestSelectionsBySpecialityRequest(id));

            return testSelections.Select(_mapper.Map<TestSelectionViewModel>).ToList();
        }

        /// <summary>
        /// Add a new Test Selection with full details
        /// </summary>
        /// <param name="form">The data describing the Test Selection details</param>
        /// <returns>The added Test Selection object</returns>
        [HttpPost("testselection")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestSelectionViewModel> AddTest([FromBody] CreateTestSelectionForm form)
        {
            var testSelectionDto = await _mediator.Send(new CreateTestSelectionRequest()
            {
                Name = form.Name,
                Description = form.Description,
                SpecialityId= form.SpecialityId,
                DescriptionVisibility = form.DescriptionVisibility
            });

            return _mapper.Map<TestSelectionViewModel>(testSelectionDto);
        }

        /// <summary>
        /// Update an existing Test Selection
        /// </summary>
        /// <param name="id">The ID of the Test Selection</param>
        /// <param name="form">The data describing the Test Selection details to update</param>
        /// <returns>The updated Test Selection object</returns>
        [HttpPut("testselection/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<TestSelectionViewModel> UpdateTest(int id, [FromBody] UpdateTestSelectionForm form)
        {
            var testSelectionDto = await _mediator.Send(new UpdateTestSelectionRequest()
            {
                Id = id,
                Name = form.Name,
                Description = form.Description,
                DescriptionVisibility = form.DescriptionVisibility,
                SpecialityId= form.SpecialityId
            });

            return _mapper.Map<TestSelectionViewModel>(testSelectionDto);
        }

        /// <summary>
        /// Deletes an existing Test Selection
        /// </summary>
        /// <param name="id">The ID of the test selection to delete</param>
        [HttpDelete("testselection/{id}")]
        [ProducesResponseType(200)]
        public Task DeleteTestSelection(int id)
        {
            return _mediator.Send(
            new DeleteTestSelectionRequest
            {
                Id = id
            });
        }

        /// <summary>
        /// Update the visibility of a mapping between a specific test to panel
        /// </summary>
        /// <param name="updatePanelTestForm"></param>
        /// <returns></returns>
        [HttpPut("paneltest")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task UpdatePanelTestVisibility([FromBody] UpdatePanelTestForm updatePanelTestForm)
        {
            await _mediator.Send(new UpdatePanelTestRequest()
            {
                PanelId = updatePanelTestForm.PanelId,
                TestId = updatePanelTestForm.TestId,
                Visibility= updatePanelTestForm.Visibility
            });
        }

        /// <summary>
        /// Get a specific Panel Test by PanelId and TestId
        /// </summary>
        /// <returns>The panel test mapping record</returns>
        [HttpGet("paneltest")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        public async Task<PanelTestViewModel> GetPanelTestByIds([FromQuery] int panelId, [FromQuery] int testId)
        {
            var panelTest = await _mediator.Send(new GetPanelTestByIdsRequest { 
                PanelId = panelId,
                TestId = testId
            });

            return _mapper.Map<PanelTestViewModel>(panelTest);
        }
    }
}
