using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    /// <summary>
    /// Deletes an existing Test configuration
    /// </summary>
    public class DeleteTestRequest : IRequest
    {
        /// <summary>
        /// The ID of the test to delete
        /// </summary>
        public int Id { get; set; }
    }
}
