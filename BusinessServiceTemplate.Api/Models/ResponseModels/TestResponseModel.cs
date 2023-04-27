namespace BusinessServiceTemplate.Api.Models.ResponseModels
{
    public class TestResponseModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public List<int> Panels { set; get; } = new();
    }
}
