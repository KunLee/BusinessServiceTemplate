namespace BusinessServiceTemplate.Core.Dtos
{
    public class PanelDto
    {
        public int Id { get; set; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; }
        public bool? Visibility { set; get; }
        public List<TestDto> Tests { get; } = new();
    }
}
