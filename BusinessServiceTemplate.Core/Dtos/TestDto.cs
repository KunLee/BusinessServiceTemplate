namespace BusinessServiceTemplate.Core.Dtos
{
    public class TestDto
    {
        public string Name { set; get; }
        public string? Description { set; get; }
        public List<PanelDto> Panels { get; } = new();
    }
}
