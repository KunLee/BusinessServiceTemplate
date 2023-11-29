using BusinessServiceTemplate.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessServiceTemplate.Api.Controllers.ExternalApis
{
    public class LocationController : Controller
    {
        private ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        /// <summary>
        /// Get all locations
        /// </summary>
        /// <returns>The location list</returns>
        [HttpGet("locations")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<List<string>>> GetAllLocations()
        {
            var list = await _locationService.GetAllLocations();

            return list == null ? NotFound() : Ok(list);
        }
    }
}
