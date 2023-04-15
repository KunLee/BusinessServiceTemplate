using BusinessServiceTemplate.Shared.DataAccess.Models;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_TestSelection : DbEntity<int>
    {
        public string Name { set; get; }
        public string? Description { set; get; }
        public int? SpecialityId { set; get; }
    }
}
