using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BusinessServiceTemplate.Api.Models.Forms
{
    public class CreateTestSelectionForm
    {
        [Required]
        public string Name { set; get; }
        public string? Description { set; get; }

        [Required]
        public int SpecialityId { set; get; }
    }
}
