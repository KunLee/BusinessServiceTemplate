using BusinessServiceTemplate.DataAccess.Entities;

namespace BusinessServiceTemplate.Core.Dtos
{
    public class MbsDto
    {
        public int ItemNum { get; set; }
        public string? SubItemNum { get; set; }
        public string? ItemStartDate { get; set; }
        public string? ItemEndDate { get; set; }
        public int? Category { get; set; }
        public string? Group { get; set; }
        public int? SubGroup { get; set; }
        public int? SubHeading { get; set; }
        public string? ItemType { get; set; }
        public string? FeeType { get; set; }
        public string? ProviderType { get; set; }
        public string? NewItem { get; set; }
        public string? ItemChange { get; set; }
        public string? AnaesChange { get; set; }
        public string? DescriptorChange { get; set; }
        public string? FeeChange { get; set; }
        public string? EMSNChange { get; set; }
        public string? EMSNCap { get; set; }
        public string? BenefitType { get; set; }
        public string? BenefitStartDate { get; set; }
        public string? FeeStartDate { get; set; }
        public double? ScheduleFee { get; set; }
        public double? Benefit100 { get; set; }
        public int? BasicUnits { get; set; }
        public string? EMSNStartDate { get; set; }
        public string? EMSNEndDate { get; set; }
        public double? EMSNFixedCapAmount { get; set; }
        public double? EMSNMaximumCap { get; set; }
        public double? EMSNPercentageCap { get; set; }
        public string? EMSNDescription { get; set; }
        public string? EMSNChangeDate { get; set; }
        public string? DescriptionStartDate { get; set; }
        public string? Description { get; set; }
        public string? QFEStartDate { get; set; }
        public string? QFEEndDate { get; set; }

        public List<SC_AMA>? AustralianMedicalAssociations { set; get; }
    }
}
