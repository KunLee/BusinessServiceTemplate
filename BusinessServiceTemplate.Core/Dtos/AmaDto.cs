namespace BusinessServiceTemplate.Core.Dtos
{
    public class AmaDto
    {
        public string AMACode { get; set; }
        public string Description { get; set; }
        public decimal AMAFee { get; set; }
        public int? MedicareItem { get; set; }
        public decimal? ScheduleFee { get; set; }

        public MbsDto MedibankSchedule { get; set; }
    }
}
