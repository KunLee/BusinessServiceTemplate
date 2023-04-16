﻿namespace BusinessServiceTemplate.Core.Dtos
{
    public class PanelDto
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string? Description { set; get; }
        public decimal? Price { set; get; }
        public List<TestDto> Tests { get; } = new();
    }
}
