using BusinessServiceTemplate.Shared.DataAccess.Models;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_Panel : DbEntity<int>
    {
        public string Name { set; get; }
        public string? Description { set; get; }
        public decimal? Price { set; get; }
        public List<SC_Test> Tests { set; get; } = new();
    }
}
