using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.Core.Dtos
{
    public class ScTestsDto
    {
        public string Name { set; get; }
        public string? Description { set; get; }
        public List<ScPanelsDto> Panels { get; } = new();
    }
}
