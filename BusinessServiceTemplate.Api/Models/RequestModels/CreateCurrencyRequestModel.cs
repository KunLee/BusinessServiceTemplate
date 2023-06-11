namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    /// <summary>
    /// Request for creating a new Currency
    /// </summary>
    public class CreateCurrencyRequestModel
    {
        public required string Name { get; set; }
        public string? Country { set; get; }
        public string? Shortcode { set; get; }
        public string? Symbol { set; get; }
        public bool? Active { set; get; }
    }
}
