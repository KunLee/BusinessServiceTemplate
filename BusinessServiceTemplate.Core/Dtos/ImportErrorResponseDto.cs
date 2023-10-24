namespace BusinessServiceTemplate.Core.Dtos
{
    public class ImportErrorResponseDto
    {
        public string Field { get; set; } = null!;
        public string Error { get; set; } = null!;
        public int RowNumber { get; set; }
    }
}
