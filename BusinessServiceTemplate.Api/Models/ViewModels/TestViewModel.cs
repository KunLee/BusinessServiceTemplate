namespace BusinessServiceTemplate.Api.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string? Description { set; get; }
        public List<PanelViewModel> Panels { get; } = new();
    }
}
