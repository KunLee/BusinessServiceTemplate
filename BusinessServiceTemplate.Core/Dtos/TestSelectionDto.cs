namespace BusinessServiceTemplate.Core.Dtos
{
    public class TestSelectionDto
    {

        public int Id { get; set; }
        public string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public int SpecialityId { set; get; }
        public List<PanelDto> Panels { get; } = new();
    }
}
