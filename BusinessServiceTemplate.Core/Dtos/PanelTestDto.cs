namespace BusinessServiceTemplate.Core.Dtos
{
    public class PanelTestDto
    {

        public int Id { get; set; }
        public int PanelId { set; get; }
        public int TestId { set; get; }

        /// <summary>
        /// Used as IsVisible flag for a relationship between a specific Test and the principle Panel
        /// </summary>
        public bool Visibility { set; get; }
    }
}
