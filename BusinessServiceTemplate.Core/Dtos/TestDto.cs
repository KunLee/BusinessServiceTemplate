namespace BusinessServiceTemplate.Core.Dtos
{
    public class TestDto
    {

        public int Id { get; set; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public List<PanelDto> Panels { get; } = new();
    }
}
