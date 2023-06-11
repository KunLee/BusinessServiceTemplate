namespace BusinessServiceTemplate.Core.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public required string Name { set; get; }
        public string? Country { set; get; }
        public string? Shortcode { set; get; }
        public string? Symbol { set; get; }
        public bool? Active { set; get; }
    }
}
