﻿namespace BusinessServiceTemplate.Api.Models.ResponseModels
{
    public class PanelResponseModel
    {
        public int Id { set;  get; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
        public bool? Visibility { set; get; }
        public decimal? Price { set; get; }
        public bool? PriceVisibility { set; get; }
        public int TestSelectionId { get; set; }
        public CurrencyResponseModel? Currency { set; get; }
        public List<TestInPanelResponseModel> Tests { set; get; } = new();
    }

    public class TestInPanelResponseModel
    {
        public int Id { set; get; }
        public required string Name { set; get; }
        public string? Description { set; get; }
        public bool? DescriptionVisibility { set; get; }
    }
}
