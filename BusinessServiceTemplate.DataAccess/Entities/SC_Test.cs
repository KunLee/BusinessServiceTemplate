using BusinessServiceTemplate.Shared.DataAccess.Models;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Test : DbEntity<int>
    {
        public string Name { set; get; }
        public string? Description { set; get; }
        public List<SC_Panel> Panels { get; } = new();
    }
}
