namespace BusinessServiceTemplate.Api.Models.Forms
{
    public class CreateTestForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> PanelIds { set; get; }
    }
}
