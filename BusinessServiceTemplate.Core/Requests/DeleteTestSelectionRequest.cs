using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    /// <summary>
    /// Deletes an existing Test Selection configuration
    /// </summary>
    public class DeleteTestSelectionRequest : IRequest<TestSelectionDto>
    {
        /// <summary>
        /// The ID of the test selection to delete
        /// </summary>
        public int Id { get; set; }
    }
}
