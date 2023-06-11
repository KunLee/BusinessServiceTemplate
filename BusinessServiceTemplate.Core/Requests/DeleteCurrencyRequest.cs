using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    /// <summary>
    /// Deletes an existing Currency configuration
    /// </summary>
    public class DeleteCurrencyRequest : IRequest<CurrencyDto>
    {
        /// <summary>
        /// The ID of the Currency to delete
        /// </summary>
        public int Id { get; set; }
    }
}
