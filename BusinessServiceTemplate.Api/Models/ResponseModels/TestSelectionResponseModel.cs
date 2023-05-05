namespace BusinessServiceTemplate.Api.Models.ResponseModels
{
    public class TestSelectionResponseModel
    {
        public int Id { set; get; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public int? SpecialityId { set; get; }
        public List<PanelResponseModel> Panels { set; get; } = new();
    }
}
