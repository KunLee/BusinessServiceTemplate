﻿namespace BusinessServiceTemplate.Api.Models.ViewModels
{
    public class TestViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public List<int> Panels { set; get; } = new();
    }
}