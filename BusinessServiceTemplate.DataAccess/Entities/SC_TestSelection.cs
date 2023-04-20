﻿using BusinessServiceTemplate.Shared.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_TestSelection : DbEntity<int>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { set; get; }
        public string? Description { set; get; }

        public bool? DescriptionVisibility { set; get; }

        [Required(ErrorMessage = "SpecialityId is required.")]
        public int SpecialityId { set; get; }

        public virtual List<SC_Panel> Panels { get; } = new(); // Collection navigation containing dependent panels
    }
}
