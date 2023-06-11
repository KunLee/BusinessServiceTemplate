using BusinessServiceTemplate.Core.Dtos;
using MediatR;

namespace BusinessServiceTemplate.Core.Requests
{
    /// <summary>
    /// Create a Currency record
    /// </summary>
    public class CreateCurrencyRequest : IRequest<CurrencyDto>
    {
        public required string Name { get; set; }
        public string? Country { set; get; }
        public string? Shortcode { set; get; }
        public string? Symbol { set; get; }
        public bool? Active { set; get; }
    }
}
