﻿using BusinessServiceTemplate.Shared.DataAccess.Models;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Panel_Test : DbEntity<int>
    {
        public int PanelId { set; get; }
        public int TestId { set; get; }
        public SC_Panel Panels { get; set; } = null!;
        public SC_Test Tests { get; set; } = null!;
        public bool? Visibility { get; set; }
    }
}
