namespace BusinessServiceTemplate.Api.Models.RequestModels
{
    /// <summary>
    /// Model used for updating Test Selection
    /// </summary>
    public class UpdateTestSelectionRequestModel : CreateTestSelectionRequestModel
    {
        /// <summary>
        /// Id field
        /// </summary>
        public int Id { get; set; }
    }
}
