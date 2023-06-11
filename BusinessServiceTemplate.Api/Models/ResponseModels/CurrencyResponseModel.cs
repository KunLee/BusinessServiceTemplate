namespace BusinessServiceTemplate.Api.Models.ResponseModels
{
    /// <summary>
    /// Currency Response Object for UI
    /// </summary>
    public class CurrencyResponseModel
    {
        public int Id { get; set; }
        public string Name { set; get; } = null!;
        public string? Country { set; get; }
        public string? Shortcode { set; get; }
        public string? Symbol { set; get; }
        public bool? Active { set; get; }
    }
}
