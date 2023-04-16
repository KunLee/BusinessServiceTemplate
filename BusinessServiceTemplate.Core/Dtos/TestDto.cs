namespace BusinessServiceTemplate.Core.Dtos
{
    public class TestDto
    {

        public int Id { get; set; }
        public string Name { set; get; }
        public string? Description { set; get; }
        public List<PanelDto> Panels { get; } = new();
    }
}
