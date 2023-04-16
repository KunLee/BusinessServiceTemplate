namespace BusinessServiceTemplate.Api.Models.Forms
{
    public class CreatePanelForm
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { set; get; }

        public List<int> TestIds { set; get; }
    }
}
