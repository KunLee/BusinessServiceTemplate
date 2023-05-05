namespace BusinessServiceTemplate.Api.Models.ResponseModels
{
    public class TestResponseModel
    {
        public int Id { set; get; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public List<PanelInTestResponseModel> Panels { set; get; } = new();
    }

    public class PanelInTestResponseModel
    {
        public int Id { set; get; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; }
    }
}
