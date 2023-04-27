namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    public class UpdatePanelTestRequestModel
    {
        public int PanelId { get; set; }
        public int TestId { get; set; }
        public bool Visibility { get; set; }
    }
}
