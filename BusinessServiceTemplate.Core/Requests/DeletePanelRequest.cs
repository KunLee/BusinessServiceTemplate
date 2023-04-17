using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    /// <summary>
    /// Deletes an existing Panel configuration
    /// </summary>
    public class DeletePanelRequest : IRequest<PanelDto>
    {
        /// <summary>
        /// The ID of the Panel to delete
        /// </summary>
        public int Id { get; set; }
    }
}
